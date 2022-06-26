using System.Text.Json;
using System.Text.Json.Serialization;

namespace EliteChroma.Internal.Json
{
    internal sealed class JsonBrightnessConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            double num = reader.GetDouble();
            return num / 100;
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)(value * 100));
        }
    }
}
