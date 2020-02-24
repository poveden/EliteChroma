using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Colore.Data;
using EliteChroma.Chroma;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class ColorExtensionsTest
    {
        [Fact]
        public void TransformThrowsOnNullTransformArgument()
        {
            Assert.Throws<ArgumentNullException>("transform", () => ColorExtensions.Transform(Color.Blue, null));
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, 255)]
        [InlineData(2, 255)]
        public void EnsureBoundKeepValuesWithinRange(double input, byte expected)
        {
            var c = ColorExtensions.Combine(Color.Black, Color.White, input);
            Assert.Equal(expected, c.R);
        }
    }
}
