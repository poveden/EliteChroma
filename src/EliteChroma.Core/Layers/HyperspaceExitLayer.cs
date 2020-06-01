using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Journal;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    internal sealed class HyperspaceExitLayer : LayerBase
    {
        private static readonly IReadOnlyDictionary<string, Color> _baseClassColors = new Dictionary<string, Color>(StringComparer.Ordinal)
        {
            [StarClass.O] = Color.FromRgb(0xA9ECFB),
            [StarClass.B] = Color.FromRgb(0xE6FDFF),
            [StarClass.A] = Color.FromRgb(0xF2F5FF),
            [StarClass.F] = Color.FromRgb(0xF7EE48),
            [StarClass.G] = Color.FromRgb(0xC79111),
            [StarClass.K] = Color.FromRgb(0xBA6901),
            [StarClass.M] = Color.FromRgb(0xA64901),

            [StarClass.L] = Color.FromRgb(0x99040A),
            [StarClass.T] = Color.FromRgb(0x850F33),
            [StarClass.Y] = Color.FromRgb(0x411136),

            [StarClass.HerbigAeBe] = Color.FromRgb(0xBA6901),
            [StarClass.TTauri] = Color.FromRgb(0xA64901),

            [StarClass.C] = Color.FromRgb(0xC07D09),
            [StarClass.MS] = Color.FromRgb(0xC07D09),
            [StarClass.S] = Color.FromRgb(0x821418),

            [StarClass.W] = Color.FromRgb(0xCBD3E5),

            [StarClass.BlackHole] = Color.Black,

            [StarClass.Neutron] = Color.FromRgb(0x3C5AFF),

            [StarClass.D] = Color.FromRgb(0x3C5AFF),
        };

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
                _starClassColor = GetStarClassColor(Game.FsdJumpStarClass);
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

            var c = PulseColor(BackgroundLayer.BackgroundColor, _starClassColor, _flashTotalLength);

            canvas.Keyboard.Max(c);
            canvas.Mouse.Max(c);
            canvas.Mousepad.Max(c);
            canvas.Keypad.Max(c);
            canvas.Headset.Max(c);
            canvas.ChromaLink.Max(c);
        }

        private static Color GetStarClassColor(string starClass)
        {
            StarClass.GetKind(starClass, out var baseClass);

            if (_baseClassColors.TryGetValue(baseClass, out var res))
            {
                return res;
            }

            return Color.Black;
        }
    }
}
