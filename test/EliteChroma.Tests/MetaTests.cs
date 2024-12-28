using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class MetaTests
    {
        public static TheoryData<Type> GetAllForms()
        {
            var allForms = (from t in typeof(Program).Assembly.GetTypes()
                            where t.IsSubclassOf(typeof(Form))
                            select t).ToList();

            Assert.NotEmpty(allForms);

            return new TheoryData<Type>(allForms);
        }

        [Theory]
        [MemberData(nameof(GetAllForms))]
        public void PictureBoxesInFormsHaveImages(Type frmType)
        {
            /*
             * Visual Studio's Windows Forms designer (preview) sometimes "forgets"
             * the assignment of image resources to PictureBox controls.
             */

            ArgumentNullException.ThrowIfNull(frmType);
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

            const string ExpectedTargetFramework = "net6.0-windows";

            string solutionDir = MetaTestsCommon.GetSolutionDirectory();

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

        [Theory]
        [MemberData(nameof(GetAllEventHandlers))]
        public void EventHandlersDeclareSenderParameterAsNullable(Type type, string eventHandlerName)
        {
            MetaTestsCommon.AssertSenderParameterIsNullable(type, eventHandlerName);
        }

        [SuppressMessage("OrderingRules", "SA1204:Static elements should appear before instance elements", Justification = "Theory data")]
        public static TheoryData<Type, string> GetAllEventHandlers()
        {
            return MetaTestsCommon.GetAllEventHandlers(typeof(Program).Assembly);
        }
    }
}
