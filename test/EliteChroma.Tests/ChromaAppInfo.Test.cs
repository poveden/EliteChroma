using System.IO;
using System.Xml.Linq;
using EliteChroma.Forms;
using Xunit;

namespace EliteChroma.Tests
{
    public class ChromaAppInfoTest
    {
        [Fact]
        public void ChromaAppInfoValuesMatchManifestValues()
        {
            var appInfo = LoadChromaAppInfo();
            
            using var frm = new FrmAboutBox();

            Assert.Equal(FrmAboutBox.AssemblyTitle, appInfo.Element("title").Value);
            Assert.Equal(frm.AssemblyDescription, appInfo.Element("description").Value);
            Assert.Equal("Jorge Poveda Coma", appInfo.Element("author").Attribute("name").Value);
            Assert.Equal(frm.AssemblyCompany, appInfo.Element("author").Attribute("contact").Value);
        }

        private static XElement LoadChromaAppInfo()
        {
            var a = typeof(Forms.FrmAboutBox).Assembly;
            var xmlFile = Path.Combine(Path.GetDirectoryName(a.Location), "ChromaAppInfo.xml");
            var xml = XDocument.Load(xmlFile);
            return xml.Root;
        }
    }
}
