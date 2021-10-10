using System;
using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using ChromaWrapper.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class LandedLayer : LayerBase
    {
        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.Status.HasFlag(Flags.Landed))
            {
                _ = StopAnimation();
                return;
            }

            _ = StartAnimation();

            canvas.Keyboard.Key[KeyboardKey.Esc] = Colors.InterfaceMode;

            ChromaColor c = PulseColor(ChromaColor.Black, Colors.VehicleThrust, TimeSpan.FromSeconds(1));
            ApplyColorToBinding(canvas.Keyboard, FlightThrust.Up, c);
        }
    }
}
