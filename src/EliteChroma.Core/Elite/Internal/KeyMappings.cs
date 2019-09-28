using System;
using System.Collections;
using System.Collections.Generic;
using EliteFiles.Bindings.Devices;
using static EliteChroma.Elite.Internal.NativeMethods;

namespace EliteChroma.Elite.Internal
{
    // Reference: <EliteRoot>\Products\elite-dangerous-64\ControlSchemes\Help.txt
    internal static class KeyMappings
    {
        private static readonly Map _map = new Map
        {
            { Keyboard.Escape, VirtualKey.VK_ESCAPE },
            { Keyboard.D1, (VirtualKey)'1' },
            { Keyboard.D2, (VirtualKey)'2' },
            { Keyboard.D3, (VirtualKey)'3' },
            { Keyboard.D4, (VirtualKey)'4' },
            { Keyboard.D5, (VirtualKey)'5' },
            { Keyboard.D6, (VirtualKey)'6' },
            { Keyboard.D7, (VirtualKey)'7' },
            { Keyboard.D8, (VirtualKey)'8' },
            { Keyboard.D9, (VirtualKey)'9' },
            { Keyboard.D0, (VirtualKey)'0' },
            { Keyboard.Minus, VirtualKey.VK_OEM_MINUS },
            { Keyboard.EqualsKey, VirtualKey.VK_OEM_PLUS },
            { Keyboard.Backspace, VirtualKey.VK_BACK },
            { Keyboard.Tab, VirtualKey.VK_TAB },
            { Keyboard.Q, (VirtualKey)'Q' },
            { Keyboard.W, (VirtualKey)'W' },
            { Keyboard.E, (VirtualKey)'E' },
            { Keyboard.R, (VirtualKey)'R' },
            { Keyboard.T, (VirtualKey)'T' },
            { Keyboard.Y, (VirtualKey)'Y' },
            { Keyboard.U, (VirtualKey)'U' },
            { Keyboard.I, (VirtualKey)'I' },
            { Keyboard.O, (VirtualKey)'O' },
            { Keyboard.P, (VirtualKey)'P' },
            { Keyboard.Enter, VirtualKey.VK_RETURN },
            { Keyboard.LeftControl, VirtualKey.VK_LCONTROL },
            { Keyboard.A, (VirtualKey)'A' },
            { Keyboard.S, (VirtualKey)'S' },
            { Keyboard.D, (VirtualKey)'D' },
            { Keyboard.F, (VirtualKey)'F' },
            { Keyboard.G, (VirtualKey)'G' },
            { Keyboard.H, (VirtualKey)'H' },
            { Keyboard.J, (VirtualKey)'J' },
            { Keyboard.K, (VirtualKey)'K' },
            { Keyboard.L, (VirtualKey)'L' },
            { Keyboard.LeftShift, VirtualKey.VK_LSHIFT },
            { Keyboard.Z, (VirtualKey)'Z' },
            { Keyboard.X, (VirtualKey)'X' },
            { Keyboard.C, (VirtualKey)'C' },
            { Keyboard.V, (VirtualKey)'V' },
            { Keyboard.B, (VirtualKey)'B' },
            { Keyboard.N, (VirtualKey)'N' },
            { Keyboard.M, (VirtualKey)'M' },
            { Keyboard.Comma, VirtualKey.VK_OEM_COMMA },
            { Keyboard.Period, VirtualKey.VK_OEM_PERIOD },
            { Keyboard.RightShift, VirtualKey.VK_RSHIFT },
            { Keyboard.NumpadMultiply, VirtualKey.VK_MULTIPLY },
            { Keyboard.LeftAlt, VirtualKey.VK_LMENU },
            { Keyboard.Space, VirtualKey.VK_SPACE },
            { Keyboard.CapsLock, VirtualKey.VK_CAPITAL },
            { Keyboard.F1, VirtualKey.VK_F1 },
            { Keyboard.F2, VirtualKey.VK_F2 },
            { Keyboard.F3, VirtualKey.VK_F3 },
            { Keyboard.F4, VirtualKey.VK_F4 },
            { Keyboard.F5, VirtualKey.VK_F5 },
            { Keyboard.F6, VirtualKey.VK_F6 },
            { Keyboard.F7, VirtualKey.VK_F7 },
            { Keyboard.F8, VirtualKey.VK_F8 },
            { Keyboard.F9, VirtualKey.VK_F9 },
            { Keyboard.F10, VirtualKey.VK_F10 },
            { Keyboard.NumLock, VirtualKey.VK_NUMLOCK },
            { Keyboard.ScrollLock, VirtualKey.VK_SCROLL },
            { Keyboard.Numpad7, VirtualKey.VK_NUMPAD7 },
            { Keyboard.Numpad8, VirtualKey.VK_NUMPAD8 },
            { Keyboard.Numpad9, VirtualKey.VK_NUMPAD9 },
            { Keyboard.NumpadSubtract, VirtualKey.VK_SUBTRACT },
            { Keyboard.Numpad4, VirtualKey.VK_NUMPAD4 },
            { Keyboard.Numpad5, VirtualKey.VK_NUMPAD5 },
            { Keyboard.Numpad6, VirtualKey.VK_NUMPAD6 },
            { Keyboard.NumpadAdd, VirtualKey.VK_ADD },
            { Keyboard.Numpad1, VirtualKey.VK_NUMPAD1 },
            { Keyboard.Numpad2, VirtualKey.VK_NUMPAD2 },
            { Keyboard.Numpad3, VirtualKey.VK_NUMPAD3 },
            { Keyboard.Numpad0, VirtualKey.VK_NUMPAD0 },
            { Keyboard.NumpadDecimal, VirtualKey.VK_DECIMAL },
            { Keyboard.Oem102, VirtualKey.VK_OEM_102 },
            { Keyboard.F11, VirtualKey.VK_F11 },
            { Keyboard.F12, VirtualKey.VK_F12 },
            { Keyboard.F13, VirtualKey.VK_F13 },
            { Keyboard.F14, VirtualKey.VK_F14 },
            { Keyboard.F15, VirtualKey.VK_F15 },
            { Keyboard.Kana, VirtualKey.VK_KANA },
            { Keyboard.AbntC1, VirtualKey.VK_ABNT_C1 },
            { Keyboard.Convert, VirtualKey.VK_CONVERT },
            { Keyboard.NoConvert, VirtualKey.VK_NONCONVERT },
            { Keyboard.AbntC2, VirtualKey.VK_ABNT_C2 },
            { Keyboard.PrevTrack, VirtualKey.VK_MEDIA_PREV_TRACK },
            { Keyboard.Kanji, VirtualKey.VK_KANJI },
            { Keyboard.Stop, VirtualKey.VK_MEDIA_STOP },
            { Keyboard.NextTrack, VirtualKey.VK_MEDIA_NEXT_TRACK },
            { Keyboard.RightControl, VirtualKey.VK_RCONTROL },
            { Keyboard.Mute, VirtualKey.VK_VOLUME_MUTE },
            { Keyboard.PlayPause, VirtualKey.VK_MEDIA_PLAY_PAUSE },
            { Keyboard.VolumeDown, VirtualKey.VK_VOLUME_DOWN },
            { Keyboard.VolumeUp, VirtualKey.VK_VOLUME_UP },
            { Keyboard.WebHome, VirtualKey.VK_BROWSER_HOME },
            { Keyboard.NumpadDivide, VirtualKey.VK_DIVIDE },
            { Keyboard.SysRQ, VirtualKey.VK_SNAPSHOT },
            { Keyboard.RightAlt, VirtualKey.VK_RMENU },
            { Keyboard.Pause, VirtualKey.VK_PAUSE },
            { Keyboard.Home, VirtualKey.VK_HOME },
            { Keyboard.UpArrow, VirtualKey.VK_UP },
            { Keyboard.PageUp, VirtualKey.VK_PRIOR },
            { Keyboard.LeftArrow, VirtualKey.VK_LEFT },
            { Keyboard.RightArrow, VirtualKey.VK_RIGHT },
            { Keyboard.End, VirtualKey.VK_END },
            { Keyboard.DownArrow, VirtualKey.VK_DOWN },
            { Keyboard.PageDown, VirtualKey.VK_NEXT },
            { Keyboard.Insert, VirtualKey.VK_INSERT },
            { Keyboard.Delete, VirtualKey.VK_DELETE },
            { Keyboard.LeftWin, VirtualKey.VK_LWIN },
            { Keyboard.RightWin, VirtualKey.VK_RWIN },
            { Keyboard.Apps, VirtualKey.VK_APPS },
            { Keyboard.Sleep, VirtualKey.VK_SLEEP },
            { Keyboard.WebSearch, VirtualKey.VK_BROWSER_SEARCH },
            { Keyboard.WebFavourites, VirtualKey.VK_BROWSER_FAVORITES },
            { Keyboard.WebRefresh, VirtualKey.VK_BROWSER_REFRESH },
            { Keyboard.WebStop, VirtualKey.VK_BROWSER_STOP },
            { Keyboard.WebForward, VirtualKey.VK_BROWSER_FORWARD },
            { Keyboard.WebBack, VirtualKey.VK_BROWSER_BACK },
            { Keyboard.Mail, VirtualKey.VK_LAUNCH_MAIL },
        };

        public static IReadOnlyDictionary<string, VirtualKey> EliteKeys => _map.EliteKeys;

        public static IReadOnlyDictionary<VirtualKey, string> VirtualKeys => _map.VirtualKeys;

        private sealed class Map : IEnumerable
        {
            public Dictionary<string, VirtualKey> EliteKeys { get; } = new Dictionary<string, VirtualKey>(StringComparer.Ordinal);

            public Dictionary<VirtualKey, string> VirtualKeys { get; } = new Dictionary<VirtualKey, string>();

            public void Add(string eliteKey, VirtualKey virtualKey)
            {
                EliteKeys.Add(eliteKey, virtualKey);
                VirtualKeys.Add(virtualKey, eliteKey);
            }

            public IEnumerator GetEnumerator() => ((IEnumerable)EliteKeys).GetEnumerator();
        }
    }
}
