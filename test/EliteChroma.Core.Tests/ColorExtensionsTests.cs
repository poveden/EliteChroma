using System.Diagnostics.CodeAnalysis;
using ChromaWrapper;
using EliteChroma.Core.Chroma;
using EliteFiles.Graphics;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional", Justification = "Matrix operations")]
    public class ColorExtensionsTests
    {
        [Fact]
        public void TransformThrowsOnNullTransformArgument()
        {
            Assert.Throws<ArgumentNullException>("transform", () => ColorExtensions.Transform(ChromaColor.Blue, null!));
        }

        [Fact]
        public void TransformCanApplyMatrixTransforms()
        {
            var c = ChromaColor.FromRgb(255, 127, 0);

            var matrix = new Matrix(new double[,]
            {
                { 0.1, 0.4, 0.6 },
                { 0.2, 0.5, 0.7 },
                { 0.8, 0.8, 0.3 },
            });

            var expected = ChromaColor.FromRgb(50, 165, 241); // 50.9, 165.5, 241.9

            var res = c.Transform(matrix);
            Assert.Equal(expected, res);
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

        private sealed class Matrix : IRgbTransformMatrix
        {
            private readonly double[,] _matrix;

            public Matrix(double[,] matrix)
            {
                _matrix = matrix;
            }

            public double this[int row, int col] => _matrix[row, col];
        }
    }
}
