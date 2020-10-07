using System;
using System.Collections.Generic;
using EliteChroma.Elite.Internal;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Internal
{
    internal static class OemKeyMappings
    {
        private static readonly VirtualKey[] _oemKeys = new[]
        {
            VirtualKey.VK_OEM_1,
            VirtualKey.VK_OEM_PLUS,
            VirtualKey.VK_OEM_COMMA,
            VirtualKey.VK_OEM_MINUS,
            VirtualKey.VK_OEM_PERIOD,
            VirtualKey.VK_OEM_2,
            VirtualKey.VK_OEM_3,
            VirtualKey.VK_ABNT_C1,
            VirtualKey.VK_ABNT_C2,
            VirtualKey.VK_OEM_4,
            VirtualKey.VK_OEM_5,
            VirtualKey.VK_OEM_6,
            VirtualKey.VK_OEM_7,
            VirtualKey.VK_OEM_8,
            VirtualKey.VK_OEM_102,
            VirtualKey.VK_OEM_PA1,
            VirtualKey.VK_OEM_PA2,
            VirtualKey.VK_OEM_PA3,
        };

        private static readonly Dictionary<string, IReadOnlyDictionary<char, VirtualKey>> _layoutCache = new Dictionary<string, IReadOnlyDictionary<char, VirtualKey>>(StringComparer.OrdinalIgnoreCase);

        public static bool TryGetKey(string keyboardLayout, char c, out VirtualKey key, INativeMethods nativeMethods)
        {
            if (!_layoutCache.TryGetValue(keyboardLayout, out var map))
            {
                map = BuildMap(keyboardLayout, nativeMethods);
                _layoutCache[keyboardLayout] = map;
            }

            return map.TryGetValue(c, out key);
        }

        private static IReadOnlyDictionary<char, VirtualKey> BuildMap(string keyboardLayout, INativeMethods nativeMethods)
        {
            var res = new Dictionary<char, VirtualKey>();

            IntPtr hkl = KeyboardLayoutMap.GetKeyboardLayout(keyboardLayout, nativeMethods);

            foreach (var key in _oemKeys)
            {
                var c = (char)(nativeMethods.MapVirtualKeyEx((uint)key, MAPVK.VK_TO_CHAR, hkl) & 0x7fffffff);

                if (c != 0 && !res.ContainsKey(c))
                {
                    res.Add(c, key);
                }
            }

            return res;
        }
    }
}
