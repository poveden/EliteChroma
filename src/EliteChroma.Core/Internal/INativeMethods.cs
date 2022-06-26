using Microsoft.Win32.SafeHandles;

namespace EliteChroma.Core.Internal
{
    internal interface INativeMethods
    {
        short GetAsyncKeyState(NativeMethods.VirtualKey vKey);

        IntPtr GetKeyboardLayout(int idThread);

        int GetKeyboardLayoutList(int nBuff, IntPtr[] lpList);

        uint MapVirtualKeyEx(uint uCode, NativeMethods.MAPVK uMapType, IntPtr dwhkl);

        IntPtr LoadLibrary(string lpFileName);

        bool FreeLibrary(IntPtr hModule);

        IntPtr GetForegroundWindow();

        int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        bool EnumProcesses(int[] lpidProcess, int cb, out int lpcbNeeded);

        SafeProcessHandle OpenProcess(NativeMethods.ProcessAccess dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        bool EnumProcessModules(SafeProcessHandle hProcess, IntPtr[] lphModule, int cb, out int lpcbNeeded);

        int GetModuleFileNameEx(SafeProcessHandle hProcess, IntPtr hModule, char[] lpFilename, int nSize);
    }
}
