using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colore;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Core.Internal;
using EliteChroma.Core.Layers;
using EliteChroma.Elite;
using EliteFiles;
using EliteFiles.Bindings;
using EliteFiles.Bindings.Binds;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using Moq;
using Xunit;

namespace EliteChroma.Core.Tests
{
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class EffectLayerTest
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private const string _mainFile = @"ControlSchemes\Keyboard.binds";

        private readonly GameInstallFolder _gif;

        public EffectLayerTest()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
        }

        [Fact]
        public void LayerComparerComparesCorrectly()
        {
            var comparer = new LayeredEffect().Layers.Comparer;

            var l1 = new Mock<EffectLayer>();
            l1.Setup(x => x.Order).Returns(500);

            var l2 = new Mock<EffectLayer>();
            l2.Setup(x => x.Order).Returns(500);

            Assert.Equal(0, comparer.Compare(l1.Object, l1.Object));

            var h1 = l1.Object.GetHashCode();
            var h2 = l2.Object.GetHashCode();

            Assert.Equal(h1.CompareTo(h2), comparer.Compare(l1.Object, l2.Object));
            Assert.Equal(h2.CompareTo(h1), comparer.Compare(l2.Object, l1.Object));
        }

        [Theory]
        [InlineData(StarClass.O, 0.25, new[] { 0x000000, 0x007F00, 0x00FF00, 0x007F00, 0x000000 })]
        [InlineData(StarClass.HerbigAeBe, 0.25, new[] { 0xFFFF00, 0xFFFF00, 0x000000, 0x000000 })]
        [InlineData(StarClass.Neutron, 0.1675, new[] { 0xFF0000, 0x000000, 0x000000, 0xFF0000 })]
        public async Task HyperspaceLayerPulsesJumpKeyBasedOnHazardLevel(string starClass, double stepSeconds, int[] rgbColors)
        {
            var colors = rgbColors.Select(x => Color.FromRgb((uint)x)).ToList();

            var binds = BindingPreset.FromFile(Path.Combine(_gif.FullName, _mainFile));
            var hyperJumpKey = GetKey(binds, FlightMiscellaneous.HyperSuperCombination);

            var hyperspaceLayer = new HyperspaceLayer();
            var le = new LayeredEffect();
            le.Layers.Add(hyperspaceLayer);

            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            KeyboardCustom keyboard;
            Mock.Get(chroma.Object.Keyboard)
                .Setup(x => x.SetCustomAsync(It.IsAny<KeyboardCustom>()))
                .Callback((KeyboardCustom c) => keyboard = c);

            var game = new GameState
            {
                FsdJumpType = StartJump.FsdJumpType.Hyperspace,
                FsdJumpStarClass = starClass,
                FsdJumpChange = DateTimeOffset.UtcNow,
                Bindings = binds.Bindings,
                PressedModifiers = new DeviceKeySet(Enumerable.Empty<DeviceKey>()),
            };

            game.Now = DateTimeOffset.UtcNow;
            await le.Render(chroma.Object, game).ConfigureAwait(false);
            Assert.False(game.InWitchSpace);

            game.Now += TimeSpan.FromSeconds(5);
            await le.Render(chroma.Object, game).ConfigureAwait(false);
            Assert.True(game.InWitchSpace);
            Assert.Equal(colors[0], keyboard[hyperJumpKey]);

            foreach (var color in colors.Skip(1))
            {
                game.Now += TimeSpan.FromSeconds(stepSeconds);
                await le.Render(chroma.Object, game).ConfigureAwait(false);
                Assert.Equal(color, keyboard[hyperJumpKey]);
            }
        }

        private static Key GetKey(BindingPreset binds, string binding)
        {
            var bps = binds.Bindings[binding].Primary;
            return KeyMappings.EliteKeys[bps.Key];
        }
    }
}
