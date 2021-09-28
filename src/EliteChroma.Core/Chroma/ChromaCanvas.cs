using System;
using System.Collections.Generic;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Headset;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;

namespace EliteChroma.Chroma
{
    public sealed class ChromaCanvas
    {
        private readonly Lazy<CustomKeyKeyboardEffect> _keyboard = new Lazy<CustomKeyKeyboardEffect>();
        private readonly Lazy<CustomMouseEffect2> _mouse = new Lazy<CustomMouseEffect2>();
        private readonly Lazy<CustomHeadsetEffect> _headset = new Lazy<CustomHeadsetEffect>();
        private readonly Lazy<CustomMousepadEffect> _mousepad = new Lazy<CustomMousepadEffect>();
        private readonly Lazy<CustomKeypadEffect> _keypad = new Lazy<CustomKeypadEffect>();
        private readonly Lazy<CustomChromaLinkEffect> _chromaLink = new Lazy<CustomChromaLinkEffect>();

        public CustomKeyKeyboardEffect Keyboard => _keyboard.Value;

        public CustomMouseEffect2 Mouse => _mouse.Value;

        public CustomHeadsetEffect Headset => _headset.Value;

        public CustomMousepadEffect Mousepad => _mousepad.Value;

        public CustomKeypadEffect Keypad => _keypad.Value;

        public CustomChromaLinkEffect ChromaLink => _chromaLink.Value;

        public IReadOnlyCollection<Guid> SetEffect(IChromaSdk chroma)
        {
            if (chroma == null)
            {
                throw new ArgumentNullException(nameof(chroma));
            }

            var effectIds = new List<Guid>(6);

            if (_keyboard.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(Keyboard));
            }

            if (_mouse.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(Mouse));
            }

            if (_headset.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(Headset));
            }

            if (_mousepad.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(Mousepad));
            }

            if (_keypad.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(Keypad));
            }

            if (_chromaLink.IsValueCreated)
            {
                effectIds.Add(chroma.CreateEffect(ChromaLink));
            }

            foreach (Guid effectId in effectIds)
            {
                chroma.SetEffect(effectId);
            }

            return effectIds;
        }
    }
}
