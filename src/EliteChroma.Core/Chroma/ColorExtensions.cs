using System;
using Colore.Data;
using EliteFiles.Graphics;

namespace EliteChroma.Chroma
{
    public static class ColorExtensions
    {
        public static Color Combine(this Color c1, Color c2, double c2pct = 0.5)
        {
            var c1pct = 1.0 - c2pct;

            var r1 = c1.R * c1pct;
            var g1 = c1.G * c1pct;
            var b1 = c1.B * c1pct;

            var r2 = c2.R * c2pct;
            var g2 = c2.G * c2pct;
            var b2 = c2.B * c2pct;

            var r = Math.Clamp((r1 + r2) / 255.0, 0, 1);
            var g = Math.Clamp((g1 + g2) / 255.0, 0, 1);
            var b = Math.Clamp((b1 + b2) / 255.0, 0, 1);

            return new Color(r, g, b);
        }

        public static Color Max(this Color c1, Color c2)
        {
            var r = Math.Max(c1.R, c2.R);
            var g = Math.Max(c1.G, c2.G);
            var b = Math.Max(c1.B, c2.B);

            return new Color(r, g, b);
        }

        public static Color Transform(this Color color, IRgbTransformMatrix transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var c0 = color.R / 255.0;
            var c1 = color.G / 255.0;
            var c2 = color.B / 255.0;

            var r =
                (transform[0, 0] * c0) +
                (transform[0, 1] * c1) +
                (transform[0, 2] * c2);

            var g =
                (transform[1, 0] * c0) +
                (transform[1, 1] * c1) +
                (transform[1, 2] * c2);

            var b =
                (transform[2, 0] * c0) +
                (transform[2, 1] * c1) +
                (transform[2, 2] * c2);

            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);

            return new Color(r, g, b);
        }

        public static Color Transform(this Color color, double multiply, double gamma = 1.0)
        {
            var r = color.R / 255.0;
            var g = color.G / 255.0;
            var b = color.B / 255.0;

            if (multiply != 1.0)
            {
                r *= multiply;
                g *= multiply;
                b *= multiply;
            }

            if (gamma != 1.0)
            {
                r = Math.Pow(r, gamma);
                g = Math.Pow(g, gamma);
                b = Math.Pow(b, gamma);
            }

            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);

            return new Color(r, g, b);
        }
    }
}
