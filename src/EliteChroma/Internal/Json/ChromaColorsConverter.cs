using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using EliteChroma.Core;

namespace EliteChroma.Internal.Json
{
    internal sealed class ChromaColorsConverter : JsonConverter<ChromaColors>
    {
        private static readonly IReadOnlyDictionary<string, PropertyInfo> _map = BuildPropertyMap();

        public override ChromaColors? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var res = new ChromaColors();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return res;
                }

                string propertyName = reader.GetString()!;
                _ = reader.Read();

                if (!_map.TryGetValue(propertyName, out PropertyInfo? pi))
                {
                    reader.Skip();
                    continue;
                }

                try
                {
                    object? v = JsonSerializer.Deserialize(ref reader, pi.PropertyType, options);
                    pi.SetValue(res, v);
                }
                catch (JsonException)
                {
                    // Keep existing value.
                }
            }

            throw new JsonException();
        }

        [ExcludeFromCodeCoverage]
        public override void Write(Utf8JsonWriter writer, ChromaColors value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private static IReadOnlyDictionary<string, PropertyInfo> BuildPropertyMap()
        {
            return typeof(ChromaColors).GetProperties().Where(x => x.CanWrite).ToDictionary(x => x.Name, StringComparer.Ordinal);
        }
    }
}
