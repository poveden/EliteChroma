using System.Diagnostics.CodeAnalysis;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class LightsLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            var lColor = Game.Status.HasFlag(Flags.LightsOn) ? Colors.VehicleLightsHighBeam : Colors.VehicleLightsOff;
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.ShipSpotLightToggle, lColor);

            if (!Game.DockedOrLanded)
            {
                var nColor = Game.Status.HasFlag(Flags.NightVision) ? Colors.VehicleLightsHighBeam : Colors.VehicleLightsOff;
                ApplyColorToBinding(canvas.Keyboard, Miscellaneous.NightVisionToggle, nColor);
            }
        }
    }
}
