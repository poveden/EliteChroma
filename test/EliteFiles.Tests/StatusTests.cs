using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using EliteFiles.Status;
using TestUtils;
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
            Assert.Equal(5880918996, status.Balance);
            Assert.Equal(-12.955701, status.Latitude);
            Assert.Equal(6.249895, status.Longitude);
            Assert.Equal(49, status.Heading);
            Assert.Equal(2516467, status.Altitude);
            Assert.Equal("Shinrarta Dezhra A 1", status.BodyName);
            Assert.Equal(5635897, status.PlanetRadius);
            Assert.NotNull(status.Destination);
            Assert.Equal(671491302809, status.Destination!.System);
            Assert.Equal(25, status.Destination.Body);
            Assert.Equal("RotslerArsenal", status.Destination.Name);
            Assert.Equal("Rotsler Arsenal", status.Destination.NameLocalized);
            Assert.Equal(2, status.AdditionalFields.Count);
            Assert.Equal("Energylink", status.AdditionalFields["SelectedWeapon_Localised"].ToString());
            Assert.Equal("AdditionalValue1", status.AdditionalFields["AdditionalField1"].ToString());
        }

        [Fact]
        public void ReturnsNullWhenTheStatusFileIsEmpty()
        {
            using var dir = new TestFolder();
            dir.WriteText("Status.json", string.Empty);

            var status = StatusEntry.FromFile(dir.Resolve("Status.json"));

            Assert.Null(status);
        }

        [Theory]
        [InlineData("", OnFootWeapon.Kind.Unknown)]
        [InlineData("DUMMY-NAME", OnFootWeapon.Kind.Unknown)]
        [InlineData("humanoid_fists", OnFootWeapon.Kind.Unarmed)]
        [InlineData("humanoid_rechargetool", OnFootWeapon.Kind.Energylink)]
        [InlineData("humanoid_companalyser", OnFootWeapon.Kind.ProfileAnalyser)]
        [InlineData("humanoid_sampletool", OnFootWeapon.Kind.GeneticSampler)]
        [InlineData("humanoid_repairtool", OnFootWeapon.Kind.ArcCutter)]
        [InlineData("wpn_m_assaultrifle_kinetic_fauto", OnFootWeapon.Kind.Weapon)]
        [InlineData("wpn_m_assaultrifle_plasma_fauto", OnFootWeapon.Kind.Weapon)]
        [InlineData("wpn_s_pistol_plasma_charged", OnFootWeapon.Kind.Weapon)]
        [InlineData("wpn_m_assaultrifle_laser_fauto", OnFootWeapon.Kind.Weapon)]
        public void SelectedWeaponIsParsedCorrectly(string weaponName, OnFootWeapon.Kind expectedKind)
        {
            var kind = OnFootWeapon.GetKind(weaponName);
            Assert.Equal(expectedKind, kind);

            string placeholder = $"${weaponName}_name;";

            var entry = JsonSerializer.Deserialize<StatusEntry>($"{{ \"event\":\"Status\", \"SelectedWeapon\":\"{placeholder}\" }}")!;
            Assert.Equal(placeholder, entry.SelectedWeapon);

            kind = OnFootWeapon.GetKind(entry.SelectedWeapon);
            Assert.Equal(expectedKind, kind);
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
            });

            Assert.Equal("Status", status!.Event);
        }

        [Fact]
        public async Task WatchesForChangesInTheStatusFile()
        {
            using var dir = new TestFolder(_jf.FullName);
            using var watcher = new StatusWatcher(new JournalFolder(dir.Name));
            watcher.Start();

            var ec = new EventCollector<StatusEntry>(h => watcher.Changed += h, h => watcher.Changed -= h, nameof(WatchesForChangesInTheStatusFile));

            var status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"One\"}\r\n"));
            Assert.Equal("One", status!.Event);

            status = await ec.WaitAsync(() => dir.WriteText("Status.json", string.Empty), 100);
            Assert.Null(status);

            status = await ec.WaitAsync(() => dir.WriteText("Status.json", "{\"event\":\"Two\"}\r\n"));
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

        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP016:Don't use disposed instance.", Justification = "IDisposable test")]
        [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP017:Prefer using.", Justification = "IDisposable test")]
        [SuppressMessage("Major Code Smell", "S3966:Objects should not be disposed more than once", Justification = "IDisposable test")]
        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new StatusWatcher(_jf);
            Assert.False(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));

            watcher.Dispose();
            Assert.True(watcher.GetPrivateField<bool>("_disposed"));
        }
    }
}
