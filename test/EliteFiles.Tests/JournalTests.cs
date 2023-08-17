using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using TestUtils;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class JournalTests
    {
        private const string _journalFolder = @"TestFiles\Journal";
        private const string _journalFile1 = "Journal.190101020000.01.log";
        private const int _journalFile1Count = 8;

        private readonly JournalFolder _jf;

        public JournalTests()
        {
            _jf = new JournalFolder(_journalFolder);
        }

        public static IEnumerable<object[]> AllJournalEntryTypes()
        {
            return typeof(JournalEntry).Assembly.GetExportedTypes()
                .Where(x => x.IsSubclassOf(typeof(JournalEntry)))
                .Select(x => new object[] { x });
        }

        [Fact]
        public void CanCreateCompleteJournalEntryInstance()
        {
            var entry = new TestEntry
            {
                Timestamp = DateTimeOffset.UtcNow,
                Event = "TestEntry",
                AdditionalFields =
                {
                    ["ExtraField"] = JsonDocument.Parse("\"Extra value\"").RootElement,
                },
            };

            Assert.Single(entry.AdditionalFields);
        }

        [Theory]
        [MemberData(nameof(AllJournalEntryTypes))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Always not null")]
        public void AllJournalEntryTypesHaveAJournalEntryAttribute(Type type)
        {
            var jea = (JournalEntryAttribute)type.GetCustomAttributes(typeof(JournalEntryAttribute), false).Single();

            Assert.NotNull(jea.EventName);
        }

        [Theory]
        [MemberData(nameof(AllJournalEntryTypes))]
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Always not null")]
        public void AllJournalEntryFieldsAreReadWritable(Type type)
        {
            var pNames = type.GetProperties()
                .Where(x => x.Name != nameof(JournalEntry.AdditionalFields) && (x.CanRead && x.GetGetMethod(false) != null) ^ (x.CanWrite && x.GetSetMethod(false) != null))
                .Select(x => x.Name)
                .ToList();

            Assert.Empty(pNames);
        }

        [Fact]
        public void ReadsJournalEntriesFromAJournalFile()
        {
            string file = Path.GetFullPath(Path.Combine(_jf.FullName, _journalFile1));

            var entries = new Queue<JournalEntry>();

            using (var jr = new JournalReader(file))
            {
                Assert.Equal(file, jr.Name);

                JournalEntry? entry;
                while ((entry = jr.ReadEntry()) != null)
                {
                    entries.Enqueue(entry);
                }
            }

            Assert.Equal(_journalFile1Count, entries.Count);

            var fh = Assert.IsType<FileHeader>(entries.Dequeue());
            Assert.Equal(1, fh.Part);
            Assert.Equal("English\\UK", fh.Language);
            Assert.True(fh.Odyssey);
            Assert.Equal("4.0.0.701", fh.GameVersion);
            Assert.Equal("r273365/r0 ", fh.Build);

            var lg = Assert.IsType<LoadGame>(entries.Dequeue());
            Assert.Equal("F123456", lg.FID);
            Assert.Equal("Jameson", lg.Commander);
            Assert.True(lg.Horizons);
            Assert.True(lg.Odyssey);
            Assert.Equal("Krait_Light", lg.Ship);
            Assert.Equal("Krait Phantom", lg.ShipLocalized);
            Assert.Equal(1, lg.ShipID);
            Assert.Equal("Nameless", lg.ShipName);
            Assert.Equal("ASD-FG", lg.ShipIdent);
            Assert.Equal(31.370001, lg.FuelLevel);
            Assert.Equal(32, lg.FuelCapacity);
            Assert.True(lg.StartLanded);
            Assert.False(lg.StartDead);
            Assert.Equal(LoadGame.PlayMode.Group, lg.GameMode);
            Assert.Equal("FleetComm", lg.Group);
            Assert.Equal(1234567890, lg.Credits);
            Assert.Equal(0, lg.Loan);
            Assert.Equal("English/UK", lg.Language);
            Assert.Equal("4.0.0.701", lg.GameVersion);
            Assert.Equal("r273365/r0 ", lg.Build);

            var mu = Assert.IsType<Music>(entries.Dequeue());
            Assert.Equal(Music.Track.NoTrack, mu.MusicTrack);

            var ua = Assert.IsType<UnderAttack>(entries.Dequeue());
            Assert.Equal(UnderAttack.AttackTarget.You, ua.Target);

            var ft = Assert.IsType<FsdTarget>(entries.Dequeue());
            Assert.Equal("Wolf 1301", ft.Name);
            Assert.Equal(1458242032322, ft.SystemAddress);
            Assert.Equal("G", ft.StarClass);
            Assert.Equal(1, ft.RemainingJumpsInRoute);

            var sj = Assert.IsType<StartJump>(entries.Dequeue());
            Assert.Equal(StartJump.FsdJumpType.Hyperspace, sj.JumpType);
            Assert.Equal("Wolf 1301", sj.StarSystem);
            Assert.Equal(1458242032322, sj.SystemAddress);
            Assert.Equal("G", sj.StarClass);

            Assert.IsType<JournalEntry>(entries.Dequeue());

            var sd = Assert.IsType<Shutdown>(entries.Dequeue());
            Assert.Equal(new DateTimeOffset(2019, 1, 1, 0, 19, 4, TimeSpan.Zero), sd.Timestamp);
            Assert.Equal("AdditionalValue1", sd.AdditionalFields["AdditionalField1"].GetString());
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
            using var dir = new TestFolder();
            string file = "Journal.entry-startjump.log";
            string body = $"{{ \"event\":\"StartJump\", \"StarClass\":\"{starClass}\" }}\r\n";
            dir.WriteText(file, body);

            using var jr = new JournalReader(dir.Resolve(file));
            var entry = jr.ReadEntry() as StartJump;
            Assert.NotNull(entry);
            Assert.Equal(starClass, entry!.StarClass);

            var kind = StarClass.GetKind(entry.StarClass, out string? baseClass);
            Assert.Equal(expectedBaseClass, baseClass);
            Assert.Equal(expectedKind, kind);
        }

        [Fact]
        public void ReaderThrowsOnAJournalEntryBiggerThanTheInternalBuffer()
        {
            using var dir = new TestFolder();
            string file = "Journal.entry-too-big.log";
            string body = $"{{ \"event\":\"One\" }}\r\n{{ \"event\":\"{new string('A', 132000)}\" }}\r\n";
            dir.WriteText(file, body);

            using var jr = new JournalReader(dir.Resolve(file));
            var entry = jr.ReadEntry()!;
            Assert.Equal("One", entry.Event);

            var ex = Assert.Throws<InvalidDataException>(() => jr.ReadEntry());
            Assert.Equal("Entry too large (greater than 131072 bytes) found in journal 'Journal.entry-too-big.log' at position 19.", ex.Message);
        }

        [Fact]
        public async Task WatcherRaisesEventsForHistoricalEntriesOnStart()
        {
            using var watcher = new JournalWatcher(_jf);
            var ecEntries = new EventCollector<JournalEntry>(h => watcher.EntryAdded += h, h => watcher.EntryAdded -= h, nameof(WatcherRaisesEventsForHistoricalEntriesOnStart));
            var ecReady = new EventCollector<EventArgs>(h => watcher.Started += h, h => watcher.Started -= h, nameof(WatcherRaisesEventsForHistoricalEntriesOnStart));

            var readyTask = ecReady.WaitAsync(() => { });
            var entries = new Queue<JournalEntry>(await ecEntries.WaitAsync(_journalFile1Count, () =>
            {
                watcher.Start();
                Assert.False(watcher.IsWatching);
            }).ConfigureAwait(false));

            var ready = await readyTask.ConfigureAwait(false);
            Assert.NotNull(ready);
            Assert.True(watcher.IsWatching);

            watcher.Stop();
            Assert.False(watcher.IsWatching);

            Assert.Equal(_journalFile1Count, entries.Count);
            Assert.IsType<FileHeader>(entries.Dequeue());
            Assert.IsType<LoadGame>(entries.Dequeue());
            Assert.IsType<Music>(entries.Dequeue());
            Assert.IsType<UnderAttack>(entries.Dequeue());
            Assert.IsType<FsdTarget>(entries.Dequeue());
            Assert.IsType<StartJump>(entries.Dequeue());
            Assert.IsType<JournalEntry>(entries.Dequeue());
            Assert.IsType<Shutdown>(entries.Dequeue());
        }

        [Fact]
        public async Task WatcherRaisesEventsForNewJournalEntries()
        {
            using var dir = new TestFolder(_jf.FullName);
            using var watcher = new JournalWatcher(new JournalFolder(dir.Name));
            var ecEntries = new EventCollector<JournalEntry>(h => watcher.EntryAdded += h, h => watcher.EntryAdded -= h, nameof(WatcherRaisesEventsForNewJournalEntries));
            var ecReady = new EventCollector<EventArgs>(h => watcher.Started += h, h => watcher.Started -= h, nameof(WatcherRaisesEventsForNewJournalEntries));

            var ev = await ecReady.WaitAsync(watcher.Start).ConfigureAwait(false);
            Assert.NotNull(ev);
            Assert.True(watcher.IsWatching);

            ev = await ecReady.WaitAsync(watcher.Start, 100).ConfigureAwait(false);
            Assert.Null(ev);

            var entry = await ecEntries.WaitAsync(() => dir.WriteText(_journalFile1, "{ \"event\":\"One\" }\r\n", true)).ConfigureAwait(false);
            Assert.Equal("One", entry!.Event);

            string file2 = _journalFile1.Replace(".01.log", ".02.log", StringComparison.Ordinal);
            entry = await ecEntries.WaitAsync(() => dir.WriteText(file2, "{ \"event\":\"Two\" }\r\n")).ConfigureAwait(false);
            Assert.Equal("Two", entry!.Event);

            watcher.Stop();
            Assert.False(watcher.IsWatching);
        }

        [Fact]
        public void WatcherThrowsWhenTheJournalFolderIsNotAValidJournalFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new JournalWatcher(null!); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new JournalWatcher(new JournalFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous journal folder.", ex.Message, StringComparison.Ordinal);
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new JournalWatcher(_jf);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void JournalReaderDoesNotThrowWhenDisposingTwice()
        {
            using var tf = new TestFolder(_journalFolder);
            var jr = new JournalReader(tf.Resolve(_journalFile1));
            Assert.False(jr.GetPrivateField<bool>("_disposed"));

            jr.Dispose();
            Assert.True(jr.GetPrivateField<bool>("_disposed"));

            jr.Dispose();
            Assert.True(jr.GetPrivateField<bool>("_disposed"));
        }

        [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Test class")]
        private class TestEntry : JournalEntry
        {
        }
    }
}
