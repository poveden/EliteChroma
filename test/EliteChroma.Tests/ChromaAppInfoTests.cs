using System.IO;
using System.Xml.Linq;
using EliteChroma.Internal;
using Xunit;

namespace EliteChroma.Tests
{
    public class ChromaAppInfoTests
    {
        [Fact]
        public void ChromaAppInfoValuesMatchManifestValues()
        {
            var appInfo = LoadChromaAppInfo();

            var assemblyInfo = new AssemblyInfo();

            Assert.Equal(assemblyInfo.Title, appInfo.Element("title")!.Value);
            Assert.Equal(assemblyInfo.Description, appInfo.Element("description")!.Value);
            Assert.Equal("Jorge Poveda Coma", appInfo.Element("author")!.Attribute("name")!.Value);
            Assert.Equal(assemblyInfo.Company, appInfo.Element("author")!.Attribute("contact")!.Value);
        }

        private static XElement LoadChromaAppInfo()
        {
            var a = typeof(AssemblyInfo).Assembly;
            string xmlFile = Path.Combine(Path.GetDirectoryName(a.Location)!, "ChromaAppInfo.xml");
            var xml = XDocument.Load(xmlFile);
            return xml.Root!;
        }
    }
}
