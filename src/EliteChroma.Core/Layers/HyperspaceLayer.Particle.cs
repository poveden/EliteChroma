using System;
using System.Diagnostics;
using Colore.Data;
using Colore.Effects.ChromaLink;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;
using EliteChroma.Chroma;

namespace EliteChroma.Core.Layers
{
    internal partial class HyperspaceLayer
    {
        private sealed class Particle
        {
            private readonly Color _color;
            private readonly double _zps;
            private readonly int _angle;

            public Particle(Color color, double z0, double zps, int angle)
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

                Color c;
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

            private void Render(CustomKeyboardEffect keyboard, Color c, double lastZ)
            {
                // Particles are drawn as stretched lines, simulating movement.
                // Lines are antialiased at both ends.
                //         x0      x1
                //        ┌───┐   ┌───┐
                //  ┌───┬───┬───┬───┬───┬───┐
                //  │   │▒▒▒│███│███│▒▒▒│   │
                //  └───┴───┴───┴───┴───┴───┘
                //   -1   0   1   2   3   4
                const double xScale = KeyboardConstants.MaxColumns / 2.0;
                const double xCenter = (KeyboardConstants.MaxColumns - 1) / 2.0;

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

                int y = _angle % KeyboardConstants.MaxRows;

                // First pixel
                int xi = (int)Math.Floor(x0);
                if (xi >= 0 && xi < KeyboardConstants.MaxColumns)
                {
                    Color caa = c.Transform(1 - (x0 - xi));
                    keyboard[y, xi] = keyboard[y, xi].Max(caa);
                }

                // Last pixel
                int xj = (int)Math.Ceiling(x1);
                if (xj >= 0 && xj < KeyboardConstants.MaxColumns)
                {
                    Color caa = c.Transform(1 - (xj - x1));
                    keyboard[y, xj] = keyboard[y, xj].Max(caa);
                }

                // Middle pixels
                xi = Math.Max(0, xi + 1);
                xj = Math.Min(xj, KeyboardConstants.MaxColumns);
                for (; xi < xj; xi++)
                {
                    keyboard[y, xi] = keyboard[y, xi].Max(c);
                }
            }

            private void Render(CustomMouseEffect mouse, Color c, double lastZ)
            {
                // Particles are drawn as stretched lines, as with keyboard.
                const int yMin = 1;
                const int yMax = MouseConstants.MaxRows - 1;
                const int xMax = MouseConstants.MaxColumns - 1;
                const double yScale = MouseConstants.MaxRows - yMin;

                if (_angle > 90)
                {
                    return;
                }

                switch (_angle % 4)
                {
                    case 0:
                        mouse[GridLed.ScrollWheel] = mouse[GridLed.ScrollWheel].Max(c);
                        return;
                    case 1:
                        mouse[GridLed.Logo] = mouse[GridLed.Logo].Max(c);
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
                    mouse[yMin, 0] = mouse[yMin, 0].Max(c);
                    mouse[yMin, xMax] = mouse[yMin, 0];
                    return;
                }

                // First pixel
                int yi = (int)Math.Floor(y0);
                if (yi >= yMin && yi <= yMax)
                {
                    Color caa = c.Transform(1 - (y0 - yi));
                    mouse[yi, 0] = mouse[yi, 0].Max(caa);
                    mouse[yi, xMax] = mouse[yi, 0];
                }

                // Last pixel
                int yj = (int)Math.Ceiling(y1);
                if (yj >= yMin && yj <= yMax)
                {
                    Color caa = c.Transform(1 - (yj - y1));
                    mouse[yj, 0] = mouse[yj, 0].Max(caa);
                    mouse[yj, xMax] = mouse[yj, 0];
                }

                // Middle pixels
                yi = Math.Max(yMin, yi + 1);
                yj = Math.Min(yj, yMax);
                for (; yi <= yj; yi++)
                {
                    mouse[yi, 0] = mouse[yi, 0].Max(c);
                    mouse[yi, xMax] = mouse[yi, 0];
                }
            }

            private void Render(CustomMousepadEffect mousepad, Color c)
            {
                if (_angle > 180)
                {
                    return;
                }

                int ia = _angle % MousepadConstants.MaxLeds;
                int ib = (ia + 1) % MousepadConstants.MaxLeds;
                int ic = (ia + 2) % MousepadConstants.MaxLeds;

                Color cf = c.Transform(0.25);

                mousepad[ia] = mousepad[ia].Max(cf);
                mousepad[ib] = mousepad[ib].Max(c);
                mousepad[ic] = mousepad[ic].Max(cf);
            }

            private void Render(CustomKeypadEffect keypad, Color c, double lastZ)
            {
                // Particles are drawn as stretched lines, as with keyboard.
                const int xMax = KeypadConstants.MaxColumns - 1;
                const double xScale = KeypadConstants.MaxColumns;

                const int thumbKey = KeypadConstants.MaxKeys - 1;
                const int thumbKeyColumn = KeypadConstants.MaxColumns - 1;
                const int thumbKeyRow = KeypadConstants.MaxRows - 1;

                if (_angle >= 180)
                {
                    return;
                }

                int y = _angle % (KeypadConstants.MaxRows + 1);

                if (y == KeypadConstants.MaxRows)
                {
                    keypad[thumbKey] = keypad[thumbKey].Max(c);
                    return;
                }

                if (Z >= 0)
                {
                    return;
                }

                void MaxNoThumb(int row, int column, Color color)
                {
                    if (row == thumbKeyRow && column == thumbKeyColumn)
                    {
                        return;
                    }

                    keypad[row, column] = keypad[row, column].Max(color);
                }

                Debug.Assert(lastZ >= Z, "Z always decreases");
                double xPrev = (lastZ < 0 ? Math.Max(1 / lastZ, -2) : -2) * xScale;
                double xCurr = Math.Max(1 / Z, -2) * xScale;

                double x0 = xMax - (xScale + xCurr);
                double x1 = xMax - (xScale + xPrev);

                // First pixel
                int xi = (int)Math.Floor(x0);
                if (xi >= 0 && xi <= xMax)
                {
                    Color caa = c.Transform(1 - (x0 - xi));
                    MaxNoThumb(y, xi, caa);
                }

                // Last pixel
                int xj = (int)Math.Ceiling(x1);
                if (xj >= 0 && xj <= xMax)
                {
                    Color caa = c.Transform(1 - (xj - x1));
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

            private void Render(CustomHeadsetEffect headset, Color c)
            {
                if (_angle > 90)
                {
                    return;
                }

                int i = _angle % HeadsetConstants.MaxLeds;
                headset[i] = headset[i].Max(c);
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

                ReadOnlySpan<double> offsets = stackalloc double[ChromaLinkConstants.MaxLeds - 1] { 1.5, 0.5, -0.5, -1.5 };
                double maxB = 0;

                for (int i = 0; i < offsets.Length; i++)
                {
                    double bi = Math.Clamp(Gamma - Math.Abs(((offsets[i] * Step) - lastZ) / Spread), 0, 1);
                    maxB = Math.Max(maxB, bi);
                    Color ci = _color.Transform(bi);
                    chromaLink[i + 1] = chromaLink[i + 1].Max(ci);
                }

                chromaLink[0] = chromaLink[0].Max(_color.Transform(maxB));
            }
        }
    }
}
