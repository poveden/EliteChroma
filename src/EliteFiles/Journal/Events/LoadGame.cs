using Newtonsoft.Json;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the loading into the game from the main menu.
    /// </summary>
    [JournalEntry("LoadGame")]
    public sealed class LoadGame : JournalEntry
    {
        /// <summary>
        /// Specifies the game mode.
        /// </summary>
        public enum PlayMode
        {
            /// <summary>Indicates no game mode is specified.</summary>
            None = 0,

            /// <summary>Indicates that the game is loading into solo play.</summary>
            Solo,

            /// <summary>Indicates that the game is loading into a private group session.</summary>
            Group,

            /// <summary>Indicates that the game is loading into open play.</summary>
            Open,
        }

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        [JsonProperty("FID")]
        public string? FID { get; set; }

        /// <summary>
        /// Gets or sets the commander name.
        /// </summary>
        [JsonProperty("Commander")]
        public string? Commander { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Horizons game features are in place.
        /// </summary>
        [JsonProperty("Horizons")]
        public bool Horizons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Odyssey game features are in place.
        /// </summary>
        [JsonProperty("Odyssey")]
        public bool Odyssey { get; set; }

        /// <summary>
        /// Gets or sets the ship's type.
        /// </summary>
        [JsonProperty("Ship")]
        public string? Ship { get; set; }

        /// <summary>
        /// Gets or sets the ship's localized type.
        /// </summary>
        [JsonProperty("Ship_Localised")]
        public string? ShipLocalized { get; set; }

        /// <summary>
        /// Gets or sets the ship's ID number.
        /// </summary>
        [JsonProperty("ShipID")]
        public long ShipID { get; set; }

        /// <summary>
        /// Gets or sets the ship's user-defined name.
        /// </summary>
        [JsonProperty("ShipName")]
        public string? ShipName { get; set; }

        /// <summary>
        /// Gets or sets the ship's user-defined ID.
        /// </summary>
        [JsonProperty("ShipIdent")]
        public string? ShipIdent { get; set; }

        /// <summary>
        /// Gets or sets the ship's fuel level.
        /// </summary>
        [JsonProperty("FuelLevel")]
        public double FuelLevel { get; set; }

        /// <summary>
        /// Gets or sets the ship's size of the main tank.
        /// </summary>
        [JsonProperty("FuelCapacity")]
        public double FuelCapacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ship is starting landed.
        /// </summary>
        [JsonProperty("StartLanded")]
        public bool StartLanded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is starting dead.
        /// </summary>
        [JsonProperty("StartDead")]
        public bool StartDead { get; set; }

        /// <summary>
        /// Gets or sets the game mode.
        /// </summary>
        [JsonProperty("GameMode")]
        public PlayMode GameMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the group if <see cref="GameMode"/> is <see cref="PlayMode.Group"/>.
        /// </summary>
        [JsonProperty("Group")]
        public string? Group { get; set; }

        /// <summary>
        /// Gets or sets the current credit balance.
        /// </summary>
        [JsonProperty("Credits")]
        public long Credits { get; set; }

        /// <summary>
        /// Gets or sets the current loan balance.
        /// </summary>
        [JsonProperty("Loan")]
        public long Loan { get; set; }

        /// <summary>
        /// Gets or sets the game's language code.
        /// </summary>
        [JsonProperty("language")]
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the game version number.
        /// </summary>
        [JsonProperty("gameversion")]
        public string? GameVersion { get; set; }

        /// <summary>
        /// Gets or sets the game build number.
        /// </summary>
        [JsonProperty("build")]
        public string? Build { get; set; }
    }
}
