using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class FoldersTest
    {
        [Fact]
        public void GetsTheListOfDefaultGameInstallFolders()
        {
            var list = Folders.GetDefaultGameInstallFolders().ToList();

            Assert.Equal(4, list.Count);
            Assert.All(list, x => x.EndsWith(@"\Products\elite-dangerous-64"));
        }

        [Fact]
        public void GetsTheDefaultGameOptionsFolder()
        {
            var folder = Folders.GetDefaultGameOptionsFolder();

            Assert.EndsWith(@"\Frontier Developments\Elite Dangerous\Options", folder);
        }

        [Fact]
        public void GetsTheDefaultJournalFolder()
        {
            var folder = Folders.GetDefaultJournalFolder();

            Assert.EndsWith(@"\Saved Games\Frontier Developments\Elite Dangerous", folder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameInstallFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameRoot";
            Func<string, bool> isValidFolder = Folders.IsValidGameInstallFolder;
            Assert.True(isValidFolder(templateFolder));

            Assert.False(isValidFolder(null));
            Assert.False(isValidFolder("Non-Existing-Path"));
            Assert.False(isValidFolder("TestFiles"));

            DeleteFileAndAssertFalse(templateFolder, "GraphicsConfiguration.xml", isValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "ControlSchemes", isValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"ControlSchemes\Keyboard.binds", isValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameOptionsFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameOptions";
            Func<string, bool> isValidFolder = Folders.IsValidGameOptionsFolder;
            Assert.True(isValidFolder(templateFolder));

            Assert.False(isValidFolder(null));
            Assert.False(isValidFolder("Non-Existing-Path"));
            Assert.False(isValidFolder("TestFiles"));

            DeleteFolderAndAssertFalse(templateFolder, "Bindings", isValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Bindings\StartPreset.start", isValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Graphics", isValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Graphics\GraphicsConfigurationOverride.xml", isValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenJournalFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\Journal";
            Func<string, bool> isValidFolder = Folders.IsValidJournalFolder;
            Assert.True(isValidFolder(templateFolder));

            Assert.False(isValidFolder(null));
            Assert.False(isValidFolder("Non-Existing-Path"));
            Assert.False(isValidFolder("TestFiles"));

            DeleteFileAndAssertFalse(templateFolder, "Status.json", isValidFolder);

            DeleteFileAndAssertFalse(templateFolder, "Journal.190101020000.01.log", isValidFolder);
        }

        private static void DeleteFileAndAssertFalse(string templatePath, string fileToDelete, Func<string, bool> testFunc)
        {
            using (var dir = new TestFolder(templatePath))
            {
                dir.DeleteFile(fileToDelete);
                Assert.False(testFunc(dir.Name));
            }
        }

        private static void DeleteFolderAndAssertFalse(string templatePath, string folderToDelete, Func<string, bool> testFunc)
        {
            using (var dir = new TestFolder(templatePath))
            {
                dir.DeleteFolder(folderToDelete);
                Assert.False(testFunc(dir.Name));
            }
        }
    }
}
