using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using ChromaWrapper.Sdk;
using EliteChroma.Core.Chroma;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Simple randomness for glitch effect.")]
    internal sealed class GlitchLayer : LayerBase
    {
        private readonly Random _rnd = new Random();

        private enum DeviceGlitch
        {
            None = 0,
            Simple,
            Full,
        }

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.Status.LegalState == LegalState.Thargoid)
            {
                _ = StartAnimation();
            }
            else
            {
                _ = StopAnimation();
                return;
            }

            RenderShiftGlitch(canvas.Keyboard.Key);
            RenderColorGlitch(canvas.Mouse.Color);
            RenderColorGlitch(canvas.Mousepad.Color);
            RenderShiftGlitch(canvas.Keypad.Color);
            RenderColorGlitch(canvas.Headset.Color);
            RenderColorGlitch(canvas.ChromaLink.Color);
        }

        private void RenderShiftGlitch(IKeyGrid grid)
        {
            DeviceGlitch glitch = GetDeviceGlitch();
            if (glitch == DeviceGlitch.None)
            {
                return;
            }

            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Columns; x++)
                {
                    if (glitch == DeviceGlitch.Full || IsLedGlitchy())
                    {
                        int x2 = Math.Clamp(x + GetGlitchKeyShift(), 0, grid.Columns - 1);
                        (grid[y, x2], grid[y, x]) = (grid[y, x], grid[y, x2]);
                    }
                }
            }
        }

        private void RenderShiftGlitch(ILedGrid grid)
        {
            DeviceGlitch glitch = GetDeviceGlitch();
            if (glitch == DeviceGlitch.None)
            {
                return;
            }

            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Columns; x++)
                {
                    if (glitch == DeviceGlitch.Full || IsLedGlitchy())
                    {
                        int x2 = Math.Clamp(x + GetGlitchKeyShift(), 0, grid.Columns - 1);
                        (grid[y, x2], grid[y, x]) = (grid[y, x], grid[y, x2]);
                    }
                }
            }
        }

        private void RenderColorGlitch(ILedArray leds)
        {
            DeviceGlitch glitch = GetDeviceGlitch();
            if (glitch == DeviceGlitch.None)
            {
                return;
            }

            for (int i = 0; i < leds.Count; i++)
            {
                if (glitch == DeviceGlitch.Full || IsLedGlitchy())
                {
                    leds[i] = GetColorGlitch(leds[i]);
                }
            }
        }

        private DeviceGlitch GetDeviceGlitch()
        {
            const double DeviceProbability = 0.05;
            const double AllLedsProbability = DeviceProbability * 0.1;

            return _rnd.NextDouble() switch
            {
                <= AllLedsProbability => DeviceGlitch.Full,
                <= DeviceProbability => DeviceGlitch.Simple,
                _ => DeviceGlitch.None,
            };
        }

        private bool IsLedGlitchy()
        {
            const double LedProbability = 0.1;

            return _rnd.NextDouble() <= LedProbability;
        }

        private int GetGlitchKeyShift()
        {
            const int MinShift = -2;
            const int MaxShift = 3;

            return _rnd.Next(MinShift, MaxShift);
        }

        private ChromaColor GetColorGlitch(ChromaColor color)
        {
            int flags = _rnd.Next(1, 7);
            byte r = (flags & 1) != 0 ? color.R : (byte)0;
            byte g = (flags & 2) != 0 ? color.G : (byte)0;
            byte b = (flags & 4) != 0 ? color.B : (byte)0;

            return ChromaColor.FromRgb(r, g, b);
        }
    }
}
