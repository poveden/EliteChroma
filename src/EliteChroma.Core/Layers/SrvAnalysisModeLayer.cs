using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class SrvAnalysisModeLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            Color colorOn = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? Game.Colors.AnalysisMode : Colors.HardpointsToggle;

            ApplyColorToBinding(canvas.Keyboard, DrivingModeSwitches.PlayerHUDModeToggle, colorOn);

            ApplyColorToBinding(canvas.Keyboard, Driving.PrimaryFire, colorOn);
            ApplyColorToBinding(canvas.Keyboard, Driving.SecondaryFire, colorOn);
            ApplyColorToBinding(canvas.Keyboard, Driving.CycleFireGroupNext, colorOn);
            ApplyColorToBinding(canvas.Keyboard, Driving.CycleFireGroupPrevious, colorOn);

            bool hardpointsDeployed = !Game.Status.HasFlag(Flags.SrvTurretRetracted);

            Color hColor;
            if (hardpointsDeployed)
            {
                _ = StartAnimation();
                hColor = PulseColor(Color.Black, colorOn, TimeSpan.FromSeconds(1));
            }
            else
            {
                _ = StopAnimation();
                hColor = colorOn;
            }

            ApplyColorToBinding(canvas.Keyboard, Driving.ToggleTurret, hColor);

            Color colorOff = Game.Status.HasFlag(Flags.HudInAnalysisMode)
                ? Game.Colors.AnalysisMode.Transform(Colors.DeviceDimBrightness)
                : Game.Colors.Hud.Transform(Colors.DeviceDimBrightness);

            Color c = hardpointsDeployed ? colorOn : colorOff;

            canvas.Mouse.Set(c);
            canvas.Mousepad.Set(c);
            canvas.Headset.Set(c);
            canvas.ChromaLink.Set(c);
        }
    }
}
