using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Elite;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        private static readonly TimeSpan _fadeDuration = TimeSpan.FromSeconds(1);

        private GameProcessState _lastState;
        private Color _animC1;
        private Color _animC2;

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.ProcessState != _lastState)
            {
                StartAnimation();
                _animC1 = GetBackgroundColor(_lastState);
                _animC2 = GetBackgroundColor(Game.ProcessState);
                _lastState = Game.ProcessState;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                StopAnimation();
            }

            var cLogo = Game.InMainMenu ? GameColors.EliteOrange : Game.Colors.Hud;
            var cBack = Animated
                ? PulseColor(_animC1, _animC2, _fadeDuration, PulseColorType.Sawtooth)
                : _animC2;

            canvas.Keyboard.Set(cBack);
            canvas.Mouse.Set(cBack);
            canvas.Mousepad.Set(cBack);
            canvas.Keypad.Set(cBack);
            canvas.Headset.Set(cBack);
            canvas.ChromaLink.Set(cBack);
            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }

        private Color GetBackgroundColor(GameProcessState state)
        {
            switch (state)
            {
                case GameProcessState.InForeground:
                    return Game.Colors.Hud.Transform(Colors.DimBrightness);
                case GameProcessState.InBackground:
                    return Game.Colors.Hud;
                default:
                    return Color.Black;
            }
        }
    }
}
