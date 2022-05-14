using System.Diagnostics.CodeAnalysis;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents the various game status flags.
    /// </summary>
    [SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Respect Elite:Dangerous naming convention.")]
    [SuppressMessage("Minor Code Smell", "S2344:Enumeration type names should not have \"Flags\" or \"Enum\" suffixes", Justification = "Respect Elite:Dangerous naming convention.")]
    [Flags]
    public enum Flags : long
    {
        /// <summary>No flags are set (e.g. when the game is not running).</summary>
        None = 0,

        /// <summary>Docked (i.e. on a landing pad).</summary>
        Docked = 1,

        /// <summary>Landed (i.e. on planet's surface).</summary>
        Landed = 1 << 1,

        /// <summary>Landing gear deployed.</summary>
        LandingGearDeployed = 1 << 2,

        /// <summary>Shields up.</summary>
        ShieldsUp = 1 << 3,

        /// <summary>In supercruise.</summary>
        Supercruise = 1 << 4,

        /// <summary>Flight assist off.</summary>
        FlightAssistOff = 1 << 5,

        /// <summary>Hardpoints deployed.</summary>
        HardpointsDeployed = 1 << 6,

        /// <summary>In wing.</summary>
        InWing = 1 << 7,

        /// <summary>Lights on.</summary>
        LightsOn = 1 << 8,

        /// <summary>Cargo scoop deployed.</summary>
        CargoScoopDeployed = 1 << 9,

        /// <summary>Silent running.</summary>
        SilentRunning = 1 << 10,

        /// <summary>Scooping fuel.</summary>
        ScoopingFuel = 1 << 11,

        /// <summary>SRV handbrake.</summary>
        SrvHandbrake = 1 << 12,

        /// <summary>SRV using turret view.</summary>
        SrvUsingTurretView = 1 << 13,

        /// <summary>SRV turret retracted (i.e. close to ship).</summary>
        SrvTurretRetracted = 1 << 14,

        /// <summary>SRV drive assist.</summary>
        SrvDriveAssist = 1 << 15,

        /// <summary>FSD mass-locked.</summary>
        FsdMassLocked = 1 << 16,

        /// <summary>FSD charging.</summary>
        FsdCharging = 1 << 17,

        /// <summary>FSD cooldown.</summary>
        FsdCooldown = 1 << 18,

        /// <summary>Low fuel (under 25%).</summary>
        LowFuel = 1 << 19,

        /// <summary>Overheating (above 100%).</summary>
        Overheating = 1 << 20,

        /// <summary>Has latitude/longitude.</summary>
        HasLatLong = 1 << 21,

        /// <summary>Is in danger.</summary>
        IsInDanger = 1 << 22,

        /// <summary>Being interdicted.</summary>
        BeingInterdicted = 1 << 23,

        /// <summary>In main ship.</summary>
        InMainShip = 1 << 24,

        /// <summary>In ship-launched fighter.</summary>
        InFighter = 1 << 25,

        /// <summary>In SRV.</summary>
        InSrv = 1 << 26,

        /// <summary>HUD in analysis mode.</summary>
        HudInAnalysisMode = 1 << 27,

        /// <summary>Night vision.</summary>
        NightVision = 1 << 28,

        /// <summary>Altitude from average radius.</summary>
        AltitudeFromAvgRadius = 1 << 29,

        /// <summary>FSD jump.</summary>
        FsdJump = 1 << 30,

        /// <summary>SRV high beam.</summary>
        SrvHighBeam = 1L << 31,
    }
}
