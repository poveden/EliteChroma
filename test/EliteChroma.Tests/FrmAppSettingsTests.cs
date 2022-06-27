using System.Windows.Forms;
using EliteChroma.Core;
using EliteChroma.Forms;
using EliteChroma.Forms.Pages;
using EliteFiles;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class FrmAppSettingsTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        private readonly GameInstallFolder _gif;
        private readonly GameOptionsFolder _gof;
        private readonly JournalFolder _jf;

        public FrmAppSettingsTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
            _gof = new GameOptionsFolder(_gameOptionsFolder);
            _jf = new JournalFolder(_journalFolder);
        }

        [Fact]
        public void AllSectionEntriesHaveTheirTagPropertySetToTheCorrespondingControl()
        {
            using var frm = new FrmAppSettings();
            var tvSections = frm.GetPrivateField<TreeView>("tvSections")!;

            foreach (TreeNode node in tvSections.Nodes)
            {
                Assert.NotNull(node.Tag);
                Assert.IsAssignableFrom<Control>(node.Tag);
            }
        }

        [Fact]
        public void ValidatesInput()
        {
            using var frm = new FrmAppSettings
            {
                GameInstallFolder = _gif.FullName,
                GameOptionsFolder = _gof.FullName,
                JournalFolder = _jf.FullName,
                DetectGameInForeground = false,
                ForceEnUSKeyboardLayout = true,
                Colors = new ChromaColors(),
            };

            var tvSections = frm.GetPrivateField<TreeView>("tvSections")!;
            Assert.Equal(4, tvSections.Nodes.Count);

            var pgGameFolders = (PageGameFolders)tvSections.Nodes["GameFolders"].Tag!;
            var pgGeneral = (PageGeneral)tvSections.Nodes["General"].Tag!;
            var pgKeyboard = (PageKeyboard)tvSections.Nodes["Keyboard"].Tag!;
            var pgColors = (PageColors)tvSections.Nodes["Colors"].Tag!;

            Assert.Equal(frm.GameInstallFolder, pgGameFolders.GameInstallFolder);
            Assert.Equal(frm.GameOptionsFolder, pgGameFolders.GameOptionsFolder);
            Assert.Equal(frm.JournalFolder, pgGameFolders.JournalFolder);
            Assert.Equal(frm.DetectGameInForeground, pgGeneral.DetectGameInForeground);
            Assert.Equal(frm.ForceEnUSKeyboardLayout, pgKeyboard.ForceEnUSKeyboardLayout);
            Assert.Equal(frm.Colors, pgColors.Colors);

            Assert.True(frm.ValidateChildren());

            frm.GameInstallFolder += "\\DOES-NOT-EXIST1";
            frm.GameOptionsFolder += "\\DOES-NOT-EXIST2";
            frm.JournalFolder += "\\DOES-NOT-EXIST3";

            Assert.False(frm.ValidateChildren());
        }
    }
}
