using Colore.Data;
using Colore.Effects.ChromaLink;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;

namespace EliteChroma.Chroma
{
    public static class EffectExtensions
    {
        private static readonly Key[] _allKeys = GetAllKeys();

        public static CustomKeyboardEffect Combine(this CustomKeyboardEffect keyboard, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < _allKeys.Length; i++)
            {
                var key = _allKeys[i];
                keyboard[key] = keyboard[key].Combine(c, cPct);
            }

            return keyboard;
        }

        public static CustomKeyboardEffect Max(this CustomKeyboardEffect keyboard, Color c)
        {
            for (var i = 0; i < _allKeys.Length; i++)
            {
                var key = _allKeys[i];
                keyboard[key] = keyboard[key].Max(c);
            }

            return keyboard;
        }

        public static CustomKeyboardEffect MaxAt(this CustomKeyboardEffect keyboard, int row, int column, Color c)
        {
            var key = GetKeyAt(row, column);
            keyboard[key] = keyboard[key].Max(c);

            return keyboard;
        }

        public static CustomMouseEffect Combine(this CustomMouseEffect mouse, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < MouseConstants.MaxLeds; i++)
            {
                mouse[i] = mouse[i].Combine(c, cPct);
            }

            return mouse;
        }

        public static CustomMouseEffect Max(this CustomMouseEffect mouse, Color c)
        {
            for (var i = 0; i < MouseConstants.MaxLeds; i++)
            {
                mouse[i] = mouse[i].Max(c);
            }

            return mouse;
        }

        public static CustomMousepadEffect Combine(this CustomMousepadEffect mousepad, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < MousepadConstants.MaxLeds; i++)
            {
                mousepad[i] = mousepad[i].Combine(c, cPct);
            }

            return mousepad;
        }

        public static CustomMousepadEffect Max(this CustomMousepadEffect mousepad, Color c)
        {
            for (var i = 0; i < MousepadConstants.MaxLeds; i++)
            {
                mousepad[i] = mousepad[i].Max(c);
            }

            return mousepad;
        }

        public static CustomKeypadEffect Combine(this CustomKeypadEffect keypad, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < KeypadConstants.MaxKeys; i++)
            {
                keypad[i] = keypad[i].Combine(c, cPct);
            }

            return keypad;
        }

        public static CustomKeypadEffect Max(this CustomKeypadEffect keypad, Color c)
        {
            for (var i = 0; i < KeypadConstants.MaxKeys; i++)
            {
                keypad[i] = keypad[i].Max(c);
            }

            return keypad;
        }

        public static CustomHeadsetEffect Combine(this CustomHeadsetEffect headset, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < HeadsetConstants.MaxLeds; i++)
            {
                headset[i] = headset[i].Combine(c, cPct);
            }

            return headset;
        }

        public static CustomHeadsetEffect Max(this CustomHeadsetEffect headset, Color c)
        {
            for (var i = 0; i < HeadsetConstants.MaxLeds; i++)
            {
                headset[i] = headset[i].Max(c);
            }

            return headset;
        }

        public static CustomChromaLinkEffect Combine(this CustomChromaLinkEffect chromaLink, Color c, double cPct = 0.5)
        {
            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
            {
                chromaLink[i] = chromaLink[i].Combine(c, cPct);
            }

            return chromaLink;
        }

        public static CustomChromaLinkEffect Max(this CustomChromaLinkEffect chromaLink, Color c)
        {
            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
            {
                chromaLink[i] = chromaLink[i].Max(c);
            }

            return chromaLink;
        }

        // We will be using this as an alternative to call keyboard[int index].
        // Since keyboard[Key key] sets KeyboardConstants.KeyFlag while keyboard[int index]
        // does not, wrong key colors get read when reading by index.
        private static Key[] GetAllKeys()
        {
            var res = new Key[KeyboardConstants.MaxRows * KeyboardConstants.MaxColumns];

            var i = 0;
            for (var row = 0; row < KeyboardConstants.MaxRows; row++)
            {
                for (var col = 0; col < KeyboardConstants.MaxColumns; col++)
                {
                    res[i++] = GetKeyAt(row, col);
                }
            }

            return res;
        }

        private static Key GetKeyAt(int row, int column) => (Key)((row << 8) + column);
    }
}
