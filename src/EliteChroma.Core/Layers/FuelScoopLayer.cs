using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using EliteChroma.Chroma;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class FuelScoopLayer : LayerBase
    {
        private const double _periodMs = 750.0;

        private static readonly Color EliteOrange = new Color(1.0, 0.2, 0);

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.Status.HasFlag(Flags.ScoopingFuel))
            {
                StopAnimation();
                return;
            }

            StartAnimation();

            const double offsetStep = -2.0 / KeyboardConstants.MaxColumns;
            double offsetPct = 0;
            for (int col = 0; col < KeyboardConstants.MaxColumns; col++, offsetPct += offsetStep)
            {
                var c = PulseColor(Color.Black, EliteOrange, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (var row = 0; row < KeyboardConstants.MaxRows; row++)
                {
                    canvas.Keyboard.MaxAt(row, col, c);
                }
            }

            var keypad = canvas.Keypad;

            offsetPct = (double)KeypadConstants.MaxColumns / KeyboardConstants.MaxColumns;
            for (int col = 0; col < KeypadConstants.MaxColumns; col++, offsetPct += offsetStep)
            {
                var c = PulseColor(Color.Black, EliteOrange, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (var row = 0; row < KeypadConstants.MaxRows; row++)
                {
                    keypad[row, col] = keypad[row, col].Max(c);
                }
            }
        }
    }
}
