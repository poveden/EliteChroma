using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Elite;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        private static readonly TimeSpan _fadeDuration = TimeSpan.FromSeconds(0.2);

        private (bool HardpointsDeployed, bool InAnalysisMode) _lastState;
        private AmbientColors _animC1;
        private AmbientColors _animC2;

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);
            var inAnalysisMode = Game.Status.HasFlag(Flags.HudInAnalysisMode);
            var state = (hardpointsDeployed, inAnalysisMode);

            if (state != _lastState)
            {
                StartAnimation();
                _animC1 = GetAmbientColors(_lastState.HardpointsDeployed, _lastState.InAnalysisMode);
                _lastState = state;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                StopAnimation();
            }

            _animC2 = GetAmbientColors(hardpointsDeployed, inAnalysisMode);

            var c = _animC2;

            if (Animated)
            {
                c.Keyboard = PulseColor(_animC1.Keyboard, _animC2.Keyboard, _fadeDuration, PulseColorType.Sawtooth);
                c.Device = PulseColor(_animC1.Device, _animC2.Device, _fadeDuration, PulseColorType.Sawtooth);
                c.Logo = PulseColor(_animC1.Logo, _animC2.Logo, _fadeDuration, PulseColorType.Sawtooth);
            }

            canvas.Keyboard.Set(c.Keyboard);
            canvas.Keypad.Set(c.Keyboard);
            canvas.Mouse.Set(c.Device);
            canvas.Mousepad.Set(c.Device);
            canvas.Headset.Set(c.Device);
            canvas.ChromaLink.Set(c.Device);

            var k = canvas.Keyboard;
            k[Key.Logo] = c.Logo;
        }

        private AmbientColors GetAmbientColors(bool hardpointsDeployed, bool inAnalysisMode)
        {
            var c = inAnalysisMode ? Game.Colors.AnalysisMode : Game.Colors.Hud;

            return new AmbientColors
            {
                Keyboard = c.Transform(Colors.KeyboardDimBrightness),
                Device = hardpointsDeployed ? c : c.Transform(Colors.DeviceDimBrightness),
                Logo = Game.InMainMenu ? GameColors.EliteOrange : c,
            };
        }

        private struct AmbientColors
        {
            public Color Keyboard { get; set; }

            public Color Device { get; set; }

            public Color Logo { get; set; }
        }
    }
}
