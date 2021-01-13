using System;
using EliteFiles.Bindings.Devices;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class KeyboardTests
    {
        [Fact]
        public void TryGetKeyThrowsOnNullKeyName()
        {
            Assert.Throws<ArgumentNullException>("keyName", () => Keyboard.TryGetKeyChar(null, out _));
        }

        [Theory]
        [InlineData("Key_A", true, 'A')]
        [InlineData("Key_Escape", false, default(char))]
        [InlineData("Mouse_1", false, default(char))]
        public void TryGetKeyCharReturnsExpectedValues(string keyName, bool expectedOk, char expectedChar)
        {
            var ok = Keyboard.TryGetKeyChar(keyName, out var c);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedChar, c);
        }

        [Theory]
        [InlineData('A', true, "Key_A")]
        [InlineData('-', true, "Key_Minus")]
        [InlineData(' ', true, "Key_Space")]
        [InlineData('\u001b', false, null)]
        public void TryGetKeyNameReturnsExpectedValues(char c, bool expectedOk, string expectedKeyName)
        {
            var ok = Keyboard.TryGetKeyName(c, out var keyName);

            Assert.Equal(expectedOk, ok);
            Assert.Equal(expectedKeyName, keyName);
        }
    }
}
