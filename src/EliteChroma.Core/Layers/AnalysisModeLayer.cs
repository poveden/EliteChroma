using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class AnalysisModeLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            var colorOn = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? Game.Colors.AnalysisMode : Colors.HardpointsToggle;

            ApplyColorToBinding(canvas.Keyboard, ModeSwitches.PlayerHUDModeToggle, colorOn);

            if (Game.DockedOrLanded)
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, Weapons.PrimaryFire, colorOn);
            ApplyColorToBinding(canvas.Keyboard, Weapons.SecondaryFire, colorOn);

            var hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);

            Color hColor;
            if (hardpointsDeployed)
            {
                StartAnimation();
                hColor = PulseColor(Color.Black, colorOn, TimeSpan.FromSeconds(1));
            }
            else
            {
                StopAnimation();
                hColor = colorOn;
            }

            ApplyColorToBinding(canvas.Keyboard, Weapons.DeployHardpointToggle, hColor);
        }
    }
}
