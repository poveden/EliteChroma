using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
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

            Color lColor;

            if (Game.Status.HasFlag(Flags.CargoScoopDeployed))
            {
                StartAnimation();
                lColor = PulseColor(Color.Black, Color.Orange, TimeSpan.FromSeconds(1));
            }
            else
            {
                StopAnimation();
                lColor = Color.Blue;
            }

            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.ToggleCargoScoop, lColor);
        }
    }
}
