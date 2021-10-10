using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using ChromaWrapper;
using Newtonsoft.Json;

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

        public override ChromaColor ReadJson(JsonReader reader, Type objectType, [AllowNull] ChromaColor existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return existingValue;
            }

            if (!TryParseRgbString((string?)reader.Value, out ChromaColor color))
            {
                return existingValue;
            }

            return color;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] ChromaColor value, JsonSerializer serializer)
        {
            writer.WriteValue(ToRgbString(value));
        }
    }
}
