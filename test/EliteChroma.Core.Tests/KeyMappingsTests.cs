using System.Reflection;
using ChromaWrapper.Keyboard;
using EliteChroma.Core.Tests.Internal;
using EliteFiles.Bindings.Devices;
using TestUtils;
using Xunit;
using static EliteChroma.Core.Internal.NativeMethods;

namespace EliteChroma.Core.Tests
{
    public class KeyMappingsTests
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
        public void AllDirectKeyMappingsToChromaKeysHaveBeenDeclared()
        {
            var keys = GetDirectChromaKeyMapping().Keys;
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
        public void AllDefinedDirectKeyMappingsToChromaKeysAreUnique()
        {
            var values = GetDirectChromaKeyMapping().Values.Where(x => x != 0);
            var unique = values.Distinct();

            AssertEqualSet(unique, values, EqualityComparer<KeyboardKey>.Default);
        }

        [Theory]
        [InlineData("Key_Escape", "en-US", true, VirtualKey.VK_ESCAPE)]
        [InlineData("Key_ß", "de-DE", true, VirtualKey.VK_OEM_4)]
        [InlineData("Key_º", "es-ES", true, VirtualKey.VK_OEM_5)]
        [InlineData("Key_INVALID_KEY_NAME", "en-US", false, (VirtualKey)0)]
        [InlineData("Key_Escape", null, true, VirtualKey.VK_ESCAPE)]
        [InlineData("Key_Slash", null, true, VirtualKey.VK_OEM_2)]
        public void TryGetVirtualKeyReturnsExpectedValues(string keyName, string keyboardLayout, bool expectedOk, Enum expectedKey)
        {
            bool ok = Elite.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, false, out var virtualKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal((VirtualKey)expectedKey, virtualKey);
        }

        [Theory]
        [InlineData("Key_Escape", "en-US", true, KeyboardKey.Esc)]
        [InlineData("Key_ß", "de-DE", true, KeyboardKey.Oem2)]
        [InlineData("Key_¤", "en-US", false, (KeyboardKey)0)]
        [InlineData("Key_º", "es-ES", true, KeyboardKey.Oem1)]
        [InlineData("Key_INVALID_KEY_NAME", "en-US", false, (KeyboardKey)0)]
        [InlineData("Key_Escape", null, true, KeyboardKey.Esc)]
        [InlineData("Key_Slash", null, true, KeyboardKey.Oem11)]
        public void TryGetChromaKeyReturnsExpectedValues(string keyName, string keyboardLayout, bool expectedOk, KeyboardKey expectedKey)
        {
            bool ok = Core.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, false, out var chromaKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedKey, chromaKey);
        }

        [Theory]
        [InlineData("Key_Grave", "es-ES", true, VirtualKey.VK_OEM_5)]
        [InlineData("Key_Slash", null, true, VirtualKey.VK_OEM_2)]
        public void TryGetVirtualKeyReturnsEnUSOverrides(string keyName, string keyboardLayout, bool expectedOk, Enum expectedKey)
        {
            bool ok = Elite.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, true, out var virtualKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal((VirtualKey)expectedKey, virtualKey);
        }

        [Theory]
        [InlineData("Key_Grave", "es-ES", true, KeyboardKey.Oem1)]
        [InlineData("Key_Slash", null, true, KeyboardKey.Oem11)]
        public void TryGetChromaKeyReturnsEnUSOverrides(string keyName, string keyboardLayout, bool expectedOk, KeyboardKey expectedKey)
        {
            bool ok = Core.Internal.KeyMappings.TryGetKey(keyName, keyboardLayout, true, out var chromaKey, NativeMethodsKeyboardMock.Instance);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedKey, chromaKey);
        }

        private static IReadOnlyDictionary<string, VirtualKey> GetDirectVirtualKeyMapping()
        {
            return typeof(Elite.Internal.KeyMappings)
                .GetPrivateStaticField<IReadOnlyDictionary<string, VirtualKey>>("_keys")!;
        }

        private static IReadOnlyDictionary<string, KeyboardKey> GetDirectChromaKeyMapping()
        {
            return typeof(Core.Internal.KeyMappings)
                .GetPrivateStaticField<IReadOnlyDictionary<string, KeyboardKey>>("_keys")!;
        }

        private static Dictionary<string, KeyTypes> GetKeyboardKeys(KeyTypes types)
        {
            var res = new Dictionary<string, KeyTypes>(_comparer);

            if (types.HasFlag(KeyTypes.Common))
            {
                var allFnKeyNames = typeof(Keyboard)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(x => (string)x.GetValue(null)!);

                foreach (string keyName in allFnKeyNames)
                {
                    res.Add(keyName, KeyTypes.Common);
                }
            }

            if (types.HasFlag(KeyTypes.Character))
            {
                foreach (string charKeyName in _characterKeyNames)
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
