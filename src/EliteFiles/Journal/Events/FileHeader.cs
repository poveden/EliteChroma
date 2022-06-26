using System.Text.Json.Serialization;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the start of a new journal file.
    /// </summary>
    [JournalEntry("Fileheader")]
    public sealed class FileHeader : JournalEntry
    {
        /// <summary>
        /// Gets or sets the journal file part number.
        /// </summary>
        [JsonPropertyName("part")]
        public int Part { get; set; }

        /// <summary>
        /// Gets or sets the journal file language code.
        /// </summary>
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current journal is an Odyssey one.
        /// </summary>
        [JsonPropertyName("Odyssey")]
        public bool Odyssey { get; set; }

        /// <summary>
        /// Gets or sets the version of the game that created the journal file.
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
