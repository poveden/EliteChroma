using System;
using EliteFiles.Bindings;
using EliteFiles.Graphics;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Elite
{
    public sealed class GameState
    {
        public static readonly TimeSpan JumpCountdownDelay = TimeSpan.FromSeconds(5);

        private GuiColourMatrix _guiColour;

        internal GameState()
        {
            Status = StatusEntry.Empty;
            _guiColour = GuiColourMatrix.Default;
            Colors = new GameColors(_guiColour);
        }

        public DateTimeOffset Now { get; internal set; }

        public GameProcessState ProcessState { get; internal set; }

        public BindingPreset? BindingPreset { get; internal set; }

        public DeviceKeySet? PressedModifiers { get; internal set; }

        public bool ForceEnUSKeyboardLayout { get; internal set; }

        public StatusEntry Status { get; internal set; }

        public GuiColourMatrix GuiColour
        {
            get => _guiColour;
            internal set
            {
                _guiColour = value;
                Colors = new GameColors(_guiColour);
            }
        }

        public GameColors Colors { get; private set; }

        public string? MusicTrack { get; internal set; }

        public StartJump.FsdJumpType FsdJumpType { get; internal set; }

        public string? FsdJumpStarClass { get; internal set; }

        public DateTimeOffset FsdJumpChange { get; internal set; }

        public bool InMainMenu => MusicTrack == "MainMenu";

        public bool InGalacticPowers => MusicTrack == "GalacticPowers";

        public bool InSquadronsView => MusicTrack == "Squadrons";

        public UnderAttack.AttackTarget AttackTarget { get; internal set; }

        public DateTimeOffset AttackTargetChange { get; internal set; }

        public bool InCockpit => (Status.HasFlag(Flags.InMainShip) || Status.HasFlag(Flags.InFighter)) && AtHelm;

        public bool InSrv => Status.HasFlag(Flags.InSrv) && AtHelm;

        public bool AtHelm
        {
            get
            {
                if ((Status.Flags & (Flags.InMainShip | Flags.InFighter | Flags.InSrv)) == Flags.None)
                {
                    return false;
                }

                switch (Status.GuiFocus ?? default)
                {
                    case GuiFocus.None:
                    case GuiFocus.InternalPanel:
                    case GuiFocus.ExternalPanel:
                    case GuiFocus.CommsPanel:
                    case GuiFocus.RolePanel:
                    case GuiFocus.StationServices:
                        return !InGalacticPowers && !InSquadronsView;
                    case GuiFocus.GalaxyMap:
                    case GuiFocus.SystemMap:
                    case GuiFocus.Orrery:
                    case GuiFocus.FssMode:
                    case GuiFocus.SaaMode:
                    case GuiFocus.Codex:
                    default:
                        return false;
                }
            }
        }

        public bool InWitchSpace =>
            FsdJumpType == StartJump.FsdJumpType.Hyperspace
            && (Now - FsdJumpChange) >= JumpCountdownDelay;

        public bool DockedOrLanded => (Status.Flags & (Flags.Docked | Flags.Landed)) != Flags.None;

        public GameState Copy()
        {
            return (GameState)MemberwiseClone();
        }
    }
}
