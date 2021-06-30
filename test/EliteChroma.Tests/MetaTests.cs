using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Xunit;

namespace EliteChroma.Tests
{
    public class MetaTests
    {
        public static IEnumerable<object[]> GetAllForms()
        {
            var allForms = (from t in typeof(Program).Assembly.GetTypes()
                            where t.IsSubclassOf(typeof(Form))
                            select new object[] { t }).ToList();

            Assert.NotEmpty(allForms);

            return allForms;
        }

        [Theory]
        [MemberData(nameof(GetAllForms))]
        public void PictureBoxesInFormsHaveImages(Type frmType)
        {
            /*
             * Visual Studio's Windows Forms designer (preview) sometimes "forgets"
             * the assignment of image resources to PictureBox controls.
             */

            _ = frmType ?? throw new ArgumentNullException(nameof(frmType));
            using var frm = (Form)frmType.GetConstructor(Type.EmptyTypes)!.Invoke(null);

            var pbs = from fld in frmType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                      where fld.FieldType == typeof(PictureBox)
                      select (PictureBox?)fld.GetValue(frm);

            var noImg = from pb in pbs
                        where pb.Image == null
                        select pb.Name;

            Assert.Empty(noImg);
        }

        [Fact]
        public void WixInstallerTargetFrameworkMatchesTheTargetFrameworkOfTheProject()
        {
            const string EliteChromaProjectPath = @"src\EliteChroma\EliteChroma.csproj";
            const string EliteChromaXPath = "/Project/PropertyGroup/TargetFramework";

            const string WixInstallerProjectPath = @"setup\EliteChromaSetup.wixproj";
            const string WixInstallerNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
            const string WixInstallerXPath = @"/x:Project/x:ItemGroup/x:ProjectReference[@Include='..\src\EliteChroma\EliteChroma.csproj']/x:TargetFrameworkIdentifier";

            const string ExpectedTargetFramework = "net5.0-windows";

            string solutionDir = GetSolutionDirectory();

            string? eliteChromaPath = Path.Combine(solutionDir, EliteChromaProjectPath);
            var eliteChromaProject = XDocument.Load(eliteChromaPath);
            var ecTargetFramework = eliteChromaProject.XPathSelectElement(EliteChromaXPath);
            Assert.NotNull(ecTargetFramework);
            Assert.Equal(ExpectedTargetFramework, ecTargetFramework!.Value);

            string wixInstallerPath = Path.Combine(solutionDir, WixInstallerProjectPath);
            var wixInstallerProject = XDocument.Load(wixInstallerPath);
            var ns = new XmlNamespaceManager(new NameTable());
            ns.AddNamespace("x", WixInstallerNamespace);
            var wixTargetFramework = wixInstallerProject.XPathSelectElement(WixInstallerXPath, ns);
            Assert.NotNull(wixTargetFramework);
            Assert.Equal(ExpectedTargetFramework, wixTargetFramework!.Value);
        }

        internal static string GetSolutionDirectory()
        {
            const string SolutionFilename = "EliteChroma.sln";

            var fi = new FileInfo(typeof(MetaTests).Assembly.Location);

            for (var di = fi.Directory; di != null; di = di.Parent)
            {
                if (di.GetFiles(SolutionFilename, SearchOption.TopDirectoryOnly).Length != 0)
                {
                    return di.FullName;
                }
            }

            throw new InvalidOperationException("Solution directory could not be found.");
        }
    }
}
