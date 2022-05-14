using ChromaWrapper;
using EliteChroma.Chroma;
using EliteChroma.Elite;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    internal sealed class HyperspaceExitLayer : LayerBase
    {
        private static readonly TimeSpan _flashTotalLength = TimeSpan.FromSeconds(2);

        private bool _enteredHyperspace;
        private ChromaColor _starClassColor;

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            bool enteredHyperspace = Game.FsdJumpType == FsdJumpType.Hyperspace;
            bool changed = enteredHyperspace != _enteredHyperspace;
            _enteredHyperspace = enteredHyperspace;

            bool enteredNow = changed && enteredHyperspace;
            bool exitedNow = changed && !enteredHyperspace;

            if (enteredNow)
            {
                _starClassColor = GameColors.GetStarClassColor(Game.FsdJumpStarClass);
                return;
            }

            if (exitedNow)
            {
                _ = StartAnimation();
            }

            if (!Animated)
            {
                return;
            }

            if (AnimationElapsed >= _flashTotalLength)
            {
                _ = StopAnimation();
                return;
            }

            ChromaColor cKey = PulseColor(Game.Colors.Hud.Transform(Colors.KeyboardDimBrightness), _starClassColor, _flashTotalLength);
            ChromaColor cDev = PulseColor(Game.Colors.Hud.Transform(Colors.DeviceDimBrightness), _starClassColor, _flashTotalLength);

            _ = canvas.Keyboard.Max(cKey);
            _ = canvas.Mouse.Max(cDev);
            _ = canvas.Mousepad.Max(cDev);
            _ = canvas.Keypad.Max(cKey);
            _ = canvas.Headset.Max(cDev);
            _ = canvas.ChromaLink.Max(cDev);
        }
    }
}
