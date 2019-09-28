using System;
using System.Collections.Generic;
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
        public bool Animated { get; private set; }

        protected internal GameState Game { get; internal set; }

        protected DateTimeOffset AnimationStart { get; private set; }

        protected void StartAnimation()
        {
            if (Animated)
            {
                return;
            }

            Animated = true;
            AnimationStart = DateTimeOffset.UtcNow;
        }

        protected void StopAnimation()
        {
            if (!Animated)
            {
                return;
            }

            Animated = false;
        }

        protected Color PulseColor(Color c1, Color c2, TimeSpan period)
        {
            var max = period.TotalSeconds;
            var t = ((DateTimeOffset.UtcNow - AnimationStart).TotalSeconds % max) / max;

            var x = t <= 0.5 ? t * 2 : (1 - t) * 2;

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
