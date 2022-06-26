using System.Diagnostics.CodeAnalysis;
using System.Timers;
using EliteChroma.Core.Internal;
using EliteFiles;
using Timer = System.Timers.Timer;

namespace EliteChroma.Core.Elite.Internal
{
    internal sealed class GameProcessWatcher : NativeMethodsAccessor, IDisposable
    {
        private const int _gameForegroundCheckInterval = 200;
        private const int _processCheckInterval = 2000;
        private const int _processCheckWaitCycles = _processCheckInterval / _gameForegroundCheckInterval;

        private readonly Timer _timer;
        private readonly GameProcessTracker _gameProcessTracker;

        private GameProcessState? _processState;
        private int _checking;
        private int _processCheckCycle;
        private bool _running;
        private bool _disposed;

        public GameProcessWatcher(GameInstallFolder gameInstallFolder, INativeMethods nativeMethods)
            : base(nativeMethods)
        {
            string mainExePath = gameInstallFolder.MainExecutable.FullName;
            _gameProcessTracker = new GameProcessTracker(mainExePath, nativeMethods);

            _timer = new Timer
            {
                Interval = _gameForegroundCheckInterval,
                AutoReset = true,
                Enabled = false,
            };
            _timer.Elapsed += Timer_Elapsed;
        }

        public event EventHandler<GameProcessState>? Changed;

        public void Start()
        {
            if (_running)
            {
                return;
            }

            _processState = null;
            _timer.Start();
            _running = true;
        }

        public void Stop()
        {
            if (!_running)
            {
                return;
            }

            _timer.Stop();
            _running = false;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _timer?.Dispose();
            _disposed = true;
        }

        private bool CheckGameForegroundState()
        {
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            int threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out int processId);

            if (threadId == 0 || processId == 0)
            {
                return false;
            }

            return _gameProcessTracker.IsGameProcess(processId);
        }

        private bool CheckGameRunningState()
        {
            _processCheckCycle = (_processCheckCycle + 1) % _processCheckWaitCycles;
            bool fullScan = _processCheckCycle == 0;

            return _gameProcessTracker.IsGameRunning(fullScan);
        }

        private GameProcessState GetProcessState()
        {
            if (CheckGameForegroundState())
            {
                return GameProcessState.InForeground;
            }

            if (CheckGameRunningState())
            {
                return GameProcessState.InBackground;
            }

            return GameProcessState.NotRunning;
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Will rethrow exceptions into calling thread")]
        private async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (Interlocked.Exchange(ref _checking, 1) == 1)
            {
                return;
            }

            try
            {
                GameProcessState newState = GetProcessState();

                if (newState == _processState)
                {
                    return;
                }

                _processState = newState;

                Changed?.Invoke(this, newState);
            }
            catch (Exception ex)
            {
                // Reference: https://docs.microsoft.com/en-us/dotnet/api/system.timers.timer?view=netcore-5.0#remarks
                await Task.FromException(ex).ConfigureAwait(false);
            }
            finally
            {
                _ = Interlocked.Exchange(ref _checking, 0);
            }
        }
    }
}
