using System.Diagnostics.CodeAnalysis;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class ModeSwitchesLayer : LayerBase
    {
        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || (Game.FsdJumpType != FsdJumpType.None && !Game.InWitchSpace))
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, ModeSwitches.All, Colors.VehicleModeSwitches);
        }
    }
}
