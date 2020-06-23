using System;
using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Elite;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Core.Layers
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated by ChromaController.InitChromaEffect().")]
    internal sealed class DangerLayer : LayerBase
    {
        private static readonly TimeSpan _underAttackDuration = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan _fastPulse = TimeSpan.FromMilliseconds(350);
        private static readonly TimeSpan _slowPulse = TimeSpan.FromSeconds(1);

        private DateTimeOffset _lastUnderAttack;
        private DateTimeOffset _underAttackFade;

        public override int Order => 700;

        protected override void OnRender(ChromaCanvas canvas)
        {
            if (Game.AttackTarget == UnderAttack.AttackTarget.You && Game.AttackTargetChange > _lastUnderAttack)
            {
                _lastUnderAttack = Game.AttackTargetChange;
                _underAttackFade = _lastUnderAttack + _underAttackDuration;
            }

            var underAttack = Now < _underAttackFade;
            var inDanger = Game.Status.HasFlag(Flags.IsInDanger);
            var otherDanger = Game.Status.HasFlag(Flags.Overheating);

            if (underAttack || inDanger || otherDanger)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
                return;
            }

            var k = canvas.Keyboard;

            if (underAttack || otherDanger)
            {
                var hiColor = GameColors.RedAlert;
                var loColor = Game.Colors.Hud.Transform(Colors.DimBrightness);

                if (!otherDanger)
                {
                    var fade = (_underAttackFade - Now).TotalSeconds / _underAttackDuration.TotalSeconds;
                    hiColor = loColor.Combine(hiColor, fade * fade);
                }

                var c = PulseColor(hiColor, loColor, _fastPulse);

                var cLogo = k[Key.Logo];
                canvas.Keyboard.Max(c);
                k[Key.Logo] = cLogo;

                canvas.Mouse.Set(c);
                canvas.Mousepad.Set(c);
                canvas.Keypad.Set(c);
                canvas.Headset.Set(c);
                canvas.ChromaLink.Set(c);
            }

            if (inDanger)
            {
                k[Key.Logo] = PulseColor(k[Key.Logo], GameColors.RedAlert, _slowPulse);
            }
        }
    }
}
