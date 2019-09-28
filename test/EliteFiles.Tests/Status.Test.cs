using System;
using System.IO;
using System.Threading.Tasks;
using EliteFiles.Status;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class StatusTest
    {
        private const string _journalFolder = @"TestFiles\Journal";

        [Fact]
        public void DeserializesStatusFiles()
        {
            var status = StatusEntry.FromFile(Path.Combine(_journalFolder, "Status.json"));

            Assert.NotNull(status);
            Assert.Equal("Status", status.Event);
            Assert.True(status.HasFlag(Flags.Docked));
            Assert.Equal(4, status.Pips.Sys);
            Assert.Equal(8, status.Pips.Eng);
            Assert.Equal(0, status.Pips.Wep);
            Assert.Equal(1, status.AdditionalFields.Count);
            Assert.Equal("AdditionalValue1", status.AdditionalFields["AdditionalField1"]);
        }

        [Fact]
        public void ReturnsNullWhenTheStatusFileIsEmpty()
        {
            using (var dir = new TestFolder())
            {
                dir.WriteText("Status.json", string.Empty);

                var status = StatusEntry.FromFile(dir.Resolve("Status.json"));

                Assert.Null(status);
            }
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using (var watcher = new StatusWatcher(_journalFolder))
            {
                var ecs = new EventCollector<StatusEntry>(h => watcher.Changed += h, h => watcher.Changed -= h);

                var status = await ecs.WaitAsync(() =>
                {
                    watcher.Start();
                    watcher.Stop();
                });

                Assert.Equal("Status", status.Event);
            }
        }

        [Fact]
        public async Task WatchesForChangesInTheStatusFile()
        {
            using (var dir = new TestFolder(_journalFolder))
            {
                using (var watcher = new StatusWatcher(dir.Name))
                {
                    watcher.Start();

                    var ec = new EventCollector<StatusEntry>(h => watcher.Changed += h, h => watcher.Changed -= h);

                    var status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"One\"}\r\n"));
                    Assert.Equal("One", status.Event);

                    status = await ec.WaitAsync(() => dir.WriteText("Status.json", string.Empty), 100);
                    Assert.Null(status);

                    status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"Two\"}\r\n"));
                    Assert.Equal("Two", status.Event);
                }
            }
        }

        [Fact]
        public void WatcherThrowsWhenTheStatusFolderIsNotAValidJournalFolder()
        {
            var ex = Assert.Throws<ArgumentException>(() => new StatusWatcher(@"TestFiles"));
            Assert.Contains("' is not a valid Elite:Dangerous journal folder.", ex.Message);
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new StatusWatcher(_journalFolder);
            watcher.Dispose();
            watcher.Dispose();
        }
    }
}
