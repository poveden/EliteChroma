using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class GalaxyMapLayer : LayerBase
    {
        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.Status.GuiFocus != GuiFocus.GalaxyMap)
            {
                return;
            }

            var k = canvas.Keyboard;
            k[Key.Escape] = Color.White;

            ApplyColorToBinding(canvas.Keyboard, GalaxyMap.All, Color.White);
        }
    }
}
