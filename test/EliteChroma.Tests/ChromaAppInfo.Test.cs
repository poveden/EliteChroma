using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Linq;
using EliteChroma.Internal;
using Xunit;

namespace EliteChroma.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class ChromaAppInfoTest
    {
        [Fact]
        public void ChromaAppInfoValuesMatchManifestValues()
        {
            var appInfo = LoadChromaAppInfo();

            var assemblyInfo = new AssemblyInfo();

            Assert.Equal(assemblyInfo.Title, appInfo.Element("title").Value);
            Assert.Equal(assemblyInfo.Description, appInfo.Element("description").Value);
            Assert.Equal("Jorge Poveda Coma", appInfo.Element("author").Attribute("name").Value);
            Assert.Equal(assemblyInfo.Company, appInfo.Element("author").Attribute("contact").Value);
        }

        private static XElement LoadChromaAppInfo()
        {
            var a = typeof(AssemblyInfo).Assembly;
            var xmlFile = Path.Combine(Path.GetDirectoryName(a.Location), "ChromaAppInfo.xml");
            var xml = XDocument.Load(xmlFile);
            return xml.Root;
        }
    }
}
