using EliteChroma.Core.Tests.Internal;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class KeyboardLayoutMapTests
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

        [Fact]
        public void GetKeyboardLayoutThrowsOnNullKeyboardLayoutName()
        {
            Assert.Throws<ArgumentNullException>("keyboardLayout", () => Elite.Internal.KeyboardLayoutMap.GetKeyboardLayout(null!, NativeMethodsKeyboardMock.Instance));
        }

        [Fact]
        public void GetCurrentLayoutFallsBackToEnUSOnUnknownKeyboardLayout()
        {
            string res = Elite.Internal.KeyboardLayoutMap.GetCurrentLayout(new BadKeyboardLayoutKeyboardMock());

            Assert.Equal("en-US", res);
        }

        private sealed class BadKeyboardLayoutKeyboardMock : NativeMethodsKeyboardMock
        {
            public override IntPtr GetKeyboardLayout(int idThread)
            {
                return new IntPtr(int.MaxValue);
            }
        }
    }
}
