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
        private static readonly Color AnalysisModeColor = new Color(0.14, 0.62, 0.81);
        private static readonly Color CombatModeColor = Color.Red;

        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            var color = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? AnalysisModeColor.Transform(Game.GuiColour) : CombatModeColor;

            ApplyColorToBinding(canvas.Keyboard, DrivingModeSwitches.PlayerHUDModeToggle, color);

            ApplyColorToBinding(canvas.Keyboard, Driving.PrimaryFire, color);
            ApplyColorToBinding(canvas.Keyboard, Driving.SecondaryFire, color);
            ApplyColorToBinding(canvas.Keyboard, Driving.CycleFireGroupNext, color);
            ApplyColorToBinding(canvas.Keyboard, Driving.CycleFireGroupPrevious, color);

            Color hColor = color;
            Color mColor = color;
            Color bColor = color.Transform(0.5);

            if (!Game.Status.HasFlag(Flags.SrvTurretRetracted))
            {
                StartAnimation();
                hColor = PulseColor(Color.Black, color, TimeSpan.FromSeconds(1));
            }
            else
            {
                StopAnimation();
                mColor = bColor;
            }

            ApplyColorToBinding(canvas.Keyboard, Driving.ToggleTurret, hColor);
            canvas.Mouse.Set(mColor);
            canvas.Mousepad.Set(mColor);
            canvas.Headset.Set(mColor);
            canvas.ChromaLink.Set(bColor);
        }
    }
}
