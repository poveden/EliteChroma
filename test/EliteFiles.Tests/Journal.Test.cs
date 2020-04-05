using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using EliteFiles.Tests.Internal;
using Newtonsoft.Json;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class JournalTest
    {
        private const string _journalFolder = @"TestFiles\Journal";
        private const string _journalFile1 = "Journal.190101020000.01.log";

        private readonly JournalFolder _jf;

        public JournalTest()
        {
            _jf = new JournalFolder(_journalFolder);
        }

        [Fact]
        public void ReadsJournalEntriesFromAJournalFile()
        {
            var file = Path.GetFullPath(Path.Combine(_jf.FullName, _journalFile1));

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

            var e0 = Assert.IsType<FileHeader>(entries[0]);
            Assert.Equal(1, e0.Part);
            Assert.Equal("English\\UK", e0.Language);
            Assert.Equal("3.5.0.200 EDH", e0.GameVersion);
            Assert.Equal("r210198/r0 ", e0.Build);

            var e1 = Assert.IsType<Music>(entries[1]);
            Assert.Equal("NoTrack", e1.MusicTrack);

            var e2 = Assert.IsType<StartJump>(entries[2]);
            Assert.Equal(StartJump.FsdJumpType.Hyperspace, e2.JumpType);
            Assert.Equal("Wolf 1301", e2.StarSystem);
            Assert.Equal(1458242032322, e2.SystemAddress);
            Assert.Equal("G", e2.StarClass);

            Assert.IsType<JournalEntry>(entries[3]);
            Assert.IsType<Shutdown>(entries[4]);
            Assert.Equal(new DateTimeOffset(2019, 1, 1, 0, 19, 4, TimeSpan.Zero), entries[4].Timestamp);
            Assert.Equal("AdditionalValue1", entries[4].AdditionalFields["AdditionalField1"]);
        }

        [Theory]
        [InlineData("", null, StarClass.Kind.Unknown)]
        [InlineData("DUMMY-CLASS", null, StarClass.Kind.Unknown)]
        [InlineData("O", StarClass.O, StarClass.Kind.MainSequence)]
        [InlineData("B", StarClass.B, StarClass.Kind.MainSequence)]
        [InlineData("A", StarClass.A, StarClass.Kind.MainSequence)]
        [InlineData("F", StarClass.F, StarClass.Kind.MainSequence)]
        [InlineData("G", StarClass.G, StarClass.Kind.MainSequence)]
        [InlineData("K", StarClass.K, StarClass.Kind.MainSequence)]
        [InlineData("M", StarClass.M, StarClass.Kind.MainSequence)]
        [InlineData("K_OrangeGiant", StarClass.K, StarClass.Kind.MainSequence)]
        [InlineData("L", StarClass.L, StarClass.Kind.BrownDwarf)]
        [InlineData("T", StarClass.T, StarClass.Kind.BrownDwarf)]
        [InlineData("Y", StarClass.Y, StarClass.Kind.BrownDwarf)]
        [InlineData("AeBe", StarClass.HerbigAeBe, StarClass.Kind.Protostar)]
        [InlineData("TTS", StarClass.TTauri, StarClass.Kind.Protostar)]
        [InlineData("C", StarClass.C, StarClass.Kind.Carbon)]
        [InlineData("CH", StarClass.C, StarClass.Kind.Carbon)]
        [InlineData("CHd", StarClass.C, StarClass.Kind.Carbon)]
        [InlineData("MS", StarClass.MS, StarClass.Kind.Carbon)]
        [InlineData("S", StarClass.S, StarClass.Kind.Carbon)]
        [InlineData("W", StarClass.W, StarClass.Kind.WolfRayet)]
        [InlineData("WC", StarClass.W, StarClass.Kind.WolfRayet)]
        [InlineData("WNC", StarClass.W, StarClass.Kind.WolfRayet)]
        [InlineData("H", StarClass.BlackHole, StarClass.Kind.BlackHole)]
        [InlineData("SupermassiveBlackHole", StarClass.BlackHole, StarClass.Kind.BlackHole)]
        [InlineData("N", StarClass.Neutron, StarClass.Kind.Neutron)]
        [InlineData("D", StarClass.D, StarClass.Kind.WhiteDwarf)]
        [InlineData("DA", StarClass.D, StarClass.Kind.WhiteDwarf)]
        [InlineData("DAB", StarClass.D, StarClass.Kind.WhiteDwarf)]
        [InlineData("X", StarClass.Exotic, StarClass.Kind.Other)]
        [InlineData("RoguePlanet", StarClass.RoguePlanet, StarClass.Kind.Other)]
        [InlineData("Nebula", StarClass.Nebula, StarClass.Kind.Other)]
        [InlineData("StellarRemnantNebula", StarClass.StellarRemnantNebula, StarClass.Kind.Other)]
        public void StarClassIsParsedCorrectly(string starClass, string expectedBaseClass, StarClass.Kind expectedKind)
        {
            var entry = (StartJump)JsonConvert.DeserializeObject<JournalEntry>($"{{ \"event\":\"StartJump\", \"StarClass\":\"{starClass}\" }}");
            Assert.Equal(starClass, entry.StarClass);

            var kind = StarClass.GetKind(entry.StarClass, out var baseClass);
            Assert.Equal(expectedBaseClass, baseClass);
            Assert.Equal(expectedKind, kind);
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
            using var watcher = new JournalWatcher(_jf);
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
            using var dir = new TestFolder(_jf.FullName);
            using var watcher = new JournalWatcher(new JournalFolder(dir.Name));
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
            Assert.Throws<ArgumentNullException>(() => { using var x = new JournalWatcher(null); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new JournalWatcher(new JournalFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous journal folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new JournalWatcher(_jf);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
