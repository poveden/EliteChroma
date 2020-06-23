using System.Diagnostics.CodeAnalysis;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class PowerDistributionLayer : LayerBase
    {
        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.DockedOrLanded || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            var cSys = Colors.PowerDistributorScale[Game.Status.Pips.Sys];
            var cEng = Colors.PowerDistributorScale[Game.Status.Pips.Eng];
            var cWep = Colors.PowerDistributorScale[Game.Status.Pips.Wep];

            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.IncreaseSystemsPower, cSys);
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.IncreaseWeaponsPower, cWep);
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.IncreaseEnginesPower, cEng);
            ApplyColorToBinding(canvas.Keyboard, Miscellaneous.ResetPowerDistribution, Colors.PowerDistributorReset);
        }
    }
}
