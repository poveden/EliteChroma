using System.Linq;
using ChromaWrapper;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ChromaColorsTests
    {
        [Theory]
        [InlineData(nameof(ChromaColors.KeyboardDimBrightness))]
        [InlineData(nameof(ChromaColors.DeviceDimBrightness))]
        [InlineData(nameof(ChromaColors.SecondaryBindingBrightness))]
        public void BrightnessValuesAreBoundedBetween0And1(string propertyName)
        {
            var cc = new ChromaColors();

            var pi = typeof(ChromaColors).GetProperty(propertyName)!;

            pi.SetValue(cc, 0.23);
            Assert.Equal(0.23, (double)pi.GetValue(cc)!);

            pi.SetValue(cc, double.NaN);
            Assert.Equal(0.23, (double)pi.GetValue(cc)!);

            pi.SetValue(cc, -8.23);
            Assert.Equal(0.0, (double)pi.GetValue(cc)!);

            pi.SetValue(cc, 2.11);
            Assert.Equal(1.0, (double)pi.GetValue(cc)!);

            pi.SetValue(cc, double.NegativeInfinity);
            Assert.Equal(0.0, (double)pi.GetValue(cc)!);

            pi.SetValue(cc, double.PositiveInfinity);
            Assert.Equal(1.0, (double)pi.GetValue(cc)!);
        }

        [Fact]
        public void PipsAreRecalculatedOnColorChanges()
        {
            var cc = new ChromaColors();

            Assert.Equal(ChromaColor.Red, cc.PowerDistributor0);
            Assert.Equal(ChromaColor.Yellow, cc.PowerDistributor100);

            Assert.Equal(ChromaColor.Red, cc.PowerDistributorScale[0]);
            Assert.Equal(ChromaColor.FromRgb(0xFF7F00), cc.PowerDistributorScale[4]);
            Assert.Equal(ChromaColor.Yellow, cc.PowerDistributorScale[8]);

            cc.PowerDistributor0 = ChromaColor.Blue;

            Assert.Equal(ChromaColor.Blue, cc.PowerDistributorScale[0]);
            Assert.Equal(ChromaColor.FromRgb(0x7F7F7F), cc.PowerDistributorScale[4]);
            Assert.Equal(ChromaColor.Yellow, cc.PowerDistributorScale[8]);

            cc.PowerDistributor100 = ChromaColor.Green;

            Assert.Equal(ChromaColor.Blue, cc.PowerDistributorScale[0]);
            Assert.Equal(ChromaColor.FromRgb(0x007F7F), cc.PowerDistributorScale[4]);
            Assert.Equal(ChromaColor.Green, cc.PowerDistributorScale[8]);
        }

        [Fact]
        public void AllColorFieldsAreReadWriteable()
        {
            var cc = new ChromaColors();
            var c = ChromaColor.Yellow;

            var colorProps = typeof(ChromaColors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(ChromaColor));

            foreach (var pi in colorProps)
            {
                pi.SetValue(cc, c);
                var c2 = (ChromaColor)pi.GetValue(cc)!;
                Assert.Equal(c, c2);
            }
        }
    }
}
