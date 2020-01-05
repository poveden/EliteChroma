using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class GameStateWatcherTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";
        private const string _journalFolder = @"TestFiles\Journal";

        [Fact]
        public async Task RaisesEventsForEachGameStateChange()
        {
            using var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder);
            var evs = new EventCollector<EventArgs>(h => watcher.Changed += h, h => watcher.Changed -= h);

            await evs.WaitAsync(10, watcher.Start, 5000).ConfigureAwait(false);
            watcher.Stop();
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new GameStateWatcher(_gameRootFolder, _gameOptionsFolder, _journalFolder);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
