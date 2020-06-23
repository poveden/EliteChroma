using System.Diagnostics.CodeAnalysis;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class DrivingLayer : LayerBase
    {
        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InSrv)
            {
                return;
            }

            var k = canvas.Keyboard;
            k[Key.Escape] = Colors.InterfaceMode;

            ApplyColorToBinding(canvas.Keyboard, Driving.Rotation, Colors.VehicleRotation);
            ApplyColorToBinding(canvas.Keyboard, Driving.Thrust, Colors.VehicleThrust);
            ApplyColorToBinding(canvas.Keyboard, DriveThrottle.All, Colors.VehicleThrottle);
            ApplyColorToBinding(canvas.Keyboard, Driving.AutoBreak, Colors.VehicleThrottle);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.All, Colors.VehicleMiscellaneous);
            ApplyColorToBinding(canvas.Keyboard, DrivingTargeting.All, Colors.VehicleTargeting);

            if (!Game.Status.HasFlag(Flags.SrvTurretRetracted))
            {
                ApplyColorToBinding(canvas.Keyboard, Driving.Weapons, Colors.VehicleWeapons);
            }
        }
    }
}
