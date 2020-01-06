using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
            Assert.Equal(4, GameInstallFolder.DefaultPaths.Count);
            Assert.All(GameInstallFolder.DefaultPaths, x => x.EndsWith(@"\Products\elite-dangerous-64", StringComparison.Ordinal));
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
