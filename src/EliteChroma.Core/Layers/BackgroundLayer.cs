using System.Diagnostics.CodeAnalysis;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Elite;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var cLogo = Game.InMainMenu ? GameColors.EliteOrange : Game.Colors.Hud;

            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }
    }
}
