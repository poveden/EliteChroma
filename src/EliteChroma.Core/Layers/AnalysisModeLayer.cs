using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using EliteChroma.Core.Chroma;
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
            if (!Game.InCockpit || Game.Status.HasFlag(Flags2.InTaxi) || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            ChromaColor colorOn = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? Game.Colors.AnalysisMode : Colors.HardpointsToggle;

            ApplyColorToBinding(canvas.Keyboard, ModeSwitches.PlayerHUDModeToggle, colorOn);

            if (Game.DockedOrLanded)
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, Weapons.PrimaryFire, colorOn);
            ApplyColorToBinding(canvas.Keyboard, Weapons.SecondaryFire, colorOn);

            bool hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);

            ChromaColor hColor;
            if (hardpointsDeployed)
            {
                _ = StartAnimation();
                hColor = PulseColor(ChromaColor.Black, colorOn, TimeSpan.FromSeconds(1));
            }
            else
            {
                _ = StopAnimation();
                hColor = colorOn;
            }

            ApplyColorToBinding(canvas.Keyboard, Weapons.DeployHardpointToggle, hColor);
        }
    }
}
