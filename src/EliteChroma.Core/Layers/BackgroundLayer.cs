using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        private static readonly Color EliteOrange = new Color(1.0, 0.2, 0);
        private static readonly Color BackgroundColor = EliteOrange.Transform(0.05);

        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var cLogo = EliteOrange;
            var cBack = BackgroundColor;

            if (!Game.InMainMenu)
            {
                cLogo = cLogo.Transform(Game.GuiColour);
                cBack = cBack.Transform(Game.GuiColour);
            }

            canvas.Keyboard.Set(cBack);
            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }
    }
}
