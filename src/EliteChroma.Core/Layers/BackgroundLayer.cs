using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        internal static readonly Color BackgroundColor = Color.FromRgb(0x0A0200);

        private const int _fadeSeconds = 1;

        private static readonly Color EliteOrange = new Color(1.0, 0.2, 0);

        private static readonly IReadOnlyDictionary<Elite.GameProcessState, Color> _stateColors =
            new Dictionary<Elite.GameProcessState, Color>
            {
                [Elite.GameProcessState.NotRunning] = Color.Black,
                [Elite.GameProcessState.InForeground] = BackgroundColor,
                [Elite.GameProcessState.InBackground] = EliteOrange,
            };

        private Elite.GameProcessState _lastState;
        private Color _animC1;
        private Color _animC2;

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.ProcessState != _lastState)
            {
                StartAnimation();
                _animC1 = _stateColors[_lastState];
                _animC2 = _stateColors[Game.ProcessState];
                _lastState = Game.ProcessState;
            }

            if (Animated && AnimationElapsed.TotalSeconds >= _fadeSeconds)
            {
                StopAnimation();
            }

            var cLogo = EliteOrange;
            var cBack = Animated
                ? PulseColor(_animC1, _animC2, TimeSpan.FromSeconds(_fadeSeconds * 2))
                : _animC2;

            if (!Game.InMainMenu)
            {
                cLogo = cLogo.Transform(Game.GuiColour);
            }

            canvas.Keyboard.Set(cBack);
            canvas.Mouse.Set(cBack);
            canvas.Mousepad.Set(cBack);
            canvas.Keypad.Set(cBack);
            canvas.ChromaLink.Set(cBack);
            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }
    }
}
