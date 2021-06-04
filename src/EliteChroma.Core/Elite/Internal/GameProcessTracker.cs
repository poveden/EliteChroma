using System;
using System.Collections.Generic;
using EliteChroma.Core.Internal;
using Microsoft.Win32.SafeHandles;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Elite.Internal
{
    internal sealed class GameProcessTracker : NativeMethodsAccessor
    {
        private readonly string _gameExePath;
        private readonly HashSet<int> _clearedProcessIds;
        private readonly char[] _buf;

        private ProcessList _plCurr;
        private ProcessList _plPrev;

        private int _gameProcessId;

        public GameProcessTracker(string gameExePath, INativeMethods nativeMethods)
            : base(nativeMethods)
        {
            _gameExePath = gameExePath;
            _clearedProcessIds = new HashSet<int>();
            _plCurr = new ProcessList(nativeMethods);
            _plPrev = new ProcessList(nativeMethods);
            _buf = new char[1024];
        }

        public bool IsGameProcess(int processId)
        {
            if (processId == 0)
            {
                return false;
            }

            if (processId == _gameProcessId)
            {
                return true;
            }

            if (_gameProcessId != 0)
            {
                // The game is running, and this process is not the game's process, so we just clear it.
                _ = _clearedProcessIds.Add(processId);
                return false;
            }

            if (!_clearedProcessIds.Add(processId))
            {
                // processId has been already cleared, no need to clear it again.
                return false;
            }

            if (!TestIfGameProcessId(processId, out bool failure))
            {
                if (failure)
                {
                    _ = _clearedProcessIds.Remove(processId);
                    _ = _plCurr.Remove(processId);
                }

                return false;
            }

            _gameProcessId = processId;
            return true;
        }

        public bool IsGameRunning(bool fullScan)
        {
            if (_gameProcessId != 0)
            {
                if (TestIfGameProcessId(_gameProcessId, out bool failure))
                {
                    return true;
                }

                if (failure)
                {
                    _ = _clearedProcessIds.Remove(_gameProcessId);
                    _ = _plCurr.Remove(_gameProcessId);
                }

                _gameProcessId = 0;
            }

            if (!fullScan)
            {
                return false;
            }

            UpdateProcessIndex();
            return _gameProcessId != 0;
        }

        private void UpdateProcessIndex()
        {
            ProcessList swap = _plPrev;
            _plPrev = _plCurr;
            _plCurr = swap;

            _plCurr.Refresh();

            foreach ((int processId, bool added) in _plCurr.Diff(_plPrev))
            {
                if (added)
                {
                    if (TestIfGameProcessId(processId, out bool failure))
                    {
                        _gameProcessId = processId;
                    }

                    if (!failure)
                    {
                        _ = _clearedProcessIds.Add(processId);
                    }
                }
                else
                {
                    _ = _clearedProcessIds.Remove(processId);

                    if (processId == _gameProcessId)
                    {
                        _gameProcessId = 0;
                    }
                }
            }
        }

        private bool TestIfGameProcessId(int processId, out bool failure)
        {
            string filename = TryGetProcessFileName(processId);
            failure = filename == null;
            return _gameExePath.Equals(filename, StringComparison.OrdinalIgnoreCase);
        }

        private string TryGetProcessFileName(int processId)
        {
            using SafeProcessHandle p = NativeMethods.OpenProcess(ProcessAccess.QUERY_INFORMATION | ProcessAccess.VM_READ, false, processId);

            if (p.IsInvalid)
            {
                return null;
            }

            var buf = new IntPtr[1]; // We are only interested in the main module
            int bufSize = buf.Length * IntPtr.Size;

            if (!NativeMethods.EnumProcessModules(p, buf, bufSize, out int retSize) || retSize < bufSize)
            {
                return null;
            }

            int n = NativeMethods.GetModuleFileNameEx(p, buf[0], _buf, _buf.Length);

            if (n == 0)
            {
                return null;
            }

            return new string(_buf, 0, n);
        }
    }
}
