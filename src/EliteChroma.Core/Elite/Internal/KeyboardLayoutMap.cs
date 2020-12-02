using System;
using System.Collections.Generic;
using System.Globalization;
using EliteChroma.Core.Internal;

namespace EliteChroma.Elite.Internal
{
    internal static class KeyboardLayoutMap
    {
        private static readonly Dictionary<string, IntPtr> _keyboardLayouts = new Dictionary<string, IntPtr>(StringComparer.OrdinalIgnoreCase);

        public static string GetCurrentLayout(INativeMethods nativeMethods)
        {
            var hkl = nativeMethods.GetKeyboardLayout(0);

            try
            {
                var ci = CultureInfo.GetCultureInfo(hkl.ToInt32() & 0xffff);
                return ci.Name;
            }
            catch (CultureNotFoundException)
            {
                return "en-US";
            }
        }

        public static IntPtr GetKeyboardLayout(string keyboardLayout, INativeMethods nativeMethods)
        {
            _ = keyboardLayout ?? throw new ArgumentNullException(nameof(keyboardLayout));

            if (_keyboardLayouts.TryGetValue(keyboardLayout, out var hkl1))
            {
                return hkl1;
            }

            try
            {
                var ci = CultureInfo.GetCultureInfo(keyboardLayout);

                var n = nativeMethods.GetKeyboardLayoutList(0, null);
                var hkls = new IntPtr[n];
                _ = nativeMethods.GetKeyboardLayoutList(n, hkls);

                foreach (var hkl2 in hkls)
                {
                    if ((hkl2.ToInt32() & 0xffff) == ci.LCID)
                    {
                        _keyboardLayouts[keyboardLayout] = hkl2;
                        return hkl2;
                    }
                }
            }
            catch (CultureNotFoundException)
            {
            }

            return nativeMethods.GetKeyboardLayout(0);
        }
    }
}
