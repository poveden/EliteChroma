using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Text;
using Colore.Data;
using EliteChroma.Core;
using EliteChroma.Internal.UI;
using Xunit;

namespace EliteChroma.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class ChromaColorsTypeDescriptorTest : IDisposable
    {
        private readonly TypeDescriptionProvider _provider;

        public ChromaColorsTypeDescriptorTest()
        {
            ChromaColorsMetadata.InitTypeDescriptionProvider();
            _provider = TypeDescriptor.GetProvider(typeof(ChromaColors));
        }

        [Fact]
        public void PropertyDescriptorRetrievesLocalizationStrings()
        {
            var pd = GetPropertyDescriptor("KeyboardDimBrightness");

            Assert.Equal("Brightness Levels", pd.Category);
            Assert.Null(pd.Description);
            Assert.Equal("Keyboard dim brightness", pd.DisplayName);
        }

        [Fact]
        public void PerformsConversionsBetweenStringAndColor()
        {
            var pd = GetPropertyDescriptor("HardpointsToggle");

            var tc = pd.Converter;
            Assert.True(tc.CanConvertFrom(typeof(string)));
            Assert.False(tc.CanConvertFrom(typeof(object)));
            Assert.True(tc.CanConvertTo(typeof(string)));

            var c = (Color)tc.ConvertFromString("AABBCC");
            Assert.Equal(Color.FromRgb(0xAABBCC), c);

            var s = tc.ConvertToString(Color.FromRgb(0x445566));
            Assert.Equal("445566", s);
        }

        [Fact]
        public void ThrowsWhenTryingToConvertAnInvalidColorString()
        {
            var pd = GetPropertyDescriptor("HardpointsToggle");

            var ex = Assert.Throws<FormatException>(() => pd.Converter.ConvertFromString("NOT-A-COLOR"));
            Assert.StartsWith("Colors must be in the form 'RRGGBB',", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void PerformsConversionsBetweenBrightnessPercentStringAndDouble()
        {
            var pd = GetPropertyDescriptor("KeyboardDimBrightness");

            var tc = pd.Converter;
            Assert.True(tc.CanConvertFrom(typeof(string)));
            Assert.False(tc.CanConvertFrom(typeof(object)));
            Assert.True(tc.CanConvertTo(typeof(string)));

            var v = (double)tc.ConvertFromString("25 %");
            Assert.Equal(0.25, v);

            var s = tc.ConvertToString(0.78);
            Assert.Equal("78 %", s);
        }

        [Fact]
        public void ThrowsWhenTryingToConvertAnInvalidBrightnessPercentString()
        {
            var pd = GetPropertyDescriptor("KeyboardDimBrightness");

            var ex = Assert.Throws<FormatException>(() => pd.Converter.ConvertFromString("NOT-A-PERCENT"));
            Assert.StartsWith("Brightness must be a percentage value in the range 0-100.", ex.Message, StringComparison.Ordinal);
        }

        public void Dispose()
        {
            TypeDescriptor.RemoveProvider(_provider, typeof(ChromaColors));
        }

        private PropertyDescriptor GetPropertyDescriptor(string name)
        {
            var pdc = _provider.GetTypeDescriptor(typeof(ChromaColors))
                .GetProperties(new Attribute[] { BrowsableAttribute.Yes });

            return pdc[name];
        }
    }
}
