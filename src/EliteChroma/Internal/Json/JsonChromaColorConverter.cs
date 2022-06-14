using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChromaWrapper;

namespace EliteChroma.Internal.Json
{
    internal sealed class JsonChromaColorConverter : JsonConverter<ChromaColor>
    {
        public static bool TryParseRgbString(string? str, out ChromaColor color)
        {
            if (!int.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int rgb))
            {
                color = default;
                return false;
            }

            color = ChromaColor.FromRgb(rgb);
            return true;
        }

        public static string ToRgbString(ChromaColor color)
        {
            int rgb = (color.R << 16) | (color.G << 8) | color.B;
            return rgb.ToString("X6", CultureInfo.InvariantCulture);
        }

        public override ChromaColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            if (!TryParseRgbString(reader.GetString(), out ChromaColor color))
            {
                throw new JsonException();
            }

            return color;
        }

        public override void Write(Utf8JsonWriter writer, ChromaColor value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(ToRgbString(value));
        }
    }
}
