using System.Text.Json.Serialization;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording a change on the game music 'mood'.
    /// </summary>
    [JournalEntry("Music")]
    public sealed class Music : JournalEntry
    {
        /// <summary>No music track is being played.</summary>
        public const string NoTrack = "NoTrack";

        /// <summary>Plays while on the game's main menu.</summary>
        public const string MainMenu = "MainMenu";

        /// <summary>Plays while on the game's CQC menu.</summary>
        public const string CqcMenu = "CQCMenu";

        /// <summary>Plays while on the system map.</summary>
        public const string SystemMap = "SystemMap";

        /// <summary>Plays while on the galaxy map.</summary>
        public const string GalaxyMap = "GalaxyMap";

        /// <summary>Plays while on the galaxy powers screen.</summary>
        public const string GalacticPowers = "GalacticPowers";

        /// <summary>Plays while playing a CQC session.</summary>
        public const string Cqc = "CQC";

        /// <summary>Plays when entering from hyperspace into supercruise upon reaching the final route destination.</summary>
        public const string DestinationFromHyperspace = "DestinationFromHyperspace";

        /// <summary>Plays when entering from supercruise into realspace upon reaching the final route destination.</summary>
        public const string DestinationFromSupercruise = "DestinationFromSupercruise";

        /// <summary>Plays while the ship is on supercruise.</summary>
        public const string Supercruise = "Supercruise";

        /// <summary>Plays while fighting thargoids.</summary>
        public const string CombatUnknown = "Combat_Unknown";

        /// <summary>Plays while encountering thargoids in a USS.</summary>
        public const string UnknownEncounter = "Unknown_Encounter";

        /// <summary>Plays while dogfighting.</summary>
        public const string CombatDogfight = "Combat_Dogfight";

        /// <summary>Plays while doing combat in a SRV.</summary>
        public const string CombatSrv = "Combat_SRV";

        /// <summary>Plays while exploring a thargoid settlement.</summary>
        public const string UnknownSettlement = "Unknown_Settlement";

        /// <summary>Plays while the docking computer is on.</summary>
        public const string DockingComputer = "DockingComputer";

        /// <summary>Plays while docked on a starport.</summary>
        public const string Starport = "Starport";

        /// <summary>Plays while encountering thargoids in a USS.</summary>
        public const string UnknownExploration = "Unknown_Exploration";

        /// <summary>Plays while exploring in realspace.</summary>
        public const string Exploration = "Exploration";

        /// <summary>Plays while on the FSS screen.</summary>
        public const string SystemAndSurfaceScanner = "SystemAndSurfaceScanner";

        /// <summary>Plays while on foot.</summary>
        public const string OnFoot = "OnFoot";

        /// <summary>Plays while on the fleet carrier management screen.</summary>
        public const string FleetCarrierManagment = "FleetCarrier_Managment";

        /// <summary>Plays while on the codex screen.</summary>
        public const string Codex = "Codex";

        /// <summary>Plays when entering realspace after being interdicted.</summary>
        public const string Interdiction = "Interdiction";

        /// <summary>Plays while dogfighting.</summary>
        public const string CombatLargeDogFight = "Combat_LargeDogFight";

        /// <summary>Plays while exploring a guardian site.</summary>
        public const string GuardianSites = "GuardianSites";

        /// <summary>Plays while on the squadrons screen.</summary>
        public const string Squadrons = "Squadrons";

        /// <summary>Plays while near of, or docked on, a fleet carrier.</summary>
        public const string NoInGameMusic = "NoInGameMusic";

        /// <summary>Plays while exploring a Lagrange cloud.</summary>
        public const string LifeformFogCloud = "Lifeform_FogCloud";

        /// <summary>Plays while near of, or docked on, a damaged starport.</summary>
        public const string DamagedStarport = "Damaged_Starport";

        /// <summary>Plays when a capital ship enters a combat zone.</summary>
        public const string CombatCapitalShip = "Combat_CapitalShip";

        /// <summary>Plays while encountering a thargoid titan.</summary>
        public const string TitanEncounter = "Titan_Encounter";

        /// <summary>
        /// Gets or sets the name of the music track.
        /// </summary>
        [JsonPropertyName("MusicTrack")]
        public string? MusicTrack { get; set; }
    }
}
