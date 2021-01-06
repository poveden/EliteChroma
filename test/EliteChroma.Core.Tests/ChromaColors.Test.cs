using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Colore.Data;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class ChromaColorsTest
    {
        [Theory]
        [InlineData(nameof(ChromaColors.KeyboardDimBrightness))]
        [InlineData(nameof(ChromaColors.DeviceDimBrightness))]
        [InlineData(nameof(ChromaColors.SecondaryBindingBrightness))]
        public void BrightnessValuesAreBoundedBetween0And1(string propertyName)
        {
            var cc = new ChromaColors();

            var pi = typeof(ChromaColors).GetProperty(propertyName);

            pi.SetValue(cc, 0.23);
            Assert.Equal(0.23, (double)pi.GetValue(cc));

            pi.SetValue(cc, double.NaN);
            Assert.Equal(0.23, (double)pi.GetValue(cc));

            pi.SetValue(cc, -8.23);
            Assert.Equal(0.0, (double)pi.GetValue(cc));

            pi.SetValue(cc, 2.11);
            Assert.Equal(1.0, (double)pi.GetValue(cc));

            pi.SetValue(cc, double.NegativeInfinity);
            Assert.Equal(0.0, (double)pi.GetValue(cc));

            pi.SetValue(cc, double.PositiveInfinity);
            Assert.Equal(1.0, (double)pi.GetValue(cc));
        }

        [Fact]
        public void PipsAreRecalculatedOnColorChanges()
        {
            var cc = new ChromaColors();

            Assert.Equal(Color.Red, cc.PowerDistributor0);
            Assert.Equal(Color.Yellow, cc.PowerDistributor100);

            Assert.Equal(Color.Red, cc.PowerDistributorScale[0]);
            Assert.Equal(Color.FromRgb(0xFF7F00), cc.PowerDistributorScale[4]);
            Assert.Equal(Color.Yellow, cc.PowerDistributorScale[8]);

            cc.PowerDistributor0 = Color.Blue;

            Assert.Equal(Color.Blue, cc.PowerDistributorScale[0]);
            Assert.Equal(Color.FromRgb(0x7F7F7F), cc.PowerDistributorScale[4]);
            Assert.Equal(Color.Yellow, cc.PowerDistributorScale[8]);

            cc.PowerDistributor100 = Color.Green;

            Assert.Equal(Color.Blue, cc.PowerDistributorScale[0]);
            Assert.Equal(Color.FromRgb(0x007F7F), cc.PowerDistributorScale[4]);
            Assert.Equal(Color.Green, cc.PowerDistributorScale[8]);
        }

        [Fact]
        public void AllColorFieldsAreReadWriteable()
        {
            var cc = new ChromaColors();
            var c = Color.Yellow;

            var colorProps = typeof(ChromaColors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Color));

            foreach (var pi in colorProps)
            {
                pi.SetValue(cc, c);
                var c2 = (Color)pi.GetValue(cc);
                Assert.Equal(c, c2);
            }
        }
    }
}
