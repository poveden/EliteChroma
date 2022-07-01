using System.Text.Json.Serialization;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the selection of a hyperspace FSD target.
    /// </summary>
    [JournalEntry("FSDTarget")]
    public sealed class FsdTarget : JournalEntry
    {
        /// <summary>
        /// Gets or sets the name of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonPropertyName("SystemAddress")]
        public long SystemAddress { get; set; }

        /// <summary>
        /// Gets or sets the main star class of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonPropertyName("StarClass")]
        public string? StarClass { get; set; }

        /// <summary>
        /// Gets or sets the number of remaining jumps in the current route.
        /// </summary>
        [JsonPropertyName("RemainingJumpsInRoute")]
        public int? RemainingJumpsInRoute { get; set; }
    }
}
