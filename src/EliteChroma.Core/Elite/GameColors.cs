using System;
using System.Collections.Generic;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Graphics;
using EliteFiles.Journal;

namespace EliteChroma.Elite
{
    public sealed class GameColors
    {
        public static readonly Color EliteOrange = new Color(1.0, 0.2, 0);

        public static readonly Color RedAlert = Color.Red;
        public static readonly Color YellowAlert = Color.Yellow;
        public static readonly Color GreenAlert = Color.Green;

        public static readonly Color SupercruiseTunnelParticle = Color.Blue;
        public static readonly Color HyperspaceTunnelParticle = Color.White;
        public static readonly Color HyperspaceDimStar = Color.Blue;
        public static readonly Color HyperspaceBrightStar = new Color(160, 255, 255);

        private static readonly Color _analysisMode = new Color(0.14, 0.62, 0.81);

        private static readonly IReadOnlyDictionary<string, Color> _baseStarClass = new Dictionary<string, Color>(StringComparer.Ordinal)
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

        internal GameColors(IRgbTransformMatrix transform)
        {
            Hud = EliteOrange.Transform(transform);
            AnalysisMode = _analysisMode.Transform(transform);
        }

        public Color Hud { get; }

        public Color AnalysisMode { get; }

        public static Color GetStarClassColor(string starClass)
        {
            StarClass.GetKind(starClass, out var baseClass);

            if (_baseStarClass.TryGetValue(baseClass, out var res))
            {
                return res;
            }

            return Color.Black;
        }
    }
}
