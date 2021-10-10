using System;
using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class SrvCargoScoopLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            ChromaColor lColor;

            if (Game.Status.HasFlag(Flags.CargoScoopDeployed))
            {
                _ = StartAnimation();
                lColor = PulseColor(ChromaColor.Black, Colors.CargoScoopDeployed, TimeSpan.FromSeconds(1));
            }
            else
            {
                _ = StopAnimation();
                lColor = Colors.CargoScoopRetracted;
            }

            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.ToggleCargoScoop, lColor);
        }
    }
}
