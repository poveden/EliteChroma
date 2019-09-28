using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class HyperspaceLayer : LayerBase
    {
        private const double _xScale = KeyboardConstants.MaxColumns / 2.0;
        private const double _yScale = KeyboardConstants.MaxRows / 2.0;
        private const double _x0 = (KeyboardConstants.MaxColumns - 1) / 2.0;
        private const double _y0 = (KeyboardConstants.MaxRows - 1) / 2.0;

        private static readonly Color _dimColor = Color.Blue;
        private static readonly Color _brightColor = new Color(160, 255, 255);

        private static readonly TimeSpan _dimLifespan = TimeSpan.FromMilliseconds(500);
        private static readonly TimeSpan _brightLifespan = TimeSpan.FromMilliseconds(1000);

        private readonly Stars _stars = new Stars();

        public override int Order => 100;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.FsdJumpType == FsdJumpType.None)
            {
                StopAnimation();
                return;
            }

            StartAnimation();

            var kbd = canvas.Keyboard;

            kbd.Set(Color.Black);

            _stars.Add(1, _dimColor, _dimLifespan);

            if (Game.FsdJumpType == FsdJumpType.Hyperspace && (DateTimeOffset.UtcNow - AnimationStart).TotalSeconds > 5)
            {
                _stars.Add(1, _brightColor, _brightLifespan);
            }

            DrawStars(_stars, kbd);
        }

        private void DrawStars(Stars stars, KeyboardCustom keyboard)
        {
            var t = (DateTimeOffset.UtcNow - Game.FsdJumpChange).TotalSeconds;

            stars.Clean();

            foreach (var star in stars)
            {
                var r = Math.Pow((DateTimeOffset.UtcNow - star.Birth).TotalMilliseconds / star.Lifespan.TotalMilliseconds, 2);

                var x = (int)Math.Round(_x0 + (star.VX * r * _xScale));
                var y = (int)Math.Round(_y0 + (star.Y * _yScale));

                if (x < 0 || x >= KeyboardConstants.MaxColumns)
                {
                    continue;
                }

                if (y < 0 || y >= KeyboardConstants.MaxRows)
                {
                    continue;
                }

                var c = keyboard[y, x].Combine(star.Color.Transform(r * 1, 1.5));

                if (Game.FsdJumpType == FsdJumpType.Hyperspace && t >= 4 && t <= 5)
                {
                    c = c.Combine(Color.White, t - 4);
                }

                keyboard[y, x] = c;
            }
        }

        private sealed class Stars : IEnumerable<Star>
        {
            private readonly Queue<Star> _stars = new Queue<Star>();

            public void Add(int stars, Color color, TimeSpan lifespan)
            {
                while (stars > 0)
                {
                    _stars.Enqueue(new Star(color, lifespan));
                    stars--;
                }
            }

            public void Clean()
            {
                while (_stars.Count != 0 && _stars.Peek().Death < DateTimeOffset.UtcNow)
                {
                    _stars.Dequeue();
                }
            }

            public IEnumerator<Star> GetEnumerator() => _stars.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _stars.GetEnumerator();
        }

        private sealed class Star
        {
            private static readonly Random _rnd = new Random();

            public Star(Color color, TimeSpan lifespan)
            {
                Color = color;
                Birth = DateTimeOffset.UtcNow;
                Lifespan = lifespan;
                Death = Birth + lifespan;

                var v = (_rnd.NextDouble() * 2) - 1;

                VX = _rnd.Next(2) != 0 ? 1 : -1;
                Y = _rnd.Next(2) != 0 ? v : -v;
            }

            public Color Color { get; }

            public DateTimeOffset Birth { get; }

            public TimeSpan Lifespan { get; }

            public DateTimeOffset Death { get; }

            public double VX { get; }

            public double Y { get; }
        }
    }
}
