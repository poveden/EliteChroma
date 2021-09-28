using System;
using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class LandingGearLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit
                || Game.DockedOrLanded
                || Game.Status.HasFlag(Flags2.InTaxi)
                || Game.FsdJumpType != FsdJumpType.None
                || Game.Status.HasFlag(Flags.Supercruise))
            {
                return;
            }

            ChromaColor lColor;

            if (Game.Status.HasFlag(Flags.LandingGearDeployed))
            {
                ApplyColorToBinding(canvas.Keyboard, FlightLandingOverrides.Rotation, Colors.VehicleRotation);
                ApplyColorToBinding(canvas.Keyboard, FlightLandingOverrides.Thrust, Colors.VehicleThrust);

                _ = StartAnimation();
                lColor = PulseColor(ChromaColor.Black, Colors.LandingGearDeployed, TimeSpan.FromSeconds(1));
            }
            else
            {
                _ = StopAnimation();
                lColor = Colors.LandingGearRetracted;
            }

            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.LandingGearToggle, lColor);
        }
    }
}
