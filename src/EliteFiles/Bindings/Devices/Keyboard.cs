using System;
using System.Collections.Generic;

namespace EliteFiles.Bindings.Devices
{
    /// <summary>
    /// Defines keyboard key names.
    /// </summary>
    /// <remarks>
    /// References:
    /// - &lt;EliteRoot&gt;\Products\elite-dangerous-64\ControlSchemes\Help.txt.
    /// - &lt;EliteRoot&gt;\Products\elite-dangerous-64\EliteDangerous64.exe (binary scraping).
    /// </remarks>
    public static class Keyboard
    {
#pragma warning disable 1591, SA1600
        public const string Escape = "Key_Escape";
        public const string Minus = "Key_Minus";
        public const string EqualsKey = "Key_Equals";
        public const string Backspace = "Key_Backspace";
        public const string Tab = "Key_Tab";
        public const string LeftBracket = "Key_LeftBracket";
        public const string RightBracket = "Key_RightBracket";
        public const string Enter = "Key_Enter";
        public const string LeftControl = "Key_LeftControl";
        public const string SemiColon = "Key_SemiColon";
        public const string Apostrophe = "Key_Apostrophe";
        public const string Grave = "Key_Grave";
        public const string LeftShift = "Key_LeftShift";
        public const string BackSlash = "Key_BackSlash";
        public const string Comma = "Key_Comma";
        public const string Period = "Key_Period";
        public const string Slash = "Key_Slash";
        public const string RightShift = "Key_RightShift";
        public const string NumpadMultiply = "Key_Numpad_Multiply";
        public const string LeftAlt = "Key_LeftAlt";
        public const string Space = "Key_Space";
        public const string CapsLock = "Key_CapsLock";
        public const string F1 = "Key_F1";
        public const string F2 = "Key_F2";
        public const string F3 = "Key_F3";
        public const string F4 = "Key_F4";
        public const string F5 = "Key_F5";
        public const string F6 = "Key_F6";
        public const string F7 = "Key_F7";
        public const string F8 = "Key_F8";
        public const string F9 = "Key_F9";
        public const string F10 = "Key_F10";
        public const string NumLock = "Key_NumLock";
        public const string ScrollLock = "Key_ScrollLock";
        public const string Numpad7 = "Key_Numpad_7";
        public const string Numpad8 = "Key_Numpad_8";
        public const string Numpad9 = "Key_Numpad_9";
        public const string NumpadSubtract = "Key_Numpad_Subtract";
        public const string Numpad4 = "Key_Numpad_4";
        public const string Numpad5 = "Key_Numpad_5";
        public const string Numpad6 = "Key_Numpad_6";
        public const string NumpadAdd = "Key_Numpad_Add";
        public const string Numpad1 = "Key_Numpad_1";
        public const string Numpad2 = "Key_Numpad_2";
        public const string Numpad3 = "Key_Numpad_3";
        public const string Numpad0 = "Key_Numpad_0";
        public const string NumpadDecimal = "Key_Numpad_Decimal";
        public const string F11 = "Key_F11";
        public const string F12 = "Key_F12";
        public const string F13 = "Key_F13";
        public const string F14 = "Key_F14";
        public const string F15 = "Key_F15";
        public const string Kana = "Key_Kana";
        public const string AbntC1 = "Key_ABNT_C1";
        public const string Convert = "Key_Convert";
        public const string NoConvert = "Key_NoConvert";
        public const string Yen = "Key_Yen";
        public const string AbntC2 = "Key_ABNT_C2";
        public const string NumpadEquals = "Key_Numpad_Equals";
        public const string PrevTrack = "Key_PrevTrack";
        public const string AT = "Key_AT";
        public const string Colon = "Key_Colon";
        public const string Underline = "Key_Underline";
        public const string Kanji = "Key_Kanji";
        public const string Stop = "Key_Stop";
        public const string AX = "Key_AX";
        public const string Unlabeled = "Key_Unlabeled";
        public const string NextTrack = "Key_NextTrack";
        public const string NumpadEnter = "Key_Numpad_Enter";
        public const string RightControl = "Key_RightControl";
        public const string Mute = "Key_Mute";
        public const string Calculator = "Key_Calculator";
        public const string PlayPause = "Key_PlayPause";
        public const string MediaStop = "Key_MediaStop";
        public const string VolumeDown = "Key_VolumeDown";
        public const string VolumeUp = "Key_VolumeUp";
        public const string WebHome = "Key_WebHome";
        public const string NumpadComma = "Key_Numpad_Comma";
        public const string NumpadDivide = "Key_Numpad_Divide";
        public const string SysRQ = "Key_SYSRQ";
        public const string RightAlt = "Key_RightAlt";
        public const string Pause = "Key_Pause";
        public const string Home = "Key_Home";
        public const string UpArrow = "Key_UpArrow";
        public const string PageUp = "Key_PageUp";
        public const string LeftArrow = "Key_LeftArrow";
        public const string RightArrow = "Key_RightArrow";
        public const string End = "Key_End";
        public const string DownArrow = "Key_DownArrow";
        public const string PageDown = "Key_PageDown";
        public const string Insert = "Key_Insert";
        public const string Delete = "Key_Delete";
        public const string LeftWin = "Key_LeftWin";
        public const string RightWin = "Key_RightWin";
        public const string Apps = "Key_Apps";
        public const string Power = "Key_Power";
        public const string Sleep = "Key_Sleep";
        public const string Wake = "Key_Wake";
        public const string WebSearch = "Key_WebSearch";
        public const string WebFavourites = "Key_WebFavourites";
        public const string WebRefresh = "Key_WebRefresh";
        public const string WebStop = "Key_WebStop";
        public const string WebForward = "Key_WebForward";
        public const string WebBack = "Key_WebBack";
        public const string MyComputer = "Key_MyComputer";
        public const string Mail = "Key_Mail";
        public const string MediaSelect = "Key_MediaSelect";
        public const string GreenModifier = "Key_GreenModifier";
        public const string OrangeModifier = "Key_OrangeModifier";
        public const string F16 = "Key_F16";
        public const string F17 = "Key_F17";
        public const string F18 = "Key_F18";
        public const string F19 = "Key_F19";
        public const string F20 = "Key_F20";
        public const string Section = "Key_Section";
        public const string Menu = "Key_Menu";
        public const string Help = "Key_Help";
        public const string Function = "Key_Function";
        public const string Clear = "Key_Clear";
        public const string LeftCommand = "Key_LeftCommand";
        public const string RightCommand = "Key_RightCommand";
        public const string Plus = "Key_Plus";
        public const string Hash = "Key_Hash";
        public const string LessThan = "Key_LessThan";
        public const string GreaterThan = "Key_GreaterThan";
        public const string Acute = "Key_Acute";
        public const string Circumflex = "Key_Circumflex";
        public const string Tilde = "Key_Tilde";
        public const string Ring = "Key_Ring";
        public const string Umlaut = "Key_Umlaut";
        public const string Half = "Key_Half";
        public const string Dollar = "Key_Dollar";
        public const string SuperscriptTwo = "Key_SuperscriptTwo";
        public const string Ampersand = "Key_Ampersand";
        public const string DoubleQuote = "Key_DoubleQuote";
        public const string LeftParenthesis = "Key_LeftParenthesis";
        public const string RightParenthesis = "Key_RightParenthesis";
        public const string Asterisk = "Key_Asterisk";
        public const string ExclamationPoint = "Key_ExclamationPoint";
        public const string Macron = "Key_Macron";
        public const string Overline = "Key_Overline";
        public const string Breve = "Key_Breve";
        public const string Overdot = "Key_Overdot";
        public const string HookAbove = "Key_HookAbove";
        public const string RingAbove = "Key_RingAbove";
        public const string DoubleAcute = "Key_DoubleAcute";
        public const string Caron = "Key_Caron";
        public const string VerticalLineAbove = "Key_VerticalLineAbove";
        public const string DoubleVerticalLineAbove = "Key_DoubleVerticalLineAbove";
        public const string DoubleGrave = "Key_DoubleGrave";
        public const string Candrabindu = "Key_Candrabindu";
        public const string InvertedBreve = "Key_InvertedBreve";
        public const string TurnedCommaAbove = "Key_TurnedCommaAbove";
        public const string CommaAbove = "Key_CommaAbove";
        public const string ReversedCommaAbove = "Key_ReversedCommaAbove";
        public const string CommaAboveRight = "Key_CommaAboveRight";
        public const string GraveBelow = "Key_GraveBelow";
        public const string AcuteBelow = "Key_AcuteBelow";
        public const string LeftTackBelow = "Key_LeftTackBelow";
        public const string RightTackBelow = "Key_RightTackBelow";
        public const string LeftAngleAbove = "Key_LeftAngleAbove";
        public const string Horn = "Key_Horn";
        public const string LeftHalfRingBelow = "Key_LeftHalfRingBelow";
        public const string UpTackBelow = "Key_UpTackBelow";
        public const string DownTackBelow = "Key_DownTackBelow";
        public const string PlusSignBelow = "Key_PlusSignBelow";
        public const string Cedilla = "Key_Cedilla";
#pragma warning restore 1591, SA1600

