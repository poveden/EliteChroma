using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EliteFiles.Bindings.Devices;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class KeyMappingsTest
    {
        private static readonly StringComparer _comparer = StringComparer.Ordinal;

        [Fact]
        public void AllKeyMappingsToVirtualKeysHaveBeenDeclared()
        {
            var keys = new HashSet<string>(Elite.Internal.KeyMappings.EliteKeys.Keys, _comparer);

            Assert.Equal(GetAllBindings(), keys);
        }

        [Fact]
        public void AllKeyMappingsToColoreKeysHaveBeenDeclared()
        {
            var keys = new HashSet<string>(Core.Internal.KeyMappings.EliteKeys.Keys, _comparer);

            Assert.Equal(GetAllBindings(), keys);
        }

        private static HashSet<string> GetAllBindings()
        {
            var allBindings = typeof(Keyboard)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(x => (string)x.GetValue(null));

            return new HashSet<string>(allBindings, _comparer);
        }
    }
}
