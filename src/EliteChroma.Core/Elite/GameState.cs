using System;
using System.Collections.Generic;
using EliteFiles.Bindings;
using EliteFiles.Graphics;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Elite
{
    public sealed class GameState
    {
        public static readonly TimeSpan JumpCountdownDelay = TimeSpan.FromSeconds(5);

        internal GameState()
        {
        }

        public DateTimeOffset Now { get; internal set; }

        public GameProcessState ProcessState { get; internal set; }

        public IReadOnlyDictionary<string, Binding> Bindings { get; internal set; }

        public DeviceKeySet PressedModifiers { get; internal set; }

        public StatusEntry Status { get; internal set; }

        public GuiColourMatrix GuiColour { get; internal set; }

        public string MusicTrack { get; internal set; }

        public StartJump.FsdJumpType FsdJumpType { get; internal set; }

        public string FsdJumpStarClass { get; internal set; }

        public DateTimeOffset FsdJumpChange { get; internal set; }

        public bool InMainMenu => MusicTrack == "MainMenu";

        public bool InGalacticPowers => MusicTrack == "GalacticPowers";

        public UnderAttack.AttackTarget AttackTarget { get; internal set; }

        public DateTimeOffset AttackTargetChange { get; internal set; }

        public bool InCockpit
        {
            get
            {
                if (Status.Flags == Flags.None)
                {
                    return false;
                }

                switch (Status.GuiFocus)
                {
                    case GuiFocus.GalaxyMap:
                    case GuiFocus.SystemMap:
                    case GuiFocus.FssMode:
                    case GuiFocus.Codex:
                        return false;
                }

                if (InGalacticPowers)
                {
                    return false;
                }

                return true;
            }
        }

        public bool InWitchSpace =>
            FsdJumpType == StartJump.FsdJumpType.Hyperspace
            && (Now - FsdJumpChange) >= JumpCountdownDelay;

        public GameState Copy() => (GameState)MemberwiseClone();
    }
}
