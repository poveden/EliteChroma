using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class SrvLightsLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            var lColor = Game.Status.HasFlag(Flags.LightsOn)
                ? (Game.Status.HasFlag(Flags.SrvHighBeam) ? Colors.VehicleLightsHighBeam : Colors.VehicleLightsMidBeam)
                : Colors.VehicleLightsOff;
            ApplyColorToBinding(canvas.Keyboard, Driving.Headlights, lColor);

            var nColor = Game.Status.HasFlag(Flags.NightVision) ? Colors.VehicleLightsHighBeam : Colors.VehicleLightsOff;
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.NightVisionToggle, nColor);
        }
    }
}
