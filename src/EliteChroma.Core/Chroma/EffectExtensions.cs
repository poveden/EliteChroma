﻿using Colore.Data;
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

        public static KeyboardCustom Max(this KeyboardCustom keyboard, Color c)
        {
            for (var i = 0; i < _allKeys.Length; i++)
            {
                var key = _allKeys[i];
                keyboard[key] = keyboard[key].Max(c);
            }

            return keyboard;
        }

        public static MouseCustom Max(this MouseCustom mouse, Color c)
        {
            for (var i = 0; i < MouseConstants.MaxLeds; i++)
            {
                mouse[i] = mouse[i].Max(c);
            }

            return mouse;
        }

        public static MousepadCustom Max(this MousepadCustom mousepad, Color c)
        {
            for (var i = 0; i < MousepadConstants.MaxLeds; i++)
            {
                mousepad[i] = mousepad[i].Max(c);
            }

            return mousepad;
        }

        public static KeypadCustom Max(this KeypadCustom keypad, Color c)
        {
            for (var i = 0; i < KeypadConstants.MaxKeys; i++)
            {
                keypad[i] = keypad[i].Max(c);
            }

            return keypad;
        }

        public static HeadsetCustom Max(this HeadsetCustom headset, Color c)
        {
            for (var i = 0; i < HeadsetConstants.MaxLeds; i++)
            {
                headset[i] = headset[i].Max(c);
            }

            return headset;
        }

        public static ChromaLinkCustom Max(this ChromaLinkCustom chromaLink, Color c)
        {
            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
            {
                chromaLink[i] = chromaLink[i].Max(c);
            }

            return chromaLink;
        }

        // We weill be using this as an alternative to call keyboard[int index].
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
                    res[i++] = (Key)((row << 8) + col);
                }
            }

            return res;
        }
    }
}
