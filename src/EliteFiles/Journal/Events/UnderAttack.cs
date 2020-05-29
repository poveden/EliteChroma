using Newtonsoft.Json;

namespace EliteFiles.Journal.Events
{
    /// <summary>
    /// Represents a journal entry recording that the player is under attack.
    /// </summary>
    [JournalEntry("UnderAttack")]
    public sealed class UnderAttack : JournalEntry
    {
        private UnderAttack()
        {
        }

        /// <summary>
        /// Specifies the possible attack targets.
        /// </summary>
        public enum AttackTarget
        {
            /// <summary>Indicates that the player is not under attack.</summary>
            None = 0,

            /// <summary>Indicates that the player is under attack.</summary>
            You,

            /// <summary>Indicates that the player's SLF is under attack.</summary>
            Fighter,

            /// <summary>Indicates that the player' main ship is under attack.</summary>
            Mothership,
        }

        /// <summary>
        /// Gets the target of the attack.
        /// </summary>
        [JsonProperty("Target")]
        public AttackTarget Target { get; private set; }
    }
}
