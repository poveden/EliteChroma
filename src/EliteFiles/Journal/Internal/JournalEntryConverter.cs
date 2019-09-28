using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EliteFiles.Journal.Internal
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Used in JsonConverterAttribute for JournalEntry.")]
    internal sealed class JournalEntryConverter : JsonConverter
    {
        private static readonly JsonSerializer _journalEntrySerializer =
            new JsonSerializer { ContractResolver = new JournalEntryContractResolver() };

        private static readonly Dictionary<string, Type> _eventMap = BuildJournayEntryEventMap();

        [ExcludeFromCodeCoverage]
        public override bool CanConvert(Type objectType)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);

            var eventName = item["event"].Value<string>();

            if (!_eventMap.TryGetValue(eventName, out var type))
            {
                type = typeof(JournalEntry);
            }

            return item.ToObject(type, _journalEntrySerializer);
        }

        [ExcludeFromCodeCoverage]
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        private static Dictionary<string, Type> BuildJournayEntryEventMap()
        {
            var journalEventTypes =
                from type in typeof(JournalEntry).Assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(JournalEntry))
                from JournalEntryAttribute attr in type.GetCustomAttributes(typeof(JournalEntryAttribute), false)
                select new
                {
                    attr.EventName,
                    Type = type,
                };

            return journalEventTypes.ToDictionary(x => x.EventName, x => x.Type, StringComparer.Ordinal);
        }

        // Reference: https://stackoverflow.com/questions/20995865/deserializing-json-to-abstract-class/30579193#30579193
        private sealed class JournalEntryContractResolver : DefaultContractResolver
        {
            protected override JsonConverter ResolveContractConverter(Type objectType)
            {
                if (typeof(JournalEntry).IsAssignableFrom(objectType))
                {
                    return null;
                }

                return base.ResolveContractConverter(objectType);
            }
        }
    }
}
