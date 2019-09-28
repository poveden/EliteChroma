using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class FlightLayer : LayerBase
    {
        private static readonly IReadOnlyCollection<string> SupercruiseMiscellaneous =
            Miscellaneous.All.Except(new[]
            {
                Miscellaneous.ToggleCargoScoop,
                Miscellaneous.EjectAllCargo,
                Miscellaneous.LandingGearToggle,
            }).ToList().AsReadOnly();

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.Status.HasFlag(Flags.Docked) || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            var k = canvas.Keyboard;
            k[Key.Escape] = Color.White;

            ApplyColorToBinding(canvas.Keyboard, FlightRotation.All, Color.White);
            ApplyColorToBinding(canvas.Keyboard, FlightThrust.All, Color.Orange);
            ApplyColorToBinding(canvas.Keyboard, AlternateFlightControls.All, Color.White);
            ApplyColorToBinding(canvas.Keyboard, FlightThrottle.All, Color.Yellow);
            ApplyColorToBinding(canvas.Keyboard, FlightLandingOverrides.All, Color.White);
            ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.All, Color.Pink);
            ApplyColorToBinding(canvas.Keyboard, Targeting.All, Color.Green);
            ApplyColorToBinding(canvas.Keyboard, Weapons.All, Color.Red);
            ApplyColorToBinding(canvas.Keyboard, Cooling.All, Color.HotPink);

            if (Game.Status.HasFlag(Flags.Supercruise))
            {
                ApplyColorToBinding(canvas.Keyboard, SupercruiseMiscellaneous, Color.Blue);
            }
            else
            {
                ApplyColorToBinding(canvas.Keyboard, Miscellaneous.All, Color.Blue);
            }
        }
    }
}
