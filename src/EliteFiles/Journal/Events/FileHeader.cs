using Newtonsoft.Json;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the start of a new journal file.
    /// </summary>
    [JournalEntry("Fileheader")]
    public sealed class FileHeader : JournalEntry
    {
        private FileHeader()
        {
        }

        /// <summary>
        /// Gets the journal file part number.
        /// </summary>
        [JsonProperty("part")]
        public int Part { get; private set; }

        /// <summary>
        /// Gets the journal file language code.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current journal is an Odyssey one.
        /// </summary>
        [JsonProperty("Odyssey")]
        public bool Odyssey { get; private set; }

        /// <summary>
        /// Gets the version of the game that created the journal file.
        /// </summary>
        [JsonProperty("gameversion")]
        public string GameVersion { get; private set; }

        /// <summary>
        /// Gets the game build number.
        /// </summary>
        [JsonProperty("build")]
        public string Build { get; private set; }
    }
}
