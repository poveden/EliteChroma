using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EliteFiles.Journal
{
    /// <summary>
    /// Represents an Elite:Dangerous player journal entry.
    /// </summary>
    /// <remarks>
    /// Reference: <a href="https://hosting.zaonce.net/community/journal/v31/Journal_Manual_v31.pdf">Elite:Dangerous Player Journal</a>.
    /// </remarks>
    public class JournalEntry
    {
        private readonly Dictionary<string, JsonElement> _additionalFields = new Dictionary<string, JsonElement>(StringComparer.Ordinal);

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalEntry"/> class.
        /// </summary>
        public JournalEntry()
        {
        }

        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the type of event.
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }

        /// <summary>
        /// Gets a collection of additional fields that may be included in the event.
        /// </summary>
        [JsonExtensionData]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779")]
        public IDictionary<string, JsonElement> AdditionalFields
        {
            get => _additionalFields;

            // Reference: https://github.com/dotnet/runtime/issues/30258#issuecomment-604732779
            [ExcludeFromCodeCoverage]
            set => throw new NotSupportedException();
        }
    }
}
