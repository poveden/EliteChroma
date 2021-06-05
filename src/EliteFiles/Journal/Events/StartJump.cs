using Newtonsoft.Json;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording the start of a hyperspace or supercruise FSD jump.
    /// </summary>
    [JournalEntry("StartJump")]
    public sealed class StartJump : JournalEntry
    {
        private StartJump()
        {
        }

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
        /// Gets the FSD jump type.
        /// </summary>
        [JsonProperty("JumpType")]
        public FsdJumpType JumpType { get; private set; }

        /// <summary>
        /// Gets the name of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonProperty("StarSystem")]
        public string? StarSystem { get; private set; }

        /// <summary>
        /// Gets the ID of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonProperty("SystemAddress")]
        public long SystemAddress { get; private set; }

        /// <summary>
        /// Gets the main star class of the destination system on hyperspace FSD jumps.
        /// </summary>
        [JsonProperty("StarClass")]
        public string? StarClass { get; private set; }
    }
}
