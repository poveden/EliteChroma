using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EliteFiles.Internal;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class GameFoldersTest
    {
        [Fact]
        public void GetsTheListOfDefaultGameInstallFolders()
        {
            Assert.True(GameInstallFolder.DefaultPaths.Count >= 4);
            Assert.All(GameInstallFolder.DefaultPaths, x => x.EndsWith(@"\Products\elite-dangerous-64", StringComparison.Ordinal));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\"UnrecognizedHeader\"\n{\n}\n")]
        [InlineData("\"LibraryFolders\"")]
        [InlineData("\"LibraryFolders\"\n{ \"1\" \"Path\" }\n")]
        public void SteamLibraryFoldersFromFileReturnsNullOnMissingOrInvalidFile(string contents)
        {
            using var tf = new TestFolder();
            string filename = $"libraryfolders.vdf";

            if (contents != null)
            {
                tf.WriteText(filename, contents);
            }

            Assert.Null(SteamLibraryFolders.FromFile(tf.Resolve(filename)));
        }

        [Fact]
        public void SteamLibraryFoldersFromFileGetsTheListOfFolders()
        {
            var slf = SteamLibraryFolders.FromFile(@"TestFiles\libraryfolders.vdf");

            Assert.Equal(2, slf.Count);
            Assert.Equal(@"C:\Games\Path1", slf[0]);
            Assert.Equal("D:", slf[1]);
        }

        [Fact]
        public void GetsTheDefaultGameOptionsFolder()
        {
            var folder = GameOptionsFolder.DefaultPath;

            Assert.EndsWith(@"\Frontier Developments\Elite Dangerous\Options", folder, StringComparison.Ordinal);
        }

        [Fact]
        public void GetsTheDefaultJournalFolder()
        {
            var folder = JournalFolder.DefaultPath;

            Assert.EndsWith(@"\Saved Games\Frontier Developments\Elite Dangerous", folder, StringComparison.Ordinal);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameInstallFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameRoot";
            static bool IsValidFolder(string path) => new GameInstallFolder(path).IsValid;

            Assert.True(IsValidFolder(templateFolder));
            Assert.False(IsValidFolder("Non-Existing-Path"));
            Assert.False(IsValidFolder("TestFiles"));

            DeleteFileAndAssertFalse(templateFolder, "EliteDangerous64.exe", IsValidFolder);

            DeleteFileAndAssertFalse(templateFolder, "GraphicsConfiguration.xml", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "ControlSchemes", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"ControlSchemes\Keyboard.binds", IsValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameOptionsFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameOptions";
            static bool IsValidFolder(string path) => new GameOptionsFolder(path).IsValid;

            Assert.True(IsValidFolder(templateFolder));
            Assert.False(IsValidFolder("Non-Existing-Path"));
            Assert.False(IsValidFolder("TestFiles"));

            DeleteFolderAndAssertFalse(templateFolder, "Bindings", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Bindings\StartPreset.start", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Graphics", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Graphics\GraphicsConfigurationOverride.xml", IsValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenJournalFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\Journal";
            static bool IsValidFolder(string path) => new JournalFolder(path).IsValid;

            Assert.True(IsValidFolder(templateFolder));
            Assert.False(IsValidFolder("Non-Existing-Path"));
            Assert.False(IsValidFolder("TestFiles"));

            DeleteFileAndAssertFalse(templateFolder, "Status.json", IsValidFolder);

            DeleteFileAndAssertFalse(templateFolder, "Journal.190101020000.01.log", IsValidFolder);
        }

        private static void DeleteFileAndAssertFalse(string templatePath, string fileToDelete, Func<string, bool> testFunc)
        {
            using var dir = new TestFolder(templatePath);
            dir.DeleteFile(fileToDelete);
            Assert.False(testFunc(dir.Name));
        }

        private static void DeleteFolderAndAssertFalse(string templatePath, string folderToDelete, Func<string, bool> testFunc)
        {
            using var dir = new TestFolder(templatePath);
            dir.DeleteFolder(folderToDelete);
            Assert.False(testFunc(dir.Name));
        }
    }
}
