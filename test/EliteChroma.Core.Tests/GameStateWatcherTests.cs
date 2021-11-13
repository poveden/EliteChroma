using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite;
using Xunit;
using static EliteChroma.Elite.GameStateWatcher;

namespace EliteChroma.Core.Tests
{
    public class GameStateWatcherTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        [Fact]
        public void RaisesEventsForEachGameStateChange()
        {
            var expected = new[]
            {
                ChangeType.StatusEntry,
                ChangeType.BindingPreset,
                ChangeType.GraphicsConfig,
                ChangeType.DeviceKeySet,
                ChangeType.GameProcessState,
                ChangeType.JournalEntry,
                ChangeType.JournalEntry,
                ChangeType.JournalEntry,
                ChangeType.JournalEntry,
                ChangeType.JournalDrain,
            };

            using var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder)
            {
                RaisePreStartupEvents = true,
            };

            var evs = new EventCollector<ChangeType>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(RaisesEventsForEachGameStateChange));

            var events = evs.Wait(expected.Length, watcher.Start, 5000);
            watcher.Stop();

            var mismatches = expected.Except(events);
            Assert.Empty(mismatches);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder);

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
            var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }

        [Fact]
        public void OnChangedIsNotReentrant()
        {
            using var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder)
            {
                RaisePreStartupEvents = false,
            };

            var evs = new EventCollector<ChangeType>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(OnChangedIsNotReentrant));
            evs.Wait(watcher.Start, 5000);

            int nOnChangedCalls = 0;
            using var mre = new ManualResetEventSlim();

            watcher.Changed += (sender, e) =>
            {
                Interlocked.Increment(ref nOnChangedCalls);
                mre.Wait();
            };

            void OnChanged()
            {
                watcher.InvokePrivateMethod<object>("OnChanged", ChangeType.JournalDrain);
                mre.Set();
            }

            Task.WaitAll(new[]
            {
                Task.Run(OnChanged),
                Task.Run(OnChanged),
            });

            watcher.Stop();

            Assert.Equal(1, nOnChangedCalls);
        }

        [Fact]
        public void EnUSOverrideIsAppliedInTheGameState()
        {
            using var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder);

            Assert.False(watcher.ForceEnUSKeyboardLayout);
            Assert.False(watcher.GetPrivateField<GameState>("_gameState")!.ForceEnUSKeyboardLayout);
            Assert.False(watcher.GetGameStateSnapshot().ForceEnUSKeyboardLayout);

            watcher.ForceEnUSKeyboardLayout = true;
            Assert.True(watcher.ForceEnUSKeyboardLayout);
            Assert.True(watcher.GetPrivateField<GameState>("_gameState")!.ForceEnUSKeyboardLayout);
            Assert.True(watcher.GetGameStateSnapshot().ForceEnUSKeyboardLayout);
        }

        [Theory]
        [InlineData("EliteDangerous64.exe")]
        [InlineData("GraphicsConfiguration.xml")]
        [InlineData(@"ControlSchemes\*.binds")]
        public void DoesNotTolerateMissingFilesInTheGameRootFolder(string missingFilesPattern)
        {
            using var dirRoot = new TestFolder(_gameRootFolder);
            Assert.NotEqual(0, dirRoot.DeleteFiles(missingFilesPattern));

            Assert.Throws<ArgumentException>("gameInstallFolder", () => { using var gsw = new GameStateWatcher(dirRoot.Name, _gameOptionsFolder, _journalFolder); });
        }

        [Theory]
        [InlineData(@"Bindings\*.binds")]
        [InlineData(@"Bindings\StartPreset.start")]
        [InlineData(@"Graphics\GraphicsConfigurationOverride.xml")]
        public void ToleratesMissingFilesInTheGameOptionsFolder(string missingFilesPattern)
        {
            using var dirOpts = new TestFolder(_gameOptionsFolder);
            Assert.NotEqual(0, dirOpts.DeleteFiles(missingFilesPattern));

            using var gsw = new GameStateWatcher(_gameRootFolder, dirOpts.Name, _journalFolder);
        }

        [Theory]
        [InlineData("Status.json")]
        [InlineData("Journal.*.log")]
        public void DoesNotTolerateMissingFilesInTheJournalFolder(string missingFilesPattern)
        {
            using var dirJournal = new TestFolder(_journalFolder);
            Assert.NotEqual(0, dirJournal.DeleteFiles(missingFilesPattern));

            Assert.Throws<ArgumentException>("journalFolder", () => { using var gsw = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, dirJournal.Name); });
        }
    }
}
