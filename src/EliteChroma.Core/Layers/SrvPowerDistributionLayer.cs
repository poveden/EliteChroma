using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class SrvPowerDistributionLayer : LayerBase
    {
        private static readonly List<Color> _pips = new List<Color>
        {
            Color.Black,
            new Color(0.20, 0.00, 0.00),
            new Color(1.00, 0.00, 0.00),
            new Color(1.00, 0.04, 0.00),
            new Color(1.00, 0.13, 0.00),
            new Color(1.00, 0.32, 0.00),
            new Color(1.00, 0.60, 0.00),
            new Color(1.00, 0.80, 0.20),
            Color.White,
        };

        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            var cSys = _pips[Game.Status.Pips.Sys];
            var cEng = _pips[Game.Status.Pips.Eng];
            var cWep = _pips[Game.Status.Pips.Wep];

            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseSystemsPower, cSys);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseWeaponsPower, cWep);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseEnginesPower, cEng);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.ResetPowerDistribution, new Color(0.5, 0.5, 0.5));
        }
    }
}
