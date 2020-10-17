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
        private readonly Lazy<CustomKeyboardEffect> _keyboard = new Lazy<CustomKeyboardEffect>(CustomKeyboardEffect.Create);
        private readonly Lazy<CustomMouseEffect> _mouse = new Lazy<CustomMouseEffect>(CustomMouseEffect.Create);
        private readonly Lazy<CustomHeadsetEffect> _headset = new Lazy<CustomHeadsetEffect>(CustomHeadsetEffect.Create);
        private readonly Lazy<CustomMousepadEffect> _mousepad = new Lazy<CustomMousepadEffect>(CustomMousepadEffect.Create);
        private readonly Lazy<CustomKeypadEffect> _keypad = new Lazy<CustomKeypadEffect>(CustomKeypadEffect.Create);
        private readonly Lazy<CustomChromaLinkEffect> _chromaLink = new Lazy<CustomChromaLinkEffect>(CustomChromaLinkEffect.Create);

        public CustomKeyboardEffect Keyboard => _keyboard.Value;

        public CustomMouseEffect Mouse => _mouse.Value;

        public CustomHeadsetEffect Headset => _headset.Value;

        public CustomMousepadEffect Mousepad => _mousepad.Value;

        public CustomKeypadEffect Keypad => _keypad.Value;

        public CustomChromaLinkEffect ChromaLink => _chromaLink.Value;

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
