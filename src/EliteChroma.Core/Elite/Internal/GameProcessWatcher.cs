using System;
using System.Timers;
using EliteFiles;

namespace EliteChroma.Elite.Internal
{
    internal sealed class GameProcessWatcher : IDisposable
    {
        private const int _gameForegroundCheckInterval = 200;
        private const int _processCheckInterval = 2000;
        private const int _processCheckWaitCycles = _processCheckInterval / _gameForegroundCheckInterval;

        private readonly string _mainExePath;
        private readonly Timer _timer;
        private readonly GameProcessTracker _gameProcessTracker;

        private int _checking;
        private int _processCheckCycle;

        public GameProcessWatcher(GameInstallFolder gameInstallFolder)
        {
            _mainExePath = gameInstallFolder.MainExecutable.FullName;
            _gameProcessTracker = new GameProcessTracker(_mainExePath);

            _timer = new Timer
            {
                Interval = _gameForegroundCheckInterval,
                AutoReset = true,
                Enabled = false,
            };
            _timer.Elapsed += Timer_Elapsed;
        }

        public event EventHandler<EventArgs> Changed;

        public GameProcessState ProcessState { get; private set; }

        public void Start()
        {
            ProcessState = GetProcessState();
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private bool CheckGameForegroundState()
        {
            var hWnd = NativeMethods.GetForegroundWindow();
            var threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);

            if (threadId == 0 || processId == 0)
            {
                return false;
            }

            return _gameProcessTracker.IsGameProcess(processId);
        }

        private bool CheckGameRunningState()
        {
            _processCheckCycle = (_processCheckCycle + 1) % _processCheckWaitCycles;
            var fullScan = _processCheckCycle == 0;

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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (System.Threading.Interlocked.Exchange(ref _checking, 1) == 1)
            {
                return;
            }

            try
            {
                var newState = GetProcessState();

                if (newState == ProcessState)
                {
                    return;
                }

                ProcessState = newState;

                Changed?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                System.Threading.Interlocked.Exchange(ref _checking, 0);
            }
        }
    }
}
