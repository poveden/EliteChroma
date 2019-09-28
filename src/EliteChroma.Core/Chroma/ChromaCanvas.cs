using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colore;
using Colore.Effects.ChromaLink;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;

namespace EliteChroma.Chroma
{
    public sealed class ChromaCanvas
    {
        private readonly Lazy<KeyboardCustom> _keyboard = new Lazy<KeyboardCustom>(KeyboardCustom.Create);
        private readonly Lazy<MouseCustom> _mouse = new Lazy<MouseCustom>(MouseCustom.Create);
        private readonly Lazy<HeadsetCustom> _headset = new Lazy<HeadsetCustom>(HeadsetCustom.Create);
        private readonly Lazy<MousepadCustom> _mousepad = new Lazy<MousepadCustom>(MousepadCustom.Create);
        private readonly Lazy<KeypadCustom> _keypad = new Lazy<KeypadCustom>(KeypadCustom.Create);
        private readonly Lazy<ChromaLinkCustom> _chromaLink = new Lazy<ChromaLinkCustom>(ChromaLinkCustom.Create);

        public KeyboardCustom Keyboard => _keyboard.Value;

        public MouseCustom Mouse => _mouse.Value;

        public HeadsetCustom Headset => _headset.Value;

        public MousepadCustom Mousepad => _mousepad.Value;

        public KeypadCustom Keypad => _keypad.Value;

        public ChromaLinkCustom ChromaLink => _chromaLink.Value;

        public Task SetEffect(IChroma chroma)
        {
            if (chroma == null)
            {
                throw new ArgumentNullException(nameof(chroma));
            }

            var tasks = new List<Task<Guid>>();

            if (_keyboard.IsValueCreated)
            {
                tasks.Add(chroma.Keyboard.SetCustomAsync(Keyboard));
            }

            if (_mouse.IsValueCreated)
            {
                tasks.Add(chroma.Mouse.SetGridAsync(Mouse));
            }

            if (_headset.IsValueCreated)
            {
                tasks.Add(chroma.Headset.SetCustomAsync(Headset));
            }

            if (_mousepad.IsValueCreated)
            {
                tasks.Add(chroma.Mousepad.SetCustomAsync(Mousepad));
            }

            if (_keypad.IsValueCreated)
            {
                tasks.Add(chroma.Keypad.SetCustomAsync(Keypad));
            }

            if (_chromaLink.IsValueCreated)
            {
                tasks.Add(chroma.ChromaLink.SetCustomAsync(ChromaLink));
            }

            return Task.WhenAll(tasks);
        }
    }
}
