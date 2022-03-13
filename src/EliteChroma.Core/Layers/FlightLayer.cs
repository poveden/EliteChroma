using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ChromaWrapper;
using ChromaWrapper.Keyboard;
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

        private static readonly IReadOnlyCollection<string> SupercruiseFlightMiscellaneous =
            FlightMiscellaneous.All.Except(new[]
            {
                FlightMiscellaneous.ToggleFlightAssist,
                FlightMiscellaneous.UseBoostJuice,
            }).ToList().AsReadOnly();

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.DockedOrLanded || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            canvas.Keyboard.Key[KeyboardKey.Esc] = Colors.InterfaceMode;

            if (Game.Status.HasFlag(Flags2.InTaxi))
            {
                return;
            }

            ApplyColorToBinding(canvas.Keyboard, FlightRotation.All, Colors.VehicleRotation);
            ApplyColorToBinding(canvas.Keyboard, FlightThrottle.All, Colors.VehicleThrottle);
            ApplyColorToBinding(canvas.Keyboard, Targeting.All, Colors.VehicleTargeting);
            ApplyColorToBinding(canvas.Keyboard, Weapons.All, Colors.VehicleWeapons);
            ApplyColorToBinding(canvas.Keyboard, Cooling.All, Colors.VehicleCooling);

            if (Game.Status.HasFlag(Flags.Supercruise))
            {
                ApplyColorToBinding(canvas.Keyboard, SupercruiseFlightMiscellaneous, Colors.VehicleMiscellaneous);
                ApplyColorToBinding(canvas.Keyboard, SupercruiseMiscellaneous, Colors.Miscellaneous);
                ApplyColorToBinding(canvas.Keyboard, ModeSwitches.ExplorationFssEnter, Colors.FullSpectrumSystemScanner);
            }
            else
            {
                ApplyColorToBinding(canvas.Keyboard, FlightThrust.All, Colors.VehicleThrust);
                ApplyColorToBinding(canvas.Keyboard, AlternateFlightControls.All, Colors.VehicleAlternate);
                ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.All, Colors.VehicleMiscellaneous);
                ApplyColorToBinding(canvas.Keyboard, Miscellaneous.All, Colors.Miscellaneous);
            }

            if (Game.Status.HasFlag(Flags.FsdCharging))
            {
                _ = StartAnimation();
                ChromaColor jumpColor = PulseColor(Colors.FrameShiftDriveControls, ChromaColor.Black, TimeSpan.FromSeconds(1));
                ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.HyperSuperCombination, jumpColor);
                ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.Supercruise, jumpColor);
                ApplyColorToBinding(canvas.Keyboard, FlightMiscellaneous.Hyperspace, jumpColor);
            }
            else
            {
                _ = StopAnimation();
            }
        }
    }
}
