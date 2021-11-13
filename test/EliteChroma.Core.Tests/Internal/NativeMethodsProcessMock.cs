using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Tests.Internal
{
    internal class NativeMethodsProcessMock : NativeMethodsStub
    {
        private static readonly IntPtr _mainModule = new IntPtr(2222);

        public IDictionary<int, string> Processes { get; set; } = new Dictionary<int, string>();

        public bool OpenProcessFailure { get; set; }

        public bool EnumProcessModulesFailure { get; set; }

        public bool GetModuleFileNameExFailure { get; set; }

        public override bool EnumProcesses(int[] lpidProcess, int cb, out int lpcbNeeded)
        {
            if (Processes == null)
            {
                lpcbNeeded = 0;
                return false;
            }

            Processes.Keys.CopyTo(lpidProcess, 0);
            lpcbNeeded = Processes.Count * Marshal.SizeOf<int>();
            return true;
        }

        public override SafeProcessHandle OpenProcess(ProcessAccess dwDesiredAccess, bool bInheritHandle, int dwProcessId)
        {
            if (OpenProcessFailure || !Processes.ContainsKey(dwProcessId))
            {
                return new SafeProcessHandle(IntPtr.Zero, false);
            }

            return new SafeProcessHandle(new IntPtr(dwProcessId), false);
        }

        [SuppressMessage("Blocker Bug", "S3869:\"SafeHandle.DangerousGetHandle\" should not be called", Justification = "Mock handles")]
        public override bool EnumProcessModules(SafeProcessHandle hProcess, IntPtr[] lphModule, int cb, out int lpcbNeeded)
        {
            if (EnumProcessModulesFailure)
            {
                lpcbNeeded = 0;
                return false;
            }

            int id = hProcess.DangerousGetHandle().ToInt32();

            if (!Processes.ContainsKey(id))
            {
                lpcbNeeded = 0;
                return false;
            }

            lphModule[0] = _mainModule;
            lpcbNeeded = Marshal.SizeOf<IntPtr>();
            return true;
        }

        [SuppressMessage("Blocker Bug", "S3869:\"SafeHandle.DangerousGetHandle\" should not be called", Justification = "Mock handles")]
        public override int GetModuleFileNameEx(SafeProcessHandle hProcess, IntPtr hModule, char[] lpFilename, int nSize)
        {
            if (GetModuleFileNameExFailure)
            {
                return 0;
            }

            int id = hProcess.DangerousGetHandle().ToInt32();

            if (!Processes.TryGetValue(id, out string? exePath))
            {
                return 0;
            }

            if (hModule != _mainModule)
            {
                return 0;
            }

            int n = exePath.Length;
            exePath.CopyTo(0, lpFilename, 0, n);
            return n;
        }
    }
}
