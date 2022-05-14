using System.Diagnostics;
using ChromaWrapper;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Headset;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using EliteChroma.Chroma;

namespace EliteChroma.Core.Layers
{
    internal partial class HyperspaceLayer
    {
        private sealed class Particle
        {
            private readonly ChromaColor _color;
            private readonly double _zps;
            private readonly int _angle;

            public Particle(ChromaColor color, double z0, double zps, int angle)
            {
                _color = color;
                _zps = zps;
                _angle = angle;
                Z = z0;
            }

            public double Z { get; private set; }

            public void MoveAndRender(ChromaCanvas canvas, TimeSpan deltaT)
            {
                double lastZ = Z;
                Z += _zps * deltaT.TotalSeconds;

                ChromaColor c;
                if (lastZ > 0 && Z < 0)
                {
                    // Particle just zoomed by. We apply full brightness.
                    c = _color;
                }
                else
                {
                    // We take the brightness from the nearest point in the light streak.
                    double nearestZ = Math.Min(Math.Abs(lastZ), Math.Abs(Z));
                    c = _color.Transform(1 / (1 + nearestZ));
                }

                Render(canvas.Keyboard, c, lastZ);
                Render(canvas.Mouse, c, lastZ);
                Render(canvas.Mousepad, c);
                Render(canvas.Keypad, c, lastZ);
                Render(canvas.Headset, c);
                Render(canvas.ChromaLink, lastZ);
            }

            private void Render(CustomKeyKeyboardEffect keyboard, ChromaColor c, double lastZ)
            {
                // Particles are drawn as stretched lines, simulating movement.
                // Lines are antialiased at both ends.
                //         x0      x1
                //        ┌───┐   ┌───┐
                //  ┌───┬───┬───┬───┬───┬───┐
                //  │   │▒▒▒│███│███│▒▒▒│   │
                //  └───┴───┴───┴───┴───┴───┘
                //   -1   0   1   2   3   4
                const double xScale = CustomKeyKeyboardEffect.TotalColumns / 2.0;
                const double xCenter = (CustomKeyKeyboardEffect.TotalColumns - 1) / 2.0;

                Debug.Assert(lastZ >= Z, "Z always decreases");
                double xPrev = (lastZ > 0 ? Math.Min(1 / lastZ, 2) : 2) * xScale;
                double xCurr = (Z > 0 ? Math.Min(1 / Z, 2) : 2) * xScale;

                double x0, x1;
                if (_angle >= 180)
                {
                    x0 = xCenter + xPrev;
                    x1 = xCenter + xCurr;
                }
                else
                {
                    x1 = xCenter - xPrev;
                    x0 = xCenter - xCurr;
                }

                int y = _angle % CustomKeyKeyboardEffect.TotalRows;

                // First pixel
                int xi = (int)Math.Floor(x0);
                if (xi is >= 0 and < CustomKeyKeyboardEffect.TotalColumns)
                {
                    ChromaColor caa = c.Transform(1 - (x0 - xi));
                    keyboard.Color[y, xi] = keyboard.Color[y, xi].Max(caa);
                }

                // Last pixel
                int xj = (int)Math.Ceiling(x1);
                if (xj is >= 0 and < CustomKeyKeyboardEffect.TotalColumns)
                {
                    ChromaColor caa = c.Transform(1 - (xj - x1));
                    keyboard.Color[y, xj] = keyboard.Color[y, xj].Max(caa);
                }

                // Middle pixels
                xi = Math.Max(0, xi + 1);
                xj = Math.Min(xj, CustomKeyKeyboardEffect.TotalColumns);
                for (; xi < xj; xi++)
                {
                    keyboard.Color[y, xi] = keyboard.Color[y, xi].Max(c);
                }
            }

            private void Render(CustomMouseEffect2 mouse, ChromaColor c, double lastZ)
            {
                // Particles are drawn as stretched lines, as with keyboard.
                const int yMin = 1;
                const int yMax = CustomMouseEffect2.TotalRows - 1;
                const int xMax = CustomMouseEffect2.TotalColumns - 1;
                const double yScale = CustomMouseEffect2.TotalRows - yMin;

                if (_angle > 90)
                {
                    return;
                }

                switch (_angle % 4)
                {
                    case 0:
                        mouse.Color[MouseLed2.ScrollWheel] = mouse.Color[MouseLed2.ScrollWheel].Max(c);
                        return;
                    case 1:
                        mouse.Color[MouseLed2.Logo] = mouse.Color[MouseLed2.Logo].Max(c);
                        return;
                    default:
                        break;
                }

                Debug.Assert(lastZ >= Z, "Z always decreases");
                double yPrev = (lastZ < 0 ? Math.Max(1 / lastZ, -2) : -2) * yScale;
                double yCurr = (Z < 0 ? Math.Max(1 / Z, -2) : -2) * yScale;

                double y0 = (yScale + yPrev) + yMin;
                double y1 = (yScale + yCurr) + yMin;

                if (y1 < yMin)
                {
                    mouse.Color[yMin, 0] = mouse.Color[yMin, 0].Max(c);
                    mouse.Color[yMin, xMax] = mouse.Color[yMin, 0];
                    return;
                }

                // First pixel
                int yi = (int)Math.Floor(y0);
                if (yi is >= yMin and <= yMax)
                {
                    ChromaColor caa = c.Transform(1 - (y0 - yi));
                    mouse.Color[yi, 0] = mouse.Color[yi, 0].Max(caa);
                    mouse.Color[yi, xMax] = mouse.Color[yi, 0];
                }

                // Last pixel
                int yj = (int)Math.Ceiling(y1);
                if (yj is >= yMin and <= yMax)
                {
                    ChromaColor caa = c.Transform(1 - (yj - y1));
                    mouse.Color[yj, 0] = mouse.Color[yj, 0].Max(caa);
                    mouse.Color[yj, xMax] = mouse.Color[yj, 0];
                }

                // Middle pixels
                yi = Math.Max(yMin, yi + 1);
                yj = Math.Min(yj, yMax);
                for (; yi <= yj; yi++)
                {
                    mouse.Color[yi, 0] = mouse.Color[yi, 0].Max(c);
                    mouse.Color[yi, xMax] = mouse.Color[yi, 0];
                }
            }

