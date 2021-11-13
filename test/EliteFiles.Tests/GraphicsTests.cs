using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EliteFiles.Graphics;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class GraphicsTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";

        private const string _mainFile = "GraphicsConfiguration.xml";
        private const string _overrideFile = @"Graphics\GraphicsConfigurationOverride.xml";

        private const string _minimalConfig = "<?xml version=\"1.0\" ?>\r\n<GraphicsConfig />";

        private readonly GameInstallFolder _gif;
        private readonly GameOptionsFolder _gof;

        public GraphicsTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
            _gof = new GameOptionsFolder(_gameOptionsFolder);
        }

        [Fact]
        public void CanCreateCompleteGraphicsConfigInstances()
        {
            var config = new GraphicsConfig
            {
                GuiColour =
                {
                    ["Default"] = new GuiColourMatrix
                    {
                        LocalisationName = "Standard",
                        MatrixRed = { Red = 1, Green = 0, Blue = 0, },
                        MatrixGreen = { Red = 0, Green = 1, Blue = 0, },
                        MatrixBlue = { Red = 0, Green = 0, Blue = 1, },
                    },
                    ["Other"] = new GuiColourMatrix
                    {
                        LocalisationName = "Other",
                        [2, 0] = 1,
                    },
                },
            };

            Assert.Equal(1, config.GuiColour.Default![0, 0]);
            Assert.Equal(1, config.GuiColour["Other"][2, 0]);
        }

        [Fact]
        public void DeserializesGraphicsConfigFiles()
        {
            var config = GraphicsConfig.FromFile(_gif.GraphicsConfiguration.FullName)!;

            Assert.NotNull(config);
            Assert.Equal(3, config.GuiColour!.Count);

            var gc = config.GuiColour.Default!;
            Assert.NotNull(gc);
            Assert.Equal("Standard", gc.LocalisationName);

            Assert.Equal(1, gc.MatrixRed.Red);
            Assert.Equal(1, gc.MatrixGreen.Green);
            Assert.Equal(1, gc.MatrixBlue.Blue);

            var cm = (IRgbTransformMatrix)gc;

            Assert.Equal(1, cm[0, 0]);
            Assert.Equal(1, cm[1, 1]);
            Assert.Equal(1, cm[2, 2]);
        }

        [Fact]
        public void ReturnsNullWhenTheGraphicsConfigFileIsMissingOrEmpty()
        {
            using var dir = new TestFolder();
            var status = GraphicsConfig.FromFile(dir.Resolve("NonExistingFile.xml"));
            Assert.Null(status);

            dir.WriteText("EmptyConfig.xml", string.Empty);

            status = GraphicsConfig.FromFile(dir.Resolve("EmptyConfig.xml"));
            Assert.Null(status);
        }

        [Fact]
        public void ReturnsNullWhenTheGraphicsConfigFileHasMalformedXml()
        {
            using var dir = new TestFolder();
            dir.WriteText("MalformedConfig.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><GraphicsConfig>");

            var status = GraphicsConfig.FromFile(dir.Resolve("MalformedConfig.xml"));
            Assert.Null(status);
        }

        [Fact]
        public void DeserializesMinimalGraphicsConfigFiles()
        {
            using var dir = new TestFolder();
            dir.WriteText("MinimalConfig.xml", _minimalConfig);

            var status = GraphicsConfig.FromFile(dir.Resolve("MinimalConfig.xml"))!;
            Assert.Empty(status.GuiColour);
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using var watcher = new GraphicsConfigWatcher(_gif, _gof);
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Null(config!.GuiColour!.Default!.LocalisationName);
        }

        [Fact]
        public async Task WatchesForChangesInTheGraphicsConfigurationFiles()
        {
            using var dirMain = new TestFolder(_gif.FullName);
            using var dirOpts = new TestFolder(_gof.FullName);
            using var watcher = new GraphicsConfigWatcher(new GameInstallFolder(dirMain.Name), new GameOptionsFolder(dirOpts.Name));
            watcher.Start();

            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatchesForChangesInTheGraphicsConfigurationFiles));

            string xmlMain = dirMain.ReadText(_mainFile);

            var config = await evs.WaitAsync(() => dirMain.WriteText(_mainFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Null(config);

            config = await evs.WaitAsync(() => dirMain.WriteText(_mainFile, xmlMain)).ConfigureAwait(false);
            Assert.Equal(0, config!.GuiColour!.Default![0, 0]);

            config = await evs.WaitAsync(() => dirOpts.WriteText(_overrideFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Equal(1, config!.GuiColour!.Default![0, 0]);

            config = await evs.WaitAsync(() => dirOpts.WriteText(_overrideFile, _minimalConfig)).ConfigureAwait(false);
            Assert.Equal(1, config!.GuiColour!.Default![0, 0]);
        }

        [Fact]
        public async Task WatcherToleratesMissingGraphicsConfigurationOverrideFiles()
        {
            using var dirOpts = new TestFolder(_gof.FullName);
            dirOpts.DeleteFile(_overrideFile);

            var gof = new GameOptionsFolder(dirOpts.Name);
            Assert.True(gof.IsValid);
            Assert.False(gof.GraphicsConfigurationOverride.Exists);

            using var watcher = new GraphicsConfigWatcher(_gif, gof);
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Standard", config!.GuiColour!.Default!.LocalisationName);
        }

        [Fact]
        public async Task WatcherToleratesEmptyGraphicsConfigurationOverrideFiles()
        {
            using var dirOpts = new TestFolder(_gof.FullName);
            dirOpts.WriteText(_overrideFile, string.Empty);

            using var watcher = new GraphicsConfigWatcher(_gif, new GameOptionsFolder(dirOpts.Name));
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Standard", config!.GuiColour!.Default!.LocalisationName);
        }

        [Fact]
        public async Task WatcherToleratesMalformedGraphicsConfigurationOverrideFiles()
        {
            using var dirOpts = new TestFolder(_gof.FullName);
            dirOpts.WriteText(_overrideFile, "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><GraphicsConfig>");

            using var watcher = new GraphicsConfigWatcher(_gif, new GameOptionsFolder(dirOpts.Name));
            var evs = new EventCollector<GraphicsConfig>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var config = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Standard", config!.GuiColour!.Default!.LocalisationName);
        }

        [Fact]
        public void WatcherThrowsWhenTheGameInstallFolderIsNotAValidInstallFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new GraphicsConfigWatcher(null!, _gof); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new GraphicsConfigWatcher(new GameInstallFolder(@"TestFiles"), _gof); });
            Assert.Contains("' is not a valid Elite:Dangerous game install folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherThrowsWhenTheGameOptionsFolderIsNotAValidOptionsFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new GraphicsConfigWatcher(_gif, null!); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new GraphicsConfigWatcher(_gif, new GameOptionsFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous game options folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new GraphicsConfigWatcher(_gif, _gof);

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
            var watcher = new GraphicsConfigWatcher(_gif, _gof);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(-1, -1)]
        [InlineData(-2, -1)]
        public void GuiColourMatrixEntryClampsValuesToTheMinus1Plus1Range(double value, double expected)
        {
            var entry = new GuiColourMatrixEntry { Red = value, Green = 0.1, Blue = 0.2 };
            Assert.Equal(expected, entry.Red);

            entry = new GuiColourMatrixEntry { Red = 0.3, Green = value, Blue = 0.4 };
            Assert.Equal(expected, entry.Green);

            entry = new GuiColourMatrixEntry { Red = 0.5, Green = 0.6, Blue = value };
            Assert.Equal(expected, entry.Blue);
        }
    }
}
