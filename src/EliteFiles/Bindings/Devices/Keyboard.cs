using System;
using System.Collections.Generic;
using System.Linq;

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
        public const string Backspace = "Key_Backspace";
        public const string Tab = "Key_Tab";
        public const string Enter = "Key_Enter";
        public const string LeftControl = "Key_LeftControl";
        public const string LeftShift = "Key_LeftShift";
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
#pragma warning restore 1591, SA1600

        private const string _keyNamePrefix = "Key_";

        private static readonly IReadOnlyDictionary<string, char> _namedKeys = new Dictionary<string, char>(StringComparer.Ordinal)
        {
            { "Key_Minus", '\u002D' },
            { "Key_Plus", '\u002B' },
            { "Key_Equals", '\u003D' },
            { Backspace, '\u0008' },
            { Tab, '\u0009' },
            { "Key_LeftBracket", '\u005B' },
            { "Key_RightBracket", '\u005D' },
            { Enter, '\u000d' },
            { "Key_SemiColon", '\u003B' },
            { "Key_Apostrophe", '\u0027' },
            { "Key_Grave", '\u0060' },
            { "Key_Hash", '\u0023' },
            { "Key_Comma", '\u002C' },
            { "Key_Period", '\u002E' },
            { "Key_Slash", '\u002F' },
            { Space, '\u0020' },
            { "Key_BackSlash", '\u005C' },
            { "Key_Colon", '\u003A' },
            { "Key_Underline", '\u005F' },
            { "Key_LessThan", '\u003C' },
            { "Key_GreaterThan", '\u003E' },
            { "Key_Acute", '\u00B4' },
            { "Key_Circumflex", '\u005E' },
            { "Key_Tilde", '\u007E' },
            { "Key_Ring", '\u00B0' },
            { "Key_Umlaut", '\u00A8' },
            { "Key_Half", '\u00BD' },
            { "Key_Dollar", '\u0024' },
            { "Key_SuperscriptTwo", '\u00B2' },
            { "Key_Ampersand", '\u0026' },
            { "Key_DoubleQuote", '\u0022' },
            { "Key_LeftParenthesis", '\u0028' },
            { "Key_RightParenthesis", '\u0029' },
            { "Key_Asterisk", '\u002A' },
            { "Key_ExclamationPoint", '\u0021' },
            { "Key_Macron", '\u00AF' },
            { "Key_Overline", '\u0305' },
            { "Key_Breve", '\u02D8' },
            { "Key_Overdot", '\u02D9' },
            { "Key_HookAbove", '\u0309' },
            { "Key_RingAbove", '\u02DA' },
            { "Key_DoubleAcute", '\u02DD' },
            { "Key_Caron", '\u02C7' },
            { "Key_VerticalLineAbove", '\u030D' },
            { "Key_DoubleVerticalLineAbove", '\u030E' },
            { "Key_DoubleGrave", '\u030F' },
            { "Key_Candrabindu", '\u0310' },
            { "Key_InvertedBreve", '\u0311' },
            { "Key_TurnedCommaAbove", '\u0312' },
            { "Key_CommaAbove", '\u0313' },
            { "Key_ReversedCommaAbove", '\u0314' },
            { "Key_CommaAboveRight", '\u0315' },
            { "Key_GraveBelow", '\u0316' },
            { "Key_AcuteBelow", '\u0317' },
            { "Key_LeftTackBelow", '\u0318' },
            { "Key_RightTackBelow", '\u0319' },
            { "Key_LeftAngleAbove", '\u031A' },
            { "Key_Horn", '\u031B' },
            { "Key_LeftHalfRingBelow", '\u031C' },
            { "Key_UpTackBelow", '\u031D' },
            { "Key_DownTackBelow", '\u031E' },
            { "Key_PlusSignBelow", '\u031F' },
            { "Key_Cedilla", '\u00B8' },
        };

        private static readonly IReadOnlyDictionary<char, string> _namedKeysInv = _namedKeys.ToDictionary(kv => kv.Value, kv => kv.Key);

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
                c = keyName[^1];
                return true;
            }

            // Named character key (e.g. "Key_Asterisk" -> '*')
            return _namedKeys.TryGetValue(keyName, out c);
        }

        /// <summary>
        /// Gets the keyboard key name associated with the specified character.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="keyName">The matching key name.</param>
        /// <returns><c>true</c> if <paramref name="c"/> is a has a corresponding keyboard key name; otherwise, <c>false</c>.</returns>
        public static bool TryGetKeyName(char c, out string? keyName)
        {
            // Return named character bindings first (e.g. '*' -> "Key_Asterisk")
            if (_namedKeysInv.TryGetValue(c, out keyName))
            {
                return true;
            }

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
