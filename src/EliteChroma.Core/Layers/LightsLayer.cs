using System.Diagnostics.CodeAnalysis;
using Colore.Data;
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

            var lColor = Game.Status.HasFlag(Flags.LightsOn) ? Color.White : Color.Blue;
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.ShipSpotLightToggle, lColor);

            if (!Game.DockedOrLanded)
            {
                var nColor = Game.Status.HasFlag(Flags.NightVision) ? Color.White : Color.Blue;
                ApplyColorToBinding(canvas.Keyboard, Miscellaneous.NightVisionToggle, nColor);
            }
        }
    }
}
