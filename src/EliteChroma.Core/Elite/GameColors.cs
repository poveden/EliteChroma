using ChromaWrapper;
using EliteChroma.Core.Chroma;
using EliteFiles.Graphics;
using EliteFiles.Journal;

namespace EliteChroma.Core.Elite
{
    public sealed class GameColors
    {
        public static readonly ChromaColor EliteOrange = ChromaColor.FromRgb(0xFF8800);

        public static readonly ChromaColor RedAlert = ChromaColor.Red;
        public static readonly ChromaColor YellowAlert = ChromaColor.Yellow;
        public static readonly ChromaColor GreenAlert = ChromaColor.Green;

        public static readonly ChromaColor SupercruiseTunnelParticle = ChromaColor.FromRgb(0x0D3277);
        public static readonly ChromaColor HyperspaceTunnelParticle = ChromaColor.FromRgb(0xD3D9DE);
        public static readonly ChromaColor HyperspaceDimStar = ChromaColor.FromRgb(0x668896);
        public static readonly ChromaColor HyperspaceBrightStar = ChromaColor.FromRgb(0xC69D75);

        private static readonly ChromaColor _analysisMode = ChromaColor.FromRgb(0x66AADD);

        private static readonly IReadOnlyDictionary<string, ChromaColor> _baseStarClass = new Dictionary<string, ChromaColor>(StringComparer.Ordinal)
        {
            [StarClass.O] = ChromaColor.FromRgb(0xB1F0FE),
            [StarClass.B] = ChromaColor.FromRgb(0xDDFBFF),
            [StarClass.A] = ChromaColor.FromRgb(0xE7ECFF),
            [StarClass.F] = ChromaColor.FromRgb(0xF1ECB8),
            [StarClass.G] = ChromaColor.FromRgb(0xFFE8A3),
            [StarClass.K] = ChromaColor.FromRgb(0xFFD46A),
            [StarClass.M] = ChromaColor.FromRgb(0xEEA349),

            [StarClass.L] = ChromaColor.FromRgb(0xD10341),
            [StarClass.T] = ChromaColor.FromRgb(0x9C0C55),
            [StarClass.Y] = ChromaColor.FromRgb(0x4A0C31),

            [StarClass.HerbigAeBe] = ChromaColor.FromRgb(0xFFD46A),
            [StarClass.TTauri] = ChromaColor.FromRgb(0xEEA349),

            [StarClass.C] = ChromaColor.FromRgb(0xFFDE86),
            [StarClass.MS] = ChromaColor.FromRgb(0xFFDE86),
            [StarClass.S] = ChromaColor.FromRgb(0xAD1137),

            [StarClass.W] = ChromaColor.FromRgb(0xC3CFE7),

            [StarClass.BlackHole] = ChromaColor.FromRgb(0x3F3F3F),

            [StarClass.Neutron] = ChromaColor.FromRgb(0x3D7CED),

            [StarClass.D] = ChromaColor.FromRgb(0x274BFE),
        };

        internal GameColors(IRgbTransformMatrix transform)
        {
            Hud = EliteOrange.Transform(transform);
            AnalysisMode = _analysisMode.Transform(transform);
        }

        public ChromaColor Hud { get; }

        public ChromaColor AnalysisMode { get; }

        public static ChromaColor GetStarClassColor(string? starClass)
        {
            StarClass.Kind kind = StarClass.GetKind(starClass, out string? baseClass);

            if (kind != StarClass.Kind.Unknown && _baseStarClass.TryGetValue(baseClass!, out ChromaColor res))
            {
                return res;
            }

            return ChromaColor.Black;
        }
    }
}
