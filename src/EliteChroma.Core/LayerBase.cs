using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Core.Internal;
using EliteChroma.Elite;
using EliteFiles.Bindings.Devices;

namespace EliteChroma.Core
{
    public abstract class LayerBase : EffectLayer
    {
        [SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute", Justification = "No flags")]
        protected enum PulseColorType
        {
            Triangle = 0,
            Square = 1,

            Default = Triangle,
        }

        public bool Animated { get; private set; }

        protected GameState Game { get; private set; }

        protected DateTimeOffset Now => Game.Now;

        protected DateTimeOffset AnimationStart { get; private set; }

        protected TimeSpan AnimationElapsed => Now - AnimationStart;

        protected override void OnRender(ChromaCanvas canvas, object state)
        {
            Game = (GameState)state;
            OnRender(canvas);
        }

        protected abstract void OnRender(ChromaCanvas canvas);

        protected virtual bool StartAnimation()
        {
            if (Animated)
            {
                return false;
            }

            Animated = true;
            AnimationStart = Now;
            return true;
        }

        protected virtual bool StopAnimation()
        {
            if (!Animated)
            {
                return false;
            }

            Animated = false;
            return true;
        }

        protected Color PulseColor(Color c1, Color c2, TimeSpan period, PulseColorType pulseType = PulseColorType.Triangle, double offsetPct = 0)
        {
            var max = period.TotalSeconds;
            var offset = max * (offsetPct - Math.Floor(offsetPct));
            var t = ((AnimationElapsed.TotalSeconds + offset) % max) / max;

            double x;

            switch (pulseType)
            {
                case PulseColorType.Square:
                    x = t < 0.5 ? 1 : 0;
                    break;
                case PulseColorType.Triangle:
                default:
                    x = t < 0.5 ? t * 2 : (1 - t) * 2;
                    break;
            }

            var r = (c1.R / 255.0 * (1 - x)) + (c2.R / 255.0 * x);
            var g = (c1.G / 255.0 * (1 - x)) + (c2.G / 255.0 * x);
            var b = (c1.B / 255.0 * (1 - x)) + (c2.B / 255.0 * x);

            return new Color(r, g, b);
        }

        protected void ApplyColorToBinding(KeyboardCustom grid, IEnumerable<string> bindingNames, Color color)
        {
            if (bindingNames == null)
            {
                throw new ArgumentNullException(nameof(bindingNames));
            }

            foreach (var bindingName in bindingNames)
            {
                ApplyColorToBinding(grid, bindingName, color);
            }
        }

        protected void ApplyColorToBinding(KeyboardCustom grid, string bindingName, Color color)
        {
            if (!Game.Bindings.TryGetValue(bindingName, out var binding))
            {
                return;
            }

            foreach (var bps in new[] { binding.Primary, binding.Secondary })
            {
                if (bps.Device != Device.Keyboard)
                {
                    continue;
                }

                if (!KeyMappings.EliteKeys.TryGetValue(bps.Key, out var key))
                {
                    continue;
                }

                if (key == Key.Invalid)
                {
                    continue;
                }

                if (!bps.Modifiers.Equals(Game.PressedModifiers))
                {
                    continue;
                }

                grid[key] = color;

                color = color.Transform(0.2);
            }
        }
    }
}
