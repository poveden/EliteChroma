using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class JournalTest
    {
        private const string _journalFolder = @"TestFiles\Journal";
        private const string _journalFile1 = "Journal.190101020000.01.log";

        [Fact]
        public void ReadsJournalEntriesFromAJournalFile()
        {
            var file = Path.GetFullPath(Path.Combine(_journalFolder, _journalFile1));

            var entries = new List<JournalEntry>();

            using (var jr = new JournalReader(file))
            {
                Assert.Equal(file, jr.Name);

                JournalEntry entry;
                while ((entry = jr.ReadEntry()) != null)
                {
                    entries.Add(entry);
                }
            }

            Assert.Equal(5, entries.Count);
            Assert.IsType<FileHeader>(entries[0]);
            Assert.IsType<Music>(entries[1]);
            Assert.IsType<StartJump>(entries[2]);
            Assert.IsType<JournalEntry>(entries[3]);
            Assert.IsType<Shutdown>(entries[4]);
            Assert.Equal("AdditionalValue1", entries[4].AdditionalFields["AdditionalField1"]);
        }

        [Fact]
        public void ReaderThrowsOnAJournalEntryBiggerThanTheInternalBuffer()
        {
            using var dir = new TestFolder();
            var file = "Journal.entry-too-big.log";
            var body = $"{{ \"event\":\"One\" }}\r\n{{ \"event\":\"{new string('A', 33000)}\" }}\r\n";
            dir.WriteText(file, body);

            using var jr = new JournalReader(dir.Resolve(file));
            var entry = jr.ReadEntry();
            Assert.Equal("One", entry.Event);

            var ex = Assert.Throws<InvalidDataException>(() => jr.ReadEntry());
            Assert.Equal("Entry too large found in journal 'Journal.entry-too-big.log' at position 19.", ex.Message);
        }

        [Fact]
        public async Task WatcherRaisesEventsForHistoricalEntriesOnStart()
        {
            using var watcher = new JournalWatcher(_journalFolder);
            var ecEntries = new EventCollector<JournalEntry>(h => watcher.EntryAdded += h, h => watcher.EntryAdded -= h);
            var ecReady = new EventCollector<EventArgs>(h => watcher.Started += h, h => watcher.Started -= h);

            var readyTask = ecReady.WaitAsync(() => { });
            var entries = await ecEntries.WaitAsync(5, () =>
            {
                watcher.Start();
                Assert.False(watcher.IsWatching);
            }).ConfigureAwait(false);

            var ready = await readyTask.ConfigureAwait(false);
            Assert.NotNull(ready);
            Assert.True(watcher.IsWatching);

            watcher.Stop();
            Assert.False(watcher.IsWatching);

            Assert.Equal(5, entries.Count);
            Assert.IsType<FileHeader>(entries[0]);
            Assert.IsType<Music>(entries[1]);
            Assert.IsType<StartJump>(entries[2]);
            Assert.IsType<JournalEntry>(entries[3]);
            Assert.IsType<Shutdown>(entries[4]);
        }

        [Fact]
        public async Task WatcherRaisesEventsForNewJournalEntries()
        {
            using var dir = new TestFolder(_journalFolder);
            using var watcher = new JournalWatcher(dir.Name);
            var ecEntries = new EventCollector<JournalEntry>(h => watcher.EntryAdded += h, h => watcher.EntryAdded -= h);
            var ecReady = new EventCollector<EventArgs>(h => watcher.Started += h, h => watcher.Started -= h);

            var ev = await ecReady.WaitAsync(watcher.Start).ConfigureAwait(false);
            Assert.NotNull(ev);
            Assert.True(watcher.IsWatching);

            ev = await ecReady.WaitAsync(watcher.Start, 100).ConfigureAwait(false);
            Assert.Null(ev);

            var entry = await ecEntries.WaitAsync(() => dir.WriteText(_journalFile1, "{ \"event\":\"One\" }\r\n", true)).ConfigureAwait(false);
            Assert.Equal("One", entry.Event);

            var file2 = _journalFile1.Replace(".01.log", ".02.log", StringComparison.Ordinal);
            entry = await ecEntries.WaitAsync(() => dir.WriteText(file2, "{ \"event\":\"Two\" }\r\n")).ConfigureAwait(false);
            Assert.Equal("Two", entry.Event);

            watcher.Stop();
            Assert.False(watcher.IsWatching);
        }

        [Fact]
        public void WatcherThrowsWhenTheJournalFolderIsNotAValidJournalFolder()
        {
            var ex = Assert.Throws<ArgumentException>(() => { using var x = new JournalWatcher(@"TestFiles"); });
            Assert.Contains("' is not a valid Elite:Dangerous journal folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new JournalWatcher(_journalFolder);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
