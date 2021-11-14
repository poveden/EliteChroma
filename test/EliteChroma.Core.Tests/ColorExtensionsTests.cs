using System;
using ChromaWrapper;
using EliteChroma.Chroma;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ColorExtensionsTests
    {
        [Fact]
        public void TransformThrowsOnNullTransformArgument()
        {
            Assert.Throws<ArgumentNullException>("transform", () => ColorExtensions.Transform(ChromaColor.Blue, null!));
        }

        [Theory]
        [InlineData(1.0, 0x808080)]
        [InlineData(2.0, 0x404040)]
        [InlineData(0.5, 0xB4B4B4)]
        public void TransformCanApplyGammaCorrection(double gamma, int expectedRgb)
        {
            var c = ChromaColor.FromRgb(0x808080);

            var ct = c.Transform(1.0, gamma);

            Assert.Equal(expectedRgb, ct.ToRgb());
        }
    }
}
