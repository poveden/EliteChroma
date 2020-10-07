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
        };

        private static readonly int[] _layouts = _map.Keys.Select(x => x.Layout).Distinct().ToArray();

        public override uint MapVirtualKeyEx(uint uCode, MAPVK uMapType, IntPtr dwhkl)
        {
            var keyboardLayout = dwhkl.ToInt32();

            return uMapType switch
            {
                MAPVK.VK_TO_CHAR => GetChar(keyboardLayout, (VirtualKey)uCode),
                MAPVK.VK_TO_VSC_EX => GetScanCode(keyboardLayout, (VirtualKey)uCode),
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

            var n = Math.Min(lpList.Length, _layouts.Length);

            for (var i = 0; i < n; i++)
            {
                lpList[i] = new IntPtr(_layouts[i]);
            }

            return n;
        }

        private static char GetChar(int keyboardLayout, VirtualKey virtualKey)
            => _map.TryGetValue((keyboardLayout, virtualKey), out var res)
            ? res.Character
            : '\0';

        private static uint GetScanCode(int keyboardLayout, VirtualKey virtualKey)
            => _map.TryGetValue((keyboardLayout, virtualKey), out var res)
            ? res.ScanCode
            : 0;
    }
}
