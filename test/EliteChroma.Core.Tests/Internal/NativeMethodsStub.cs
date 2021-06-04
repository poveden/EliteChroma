using System;
using EliteChroma.Core.Internal;
using Microsoft.Win32.SafeHandles;

namespace EliteChroma.Core.Tests.Internal
{
    internal class NativeMethodsStub : INativeMethods
    {
        public virtual bool EnumProcesses(int[] lpidProcess, int cb, out int lpcbNeeded)
        {
            throw new NotImplementedException();
        }

        public virtual bool EnumProcessModules(SafeProcessHandle hProcess, IntPtr[] lphModule, int cb, out int lpcbNeeded)
        {
            throw new NotImplementedException();
        }

        public virtual bool FreeLibrary(IntPtr hModule)
        {
            throw new NotImplementedException();
        }

        public virtual short GetAsyncKeyState(NativeMethods.VirtualKey vKey)
        {
            throw new NotImplementedException();
        }

        public virtual IntPtr GetForegroundWindow()
        {
            throw new NotImplementedException();
        }

        public virtual IntPtr GetKeyboardLayout(int idThread)
        {
            throw new NotImplementedException();
        }

        public virtual int GetKeyboardLayoutList(int nBuff, IntPtr[] lpList)
        {
            throw new NotImplementedException();
        }

        public virtual int GetModuleFileNameEx(SafeProcessHandle hProcess, IntPtr hModule, char[] lpFilename, int nSize)
        {
            throw new NotImplementedException();
        }

        public virtual int GetWindowThreadProcessId(IntPtr hWnd, out int processId)
        {
            throw new NotImplementedException();
        }

        public virtual IntPtr LoadLibrary(string lpFileName)
        {
            throw new NotImplementedException();
        }

        public virtual uint MapVirtualKeyEx(uint uCode, NativeMethods.MAPVK uMapType, IntPtr dwhkl)
        {
            throw new NotImplementedException();
        }

        public virtual SafeProcessHandle OpenProcess(NativeMethods.ProcessAccess dwDesiredAccess, bool bInheritHandle, int dwProcessId)
        {
            throw new NotImplementedException();
        }
    }
}
