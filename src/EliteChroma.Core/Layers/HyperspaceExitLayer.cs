using System;
using Colore.Data;
using EliteChroma.Chroma;
using EliteChroma.Elite;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    internal sealed class HyperspaceExitLayer : LayerBase
    {
        private static readonly TimeSpan _flashTotalLength = TimeSpan.FromSeconds(2);

        private bool _enteredHyperspace;
        private Color _starClassColor;

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            var enteredHyperspace = Game.FsdJumpType == FsdJumpType.Hyperspace;
            var changed = enteredHyperspace != _enteredHyperspace;
            _enteredHyperspace = enteredHyperspace;

            var enteredNow = changed && enteredHyperspace;
            var exitedNow = changed && !enteredHyperspace;

            if (enteredNow)
            {
                _starClassColor = GameColors.GetStarClassColor(Game.FsdJumpStarClass);
                return;
            }

            if (exitedNow)
            {
                StartAnimation();
            }

            if (!Animated)
            {
                return;
            }

            if (AnimationElapsed >= _flashTotalLength)
            {
                StopAnimation();
                return;
            }

            var c = PulseColor(Game.Colors.Hud.Transform(BackgroundLayer.DimBrightness), _starClassColor, _flashTotalLength);

            canvas.Keyboard.Max(c);
            canvas.Mouse.Max(c);
            canvas.Mousepad.Max(c);
            canvas.Keypad.Max(c);
            canvas.Headset.Max(c);
            canvas.ChromaLink.Max(c);
        }
    }
}
