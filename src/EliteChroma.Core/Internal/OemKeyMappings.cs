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

        private static readonly IReadOnlyDictionary<char, uint> _enUSScanCodes = new Dictionary<char, uint>
        {
            [';'] = 0x27, // VK_OEM_1
            ['='] = 0x0D, // VK_OEM_PLUS
            [','] = 0x33, // VK_OEM_COMMA
            ['-'] = 0x0C, // VK_OEM_MINUS
            ['.'] = 0x34, // VK_OEM_PERIOD
            ['/'] = 0x35, // VK_OEM_2
            ['`'] = 0x29, // VK_OEM_3
            ['['] = 0x1A, // VK_OEM_4
            ['\\'] = 0x2B, // VK_OEM_5, VK_OEM_102
            [']'] = 0x1B, // VK_OEM_6
            ['\''] = 0x28, // VK_OEM_7
        };

        private static readonly Dictionary<string, IReadOnlyDictionary<char, VirtualKey>> _layoutCache = new Dictionary<string, IReadOnlyDictionary<char, VirtualKey>>(StringComparer.OrdinalIgnoreCase);

        public static bool TryGetKey(string? keyboardLayout, char c, bool enUSOverride, out VirtualKey key, INativeMethods nativeMethods)
        {
            keyboardLayout ??= KeyboardLayoutMap.GetCurrentLayout(nativeMethods);

            if (!_layoutCache.TryGetValue(keyboardLayout, out IReadOnlyDictionary<char, VirtualKey>? map))
            {
                map = BuildMap(keyboardLayout, nativeMethods);
                _layoutCache[keyboardLayout] = map;
            }

            if (!enUSOverride)
            {
                return map.TryGetValue(c, out key);
            }

            // Elite:Dangerous won't recognize some keyboard layouts (at least, es-ES)
            // and treats them as en-US. Here we fix this behavior.
            _ = _enUSScanCodes.TryGetValue(c, out uint scanCode);

            IntPtr hkl = KeyboardLayoutMap.GetKeyboardLayout(keyboardLayout, nativeMethods);
            key = (VirtualKey)nativeMethods.MapVirtualKeyEx(scanCode, MAPVK.VSC_TO_VK_EX, hkl);
            return key != 0;
        }

        private static IReadOnlyDictionary<char, VirtualKey> BuildMap(string keyboardLayout, INativeMethods nativeMethods)
        {
            var res = new Dictionary<char, VirtualKey>();

            IntPtr hkl = KeyboardLayoutMap.GetKeyboardLayout(keyboardLayout, nativeMethods);

            foreach (VirtualKey key in _oemKeys)
            {
                char c = (char)(nativeMethods.MapVirtualKeyEx((uint)key, MAPVK.VK_TO_CHAR, hkl) & 0x7fffffff);

                if (c != 0 && !res.ContainsKey(c))
                {
                    res.Add(c, key);
                }
            }

            return res;
        }
    }
}
