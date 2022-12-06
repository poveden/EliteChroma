using System.Text.Json.Serialization;
using EliteFiles.Internal;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording a change on the game music 'mood'.
    /// </summary>
    [JournalEntry("Music")]
    public sealed class Music : JournalEntry
    {
        /// <summary>
        /// Specifies the music track.
        /// </summary>
        public enum Track
        {
            /// <summary>No music track is being played.</summary>
            NoTrack = 0,

            /// <summary>Plays while on the game's main menu.</summary>
            MainMenu,

            /// <summary>Plays while on the game's CQC menu.</summary>
            [JsonStringEnumName("CQCMenu")]
            CqcMenu,

            /// <summary>Plays while on the system map.</summary>
            SystemMap,

            /// <summary>Plays while on the galaxy map.</summary>
            GalaxyMap,

            /// <summary>Plays while on the galaxy powers screen.</summary>
            GalacticPowers,

            /// <summary>Plays while playing a CQC session.</summary>
            [JsonStringEnumName("CQC")]
            Cqc,

            /// <summary>Plays when entering from hyperspace into supercruise upon reaching the final route destination.</summary>
            DestinationFromHyperspace,

            /// <summary>Plays when entering from supercruise into realspace upon reaching the final route destination.</summary>
            DestinationFromSupercruise,

            /// <summary>Plays while the ship is on supercruise.</summary>
            Supercruise,

            /// <summary>Plays while fighting thargoids.</summary>
            [JsonStringEnumName("Combat_Unknown")]
            CombatUnknown,

            /// <summary>Plays while encountering thargoids in a USS.</summary>
            [JsonStringEnumName("Unknown_Encounter")]
            UnknownEncounter,

            /// <summary>Plays while dogfighting.</summary>
            [JsonStringEnumName("Combat_Dogfight")]
            CombatDogfight,

            /// <summary>Plays while doing combat in a SRV.</summary>
            [JsonStringEnumName("Combat_SRV")]
            CombatSrv,

            /// <summary>Plays while exploring a thargoid settlement.</summary>
            [JsonStringEnumName("Unknown_Settlement")]
            UnknownSettlement,

            /// <summary>Plays while the docking computer is on.</summary>
            DockingComputer,

            /// <summary>Plays while docked on a starport.</summary>
            Starport,

            /// <summary>Plays while encountering thargoids in a USS.</summary>
            [JsonStringEnumName("Unknown_Exploration")]
            UnknownExploration,

            /// <summary>Plays while exploring in realspace.</summary>
            Exploration,

            /// <summary>Plays while on the FSS screen.</summary>
            SystemAndSurfaceScanner,

            /// <summary>Plays while on foot.</summary>
            OnFoot,

            /// <summary>Plays while on the fleet carrier management screen.</summary>
            [JsonStringEnumName("FleetCarrier_Managment")]
            FleetCarrierManagment,

            /// <summary>Plays while on the codex screen.</summary>
            Codex,

            /// <summary>Plays when entering realspace after being interdicted.</summary>
            Interdiction,

            /// <summary>Plays while dogfighting.</summary>
            [JsonStringEnumName("Combat_LargeDogFight")]
            CombatLargeDogFight,

            /// <summary>Plays while exploring a guardian site.</summary>
            GuardianSites,

            /// <summary>Plays while on the squadrons screen.</summary>
            Squadrons,

            /// <summary>Plays while near of, or docked on, a fleet carrier.</summary>
            NoInGameMusic,

            /// <summary>Plays while exploring a Lagrange cloud.</summary>
            [JsonStringEnumName("Lifeform_FogCloud")]
            LifeformFogCloud,

            /// <summary>Plays while near of, or docked on, a damaged starport.</summary>
            [JsonStringEnumName("Damaged_Starport")]
            DamagedStarport,

            /// <summary>Plays when a capital ship enters a combat zone.</summary>
            [JsonStringEnumName("Combat_CapitalShip")]
            CombatCapitalShip,
        }

        /// <summary>
        /// Gets or sets the name of the music track.
        /// </summary>
        [JsonPropertyName("MusicTrack")]
        [JsonConverter(typeof(JsonStringEnumNameConverter<Track>))]
        public Track MusicTrack { get; set; }
    }
}
