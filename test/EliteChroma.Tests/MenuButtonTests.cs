using System.Windows.Forms;
using EliteChroma.Controls;
using Xunit;

namespace EliteChroma.Tests
{
    public class MenuButtonTests
    {
        [Fact]
        public void MenuButtonInnerTextPropertiesEndWithEmSpaceCharacter()
        {
            using var button = new MenuButton
            {
                Text = "Click me!",
            };

            Assert.Equal("Click me!", button.Text);
            Assert.Equal("Click me!\u2003", ((Button)button).Text);
        }
    }
}
