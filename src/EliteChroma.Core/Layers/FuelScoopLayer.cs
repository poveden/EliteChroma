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

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.Status.HasFlag(Flags.ScoopingFuel))
            {
                _ = StopAnimation();
                return;
            }

            _ = StartAnimation();

            const double offsetStep = -2.0 / KeyboardConstants.MaxColumns;
            double offsetPct = 0;
            for (int col = 0; col < KeyboardConstants.MaxColumns; col++, offsetPct += offsetStep)
            {
                Color c = PulseColor(Color.Black, Game.Colors.Hud, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (int row = 0; row < KeyboardConstants.MaxRows; row++)
                {
                    _ = canvas.Keyboard.MaxAt(row, col, c);
                }
            }

            CustomKeypadEffect keypad = canvas.Keypad;

            offsetPct = (double)KeypadConstants.MaxColumns / KeyboardConstants.MaxColumns;
            for (int col = 0; col < KeypadConstants.MaxColumns; col++, offsetPct += offsetStep)
            {
                Color c = PulseColor(Color.Black, Game.Colors.Hud, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (int row = 0; row < KeypadConstants.MaxRows; row++)
                {
                    keypad[row, col] = keypad[row, col].Max(c);
                }
            }
        }
    }
}
