using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using ChromaWrapper.Keyboard;
using EliteChroma.Core.Chroma;
using EliteChroma.Core.Elite;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        private static readonly TimeSpan _fadeDuration = TimeSpan.FromSeconds(0.2);

        private (bool HardpointsDeployed, VehicleMode Mode) _lastState;
        private AmbientColors _animC1;

        private enum VehicleMode
        {
            Combat,
            Analysis,
            Landing,
        }

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            bool hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);

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

            (bool, VehicleMode) state = (hardpointsDeployed, mode);

            if (state != _lastState)
            {
                _ = StartAnimation();
                _animC1 = GetAmbientColors(_lastState.HardpointsDeployed, _lastState.Mode);
                _lastState = state;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                _ = StopAnimation();
            }

            AmbientColors animC2 = GetAmbientColors(hardpointsDeployed, mode);

            AmbientColors c = animC2;

            if (Animated)
            {
                c.Keyboard = PulseColor(_animC1.Keyboard, animC2.Keyboard, _fadeDuration, PulseColorType.Sawtooth);
                c.Device = PulseColor(_animC1.Device, animC2.Device, _fadeDuration, PulseColorType.Sawtooth);
                c.Logo = PulseColor(_animC1.Logo, animC2.Logo, _fadeDuration, PulseColorType.Sawtooth);
            }

            canvas.Keyboard.Color.Fill(c.Keyboard);
            canvas.Keypad.Color.Fill(c.Keyboard);
            canvas.Mouse.Color.Fill(c.Device);
            canvas.Mousepad.Color.Fill(c.Device);
            canvas.Headset.Color.Fill(c.Device);
            canvas.ChromaLink.Color.Fill(c.Device);

            canvas.Keyboard.Key[KeyboardKey.Logo] = c.Logo;
        }

        private AmbientColors GetAmbientColors(bool hardpointsDeployed, VehicleMode mode)
        {
            ChromaColor c = mode switch
            {
                VehicleMode.Combat => Game.Colors.Hud,
                VehicleMode.Analysis => Game.Colors.AnalysisMode,
                VehicleMode.Landing => Colors.LandingMode,
                _ => throw new ArgumentOutOfRangeException(nameof(mode)),
            };

            return new AmbientColors
            {
                Keyboard = c.Transform(Colors.KeyboardDimBrightness),
                Device = hardpointsDeployed ? c : c.Transform(Colors.DeviceDimBrightness),
                Logo = Game.InMainMenu ? GameColors.EliteOrange : c,
            };
        }

        private struct AmbientColors
        {
            public ChromaColor Keyboard { get; set; }

            public ChromaColor Device { get; set; }

            public ChromaColor Logo { get; set; }
        }
    }
}
