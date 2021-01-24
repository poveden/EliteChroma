using System.Diagnostics.CodeAnalysis;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Elite;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class BackgroundLayer : LayerBase
    {
        public override int Order => 0;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var hardpointsDeployed = Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise);

            var c = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? Game.Colors.AnalysisMode : Game.Colors.Hud;

            var cKbd = c.Transform(Colors.KeyboardDimBrightness);
            var cDev = hardpointsDeployed ? c : c.Transform(Colors.DeviceDimBrightness);

            canvas.Keyboard.Set(cKbd);
            canvas.Keypad.Set(cKbd);
            canvas.Mouse.Set(cDev);
            canvas.Mousepad.Set(cDev);
            canvas.Headset.Set(cDev);
            canvas.ChromaLink.Set(cDev);

            var cLogo = Game.InMainMenu ? GameColors.EliteOrange : c;

            var k = canvas.Keyboard;
            k[Key.Logo] = cLogo;
        }
    }
}
