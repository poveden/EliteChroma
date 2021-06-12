using System;
using Colore.Data;
using EliteChroma.Chroma;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ColorExtensionsTests
    {
        [Fact]
        public void TransformThrowsOnNullTransformArgument()
        {
            Assert.Throws<ArgumentNullException>("transform", () => ColorExtensions.Transform(Color.Blue, null!));
        }
    }
}
