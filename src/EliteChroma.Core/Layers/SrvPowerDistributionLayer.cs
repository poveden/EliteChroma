using System.Diagnostics.CodeAnalysis;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class SrvPowerDistributionLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            var cSys = Colors.PowerDistributorScale[Game.Status.Pips.Sys];
            var cEng = Colors.PowerDistributorScale[Game.Status.Pips.Eng];
            var cWep = Colors.PowerDistributorScale[Game.Status.Pips.Wep];

            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseSystemsPower, cSys);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseWeaponsPower, cWep);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.IncreaseEnginesPower, cEng);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.ResetPowerDistribution, Colors.PowerDistributorReset);
        }
    }
}
