using System.Diagnostics.CodeAnalysis;
using Colore.Data;
using EliteChroma.Elite;
using EliteFiles.Journal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class GameColorsTest
    {
        [Theory]
        [InlineData(StarClass.O, 0xB1F0FE)]
        [InlineData(StarClass.RoguePlanet, 0x000000)]
        public void GetsColorsMatchingTheStarClass(string starClass, int rgbColor)
        {
            var expectedColor = Color.FromRgb((uint)rgbColor);

            var color = GameColors.GetStarClassColor(starClass);
            Assert.Equal(expectedColor, color);
        }
    }
}