            private void Render(CustomMousepadEffect mousepad, ChromaColor c)
            {
                if (_angle > 180)
                {
                    return;
                }

                int ia = _angle % CustomMousepadEffect.TotalLeds;
                int ib = (ia + 1) % CustomMousepadEffect.TotalLeds;
                int ic = (ia + 2) % CustomMousepadEffect.TotalLeds;

                ChromaColor cf = c.Transform(0.25);

                mousepad.Color[ia] = mousepad.Color[ia].Max(cf);
                mousepad.Color[ib] = mousepad.Color[ib].Max(c);
                mousepad.Color[ic] = mousepad.Color[ic].Max(cf);
            }

            private void Render(CustomKeypadEffect keypad, ChromaColor c, double lastZ)
            {
                // Particles are drawn as stretched lines, as with keyboard.
                const int xMax = CustomKeypadEffect.TotalColumns - 1;
                const double xScale = CustomKeypadEffect.TotalColumns;

                const int thumbKey = CustomKeypadEffect.TotalLeds - 1;
                const int thumbKeyColumn = CustomKeypadEffect.TotalColumns - 1;
                const int thumbKeyRow = CustomKeypadEffect.TotalRows - 1;

                if (_angle >= 180)
                {
                    return;
                }

                int y = _angle % (CustomKeypadEffect.TotalRows + 1);

                if (y == CustomKeypadEffect.TotalRows)
                {
                    keypad.Color[thumbKey] = keypad.Color[thumbKey].Max(c);
                    return;
                }

                if (Z >= 0)
                {
                    return;
                }

                void MaxNoThumb(int row, int column, ChromaColor color)
                {
                    if (row == thumbKeyRow && column == thumbKeyColumn)
                    {
                        return;
                    }

                    keypad.Color[row, column] = keypad.Color[row, column].Max(color);
                }

                Debug.Assert(lastZ >= Z, "Z always decreases");
                double xPrev = (lastZ < 0 ? Math.Max(1 / lastZ, -2) : -2) * xScale;
                double xCurr = Math.Max(1 / Z, -2) * xScale;

                double x0 = xMax - (xScale + xCurr);
                double x1 = xMax - (xScale + xPrev);

                // First pixel
                int xi = (int)Math.Floor(x0);
                if (xi is >= 0 and <= xMax)
                {
                    ChromaColor caa = c.Transform(1 - (x0 - xi));
                    MaxNoThumb(y, xi, caa);
                }

                // Last pixel
                int xj = (int)Math.Ceiling(x1);
                if (xj is >= 0 and <= xMax)
                {
                    ChromaColor caa = c.Transform(1 - (xj - x1));
                    MaxNoThumb(y, xj, caa);
                }

                // Middle pixels
                xi = Math.Max(0, xi + 1);
                xj = Math.Min(xj, xMax);
                for (; xi <= xj; xi++)
                {
                    MaxNoThumb(y, xi, c);
                }
            }

            private void Render(CustomHeadsetEffect headset, ChromaColor c)
            {
                if (_angle > 90)
                {
                    return;
                }

                int i = _angle % CustomHeadsetEffect.TotalLeds;
                headset.Color[i] = headset.Color[i].Max(c);
            }

            private void Render(CustomChromaLinkEffect chromaLink, double lastZ)
            {
                // Particles are drawn as moving front to back.
                //             4   2   0  -2  -4
                //  ┌───┐      ┌───┬───┬───┬───┐
                //  │CL1│      │CL2│CL3│CL4│CL5│
                //  └───┘      └───┴───┴───┴───┘
                //  Base     Front     →      Back
                // Reference: https://developer.razer.com/works-with-chroma/chroma-link-guide/
                const double Step = 2;
                const double Spread = 6;
                const double Gamma = 1.25;

                if (_angle > 30)
                {
                    return;
                }

                ReadOnlySpan<double> offsets = stackalloc double[CustomChromaLinkEffect.TotalLeds - 1] { 1.5, 0.5, -0.5, -1.5 };
                double maxB = 0;

                for (int i = 0; i < offsets.Length; i++)
                {
                    double bi = Math.Clamp(Gamma - Math.Abs(((offsets[i] * Step) - lastZ) / Spread), 0, 1);
                    maxB = Math.Max(maxB, bi);
                    ChromaColor ci = _color.Transform(bi);
                    chromaLink.Color[i + 1] = chromaLink.Color[i + 1].Max(ci);
                }

                chromaLink.Color[0] = chromaLink.Color[0].Max(_color.Transform(maxB));
            }
        }
    }
}
