using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EliteFiles.Status.Internal
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Used in JsonConverterAttribute for PowerDistributor.")]
    internal sealed class PowerDistributorConverter : JsonConverter<PowerDistributor>
    {
        public override PowerDistributor? ReadJson(JsonReader reader, Type objectType, PowerDistributor? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            byte sys = (byte)reader.ReadAsInt32()!;
            byte eng = (byte)reader.ReadAsInt32()!;
            byte wep = (byte)reader.ReadAsInt32()!;

            _ = reader.Read(); // JsonToken.EndArray

            return new PowerDistributor(sys, eng, wep);
        }

        [ExcludeFromCodeCoverage]
        public override void WriteJson(JsonWriter writer, PowerDistributor? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
