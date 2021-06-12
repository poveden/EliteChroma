using System;
using System.Collections.Generic;
using System.Linq;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Tests.Internal
{
    internal class NativeMethodsKeyboardMock : NativeMethodsStub
    {
        public const int DeDE = 0x04070407;
        public const int EnUS = 0x04090409;
        public const int EsES = 0x040A0C0A;

        public static readonly NativeMethodsKeyboardMock Instance = new NativeMethodsKeyboardMock();

        private static readonly Dictionary<(int Layout, VirtualKey VirtualKey), (char Character, uint ScanCode)> _map = new Dictionary<(int, VirtualKey), (char, uint)>
        {
            [(DeDE, VirtualKey.VK_OEM_4)] = ('ß', 0x0C),
            [(EsES, VirtualKey.VK_OEM_5)] = ('º', 0x29),
            [(EnUS, VirtualKey.VK_OEM_3)] = ('`', 0x29),
            [(EsES, VirtualKey.VK_OEM_3)] = ('ñ', 0x27),
            [(EnUS, VirtualKey.VK_OEM_2)] = ('/', 0x35),
        };

        private static readonly int[] _layouts = _map.Keys.Select(x => x.Layout).Distinct().ToArray();

        public override uint MapVirtualKeyEx(uint uCode, MAPVK uMapType, IntPtr dwhkl)
        {
            int keyboardLayout = dwhkl.ToInt32();

            return uMapType switch
            {
                MAPVK.VK_TO_CHAR => GetChar(keyboardLayout, (VirtualKey)uCode),
                MAPVK.VK_TO_VSC_EX => GetScanCode(keyboardLayout, (VirtualKey)uCode),
                MAPVK.VSC_TO_VK_EX => GetVirtualKey(keyboardLayout, uCode),
                MAPVK.VK_TO_VSC => throw new NotImplementedException(),
                MAPVK.VSC_TO_VK => throw new NotImplementedException(),
                _ => 0,
            };
        }

        public override IntPtr GetKeyboardLayout(int idThread)
        {
            return new IntPtr(EnUS);
        }

        public override int GetKeyboardLayoutList(int nBuff, IntPtr[] lpList)
        {
            if (nBuff == 0)
            {
                return _layouts.Length;
            }

            int n = Math.Min(lpList.Length, _layouts.Length);

            for (int i = 0; i < n; i++)
            {
                lpList[i] = new IntPtr(_layouts[i]);
            }

            return n;
        }

        private static char GetChar(int keyboardLayout, VirtualKey virtualKey)
        {
            return _map.TryGetValue((keyboardLayout, virtualKey), out var res)
                       ? res.Character
                       : '\0';
        }

        private static uint GetScanCode(int keyboardLayout, VirtualKey virtualKey)
        {
            return _map.TryGetValue((keyboardLayout, virtualKey), out var res)
                       ? res.ScanCode
                       : 0;
        }

        private static uint GetVirtualKey(int keyboardLayout, uint scanCode)
        {
            return _map.Where(kv => kv.Key.Layout == keyboardLayout && kv.Value.ScanCode == scanCode)
                       .Select(kv => (uint)kv.Key.VirtualKey)
                       .FirstOrDefault();
        }
    }
}
