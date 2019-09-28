using System;
using System.Diagnostics.CodeAnalysis;
using EliteFiles.Status;
using Newtonsoft.Json;

namespace EliteFiles.Journal.Internal
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Used in JsonConverterAttribute for PowerDistributor.")]
    internal sealed class PowerDistributorConverter : JsonConverter<PowerDistributor>
    {
        public override PowerDistributor ReadJson(JsonReader reader, Type objectType, PowerDistributor existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var sys = (byte)reader.ReadAsInt32();
            var eng = (byte)reader.ReadAsInt32();
            var wep = (byte)reader.ReadAsInt32();

            reader.Read(); // JsonToken.EndArray

            return new PowerDistributor(sys, eng, wep);
        }

        [ExcludeFromCodeCoverage]
        public override void WriteJson(JsonWriter writer, PowerDistributor value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
