using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Colore.Effects.Keyboard;
using EliteChroma.Core.Tests.Internal;
using EliteFiles.Bindings.Devices;
using Xunit;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class KeyMappingsTest
    {
        private static readonly StringComparer _comparer = StringComparer.Ordinal;

        private static readonly string[] _characterKeyNames = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            .Select(c => $"Key_{c}").ToArray();

        [Flags]
        private enum KeyTypes
        {
            Common = 1,
            Character = 2,
        }

        [Fact]
        public void AllDirectKeyMappingsToVirtualKeysHaveBeenDeclared()
        {
            var keys = GetDirectVirtualKeyMapping().Keys;
            var expected = GetKeyboardKeys(KeyTypes.Common | KeyTypes.Character).Keys;

            AssertEqualSet(expected, keys, _comparer);
        }

        [Fact]
        public void AllDirectKeyMappingsToColoreKeysHaveBeenDeclared()
        {
            var keys = GetDirectColoreKeyMapping().Keys;
            var expected = GetKeyboardKeys(KeyTypes.Common | KeyTypes.Character).Keys;

            AssertEqualSet(expected, keys, _comparer);
        }

        [Fact]
        public void AllDefinedDirectKeyMappingsToVirtualKeysAreUnique()
        {
            var values = GetDirectVirtualKeyMapping().Values.Where(x => x != 0);
            var unique = values.Distinct();

            AssertEqualSet(unique, values, EqualityComparer<VirtualKey>.Default);
        }

        [Fact]
        public void AllDefinedDirectKeyMappingsToColoreKeysAreUnique()
        {
            var values = GetDirectColoreKeyMapping().Values.Where(x => x != 0);
            var unique = values.Distinct();

            AssertEqualSet(unique, values, EqualityComparer<Key>.Default);
        }

        [Theory]
        [InlineData("Key_Escape", "en-US", true, VirtualKey.VK_ESCAPE)]
        [InlineData("Key_ß", "de-DE", true, VirtualKey.VK_OEM_4)]
        [InlineData("Key_º", "es-ES", true, VirtualKey.VK_OEM_5)]
        [InlineData("Key_INVALID_KEY_NAME", "en-US", false, (VirtualKey)0)]
        public void TryGetVirtualKeyReturnsExpectedValues(string keyName, string keyboardLayout, bool expectedOk, Enum expectedKey)
        {
            var ok = Elite.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, false, out var virtualKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal((VirtualKey)expectedKey, virtualKey);
        }

        [Theory]
        [InlineData("Key_Escape", "en-US", true, Key.Escape)]
        [InlineData("Key_ß", "de-DE", true, Key.OemMinus)]
        [InlineData("Key_¤", "en-US", false, (Key)0)]
        [InlineData("Key_º", "es-ES", true, Key.OemTilde)]
        [InlineData("Key_INVALID_KEY_NAME", "en-US", false, (Key)0)]
        public void TryGetChromaKeyReturnsExpectedValues(string keyName, string keyboardLayout, bool expectedOk, Key expectedKey)
        {
            var ok = Core.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, false, out var chromaKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedKey, chromaKey);
        }

        [Theory]
        [InlineData("Key_Grave", "es-ES", true, VirtualKey.VK_OEM_5)]
        public void TryGetVirtualKeyReturnsEnUSOverrides(string keyName, string keyboardLayout, bool expectedOk, Enum expectedKey)
        {
            var ok = Elite.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, true, out var virtualKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal((VirtualKey)expectedKey, virtualKey);
        }

        [Theory]
        [InlineData("Key_Grave", "es-ES", true, Key.OemTilde)]
        public void TryGetChromaKeyReturnsEnUSOverrides(string keyName, string keyboardLayout, bool expectedOk, Key expectedKey)
        {
            var ok = Core.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, true, out var chromaKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedKey, chromaKey);
        }

        private static IReadOnlyDictionary<string, VirtualKey> GetDirectVirtualKeyMapping()
        {
            return (IReadOnlyDictionary<string, VirtualKey>)typeof(Elite.Internal.KeyMappings)
                .GetField("_keys", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);
        }

        private static IReadOnlyDictionary<string, Key> GetDirectColoreKeyMapping()
        {
            return (IReadOnlyDictionary<string, Key>)typeof(Core.Internal.KeyMappings)
                .GetField("_keys", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);
        }

        private static Dictionary<string, KeyTypes> GetKeyboardKeys(KeyTypes types)
        {
            var res = new Dictionary<string, KeyTypes>(_comparer);

            if (types.HasFlag(KeyTypes.Common))
            {
                var allFnKeyNames = typeof(Keyboard)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(x => (string)x.GetValue(null));

                foreach (var keyName in allFnKeyNames)
                {
                    res.Add(keyName, KeyTypes.Common);
                }
            }

            if (types.HasFlag(KeyTypes.Character))
            {
                foreach (var charKeyName in _characterKeyNames)
                {
                    res.Add(charKeyName, KeyTypes.Character);
                }
            }

            return res;
        }

        private static void AssertEqualSet<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer)
        {
            var hExp = new HashSet<T>(expected, comparer);
            var hAct = new HashSet<T>(actual, comparer);

            Assert.Empty(hExp.Except(hAct));
            Assert.Empty(hAct.Except(hExp));
        }
    }
}
