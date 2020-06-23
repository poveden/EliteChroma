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
        private Color _animKbdC1;
        private Color _animKbdC2;
        private Color _animDevC1;
        private Color _animDevC2;

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.ProcessState != _lastState)
            {
                StartAnimation();
                _animKbdC1 = GetBackgroundColor(_lastState, Colors.KeyboardDimBrightness);
                _animDevC1 = GetBackgroundColor(_lastState, Colors.DeviceDimBrightness);
                _lastState = Game.ProcessState;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                StopAnimation();
            }

            _animKbdC2 = GetBackgroundColor(Game.ProcessState, Colors.KeyboardDimBrightness);
            _animDevC2 = GetBackgroundColor(Game.ProcessState, Colors.DeviceDimBrightness);

            var cLogo = Game.InMainMenu ? GameColors.EliteOrange : Game.Colors.Hud;

            Color cKbd = Animated
                ? PulseColor(_animKbdC1, _animKbdC2, _fadeDuration, PulseColorType.Sawtooth)
                : _animKbdC2;

            Color cDev = Animated
                ? PulseColor(_animDevC1, _animDevC2, _fadeDuration, PulseColorType.Sawtooth)
                : _animDevC2;

            canvas.Keyboard.Set(cKbd);
            canvas.Mouse.Set(cDev);
            canvas.Mousepad.Set(cDev);
            canvas.Keypad.Set(cKbd);
            canvas.Headset.Set(cDev);
            canvas.ChromaLink.Set(cDev);
            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }

        private Color GetBackgroundColor(GameProcessState state, double brightness)
        {
            switch (state)
            {
                case GameProcessState.InForeground:
                    return Game.Colors.Hud.Transform(brightness);
                case GameProcessState.InBackground:
                    return Game.Colors.Hud;
                default:
                    return Color.Black;
            }
        }
    }
}
