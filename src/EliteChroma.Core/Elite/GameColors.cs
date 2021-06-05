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
        public static readonly Color EliteOrange = Color.FromRgb(0xFF8800);

        public static readonly Color RedAlert = Color.Red;
        public static readonly Color YellowAlert = Color.Yellow;
        public static readonly Color GreenAlert = Color.Green;

        public static readonly Color SupercruiseTunnelParticle = Color.FromRgb(0x0D3277);
        public static readonly Color HyperspaceTunnelParticle = Color.FromRgb(0xD3D9DE);
        public static readonly Color HyperspaceDimStar = Color.FromRgb(0x668896);
        public static readonly Color HyperspaceBrightStar = Color.FromRgb(0xC69D75);

        private static readonly Color _analysisMode = Color.FromRgb(0x66AADD);

        private static readonly IReadOnlyDictionary<string, Color> _baseStarClass = new Dictionary<string, Color>(StringComparer.Ordinal)
        {
            [StarClass.O] = Color.FromRgb(0xB1F0FE),
            [StarClass.B] = Color.FromRgb(0xDDFBFF),
            [StarClass.A] = Color.FromRgb(0xE7ECFF),
            [StarClass.F] = Color.FromRgb(0xF1ECB8),
            [StarClass.G] = Color.FromRgb(0xFFE8A3),
            [StarClass.K] = Color.FromRgb(0xFFD46A),
            [StarClass.M] = Color.FromRgb(0xEEA349),

            [StarClass.L] = Color.FromRgb(0xD10341),
            [StarClass.T] = Color.FromRgb(0x9C0C55),
            [StarClass.Y] = Color.FromRgb(0x4A0C31),

            [StarClass.HerbigAeBe] = Color.FromRgb(0xFFD46A),
            [StarClass.TTauri] = Color.FromRgb(0xEEA349),

            [StarClass.C] = Color.FromRgb(0xFFDE86),
            [StarClass.MS] = Color.FromRgb(0xFFDE86),
            [StarClass.S] = Color.FromRgb(0xAD1137),

            [StarClass.W] = Color.FromRgb(0xC3CFE7),

            [StarClass.BlackHole] = Color.FromRgb(0x3F3F3F),

            [StarClass.Neutron] = Color.FromRgb(0x3D7CED),

            [StarClass.D] = Color.FromRgb(0x274BFE),
        };

        internal GameColors(IRgbTransformMatrix transform)
        {
            Hud = EliteOrange.Transform(transform);
            AnalysisMode = _analysisMode.Transform(transform);
        }

        public Color Hud { get; }

        public Color AnalysisMode { get; }

        public static Color GetStarClassColor(string? starClass)
        {
            StarClass.Kind kind = StarClass.GetKind(starClass, out string? baseClass);

            if (kind != StarClass.Kind.Unknown && _baseStarClass.TryGetValue(baseClass!, out Color res))
            {
                return res;
            }

            return Color.Black;
        }
    }
}
