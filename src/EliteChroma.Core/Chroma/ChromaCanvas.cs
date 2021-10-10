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
        private readonly CustomKeyKeyboardEffect _keyboard = new CustomKeyKeyboardEffect();
        private readonly CustomMouseEffect2 _mouse = new CustomMouseEffect2();
        private readonly CustomHeadsetEffect _headset = new CustomHeadsetEffect();
        private readonly CustomMousepadEffect _mousepad = new CustomMousepadEffect();
        private readonly CustomKeypadEffect _keypad = new CustomKeypadEffect();
        private readonly CustomChromaLinkEffect _chromaLink = new CustomChromaLinkEffect();

        private bool _keyboardAccessed;
        private bool _mouseAccessed;
        private bool _headsetAccessed;
        private bool _mousepadAccessed;
        private bool _keypadAccessed;
        private bool _chromaLinkAccessed;

        public CustomKeyKeyboardEffect Keyboard
        {
            get
            {
                _keyboardAccessed = true;
                return _keyboard;
            }
        }

        public CustomMouseEffect2 Mouse
        {
            get
            {
                _mouseAccessed = true;
                return _mouse;
            }
        }

        public CustomHeadsetEffect Headset
        {
            get
            {
                _headsetAccessed = true;
                return _headset;
            }
        }

        public CustomMousepadEffect Mousepad
        {
            get
            {
                _mousepadAccessed = true;
                return _mousepad;
            }
        }

        public CustomKeypadEffect Keypad
        {
            get
            {
                _keypadAccessed = true;
                return _keypad;
            }
        }

        public CustomChromaLinkEffect ChromaLink
        {
            get
            {
                _chromaLinkAccessed = true;
                return _chromaLink;
            }
        }

        public IReadOnlyCollection<Guid> SetEffect(IChromaSdk chroma)
        {
            if (chroma == null)
            {
                throw new ArgumentNullException(nameof(chroma));
            }

            List<Guid> effectIds = CreateAccessedEffects(chroma);

            foreach (Guid effectId in effectIds)
            {
                chroma.SetEffect(effectId);
            }

            return effectIds;
        }

        public void ClearCanvas()
        {
            if (_keyboardAccessed)
            {
                _keyboard.Color.Clear();
                _keyboard.Key.Clear();
                _keyboardAccessed = false;
            }

            if (_mouseAccessed)
            {
                _mouse.Color.Clear();
                _mouseAccessed = false;
            }

            if (_headsetAccessed)
            {
                _headset.Color.Clear();
                _headsetAccessed = false;
            }

            if (_mousepadAccessed)
            {
                _mousepad.Color.Clear();
                _mousepadAccessed = false;
            }

            if (_keypadAccessed)
            {
                _keypad.Color.Clear();
                _keypadAccessed = false;
            }

            if (_chromaLinkAccessed)
            {
                _chromaLink.Color.Clear();
                _chromaLinkAccessed = false;
            }
        }

        private List<Guid> CreateAccessedEffects(IChromaSdk chroma)
        {
            var effectIds = new List<Guid>(6);

            if (_keyboardAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_keyboard));
            }

            if (_mouseAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_mouse));
            }

            if (_headsetAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_headset));
            }

            if (_mousepadAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_mousepad));
            }

            if (_keypadAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_keypad));
            }

            if (_chromaLinkAccessed)
            {
                effectIds.Add(chroma.CreateEffect(_chromaLink));
            }

            return effectIds;
        }
    }
}
