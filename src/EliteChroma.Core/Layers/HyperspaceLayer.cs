using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.ChromaLink;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Journal;
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

        private HazardLevel _hazardLevel;

        private enum HazardLevel
        {
            Low = 0,
            Medium = 1,
            High = 2,
        }

        public override int Order => 100;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.FsdJumpType == FsdJumpType.None)
            {
                StopAnimation();
                return;
            }

            if (!Animated && Game.FsdJumpType == FsdJumpType.Hyperspace)
            {
                _hazardLevel = GetHazardLevel(Game.FsdJumpStarClass);
            }

            StartAnimation();

            var kbd = canvas.Keyboard;
            var cl = canvas.ChromaLink;

            kbd.Set(Color.Black);
            cl.Set(Color.Black);

            _stars.Add(1, _dimColor, _dimLifespan);

            if (Game.InWitchSpace)
            {
                _stars.Add(1, _brightColor, _brightLifespan);
            }

            DrawStars(_stars, kbd, cl);

            if (!Game.InWitchSpace)
            {
                return;
            }

            var hazardColor = Color.Green;
            var period = TimeSpan.FromSeconds(1);
            var pulseType = PulseColorType.Triangle;

            switch (_hazardLevel)
            {
                case HazardLevel.Medium:
                    hazardColor = Color.Yellow;
                    pulseType = PulseColorType.Square;
                    break;

                case HazardLevel.High:
                    hazardColor = Color.Red;
                    period = TimeSpan.FromSeconds(0.67);
                    pulseType = PulseColorType.Square;
                    break;
            }

            var color = PulseColor(Color.Black, hazardColor, period, pulseType);
            ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.HyperSuperCombination, color);
            ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.Hyperspace, color);
        }

        private static HazardLevel GetHazardLevel(string starClass)
        {
            switch (StarClass.GetKind(starClass, out _))
            {
                case StarClass.Kind.MainSequence:
                    return HazardLevel.Low;

                case StarClass.Kind.Neutron:
                case StarClass.Kind.WhiteDwarf:
                case StarClass.Kind.BlackHole:
                    return HazardLevel.High;

                default:
                    return HazardLevel.Medium;
            }
        }

        private void DrawStars(Stars stars, KeyboardCustom keyboard, ChromaLinkCustom chromaLink)
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

                if (y == 0 && x >= 1 && x < ChromaLinkConstants.MaxLeds)
                {
                    chromaLink[x] = c;
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
