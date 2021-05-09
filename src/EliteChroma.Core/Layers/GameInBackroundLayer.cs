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
                StopAnimation();
                _inBackground = false;
                return;
            }

            var inBackground = Game.ProcessState == Elite.GameProcessState.InBackground;

            if (inBackground != _inBackground)
            {
                StartAnimation();
                _inBackground = inBackground;
            }

            if (Animated && AnimationElapsed >= _fadeDuration)
            {
                StopAnimation();
            }

            if (!inBackground && !Animated)
            {
                return;
            }

            var pct = Animated
                ? Math.Min(1, AnimationElapsed.TotalSeconds / _fadeDuration.TotalSeconds)
                : 1;

            if (!inBackground)
            {
                pct = 1 - pct;
            }

            canvas.Keyboard.Combine(Game.Colors.Hud, pct);
            canvas.Mouse.Combine(Game.Colors.Hud, pct);
            canvas.Mousepad.Combine(Game.Colors.Hud, pct);
            canvas.Keypad.Combine(Game.Colors.Hud, pct);
            canvas.Headset.Combine(Game.Colors.Hud, pct);
            canvas.ChromaLink.Combine(Game.Colors.Hud, pct);
        }
    }
}
