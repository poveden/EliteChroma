using ChromaWrapper.Keyboard;
using EliteChroma.Core.Elite.Internal;
using EliteFiles.Bindings.Devices;

namespace EliteChroma.Core.Internal
{
    // References:
    // - <EliteRoot>\Products\elite-dangerous-64\ControlSchemes\Help.txt
    // - <EliteRoot>\Products\elite-dangerous-64\EliteDangerous64.exe (binary scraping)
    // - https://developer.razer.com/works-with-chroma/razer-chroma-led-profiles/
    internal static class KeyMappings
    {
        private static readonly Dictionary<string, KeyboardKey> _keys = new Dictionary<string, KeyboardKey>(StringComparer.Ordinal)
        {
            { Keyboard.Escape, KeyboardKey.Esc },
            { GetKeyName('1'), KeyboardKey.D1 },
            { GetKeyName('2'), KeyboardKey.D2 },
            { GetKeyName('3'), KeyboardKey.D3 },
            { GetKeyName('4'), KeyboardKey.D4 },
            { GetKeyName('5'), KeyboardKey.D5 },
            { GetKeyName('6'), KeyboardKey.D6 },
            { GetKeyName('7'), KeyboardKey.D7 },
            { GetKeyName('8'), KeyboardKey.D8 },
            { GetKeyName('9'), KeyboardKey.D9 },
            { GetKeyName('0'), KeyboardKey.D0 },
            { Keyboard.Backspace, KeyboardKey.Backspace },
            { Keyboard.Tab, KeyboardKey.Tab },
            { GetKeyName('Q'), KeyboardKey.Q },
            { GetKeyName('W'), KeyboardKey.W },
            { GetKeyName('E'), KeyboardKey.E },
            { GetKeyName('R'), KeyboardKey.R },
            { GetKeyName('T'), KeyboardKey.T },
            { GetKeyName('Y'), KeyboardKey.Y },
            { GetKeyName('U'), KeyboardKey.U },
            { GetKeyName('I'), KeyboardKey.I },
            { GetKeyName('O'), KeyboardKey.O },
            { GetKeyName('P'), KeyboardKey.P },
            { Keyboard.Enter, KeyboardKey.Enter },
            { Keyboard.LeftControl, KeyboardKey.LCtrl },
            { GetKeyName('A'), KeyboardKey.A },
            { GetKeyName('S'), KeyboardKey.S },
            { GetKeyName('D'), KeyboardKey.D },
            { GetKeyName('F'), KeyboardKey.F },
            { GetKeyName('G'), KeyboardKey.G },
            { GetKeyName('H'), KeyboardKey.H },
            { GetKeyName('J'), KeyboardKey.J },
            { GetKeyName('K'), KeyboardKey.K },
            { GetKeyName('L'), KeyboardKey.L },
            { Keyboard.LeftShift, KeyboardKey.LShift },
            { GetKeyName('Z'), KeyboardKey.Z },
            { GetKeyName('X'), KeyboardKey.X },
            { GetKeyName('C'), KeyboardKey.C },
            { GetKeyName('V'), KeyboardKey.V },
            { GetKeyName('B'), KeyboardKey.B },
            { GetKeyName('N'), KeyboardKey.N },
            { GetKeyName('M'), KeyboardKey.M },
            { Keyboard.RightShift, KeyboardKey.RShift },
            { Keyboard.NumpadMultiply, KeyboardKey.NumPadMultiply },
            { Keyboard.LeftAlt, KeyboardKey.LAlt },
            { Keyboard.Space, KeyboardKey.Space },
            { Keyboard.CapsLock, KeyboardKey.CapsLock },
            { Keyboard.F1, KeyboardKey.F1 },
            { Keyboard.F2, KeyboardKey.F2 },
            { Keyboard.F3, KeyboardKey.F3 },
            { Keyboard.F4, KeyboardKey.F4 },
            { Keyboard.F5, KeyboardKey.F5 },
            { Keyboard.F6, KeyboardKey.F6 },
            { Keyboard.F7, KeyboardKey.F7 },
            { Keyboard.F8, KeyboardKey.F8 },
            { Keyboard.F9, KeyboardKey.F9 },
            { Keyboard.F10, KeyboardKey.F10 },
            { Keyboard.NumLock, KeyboardKey.NumLock },
            { Keyboard.ScrollLock, KeyboardKey.Scroll },
            { Keyboard.Numpad7, KeyboardKey.NumPad7 },
            { Keyboard.Numpad8, KeyboardKey.NumPad8 },
            { Keyboard.Numpad9, KeyboardKey.NumPad9 },
            { Keyboard.NumpadSubtract, KeyboardKey.NumPadSubtract },
            { Keyboard.Numpad4, KeyboardKey.NumPad4 },
            { Keyboard.Numpad5, KeyboardKey.NumPad5 },
            { Keyboard.Numpad6, KeyboardKey.NumPad6 },
            { Keyboard.NumpadAdd, KeyboardKey.NumPadAdd },
            { Keyboard.Numpad1, KeyboardKey.NumPad1 },
            { Keyboard.Numpad2, KeyboardKey.NumPad2 },
            { Keyboard.Numpad3, KeyboardKey.NumPad3 },
            { Keyboard.Numpad0, KeyboardKey.NumPad0 },
            { Keyboard.NumpadDecimal, KeyboardKey.NumPadDecimal },
            { Keyboard.F11, KeyboardKey.F11 },
            { Keyboard.F12, KeyboardKey.F12 },
            { Keyboard.F13, 0 },
            { Keyboard.F14, 0 },
            { Keyboard.F15, 0 },
            { Keyboard.Kana, 0 },
            { Keyboard.AbntC1, 0 },
            { Keyboard.Convert, KeyboardKey.Jpn3 },
            { Keyboard.NoConvert, KeyboardKey.Jpn4 },
            { Keyboard.Yen, KeyboardKey.Jpn1 },
            { Keyboard.AbntC2, 0 },
            { Keyboard.NumpadEquals, 0 },
            { Keyboard.PrevTrack, 0 },
            { Keyboard.AT, 0 },
            { Keyboard.Kanji, 0 },
            { Keyboard.Stop, 0 },
            { Keyboard.AX, 0 },
            { Keyboard.Unlabeled, 0 },
            { Keyboard.NextTrack, 0 },
            { Keyboard.NumpadEnter, KeyboardKey.NumPadEnter },
            { Keyboard.RightControl, KeyboardKey.RCtrl },
            { Keyboard.Mute, 0 },
            { Keyboard.Calculator, 0 },
            { Keyboard.PlayPause, 0 },
            { Keyboard.MediaStop, 0 },
            { Keyboard.VolumeDown, 0 },
            { Keyboard.VolumeUp, 0 },
            { Keyboard.WebHome, 0 },
            { Keyboard.NumpadComma, 0 },
            { Keyboard.NumpadDivide, KeyboardKey.NumPadDivide },
            { Keyboard.SysRQ, KeyboardKey.PrintScreen },
            { Keyboard.RightAlt, KeyboardKey.RAlt },
            { Keyboard.Pause, KeyboardKey.Pause },
            { Keyboard.Home, KeyboardKey.Home },
            { Keyboard.UpArrow, KeyboardKey.Up },
            { Keyboard.PageUp, KeyboardKey.PageUp },
            { Keyboard.LeftArrow, KeyboardKey.Left },
            { Keyboard.RightArrow, KeyboardKey.Right },
            { Keyboard.End, KeyboardKey.End },
            { Keyboard.DownArrow, KeyboardKey.Down },
            { Keyboard.PageDown, KeyboardKey.PageDown },
            { Keyboard.Insert, KeyboardKey.Insert },
            { Keyboard.Delete, KeyboardKey.Delete },
            { Keyboard.LeftWin, KeyboardKey.LWin },
            { Keyboard.RightWin, 0 },
            { Keyboard.Apps, KeyboardKey.RMenu },
            { Keyboard.Power, 0 },
            { Keyboard.Sleep, 0 },
            { Keyboard.Wake, 0 },
            { Keyboard.WebSearch, 0 },
            { Keyboard.WebFavourites, 0 },
            { Keyboard.WebRefresh, 0 },
            { Keyboard.WebStop, 0 },
            { Keyboard.WebForward, 0 },
            { Keyboard.WebBack, 0 },
            { Keyboard.MyComputer, 0 },
            { Keyboard.Mail, 0 },
            { Keyboard.MediaSelect, 0 },
            { Keyboard.GreenModifier, 0 },
            { Keyboard.OrangeModifier, 0 },
            { Keyboard.F16, 0 },
            { Keyboard.F17, 0 },
            { Keyboard.F18, 0 },
            { Keyboard.F19, 0 },
            { Keyboard.F20, 0 },
            { Keyboard.Section, 0 },
            { Keyboard.Menu, 0 },
            { Keyboard.Help, 0 },
            { Keyboard.Function, KeyboardKey.Fn },
            { Keyboard.Clear, 0 },
            { Keyboard.LeftCommand, 0 },
            { Keyboard.RightCommand, 0 },
        };

