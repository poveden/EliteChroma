using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Chroma;
using EliteFiles.Bindings.Binds;
using EliteFiles.Status;
using static EliteFiles.Journal.Events.StartJump;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class AnalysisModeLayer : LayerBase
    {
        private static readonly Color CombatModeColor = Color.Red;

        public override int Order => 600;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (!Game.InCockpit || Game.FsdJumpType != FsdJumpType.None)
            {
                return;
            }

            var color = Game.Status.HasFlag(Flags.HudInAnalysisMode) ? Game.Colors.AnalysisMode : CombatModeColor;

            ApplyColorToBinding(canvas.Keyboard, ModeSwitches.PlayerHUDModeToggle, color);

            if (!Game.DockedOrLanded)
            {
                ApplyColorToBinding(canvas.Keyboard, Weapons.PrimaryFire, color);
                ApplyColorToBinding(canvas.Keyboard, Weapons.SecondaryFire, color);

                Color hColor = color;
                Color mColor = color;
                Color bColor = color.Transform(0.5);

                if (Game.Status.HasFlag(Flags.HardpointsDeployed) && !Game.Status.HasFlag(Flags.Supercruise))
                {
                    StartAnimation();
                    hColor = PulseColor(Color.Black, color, TimeSpan.FromSeconds(1));
                }
                else
                {
                    StopAnimation();
                    mColor = bColor;
                }

                ApplyColorToBinding(canvas.Keyboard, Weapons.DeployHardpointToggle, hColor);
                canvas.Mouse.Set(mColor);
                canvas.Mousepad.Set(mColor);
                canvas.Headset.Set(mColor);
                canvas.ChromaLink.Set(bColor);
            }
        }
    }
}
