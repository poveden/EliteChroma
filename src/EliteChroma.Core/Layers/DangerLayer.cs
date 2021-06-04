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

            bool underAttack = Now < _underAttackFade;
            bool inDanger = Game.Status.HasFlag(Flags.IsInDanger);
            bool otherDanger = Game.Status.HasFlag(Flags.Overheating);

            if (underAttack || inDanger || otherDanger)
            {
                _ = StartAnimation();
            }
            else
            {
                _ = StopAnimation();
                return;
            }

            CustomKeyboardEffect k = canvas.Keyboard;

            if (underAttack || otherDanger)
            {
                double flarePct = PulseColor(Color.Black, Color.White, _fastPulse).R / 255.0;

                if (!otherDanger)
                {
                    double fade = (_underAttackFade - Now).TotalSeconds / _underAttackDuration.TotalSeconds;
                    flarePct = flarePct * fade * fade;
                }

                Color c = GameColors.RedAlert.Transform(flarePct);

                Color cLogo = k[Key.Logo];
                _ = canvas.Keyboard.Max(c);
                k[Key.Logo] = cLogo;

                _ = canvas.Mouse.Combine(GameColors.RedAlert, flarePct);
                _ = canvas.Mousepad.Combine(GameColors.RedAlert, flarePct);
                _ = canvas.Keypad.Max(c);
                _ = canvas.Headset.Combine(GameColors.RedAlert, flarePct);
                _ = canvas.ChromaLink.Combine(GameColors.RedAlert, flarePct);
            }

            if (inDanger)
            {
                k[Key.Logo] = PulseColor(k[Key.Logo], GameColors.RedAlert, _slowPulse);
            }
        }
    }
}
