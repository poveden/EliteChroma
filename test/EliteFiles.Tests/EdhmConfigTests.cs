using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EliteFiles.Graphics;
using EliteFiles.Internal;
using TestUtils;
using Xunit;

namespace EliteFiles.Tests
{
    public class EdhmConfigTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _d3dxIniFile = @"d3dx.ini";

        private readonly GameInstallFolder _gif;

        public EdhmConfigTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
        }

        [Fact]
        public void CanCreateCompleteEdhmConfigInstances()
        {
            var config = new EdhmConfig
            {
                Constants =
                {
                    ["x150"] = 0.9,
                    ["y150"] = 0.8,
                    ["z150"] = 0.7,

                    ["x151"] = 0.6,
                    ["y151"] = 0.5,
                    ["z151"] = 0.4,

                    ["x152"] = 0.3,
                    ["y152"] = 0.2,
                    ["z152"] = 0.1,
                },
            };

            Assert.Equal(9, config.Constants.Count);

            var colourMatrix = config.GetColourMatrix()!;
            Assert.NotNull(colourMatrix);

            Assert.Equal(0.9, colourMatrix[0, 0]);
            Assert.Equal(0.8, colourMatrix[0, 1]);
            Assert.Equal(0.7, colourMatrix[0, 2]);

            Assert.Equal(0.6, colourMatrix[1, 0]);
            Assert.Equal(0.5, colourMatrix[1, 1]);
            Assert.Equal(0.4, colourMatrix[1, 2]);

            Assert.Equal(0.3, colourMatrix[2, 0]);
            Assert.Equal(0.2, colourMatrix[2, 1]);
            Assert.Equal(0.1, colourMatrix[2, 2]);
        }

        [Fact]
        public void DeserializesConfigFiles()
        {
            var config = EdhmConfig.FromFile(_gif.D3DXIni.FullName)!;

            Assert.NotNull(config);

            var colourMatrix = config.GetColourMatrix()!;
            Assert.NotNull(colourMatrix);

            Assert.Equal(0.99, colourMatrix[0, 0]);
            Assert.Equal(0.01, colourMatrix[0, 1]);
            Assert.Equal(0, colourMatrix[0, 2]);

            Assert.Equal(0, colourMatrix[1, 0]);
            Assert.Equal(1, colourMatrix[1, 1]);
            Assert.Equal(0, colourMatrix[1, 2]);

            Assert.Equal(0, colourMatrix[2, 0]);
            Assert.Equal(0.01, colourMatrix[2, 1]);
            Assert.Equal(0.99, colourMatrix[2, 2]);
        }

        [Fact]
        public void GetColourMatrixReturnsNullOnIncompleteMatrix()
        {
            var config = EdhmConfig.FromFile(_gif.D3DXIni.FullName)!;

            var colourMatrix = config.GetColourMatrix()!;
            Assert.NotNull(colourMatrix);

            config.Constants["y151"] = double.NaN;
            Assert.Null(config.GetColourMatrix());

            config.Constants.Remove("y151");
            Assert.Null(config.GetColourMatrix());
        }

        [Fact]
        public void ReturnsNullWhenTheConfigFileIsMissingOrEmpty()
        {
            using var dir = new TestFolder();
            var config = EdhmConfig.FromFile(dir.Resolve("NonExistingFile.ini"));
            Assert.Null(config);

            dir.WriteText("EmptyProfile.ini", string.Empty);

            config = EdhmConfig.FromFile(dir.Resolve("EmptyProfile.ini"));
            Assert.Null(config);
        }

        [Fact]
        public void ReturnsNullWhenTheConfigFileIsAMalformedIniFile()
        {
            using var dir = new TestFolder();
            dir.WriteText("MalformedProfile.ini", "[SectionName\r\nKeyValue\r\n");

            var config = EdhmConfig.FromFile(dir.Resolve("MalformedProfile.ini"));
            Assert.Null(config);
        }

        [Fact]
        public void IgnoresIniSectionsOtherThanTheConstantsSection()
        {
            using var dir = new TestFolder();
            dir.WriteText("NoConstantsProfile.ini", "[SectionName]\r\nKey=Value\r\n");

            var config = EdhmConfig.FromFile(dir.Resolve("NoConstantsProfile.ini"))!;
            Assert.NotNull(config);

            Assert.Empty(config.Constants);
        }

        [Fact]
        public void IgnoresDuplicateConfigEntries()
        {
            using var dir = new TestFolder();
            dir.WriteText("DuplicateKeysProfile.ini", "[Constants]\r\nKey=1\r\nKey=2\r\n");

            var config = EdhmConfig.FromFile(dir.Resolve("DuplicateKeysProfile.ini"))!;
            Assert.NotNull(config);

            Assert.NotEmpty(config.Constants);
            Assert.Equal(1, config.Constants["Key"]);
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using var watcher = new EdhmConfigWatcher(_gif);
            var evs = new EventCollector<EdhmConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.NotNull(config);
        }

        [Fact]
        public async Task WatchesForChangesInTheEdhmConfigurationFiles()
        {
            using var dirMain = new TestFolder(_gif.FullName);
            using var watcher = new EdhmConfigWatcher(new GameInstallFolder(dirMain.Name));
            watcher.Start();

            var includesWatcher = watcher.GetPrivateField<EliteFileSystemWatcher>("_includesWatcher")!;
            var includedFiles = watcher.GetPrivateField<HashSet<string>>("_files")!;

            Assert.Equal(dirMain.Resolve("EDHM-ini"), includesWatcher.Path, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(3, includedFiles.Count);
            Assert.All(
                new[]
                {
                    dirMain.Resolve(@"EDHM-ini\35aac.ini"),
                    dirMain.Resolve(@"EDHM-ini\XML-Profile.ini"),
                    dirMain.Resolve(@"EDHM-ini\3rdPartyMods\Keybindings.ini"),
                },
                x => Assert.Contains(x, includedFiles));

            var evs = new EventCollector<EdhmConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatchesForChangesInTheEdhmConfigurationFiles));

            string edhmProfile = dirMain.ReadText(_d3dxIniFile);

            var config = await evs.WaitAsync(() => dirMain.WriteText(_d3dxIniFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Null(config);
            Assert.Empty(includedFiles);

            config = await evs.WaitAsync(() => dirMain.WriteText(_d3dxIniFile, "[Constants]"), 100).ConfigureAwait(false);
            Assert.Null(config!.GetColourMatrix());
            Assert.Empty(includedFiles);

            config = await evs.WaitAsync(() => dirMain.WriteText(_d3dxIniFile, edhmProfile)).ConfigureAwait(false);
            Assert.Equal(0.99, config!.GetColourMatrix()![0, 0]);

            config = await evs.WaitAsync(() => dirMain.WriteText(@"EDHM-ini\UNWATCHED.ini", edhmProfile), 100).ConfigureAwait(false);
            Assert.Null(config);

            config = await evs.WaitAsync(() => dirMain.WriteText(@"EDHM-ini\XML-Profile.ini", string.Empty), 100).ConfigureAwait(false);
            Assert.Null(config!.GetColourMatrix());
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new EdhmConfigWatcher(_gif);

            bool IsRunning()
            {
                return watcher.GetPrivateField<bool>("_running");
            }

            Assert.False(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Start();
            Assert.True(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());

            watcher.Stop();
            Assert.False(IsRunning());
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new EdhmConfigWatcher(_gif);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }
    }
}
