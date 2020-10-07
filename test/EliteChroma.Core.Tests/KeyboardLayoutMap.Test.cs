using System.Diagnostics.CodeAnalysis;
using EliteChroma.Core.Tests.Internal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class KeyboardLayoutMapTest
    {
        [Theory]
        [InlineData("xx-XX")]
        [InlineData("NOT_A_VALID_LAYOUT")]
        public void GetKeyboardLayoutFallsBackOnUnknownLayouts(string keyboardLayout)
        {
            var hkl = Elite.Internal.KeyboardLayoutMap.GetKeyboardLayout(keyboardLayout, NativeMethodsKeyboardMock.Instance);

            var expected = NativeMethodsKeyboardMock.Instance.GetKeyboardLayout(0);

            Assert.Equal(expected, hkl);
        }
    }
}
