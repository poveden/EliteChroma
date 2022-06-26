using ChromaWrapper;
using EliteChroma.Core.Elite;
using EliteFiles.Journal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class GameColorsTests
    {
        [Theory]
        [InlineData(StarClass.O, 0xB1F0FE)]
        [InlineData(StarClass.RoguePlanet, 0x000000)]
        [InlineData("SOME_UNDOCUMENTED_STAR_CLASS", 0x000000)]
        public void GetsColorsMatchingTheStarClass(string starClass, int rgbColor)
        {
            var expectedColor = ChromaColor.FromRgb(rgbColor);

            var color = GameColors.GetStarClassColor(starClass);
            Assert.Equal(expectedColor, color);
        }
    }
}
