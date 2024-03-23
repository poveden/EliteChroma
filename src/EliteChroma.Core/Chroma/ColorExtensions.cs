using ChromaWrapper;
using EliteFiles.Graphics;

namespace EliteChroma.Core.Chroma
{
    public static class ColorExtensions
    {
        public static ChromaColor Combine(this ChromaColor c1, ChromaColor c2, double c2pct = 0.5)
        {
            double c1pct = 1.0 - c2pct;

            double r1 = c1.R * c1pct;
            double g1 = c1.G * c1pct;
            double b1 = c1.B * c1pct;

            double r2 = c2.R * c2pct;
            double g2 = c2.G * c2pct;
            double b2 = c2.B * c2pct;

            double r = Math.Clamp((r1 + r2) / 255.0, 0, 1);
            double g = Math.Clamp((g1 + g2) / 255.0, 0, 1);
            double b = Math.Clamp((b1 + b2) / 255.0, 0, 1);

            return ChromaColor.FromRgb(r, g, b);
        }

        public static ChromaColor Max(this ChromaColor c1, ChromaColor c2)
        {
            byte r = Math.Max(c1.R, c2.R);
            byte g = Math.Max(c1.G, c2.G);
            byte b = Math.Max(c1.B, c2.B);

            return ChromaColor.FromRgb(r, g, b);
        }

        public static ChromaColor Transform(this ChromaColor color, IRgbTransformMatrix transform)
        {
            ArgumentNullException.ThrowIfNull(transform);

            double c0 = color.R / 255.0;
            double c1 = color.G / 255.0;
            double c2 = color.B / 255.0;

            double r =
                (c0 * transform[0, 0]) +
                (c1 * transform[1, 0]) +
                (c2 * transform[2, 0]);

            double g =
                (c0 * transform[0, 1]) +
                (c1 * transform[1, 1]) +
                (c2 * transform[2, 1]);

            double b =
                (c0 * transform[0, 2]) +
                (c1 * transform[1, 2]) +
                (c2 * transform[2, 2]);

            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);

            return ChromaColor.FromRgb(r, g, b);
        }

        public static ChromaColor Transform(this ChromaColor color, double multiply, double gamma = 1.0)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            if (multiply is < 1.0 or > 1.0)
            {
                r *= multiply;
                g *= multiply;
                b *= multiply;
            }

            if (gamma is < 1.0 or > 1.0)
            {
                r = Math.Pow(r, gamma);
                g = Math.Pow(g, gamma);
                b = Math.Pow(b, gamma);
            }

            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);

            return ChromaColor.FromRgb(r, g, b);
        }
    }
}
