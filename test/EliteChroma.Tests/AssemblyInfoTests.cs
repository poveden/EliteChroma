using EliteChroma.Internal;
using Xunit;

namespace EliteChroma.Tests
{
    public class AssemblyInfoTests
    {
        [Fact]
        public void AllAssemblyInfoPropertiesHaveValues()
        {
            var assemblyInfo = new AssemblyInfo();

            Assert.False(string.IsNullOrEmpty(assemblyInfo.Title));
            Assert.NotNull(assemblyInfo.Version);
            Assert.False(string.IsNullOrEmpty(assemblyInfo.Description));
            Assert.False(string.IsNullOrEmpty(assemblyInfo.Product));
            Assert.False(string.IsNullOrEmpty(assemblyInfo.Copyright));
            Assert.False(string.IsNullOrEmpty(assemblyInfo.Company));
        }
    }
}
