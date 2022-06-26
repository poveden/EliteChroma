using System.Text.Json;
using EliteChroma.Core;
using EliteChroma.Internal.Json;
using Xunit;

namespace EliteChroma.Tests
{
    public class ChromaColorsConverterTests
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            Converters =
            {
                new ChromaColorsConverter(),
            },
        };

        [Theory]
        [InlineData("[]")]
        [InlineData("123")]
        [InlineData("\"ABC\"")]
        public void ThrowsWhenTryingToDeserializeNonObjects(string json)
        {
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ChromaColors>(json, _options));
        }
    }
}
