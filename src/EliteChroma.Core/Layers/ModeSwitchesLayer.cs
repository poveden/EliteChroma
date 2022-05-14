using System.Diagnostics.CodeAnalysis;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class ModeSwitchesLayer : LayerBase
    {
        private static readonly IReadOnlyCollection<string> _modeSwitchesUnavailableInTaxi = new[]
        {
            ModeSwitches.PlayerHUDModeToggle,
            ModeSwitches.ExplorationFssEnter,
        };

        private static readonly IReadOnlyCollection<string> _modeSwitchesWhenInTaxi =
            ModeSwitches.All.Except(_modeSwitchesUnavailableInTaxi).ToList().AsReadOnly();

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || (Game.FsdJumpType != FsdJumpType.None && !Game.InWitchSpace))
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, _modeSwitchesWhenInTaxi, Colors.VehicleModeSwitches);

            if (Game.Status.HasFlag(Flags2.InTaxi))
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, _modeSwitchesUnavailableInTaxi, Colors.VehicleModeSwitches);
        }
    }
}
