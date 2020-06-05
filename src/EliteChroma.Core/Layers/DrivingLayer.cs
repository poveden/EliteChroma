using System.Diagnostics.CodeAnalysis;
using Colore.Data;
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
            k[Key.Escape] = Color.White;

            ApplyColorToBinding(canvas.Keyboard, Driving.Rotation, Color.White);
            ApplyColorToBinding(canvas.Keyboard, Driving.Thrust, Color.Orange.Combine(Color.Red));
            ApplyColorToBinding(canvas.Keyboard, DriveThrottle.All, Color.Yellow);
            ApplyColorToBinding(canvas.Keyboard, Driving.AutoBreak, Color.Yellow);
            ApplyColorToBinding(canvas.Keyboard, DrivingMiscellaneous.All, Color.Pink);
            ApplyColorToBinding(canvas.Keyboard, DrivingTargeting.All, Color.Green);

            if (!Game.Status.HasFlag(Flags.SrvTurretRetracted))
            {
                ApplyColorToBinding(canvas.Keyboard, Driving.Weapons, Color.Red);
            }
        }
    }
}
