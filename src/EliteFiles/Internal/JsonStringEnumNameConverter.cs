using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EliteFiles.Internal
{
    internal sealed class JsonStringEnumNameConverter<T> : JsonConverter<T>
        where T : Enum
    {
        private static readonly IReadOnlyDictionary<string, T> _map = BuildMap();

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string name = reader.GetString()!;

            return _map[name];
        }

        [ExcludeFromCodeCoverage]
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private static IReadOnlyDictionary<string, T> BuildMap()
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static).ToDictionary(
                x => x.GetCustomAttribute<JsonStringEnumNameAttribute>()?.Name ?? x.Name,
                x => (T)x.GetValue(null)!);
        }
    }
}
