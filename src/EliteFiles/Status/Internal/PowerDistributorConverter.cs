using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EliteFiles.Status.Internal
{
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Used in JsonConverterAttribute for PowerDistributor.")]
    internal sealed class PowerDistributorConverter : JsonConverter<PowerDistributor>
    {
        public override PowerDistributor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            _ = reader.Read(); // JsonTokenType.StartArray

            byte sys = reader.GetByte();
            _ = reader.Read();
            byte eng = reader.GetByte();
            _ = reader.Read();
            byte wep = reader.GetByte();
            _ = reader.Read();

            return new PowerDistributor(sys, eng, wep);
        }

        [ExcludeFromCodeCoverage]
        public override void Write(Utf8JsonWriter writer, PowerDistributor value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