        private const string _keyNamePrefix = "Key_";

        private static readonly Dictionary<char, string> _charKeyNames = new Dictionary<char, string>();

        /// <summary>
        /// Gets the character associated with the specified keyboard key name.
        /// </summary>
        /// <param name="keyName">The keyboard key name.</param>
        /// <param name="c">The matching character.</param>
        /// <returns><c>true</c> if <paramref name="keyName"/> is a character key name; otherwise, <c>false</c>.</returns>
        public static bool TryGetKeyChar(string keyName, out char c)
        {
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            if (!keyName.StartsWith(_keyNamePrefix, StringComparison.Ordinal))
            {
                c = default;
                return false;
            }

            // Character key binding (e.g. "Key_A" → 'A', "Key_ß" → 'ß')
            if (keyName.Length - _keyNamePrefix.Length == 1)
            {
                c = keyName[keyName.Length - 1];
                return true;
            }

            c = default;
            return false;
        }

        /// <summary>
        /// Gets the keyboard key name associated with the specified character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="keyName">The matching key name.</param>
        /// <returns><c>true</c> if <paramref name="c"/> is a has a corresponding keyboard key name; otherwise, <c>false</c>.</returns>
        public static bool TryGetKeyName(char c, out string keyName)
        {
            if (c <= ' ')
            {
                keyName = null;
                return false;
            }

            if (!_charKeyNames.TryGetValue(c, out keyName))
            {
                keyName = $"{_keyNamePrefix}{c}";
                _charKeyNames[c] = keyName;
            }

            return true;
        }
    }
}
