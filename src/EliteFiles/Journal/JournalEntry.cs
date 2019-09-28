using System;
using System.Collections.Generic;
using EliteFiles.Journal.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EliteFiles.Journal
{
    /// <summary>
    /// Represents an Elite:Dangerous player journal entry.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="http://hosting.zaonce.net/community/journal/v26/Journal-Manual-v26.pdf">Elite:Dangerous Player Journal</a>.
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
        /// Gets the timestamp of the event.
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; private set; }

        /// <summary>
        /// Gets a collection of additional fields that may be included in the event.
        /// </summary>
        public IReadOnlyDictionary<string, JToken> AdditionalFields => _additionalFields;
    }
}