        private static readonly Dictionary<uint, KeyboardKey> _scanCodes = new Dictionary<uint, KeyboardKey>()
        {
            { 0x7D, KeyboardKey.Jpn1 },
            { 0x29, KeyboardKey.Oem1 },
            { 0x0C, KeyboardKey.Oem2 },
            { 0x0D, KeyboardKey.Oem3 },
            { 0x1A, KeyboardKey.Oem4 },
            { 0x1B, KeyboardKey.Oem5 },
            { 0x2B, KeyboardKey.Oem6 },
            { 0x27, KeyboardKey.Oem7 },
            { 0x28, KeyboardKey.Oem8 },
            { 0x56, KeyboardKey.Eur2 },
            { 0x33, KeyboardKey.Oem9 },
            { 0x34, KeyboardKey.Oem10 },
            { 0x35, KeyboardKey.Oem11 },
            { 0x73, KeyboardKey.Jpn2 },
            { 0xE01C, KeyboardKey.NumPadEnter },
            { 0x7B, KeyboardKey.Jpn3 },
            { 0x79, KeyboardKey.Jpn4 },
            { 0x70, KeyboardKey.Jpn5 },
        };

        public static bool TryGetKey(string keyName, string? keyboardLayout, bool enUSOverride, out KeyboardKey key, INativeMethods nativeMethods)
        {
            if (_keys.TryGetValue(keyName, out key))
            {
                return true;
            }

            keyboardLayout ??= KeyboardLayoutMap.GetCurrentLayout(nativeMethods);

            _ = Elite.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, enUSOverride, out NativeMethods.VirtualKey vk, nativeMethods);

            IntPtr hkl = KeyboardLayoutMap.GetKeyboardLayout(keyboardLayout, nativeMethods);

            uint scanCode = nativeMethods.MapVirtualKeyEx((uint)vk, NativeMethods.MAPVK.VK_TO_VSC_EX, hkl);

            return _scanCodes.TryGetValue(scanCode, out key);
        }

        private static string GetKeyName(char c)
        {
            _ = Keyboard.TryGetKeyName(c, out string? keyName);
            return keyName!;
        }
    }
}
