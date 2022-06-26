using EliteChroma.Core.Chroma;
using EliteChroma.Core.Elite;
using EliteFiles.Journal.Events;

namespace EliteChroma.Core.Layers
{
    internal sealed class HardcodedBindingsLayer : LayerBase
    {
        public override int Order => 200;

        protected override void OnRender(ChromaCanvas canvas)
        {
            ApplyColorToBinding(canvas.Keyboard, GameBindings.Screenshot, Game.Colors.Hud);
            ApplyColorToBinding(canvas.Keyboard, GameBindings.ToggleFps, Game.Colors.Hud);
            ApplyColorToBinding(canvas.Keyboard, GameBindings.ToggleBandwidth, Game.Colors.Hud);

            if (Game.GameMode != LoadGame.PlayMode.Open)
            {
                ApplyColorToBinding(canvas.Keyboard, GameBindings.HiResScreenshot, Game.Colors.Hud);
            }

            if (Game.AtHelm || Game.IsWalking)
            {
                ApplyColorToBinding(canvas.Keyboard, GameBindings.ToggleHud, Game.Colors.Hud);
            }
        }
    }
}
