using EliteChroma.Internal.UI;
using Xunit;

namespace EliteChroma.Tests
{
    public class LocalizedPropertyDescriptorTests
    {
        [Fact]
        public void ConstructorThrowsOnNullBaseDescriptorArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new LocalizedPropertyDescriptor(null!, "SomePrefix_"));
        }
    }
}
