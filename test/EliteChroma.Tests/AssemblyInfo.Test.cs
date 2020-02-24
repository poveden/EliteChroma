using System.Diagnostics.CodeAnalysis;
using EliteChroma.Internal;
using Xunit;

namespace EliteChroma.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class AssemblyInfoTest
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
