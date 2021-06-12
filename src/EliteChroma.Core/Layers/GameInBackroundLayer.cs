using System;
using EliteChroma.Chroma;

namespace EliteChroma.Core.Layers
{
    internal sealed class GameInBackroundLayer : LayerBase
    {
        private static readonly TimeSpan _fadeDuration = TimeSpan.FromSeconds(1);

        private bool _inBackground;

        public override int Order => 1000;

        // InBackground -> InForeground => HUD -> Black
        // InForeground -> InBackground => Black -> HUD
        // NotRunning   -> InBackground => Black -> HUD
        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.ProcessState == Elite.GameProcessState.NotRunning)
            {
                _ = StopAnimation();
                _inBackground = false;
                return;
            }

            bool inBackground = Game.ProcessState == Elite.GameProcessState.InBackground;

            if (inBackground != _inBackground)
            {
                _ = StartAnimation();
                _inBackground = inBackground;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                _ = StopAnimation();
            }

            if (!inBackground && !Animated)
            {
                return;
            }

            double pct = Animated
                ? Math.Min(1, AnimationElapsed.TotalSeconds / _fadeDuration.TotalSeconds)
                : 1;

            if (!inBackground)
            {
                pct = 1 - pct;
            }

            _ = canvas.Keyboard.Combine(Game.Colors.Hud, pct);
            _ = canvas.Mouse.Combine(Game.Colors.Hud, pct);
            _ = canvas.Mousepad.Combine(Game.Colors.Hud, pct);
            _ = canvas.Keypad.Combine(Game.Colors.Hud, pct);
            _ = canvas.Headset.Combine(Game.Colors.Hud, pct);
            _ = canvas.ChromaLink.Combine(Game.Colors.Hud, pct);
        }
    }
}
