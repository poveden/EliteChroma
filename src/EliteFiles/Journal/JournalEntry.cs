using EliteFiles.Journal.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteFiles.Journal
{
    /// <summary>
    /// Represents an Elite:Dangerous player journal entry.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://hosting.zaonce.net/community/journal/v31/Journal_Manual_v31.pdf">Elite:Dangerous Player Journal</a>.
    /// </remarks>
    [JsonConverter(typeof(JournalEntryConverter))]
    public class JournalEntry
    {
        [JsonExtensionData]
        private readonly Dictionary<string, JToken> _additionalFields = new Dictionary<string, JToken>(StringComparer.Ordinal);

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalEntry"/> class.
        /// </summary>
        protected JournalEntry()
        {
        }

        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the type of event.
        /// </summary>
        [JsonProperty("event")]
        public string? Event { get; set; }

        /// <summary>
        /// Gets a collection of additional fields that may be included in the event.
        /// </summary>
        public IDictionary<string, JToken> AdditionalFields => _additionalFields;
    }
}
