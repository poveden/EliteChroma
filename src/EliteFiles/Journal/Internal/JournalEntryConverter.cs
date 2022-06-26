using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using EliteFiles.Internal;

namespace EliteFiles.Journal.Internal
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Used in JsonConverterAttribute for JournalEntry.")]
    internal sealed class JournalEntryConverter : JsonConverter<JournalEntry>
    {
        private static readonly Dictionary<string, Type> _eventMap = BuildJournayEntryEventMap();
        private static readonly EliteFilesSerializerContext _serializerContext = new EliteFilesSerializerContext();

        [ExcludeFromCodeCoverage]
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(JournalEntry).IsAssignableFrom(typeToConvert);
        }

        public override JournalEntry? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var item = JsonDocument.ParseValue(ref reader);

            string? eventName = item.RootElement.TryGetProperty("event", out JsonElement pEvent) ? pEvent.GetString() : null;

            if (string.IsNullOrEmpty(eventName) || !_eventMap.TryGetValue(eventName, out Type? type))
            {
                type = typeof(JournalEntry);
            }

            return (JournalEntry?)item.Deserialize(type, _serializerContext);
        }

        [ExcludeFromCodeCoverage]
        public override void Write(Utf8JsonWriter writer, JournalEntry value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private static Dictionary<string, Type> BuildJournayEntryEventMap()
        {
            IEnumerable<(string EventName, Type Type)> journalEventTypes =
                from type in typeof(JournalEntry).Assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(JournalEntry))
                from JournalEntryAttribute attr in type.GetCustomAttributes(typeof(JournalEntryAttribute), false)
                select (attr.EventName, type);

            return journalEventTypes.ToDictionary(x => x.EventName, x => x.Type, StringComparer.Ordinal);
        }
    }
}
