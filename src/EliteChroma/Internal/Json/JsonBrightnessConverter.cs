using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;

namespace EliteChroma.Internal.Json
{
    internal sealed class JsonBrightnessConverter : JsonConverter<double>
    {
        public override double ReadJson(JsonReader reader, Type objectType, [AllowNull] double existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                return existingValue;
            }

            var num = Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture);
            return num / 100;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] double value, JsonSerializer serializer)
        {
            writer.WriteValue((int)(value * 100));
        }
    }
}
