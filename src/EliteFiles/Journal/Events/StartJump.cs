using System.Text.Json.Serialization;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the start of a hyperspace or supercruise FSD jump.
    /// </summary>
    [JournalEntry("StartJump")]
    public sealed class StartJump : JournalEntry
    {
        /// <summary>
        /// Specifies the kind of FSD jump.
        /// </summary>
        public enum FsdJumpType
        {
            /// <summary>Indicates that no FSD jump is in progress.</summary>
            None = 0,

            /// <summary>Indicates a supercruise FSD jump.</summary>
            Supercruise,

            /// <summary>Indicates a hyperspace FSD jump.</summary>
            Hyperspace,
        }

        /// <summary>
        /// Gets or sets the FSD jump type.
        /// </summary>
        [JsonPropertyName("JumpType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FsdJumpType JumpType { get; set; }

        /// <summary>
        /// Gets or sets the name of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonPropertyName("StarSystem")]
        public string? StarSystem { get; set; }

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
    }
}
