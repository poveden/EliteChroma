using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
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

            ChromaColor lColor;
            if (!Game.Status.HasFlag(Flags.LightsOn))
            {
                lColor = Colors.VehicleLightsOff;
            }
            else if (Game.Status.HasFlag(Flags.SrvHighBeam))
            {
                lColor = Colors.VehicleLightsHighBeam;
            }
            else
            {
                lColor = Colors.VehicleLightsMidBeam;
            }

            ApplyColorToBinding(canvas.Keyboard, Driving.Headlights, lColor);

            ChromaColor nColor = Game.Status.HasFlag(Flags.NightVision) ? Colors.VehicleLightsHighBeam : Colors.VehicleLightsOff;
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.NightVisionToggle, nColor);
        }
    }
}
