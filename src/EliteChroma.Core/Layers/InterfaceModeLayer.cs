using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class InterfaceModeLayer : LayerBase
    {
        public override int Order => 1000;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.InCockpit && !Game.Status.HasFlag(Flags.Docked) && Game.Status.GuiFocus == GuiFocus.None)
            {
                return;
            }

            if (Game.Status.GuiFocus == GuiFocus.FssMode)
            {
                return;
            }

            var k = canvas.Keyboard;
            k[Key.Escape] = Color.White;

            ApplyColorToBinding(canvas.Keyboard, InterfaceMode.All, Color.White);
        }
    }
}
