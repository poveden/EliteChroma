using System;
using System.Diagnostics;
using System.Timers;
using EliteFiles;

namespace EliteChroma.Elite.Internal
{
    internal sealed class GameProcessWatcher : IDisposable
    {
        private readonly string _mainExePath;
        private readonly Timer _timer;

        public GameProcessWatcher(GameInstallFolder gameInstallFolder)
        {
            _mainExePath = gameInstallFolder.MainExecutable.FullName;

            _timer = new Timer
            {
                Interval = 200,
                AutoReset = true,
                Enabled = false,
            };
            _timer.Elapsed += Timer_Elapsed;
        }

        public event EventHandler<EventArgs> Changed;

        public bool GameInForeground { get; private set; }

        public void Start()
        {
            GameInForeground = CheckGameForegroundState();
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

        private static string TryGetProcessFileName(int processId)
        {
            try
            {
                using (var p = Process.GetProcessById(processId))
                {
                    return p.MainModule.FileName;
                }
            }
#pragma warning disable CA1031
            catch
            {
                return null;
            }
#pragma warning restore CA1031
        }

        private bool CheckGameForegroundState()
        {
            var hWnd = NativeMethods.GetForegroundWindow();
            var threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out var processId);

            if (threadId == 0 || processId == 0)
            {
                return false;
            }

            var fgFileName = TryGetProcessFileName(processId);

            return _mainExePath.Equals(fgFileName, StringComparison.OrdinalIgnoreCase);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var newFG = CheckGameForegroundState();

            if (newFG == GameInForeground)
            {
                return;
            }

            GameInForeground = newFG;

            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
