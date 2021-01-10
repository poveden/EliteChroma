using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colore.Data;
using Newtonsoft.Json;

namespace EliteChroma.Internal.Json
{
    internal sealed class JsonColoreColorConverter : JsonConverter<Color>
    {
        public override Color ReadJson(JsonReader reader, Type objectType, [AllowNull] Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return existingValue;
            }

            if (!TryParseRgbString((string)reader.Value, out var color))
            {
                return existingValue;
            }

            return color;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] Color value, JsonSerializer serializer)
        {
            writer.WriteValue(ToRgbString(value));
        }

        public static bool TryParseRgbString(string str, out Color color)
        {
            if (!uint.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var rgb))
            {
                color = default;
                return false;
            }

            color = Color.FromRgb(rgb);
            return true;
        }

        public static string ToRgbString(Color color)
        {
            var rgb = (color.R << 16) | (color.G << 8) | color.B;
            return rgb.ToString("X6", CultureInfo.InvariantCulture);
        }
    }
}
