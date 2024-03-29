﻿using EliteFiles.Bindings;
using EliteFiles.Graphics;
using EliteFiles.Journal.Events;
using EliteFiles.Status;

namespace EliteChroma.Core.Elite
{
    public sealed class GameState
    {
        public static readonly TimeSpan JumpCountdownDelay = TimeSpan.FromSeconds(5);

        private GuiColourMatrix _guiColour;
        private EdhmConfig? _edhmConfig;

        internal GameState()
        {
            Status = new StatusEntry();
            _guiColour = GuiColourMatrix.Default;
            Colors = new GameColors(_guiColour);
        }

        public DateTimeOffset Now { get; internal set; }

        public GameProcessState ProcessState { get; internal set; }

        public GameBindings? Bindings { get; internal set; }

        public DeviceKeySet? PressedModifiers { get; internal set; }

        public bool ForceEnUSKeyboardLayout { get; internal set; }

        public StatusEntry Status { get; internal set; }

        public GuiColourMatrix GuiColour
        {
            get => _guiColour;
            internal set
            {
                _guiColour = value;
                Colors = new GameColors(_edhmConfig?.GetColourMatrix() ?? _guiColour);
            }
        }

        public EdhmConfig? EdhmConfig
        {
            get => _edhmConfig;
            internal set
            {
                _edhmConfig = value;
                Colors = new GameColors(_edhmConfig?.GetColourMatrix() ?? _guiColour);
            }
        }

        public GameColors Colors { get; private set; }

        public string? MusicTrack { get; internal set; }

        public StartJump.FsdJumpType FsdJumpType { get; internal set; }

        public string? FsdJumpStarClass { get; internal set; }

        public DateTimeOffset FsdJumpChange { get; internal set; }

        public UnderAttack.AttackTarget AttackTarget { get; internal set; }

        public DateTimeOffset AttackTargetChange { get; internal set; }

        public bool Horizons { get; internal set; }

        public bool Odyssey { get; internal set; }

        public LoadGame.PlayMode GameMode { get; internal set; }

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

                return (Status.GuiFocus ?? default) switch
                {
                    GuiFocus.None or
                    GuiFocus.InternalPanel or
                    GuiFocus.ExternalPanel or
                    GuiFocus.CommsPanel or
                    GuiFocus.RolePanel or
                    GuiFocus.StationServices
                    => MusicTrack is not Music.GalacticPowers and not Music.Squadrons,

                    GuiFocus.GalaxyMap or
                    GuiFocus.SystemMap or
                    GuiFocus.Orrery or
                    GuiFocus.FssMode or
                    GuiFocus.SaaMode or
                    GuiFocus.Codex
                    => false,

                    _ => false,
                };
            }
        }

        public bool IsWalking
        {
            get
            {
                if (!Status.HasFlag(Flags2.OnFoot))
                {
                    return false;
                }

                if (Status.GuiFocus is GuiFocus.GalaxyMap or GuiFocus.SystemMap or GuiFocus.Codex)
                {
                    return false;
                }

                if (MusicTrack is Music.GalacticPowers or Music.Squadrons or Music.FleetCarrierManagment)
                {
                    return false;
                }

                return true;
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
