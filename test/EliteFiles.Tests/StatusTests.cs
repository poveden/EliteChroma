using System;
using System.Threading.Tasks;
using EliteFiles.Status;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    public sealed class StatusTests
    {
        private const string _journalFolder = @"TestFiles\Journal";

        private readonly JournalFolder _jf;

        public StatusTests()
        {
            _jf = new JournalFolder(_journalFolder);
        }

        [Fact]
        public void DeserializesStatusFiles()
        {
            var status = StatusEntry.FromFile(_jf.Status.FullName)!;

            Assert.NotNull(status);
            Assert.Equal(new DateTimeOffset(2019, 1, 1, 0, 0, 39, TimeSpan.Zero), status.Timestamp);
            Assert.Equal("Status", status.Event);
            Assert.True(status.HasFlag(Flags.Docked));
            Assert.True(status.HasFlag(Flags2.OnFootExterior));
            Assert.Equal(1, status.Oxygen);
            Assert.Equal(1, status.Health);
            Assert.Equal(57.688763, status.Temperature);
            Assert.Equal("$humanoid_rechargetool_name;", status.SelectedWeapon);
            Assert.Equal(0.483871, status.Gravity);
            Assert.Equal(4, status.Pips!.Sys);
            Assert.Equal(8, status.Pips.Eng);
            Assert.Equal(0, status.Pips.Wep);
            Assert.Equal((byte)0, status.FireGroup);
            Assert.Equal(GuiFocus.None, status.GuiFocus);
            Assert.Equal(32, status.Fuel!.FuelMain);
            Assert.Equal(0.63, status.Fuel.FuelReservoir);
            Assert.Equal(0, status.Cargo);
            Assert.Equal(LegalState.Clean, status.LegalState);
            Assert.Equal(-12.955701, status.Latitude);
            Assert.Equal(6.249895, status.Longitude);
            Assert.Equal(49, status.Heading);
            Assert.Equal(2516467, status.Altitude);
            Assert.Equal("Shinrarta Dezhra A 1", status.BodyName);
            Assert.Equal(5635897, status.PlanetRadius);
            Assert.Equal(2, status.AdditionalFields.Count);
            Assert.Equal("Energylink", status.AdditionalFields["SelectedWeapon_Localised"]);
            Assert.Equal("AdditionalValue1", status.AdditionalFields["AdditionalField1"]);
        }

        [Fact]
        public void ReturnsNullWhenTheStatusFileIsEmpty()
        {
            using var dir = new TestFolder();
            dir.WriteText("Status.json", string.Empty);

            var status = StatusEntry.FromFile(dir.Resolve("Status.json"));

            Assert.Null(status);
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using var watcher = new StatusWatcher(_jf);
            var ecs = new EventCollector<StatusEntry>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatcherRaisesTheChangedEventOnStart));

            var status = await ecs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Status", status!.Event);
        }

        [Fact]
        public async Task WatchesForChangesInTheStatusFile()
        {
            using var dir = new TestFolder(_jf.FullName);
            using var watcher = new StatusWatcher(new JournalFolder(dir.Name));
            watcher.Start();

            var ec = new EventCollector<StatusEntry>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatchesForChangesInTheStatusFile));

            var status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"One\"}\r\n")).ConfigureAwait(false);
            Assert.Equal("One", status!.Event);

            status = await ec.WaitAsync(() => dir.WriteText("Status.json", string.Empty), 100).ConfigureAwait(false);
            Assert.Null(status);

            status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"Two\"}\r\n")).ConfigureAwait(false);
            Assert.Equal("Two", status!.Event);
        }

        [Fact]
        public void WatcherThrowsWhenTheStatusFolderIsNotAValidJournalFolder()
        {
            Assert.Throws<ArgumentNullException>(() => { using var x = new StatusWatcher(null!); });

            var ex = Assert.Throws<ArgumentException>(() => { using var x = new StatusWatcher(new JournalFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous journal folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void StartAndStopAreNotReentrant()
        {
            using var watcher = new StatusWatcher(_jf);

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

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new StatusWatcher(_jf);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }
    }
}
