using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using Colore.Data;
using EliteChroma.Core;
using EliteChroma.Internal.UI;
using Xunit;

namespace EliteChroma.Tests
{
    public class ChromaColorsMetadataTests
    {
        [Fact]
        public void AllPublicReadWritePropertiesAreMapped()
        {
            var cc = GetPublicReadWriteProperties<ChromaColors>();
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>();

            var hcc = new HashSet<(string, Type)>(cc.Select(x => (x.Name, x.PropertyType)));
            var hccm = new HashSet<(string, Type)>(ccm.Select(x => (x.Name, x.PropertyType)));

            Assert.Empty(hcc.Except(hccm));
            Assert.Empty(hccm.Except(hcc));
        }

        [Fact]
        public void AllMappedPropertiesHaveACategoryAttribute()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>();

            Assert.All(ccm, pi => Assert.NotNull(pi.GetCustomAttribute<CategoryAttribute>()));
        }

        [Fact]
        public void AllCategoriesHaveAResourceStringDefined()
        {
            var cNames = GetPublicReadWriteProperties<ChromaColorsMetadata>()
                .Select(x => x.GetCustomAttribute<CategoryAttribute>()!.Category)
                .Distinct(StringComparer.Ordinal);

            Assert.All(cNames, AssertResourceStringIsDefined);
        }

        [Fact]
        public void AllMappedPropertiesHaveADisplayNameResourceStringDefined()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>();

            Assert.All(ccm, pi => AssertResourceStringIsDefined($"Colors_{pi.Name}"));
        }

        [Fact]
        public void AllMappedColorPropertiesHaveTheExpectedTypeConverterAttribute()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>().Where(x => x.PropertyType == typeof(Color));

            Assert.All(ccm, pi => AssertHasTypeConverterAttribute(pi, typeof(ColoreColorConverter)));
        }

        [Fact]
        public void AllMappedColorPropertiesHaveTheExpectedEditorAttribute()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>().Where(x => x.PropertyType == typeof(Color));

            Assert.All(ccm, pi => AssertHasEditorAttributeAttribute(pi, typeof(ColoreColorEditor), typeof(UITypeEditor)));
        }

        [Fact]
        public void AllMappedDoublePropertiesHaveTheExpectedTypeConverterAttribute()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>().Where(x => x.PropertyType == typeof(double));

            Assert.All(ccm, pi => AssertHasTypeConverterAttribute(pi, typeof(BrightnessConverter)));
        }

        [Fact]
        public void AllMappedDoublePropertiesHaveTheExpectedEditorAttribute()
        {
            var ccm = GetPublicReadWriteProperties<ChromaColorsMetadata>().Where(x => x.PropertyType == typeof(double));

            Assert.All(ccm, pi => AssertHasEditorAttributeAttribute(pi, typeof(BrightnessEditor), typeof(UITypeEditor)));
        }

        private static void AssertHasTypeConverterAttribute(PropertyInfo pi, Type expectedType)
        {
            var tca = pi.GetCustomAttribute<TypeConverterAttribute>()!;
            Assert.NotNull(tca);

            Assert.Equal(expectedType.AssemblyQualifiedName, tca.ConverterTypeName);
        }

        private static void AssertHasEditorAttributeAttribute(PropertyInfo pi, Type expectedType, Type expectedBaseType)
        {
            var ea = pi.GetCustomAttribute<EditorAttribute>()!;
            Assert.NotNull(ea);

            Assert.Equal(expectedType.AssemblyQualifiedName, ea.EditorTypeName);
            Assert.Equal(expectedBaseType.AssemblyQualifiedName, ea.EditorBaseTypeName);
        }

        private static void AssertResourceStringIsDefined(string name)
        {
            var rm = Properties.Resources.ResourceManager;
            var ci = Properties.Resources.Culture;

            string? rs = rm.GetString(name, ci);
            Assert.False(string.IsNullOrEmpty(rs));
        }

        // Reference: https://stackoverflow.com/questions/824802/c-how-to-get-all-public-both-get-and-set-string-properties-of-a-type/824854#824854
        private static IEnumerable<PropertyInfo> GetPublicReadWriteProperties<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.GetGetMethod(false) != null && x.GetSetMethod(false) != null);
        }
    }
}
