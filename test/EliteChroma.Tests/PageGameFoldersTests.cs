using System.ComponentModel;
using System.Windows.Forms;
using EliteChroma.Forms.Pages;
using EliteFiles;
using TestUtils;
using Xunit;

namespace EliteChroma.Tests
{
    public class PageGameFoldersTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        private readonly GameInstallFolder _gif;
        private readonly GameOptionsFolder _gof;
        private readonly JournalFolder _jf;

        public PageGameFoldersTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
            _gof = new GameOptionsFolder(_gameOptionsFolder);
            _jf = new JournalFolder(_journalFolder);
        }

        [Fact]
        public void ValidatesInput()
        {
            using var page = new PageGameFolders
            {
                GameInstallFolder = _gif.FullName,
                GameOptionsFolder = _gof.FullName,
                JournalFolder = _jf.FullName,
            };

            var errors = new Dictionary<object, string>();
            page.Error += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e))
                {
                    errors[sender!] = e;
                }
                else
                {
                    errors.Remove(sender!);
                }
            };

            page.CreateControl();

            Assert.True(page.ValidateChildren());
            Assert.Empty(errors);

            page.GameInstallFolder += "\\DOES-NOT-EXIST1";
            page.GameOptionsFolder += "\\DOES-NOT-EXIST2";
            page.JournalFolder += "\\DOES-NOT-EXIST3";

            Assert.False(page.ValidateChildren());
            Assert.Equal(3, errors.Count);
        }

        [Fact]
        public void PopulateTheGameInstallDropDownListWithTheAvailableDefaultPaths()
        {
            using var page = new PageGameFolders
            {
                GameInstallFolder = _gif.FullName,
                GameOptionsFolder = _gof.FullName,
                JournalFolder = _jf.FullName,
            };

            var ctxGameInstall = page.GetPrivateField<ContextMenuStrip>("ctxGameInstall")!;

            Assert.Equal(2, ctxGameInstall.Items.Count);
            Assert.IsType<ToolStripSeparator>(ctxGameInstall.Items[0]);

            var availablePaths = GameInstallFolder.DefaultPaths.Take(3).ToList();

            var gameInstallFolders = page.GetPrivateField<List<ToolStripMenuItem>>("_gameInstallFolders")!;
            gameInstallFolders.Clear();
            gameInstallFolders.AddRange(availablePaths.Select(x => page.InvokePrivateMethod<ToolStripMenuItem>("CreateGameInstallFolderMenuItem", x)!));

            page.CreateControl();

            Assert.Equal(2 + availablePaths.Count, ctxGameInstall.Items.Count);
            Assert.Equal(availablePaths, ctxGameInstall.Items.Cast<ToolStripItem>().Take(availablePaths.Count).Select(x => (string)x.Tag));
        }

        [Fact]
        public void GameInstallDropDownListMarksAsSelectedTheEntryThatMatchesTheTextBox()
        {
            using var page = new PageGameFolders
            {
                GameInstallFolder = _gif.FullName,
                GameOptionsFolder = _gof.FullName,
                JournalFolder = _jf.FullName,
            };

            string[] availablePaths = new[]
            {
                GameInstallFolder.DefaultPaths.First(),
                _gif.FullName,
            };

            var menuItems = availablePaths.Select(x => page.InvokePrivateMethod<ToolStripMenuItem>("CreateGameInstallFolderMenuItem", x)!).ToList();

            var gameInstallFolders = page.GetPrivateField<List<ToolStripMenuItem>>("_gameInstallFolders")!;
            gameInstallFolders.Clear();
            gameInstallFolders.AddRange(menuItems);

            page.CreateControl();

            var ctxGameInstall = page.GetPrivateField<ContextMenuStrip>("ctxGameInstall")!;
            ctxGameInstall.InvokePrivateMethod<object?>("OnOpening", new CancelEventArgs());

            Assert.False(menuItems[0].Checked);
            Assert.True(menuItems[1].Checked);
        }

        [Fact]
        public void ClickingOnAGameInstallDropDownEntryUpdatesTheTextBox()
        {
            using var page = new PageGameFolders
            {
                GameInstallFolder = _gif.FullName,
                GameOptionsFolder = _gof.FullName,
                JournalFolder = _jf.FullName,
            };

            string[] availablePaths = new[]
            {
                GameInstallFolder.DefaultPaths.First(),
                _gif.FullName,
            };

            var menuItems = availablePaths.Select(x => page.InvokePrivateMethod<ToolStripMenuItem>("CreateGameInstallFolderMenuItem", x)!).ToList();

            var gameInstallFolders = page.GetPrivateField<List<ToolStripMenuItem>>("_gameInstallFolders")!;
            gameInstallFolders.Clear();
            gameInstallFolders.AddRange(menuItems);

            page.CreateControl();

            Assert.Equal(_gif.FullName, page.GameInstallFolder);

            menuItems[0].PerformClick();

            Assert.Equal(availablePaths[0], page.GameInstallFolder);
        }
    }
}
