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

        private (bool HardpointsDeployed, VehicleMode Mode) _lastState;
        private AmbientColors _animC1;
        private AmbientColors _animC2;

        private enum VehicleMode
        {
            Combat,
            Analysis,
            Landing,
        }

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);

            VehicleMode mode;
            if (Game.Status.HasFlag(Flags.LandingGearDeployed))
            {
                mode = VehicleMode.Landing;
            }
            else if (Game.Status.HasFlag(Flags.HudInAnalysisMode))
            {
                mode = VehicleMode.Analysis;
            }
            else
            {
                mode = VehicleMode.Combat;
            }

            var state = (hardpointsDeployed, mode);

            if (state != _lastState)
            {
                StartAnimation();
                _animC1 = GetAmbientColors(_lastState.HardpointsDeployed, _lastState.Mode);
                _lastState = state;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                StopAnimation();
            }

            _animC2 = GetAmbientColors(hardpointsDeployed, mode);

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

        private AmbientColors GetAmbientColors(bool hardpointsDeployed, VehicleMode mode)
        {
            Color c;
            switch (mode)
            {
                case VehicleMode.Combat:
                    c = Game.Colors.Hud;
                    break;
                case VehicleMode.Analysis:
                    c = Game.Colors.AnalysisMode;
                    break;
                case VehicleMode.Landing:
                default:
                    c = Colors.LandingMode;
                    break;
            }

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
