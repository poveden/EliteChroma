using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using EliteChroma.Core.Chroma;
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

            const double offsetStep = -2.0 / CustomKeyKeyboardEffect.TotalColumns;
            double offsetPct = 0;
            for (int col = 0; col < CustomKeyKeyboardEffect.TotalColumns; col++, offsetPct += offsetStep)
            {
                ChromaColor c = PulseColor(ChromaColor.Black, Game.Colors.Hud, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (int row = 0; row < CustomKeyKeyboardEffect.TotalRows; row++)
                {
                    _ = canvas.Keyboard.MaxAt(row, col, c);
                }
            }

            CustomKeypadEffect keypad = canvas.Keypad;

            offsetPct = (double)CustomKeypadEffect.TotalColumns / CustomKeyKeyboardEffect.TotalColumns;
            for (int col = 0; col < CustomKeypadEffect.TotalColumns; col++, offsetPct += offsetStep)
            {
                ChromaColor c = PulseColor(ChromaColor.Black, Game.Colors.Hud, TimeSpan.FromMilliseconds(_periodMs), offsetPct: offsetPct);

                for (int row = 0; row < CustomKeypadEffect.TotalRows; row++)
                {
                    keypad.Color[row, col] = keypad.Color[row, col].Max(c);
                }
            }
        }
    }
}
