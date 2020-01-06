using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using EliteFiles.Bindings;
using EliteFiles.Tests.Internal;
using Xunit;

namespace EliteFiles.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public sealed class BindingsTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";
        private const string _gameOptionsFolder = @"TestFiles\GameOptions";

        private const string _startPresetFile = @"Bindings\StartPreset.start";
        private const string _mainFile = @"ControlSchemes\Keyboard.binds";
        private const string _customFile = @"Bindings\Custom.3.0.binds";

        private readonly GameInstallFolder _gif;
        private readonly GameOptionsFolder _gof;

        public BindingsTest()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
            _gof = new GameOptionsFolder(_gameOptionsFolder);
        }

        [Fact]
        public void DeserializesBindingPresetsFiles()
        {
            var binds = BindingPreset.FromFile(Path.Combine(_gif.FullName, _mainFile));

            Assert.NotNull(binds);
            Assert.Equal("Keyboard", binds.PresetName);
            Assert.Null(binds.Version);
            Assert.Null(binds.KeyboardLayout);

            binds = BindingPreset.FromFile(Path.Combine(_gof.FullName, _customFile));

            Assert.NotNull(binds);
            Assert.Equal("Custom", binds.PresetName);
            Assert.Equal(new Version(3, 0), binds.Version);
            Assert.Equal("es-ES", binds.KeyboardLayout);
        }

        [Fact]
        public void ReturnsNullWhenTheBindingPresetsFileIsEmpty()
        {
            using var dir = new TestFolder();
            dir.WriteText("Empty.binds", string.Empty);

            var binds = BindingPreset.FromFile(dir.Resolve("Empty.binds"));
            Assert.Null(binds);
        }

        [Fact]
        public void ToleratesMalformedBindingPresets()
        {
            var file = Path.Combine(_gof.FullName, @"Bindings\Malformed.3.0.binds");
            var binds = BindingPreset.FromFile(file);

            Assert.Null(binds.PresetName);
            Assert.Null(binds.Version);
            Assert.Null(binds.KeyboardLayout);
            Assert.Equal(2, binds.Bindings.Count);

            var mb1 = binds.Bindings["MalformedBinding1"];
            var mb2 = binds.Bindings["MalformedBinding2"];

            Assert.Null(mb1.Primary.Device);
            Assert.Null(mb1.Primary.Key);
            Assert.Null(mb1.Secondary.Device);
            Assert.Null(mb1.Secondary.Key);
            Assert.Single(mb1.Primary.Modifiers);
            Assert.Empty(mb1.Secondary.Modifiers);

            Assert.Null(mb2.Primary.Device);
            Assert.Null(mb2.Primary.Key);
            Assert.Null(mb2.Secondary.Device);
            Assert.Null(mb2.Secondary.Key);
            Assert.Empty(mb2.Primary.Modifiers);
            Assert.Single(mb2.Secondary.Modifiers);
        }

        [Fact]
        public async Task WatcherRaisesTheChangedEventOnStart()
        {
            using var watcher = new BindingsWatcher(_gif, _gof);
            var evs = new EventCollector<BindingPreset>(h => watcher.Changed += h, h => watcher.Changed -= h);

            var binds = await evs.WaitAsync(() =>
            {
                watcher.Start();
                watcher.Stop();
            }).ConfigureAwait(false);

            Assert.Equal("Custom", binds.PresetName);
            Assert.Equal(new Version(3, 0), binds.Version);
            Assert.Equal("es-ES", binds.KeyboardLayout);

            var k = binds.Bindings["Supercruise"].Primary;
            Assert.Equal("Keyboard", k.Device);
            Assert.Equal("Key_J", k.Key);
            Assert.Equal(2, k.Modifiers.Count);
            var modifiers = k.Modifiers.OrderBy(x => x.Key).ToList();
            Assert.Equal("Keyboard", modifiers[0].Device);
            Assert.Equal("Key_LeftAlt", modifiers[0].Key);
            Assert.Equal("Keyboard", modifiers[1].Device);
            Assert.Equal("Key_LeftShift", modifiers[1].Key);
        }

        [Fact]
        public async Task WatchesForChangesInTheBidingsFiles()
        {
            using var dirMain = new TestFolder(_gif.FullName);
            using var dirOpts = new TestFolder(_gof.FullName);
            using var watcher = new BindingsWatcher(new GameInstallFolder(dirMain.Name), new GameOptionsFolder(dirOpts.Name));
            watcher.Start();

            var evs = new EventCollector<BindingPreset>(h => watcher.Changed += h, h => watcher.Changed -= h);

            var bindsCustom = dirOpts.ReadText(_customFile);

            var binds = await evs.WaitAsync(() => dirOpts.WriteText(_customFile, string.Empty), 100).ConfigureAwait(false);
            Assert.Null(binds);

            binds = await evs.WaitAsync(() => dirOpts.WriteText(_customFile, bindsCustom)).ConfigureAwait(false);
            Assert.NotNull(binds);

            binds = await evs.WaitAsync(() => dirOpts.WriteText(_startPresetFile, "Keyboard")).ConfigureAwait(false);
            Assert.NotNull(binds);
        }

        [Fact]
        public void BindNameCollectionsReturnACollectionOfAllValuesThroughTheAllProperty()
        {
            var types = typeof(BindingPreset).Assembly.GetTypes()
                .Where(x => x.Namespace == "EliteFiles.Bindings.Binds")
                .ToList();

            Assert.NotEmpty(types);

            foreach (var type in types)
            {
                var pi = type.GetProperty("All", BindingFlags.Public | BindingFlags.Static);
                var all = (IReadOnlyCollection<string>)pi.GetValue(null);

                Assert.NotEmpty(all);
            }
        }

        [Fact]
        public void WatcherThrowsWhenTheGameInstallFolderIsNotAValidInstallFolder()
        {
            var ex = Assert.Throws<ArgumentException>(() => { using var x = new BindingsWatcher(new GameInstallFolder(@"TestFiles"), _gof); });
            Assert.Contains("' is not a valid Elite:Dangerous game install folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherThrowsWhenTheGameOptionsFolderIsNotAValidOptionsFolder()
        {
            var ex = Assert.Throws<ArgumentException>(() => { using var x = new BindingsWatcher(_gif, new GameOptionsFolder(@"TestFiles")); });
            Assert.Contains("' is not a valid Elite:Dangerous game options folder.", ex.Message, StringComparison.Ordinal);
        }

        [Fact]
        public void WatcherDoesNotThrowWhenDisposingTwice()
        {
            var watcher = new BindingsWatcher(_gif, _gof);
#pragma warning disable IDISP016, IDISP017
            watcher.Dispose();
            watcher.Dispose();
#pragma warning restore IDISP016, IDISP017
        }

        [Fact]
        public void DeviceKeyBaseImplementIEquatable()
        {
            var key1 = FromXml<DeviceKey>("<Key1 Device='Keyboard' Key='Key_A' />");
            var key2 = FromXml<DeviceKeyCombination>("<Key2 Device='Keyboard' Key='Key_A' />");
            var key3 = FromXml<DeviceKey>("<Key3 Device='Keyboard' Key='Key_B' />");

            Assert.Equal<DeviceKeyBase>(key1, key2);
            Assert.Equal<DeviceKeyBase>(key2, key1);
            Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
            Assert.NotEqual<DeviceKeyBase>(key1, key3);
            Assert.NotEqual<DeviceKeyBase>(key3, key1);
            Assert.Equal<object>(key1, key2);
            Assert.Equal<object>(key2, key1);
            Assert.NotEqual<object>(key1, key3);
            Assert.NotEqual<object>(key3, key1);
            Assert.False(key1.Equals(null));
            Assert.False(key2.Equals(null));
            Assert.False(key1.Equals("Not a device key"));
            Assert.False(key2.Equals("Not a device key"));
        }

        [Fact]
        public void DeviceKeySetThrowsOnANullListOfDeviceKeys()
        {
            Assert.Throws<ArgumentNullException>("modifiers", () => new DeviceKeySet(null));
        }

        [Fact]
        public void DeviceKeySetImplementsIEquatable()
        {
            var key1 = FromXml<DeviceKey>("<Key1 Device='Keyboard' Key='Key_A' />");
            var key2 = FromXml<DeviceKey>("<Key2 Device='Keyboard' Key='Key_A' />");
            var key3 = FromXml<DeviceKey>("<Key3 Device='Keyboard' Key='Key_B' />");

            var set1 = new DeviceKeySet(new[] { key1, key3 });
            var set2 = new DeviceKeySet(new[] { key2, key3 });
            var set3 = new DeviceKeySet(new[] { key1 });

            Assert.Equal(set1, set2);
            Assert.Equal(set2, set1);
            Assert.Equal(set1.GetHashCode(), set2.GetHashCode());
            Assert.NotEqual(set1, set3);
            Assert.NotEqual(set3, set1);
            Assert.True(set1.Equals((object)set2));
            Assert.True(set2.Equals((object)set1));
            Assert.NotEqual<object>(set1, set3);
            Assert.NotEqual<object>(set3, set1);
            Assert.False(set1.Equals(null));
            Assert.False(set2.Equals(null));
            Assert.False(set1.Equals("Not a device key set"));
            Assert.False(set2.Equals("Not a device key set"));
        }

        private static T FromXml<T>(string xml)
            where T : DeviceKeyBase
        {
            var xe = XElement.Parse(xml);

            var fromXml = typeof(T).GetMethod("FromXml", BindingFlags.NonPublic | BindingFlags.Static);

            return (T)fromXml.Invoke(null, new object[] { xe });
        }
    }
}
