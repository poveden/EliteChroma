using System.Text.Json.Serialization;

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
        [JsonPropertyName("FID")]
        public string? FID { get; set; }

        /// <summary>
        /// Gets or sets the commander name.
        /// </summary>
        [JsonPropertyName("Commander")]
        public string? Commander { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Horizons game features are in place.
        /// </summary>
        [JsonPropertyName("Horizons")]
        public bool Horizons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Odyssey game features are in place.
        /// </summary>
        [JsonPropertyName("Odyssey")]
        public bool Odyssey { get; set; }

        /// <summary>
        /// Gets or sets the ship's type.
        /// </summary>
        [JsonPropertyName("Ship")]
        public string? Ship { get; set; }

        /// <summary>
        /// Gets or sets the ship's localized type.
        /// </summary>
        [JsonPropertyName("Ship_Localised")]
        public string? ShipLocalized { get; set; }

        /// <summary>
        /// Gets or sets the ship's ID number.
        /// </summary>
        [JsonPropertyName("ShipID")]
        public long ShipID { get; set; }

        /// <summary>
        /// Gets or sets the ship's user-defined name.
        /// </summary>
        [JsonPropertyName("ShipName")]
        public string? ShipName { get; set; }

        /// <summary>
        /// Gets or sets the ship's user-defined ID.
        /// </summary>
        [JsonPropertyName("ShipIdent")]
        public string? ShipIdent { get; set; }

        /// <summary>
        /// Gets or sets the ship's fuel level.
        /// </summary>
        [JsonPropertyName("FuelLevel")]
        public double FuelLevel { get; set; }

        /// <summary>
        /// Gets or sets the ship's size of the main tank.
        /// </summary>
        [JsonPropertyName("FuelCapacity")]
        public double FuelCapacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ship is starting landed.
        /// </summary>
        [JsonPropertyName("StartLanded")]
        public bool StartLanded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is starting dead.
        /// </summary>
        [JsonPropertyName("StartDead")]
        public bool StartDead { get; set; }

        /// <summary>
        /// Gets or sets the game mode.
        /// </summary>
        [JsonPropertyName("GameMode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PlayMode GameMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the group if <see cref="GameMode"/> is <see cref="PlayMode.Group"/>.
        /// </summary>
        [JsonPropertyName("Group")]
        public string? Group { get; set; }

        /// <summary>
        /// Gets or sets the current credit balance.
        /// </summary>
        [JsonPropertyName("Credits")]
        public long Credits { get; set; }

        /// <summary>
        /// Gets or sets the current loan balance.
        /// </summary>
        [JsonPropertyName("Loan")]
        public long Loan { get; set; }

        /// <summary>
        /// Gets or sets the game's language code.
        /// </summary>
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the game version number.
        /// </summary>
        [JsonPropertyName("gameversion")]
        public string? GameVersion { get; set; }

        /// <summary>
        /// Gets or sets the game build number.
        /// </summary>
        [JsonPropertyName("build")]
        public string? Build { get; set; }
    }
}
