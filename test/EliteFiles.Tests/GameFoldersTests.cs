using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EliteFiles.Status.Internal;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class GameFoldersTests
    {
        [Fact]
        public void GetsTheListOfDefaultGameInstallFolders()
        {
            Assert.True(GameInstallFolder.DefaultPaths.Count == 9);
            Assert.All(
                GameInstallFolder.DefaultPaths,
                x => Assert.True(x.Contains(@"\Oculus\", StringComparison.Ordinal)
                ^ (x.EndsWith(@"\Products\elite-dangerous-64", StringComparison.Ordinal) || x.EndsWith(@"\Products\elite-dangerous-odyssey-64", StringComparison.Ordinal))));
        }

        [Fact]
        public void GetsTheListOfAlternateGameInstallFolders()
        {
            var mockRegistry = new MockWindowsRegistry();

            var paths = GameInstallFolder.GetAlternatePaths(mockRegistry, @"TestFiles\libraryfolders.vdf").ToList();

            string[] expectedPaths = new[]
            {
                @"INSTALLPATH\Products\elite-dangerous-64",
                @"INSTALLPATH\Products\elite-dangerous-odyssey-64",
                @"C:\Games\Path1\steamapps\common\Elite Dangerous\Products\elite-dangerous-64",
                @"C:\Games\Path1\steamapps\common\Elite Dangerous\Products\elite-dangerous-odyssey-64",
                @"D:\steamapps\common\Elite Dangerous\Products\elite-dangerous-64",
                @"D:\steamapps\common\Elite Dangerous\Products\elite-dangerous-odyssey-64",
            };

            Assert.Equal(expectedPaths, paths);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("\"UnrecognizedHeader\"\n{\n}\n")]
        [InlineData("\"LibraryFolders\"")]
        [InlineData("\"LibraryFolders\"\n{ \"1\" \"Path\" }\n")]
        public void SteamLibraryFoldersFromFileReturnsEmptyOnMissingOrInvalidFile(string contents)
        {
            using var tf = new TestFolder();
            string filename = $"libraryfolders.vdf";

            if (contents != null)
            {
                tf.WriteText(filename, contents);
            }

            Assert.Empty(SteamLibraryFoldersFromFile(tf.Resolve(filename)));
        }

        [Fact]
        public void SteamLibraryFoldersFromFileGetsTheListOfFolders()
        {
            var slf = SteamLibraryFoldersFromFile(@"TestFiles\libraryfolders.vdf");

            Assert.Equal(2, slf.Count);
            Assert.Equal(@"C:\Games\Path1", slf[0]);
            Assert.Equal("D:", slf[1]);
        }

        [Fact]
        public void GetsTheDefaultGameOptionsFolder()
        {
            string folder = GameOptionsFolder.DefaultPath;

            Assert.EndsWith(@"\Frontier Developments\Elite Dangerous\Options", folder, StringComparison.Ordinal);
        }

        [Fact]
        public void GetsTheDefaultJournalFolder()
        {
            string folder = JournalFolder.DefaultPath;

            Assert.EndsWith(@"\Saved Games\Frontier Developments\Elite Dangerous", folder, StringComparison.Ordinal);
        }

        [Fact]
        public void GetsGameInstallFolderInfo()
        {
            const string templateFolder = @"TestFiles\GameRoot";

            var gif = new GameInstallFolder(templateFolder);

            Assert.True(gif.IsValid);
            Assert.Equal(Path.Combine(gif.FullName, "EliteDangerous64.exe"), gif.MainExecutable.FullName, true);
            Assert.Equal(Path.Combine(gif.FullName, "GraphicsConfiguration.xml"), gif.GraphicsConfiguration.FullName, true);
            Assert.Equal(Path.Combine(gif.FullName, "ControlSchemes"), gif.ControlSchemes.FullName, true);
        }

        [Fact]
        public void GetsGameOptionsFolderInfo()
        {
            const string templateFolder = @"TestFiles\GameOptions";

            var gof = new GameOptionsFolder(templateFolder);

            Assert.True(gof.IsValid);
            Assert.Equal(Path.Combine(gof.FullName, "Audio"), gof.Audio.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Audio\StartPreset.start"), gof.AudioStartPreset.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, "Bindings"), gof.Bindings.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Bindings\StartPreset.start"), gof.BindingsStartPreset.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, "Development"), gof.Development.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Development\StartPreset.start"), gof.DevelopmentStartPreset.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, "Graphics"), gof.Graphics.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Graphics\GraphicsConfigurationOverride.xml"), gof.GraphicsConfigurationOverride.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Graphics\StartPreset.start"), gof.GraphicsStartPreset.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, "Player"), gof.Player.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Player\StartPreset.start"), gof.PlayerStartPreset.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, "Startup"), gof.Startup.FullName, true);
            Assert.Equal(Path.Combine(gof.FullName, @"Startup\Settings.xml"), gof.StartupSettings.FullName, true);
        }

        [Fact]
        public void GetsJournalFolderInfo()
        {
            const string templateFolder = @"TestFiles\Journal";

            var jf = new JournalFolder(templateFolder);

            Assert.True(jf.IsValid);
            Assert.Equal(Path.Combine(jf.FullName, "Status.json"), jf.Status.FullName, true);
            Assert.True(File.Exists(Path.Combine(jf.FullName, "Journal.190101020000.01.log")));
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameInstallFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameRoot";

            static bool IsValidFolder(string path)
            {
                return new GameInstallFolder(path).IsValid;
            }

            Assert.True(IsValidFolder(templateFolder));
            Assert.False(IsValidFolder("Non-Existing-Path"));
            Assert.False(IsValidFolder("TestFiles"));

            DeleteFileAndAssertFalse(templateFolder, "EliteDangerous64.exe", IsValidFolder);

            DeleteFileAndAssertFalse(templateFolder, "GraphicsConfiguration.xml", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "ControlSchemes", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"ControlSchemes\KeyboardMouseOnly.binds", IsValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenGameOptionsFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\GameOptions";

            static bool IsValidFolder(string path)
            {
                return new GameOptionsFolder(path).IsValid;
            }

            Assert.True(IsValidFolder(templateFolder));
            Assert.False(IsValidFolder("Non-Existing-Path"));
            Assert.False(IsValidFolder("TestFiles"));

            DeleteFolderAndAssertFalse(templateFolder, "Audio", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Audio\StartPreset.start", IsValidFolder);

            /* Exception: Odyssey does not create the Bindings folder by default. */
            DeleteFolderAndAssertTrue(templateFolder, "Bindings", IsValidFolder);
            DeleteFileAndAssertTrue(templateFolder, @"Bindings\StartPreset.start", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Development", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Development\StartPreset.start", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Graphics", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Graphics\StartPreset.start", IsValidFolder);
            /* Exception: GraphicsConfigurationOverride is not required to be present. */
            DeleteFileAndAssertTrue(templateFolder, @"Graphics\GraphicsConfigurationOverride.xml", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Player", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Player\StartPreset.start", IsValidFolder);

            DeleteFolderAndAssertFalse(templateFolder, "Startup", IsValidFolder);
            DeleteFileAndAssertFalse(templateFolder, @"Startup\Settings.xml", IsValidFolder);
        }

        [Fact]
        public void ReturnsFalseWhenTheGivenJournalFolderIsNotAValidFolder()
        {
            const string templateFolder = @"TestFiles\Journal";

            static bool IsValidFolder(string path)
            {
                return new JournalFolder(path).IsValid;
            }

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

        private static void DeleteFileAndAssertTrue(string templatePath, string fileToDelete, Func<string, bool> testFunc)
        {
            using var dir = new TestFolder(templatePath);
            dir.DeleteFile(fileToDelete);
            Assert.True(testFunc(dir.Name));
        }

        private static void DeleteFolderAndAssertFalse(string templatePath, string folderToDelete, Func<string, bool> testFunc)
        {
            using var dir = new TestFolder(templatePath);
            dir.DeleteFolder(folderToDelete);
            Assert.False(testFunc(dir.Name));
        }

        private static void DeleteFolderAndAssertTrue(string templatePath, string folderToDelete, Func<string, bool> testFunc)
        {
            using var dir = new TestFolder(templatePath);
            dir.DeleteFolder(folderToDelete);
            Assert.True(testFunc(dir.Name));
        }

        private static IReadOnlyList<string> SteamLibraryFoldersFromFile(string path)
        {
            var ti = typeof(JournalFolder).Assembly.DefinedTypes
                .Single(x => x.IsNotPublic && x.Name == "SteamLibraryFolders");

            var mi = ti.GetMethod("FromFile", BindingFlags.Public | BindingFlags.Static)!;

            return (IReadOnlyList<string>)mi.Invoke(null, new object[] { path })!;
        }

        private sealed class MockWindowsRegistry : IWindowsRegistry
        {
            public object? GetValue(string keyName, string? valueName, object? defaultValue)
            {
                if (keyName == @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{696F8871-C91D-4CB1-825D-36BE18065575}_is1"
                    && valueName == "InstallLocation")
                {
                    return "INSTALLPATH";
                }

                return defaultValue;
            }
        }
    }
}
