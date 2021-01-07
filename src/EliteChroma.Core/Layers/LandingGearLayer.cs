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
    internal sealed class LandingGearLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit
                || Game.DockedOrLanded
                || Game.FsdJumpType != FsdJumpType.None
                || Game.Status.HasFlag(Flags.Supercruise))
            {
                return;
            }

            Color lColor;

            if (Game.Status.HasFlag(Flags.LandingGearDeployed))
            {
                ApplyColorToBinding(canvas.Keyboard, FlightLandingOverrides.Rotation, Colors.VehicleRotation);
                ApplyColorToBinding(canvas.Keyboard, FlightLandingOverrides.Thrust, Colors.VehicleThrust);

                StartAnimation();
                lColor = PulseColor(Color.Black, Colors.LandingGearDeployed, TimeSpan.FromSeconds(1));
            }
            else
            {
                StopAnimation();
                lColor = Colors.LandingGearRetracted;
            }

            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.LandingGearToggle, lColor);
        }
    }
}
