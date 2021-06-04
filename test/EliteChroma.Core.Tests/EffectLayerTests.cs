using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Colore;
using Colore.Data;
using Colore.Effects.Keyboard;
using EliteChroma.Chroma;
using EliteChroma.Core.Internal;
using EliteChroma.Core.Layers;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Elite;
using EliteFiles;
using EliteFiles.Bindings;
using EliteFiles.Bindings.Binds;
using EliteFiles.Graphics;
using EliteFiles.Journal;
using EliteFiles.Journal.Events;
using Moq;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class EffectLayerTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private const string _mainFile = @"ControlSchemes\Keyboard.binds";

        private readonly GameInstallFolder _gif;

        public EffectLayerTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
        }

        [Fact]
        public void LayerComparerComparesCorrectly()
        {
            var comparer = (IComparer<EffectLayer>)typeof(LayeredEffect)
                .GetField("_comparer", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            var l1 = new Mock<EffectLayer>();
            l1.Setup(x => x.Order).Returns(500);

            var l2 = new Mock<EffectLayer>();
            l2.Setup(x => x.Order).Returns(500);

            Assert.Equal(0, comparer.Compare(l1.Object, l1.Object));

            int h1 = l1.Object.GetHashCode();
            int h2 = l2.Object.GetHashCode();

            Assert.Equal(h1.CompareTo(h2), comparer.Compare(l1.Object, l2.Object));
            Assert.Equal(h2.CompareTo(h1), comparer.Compare(l2.Object, l1.Object));
        }

        [Fact]
        public void DuplicatedLayerObjectsAreNotAddedToTheCollection()
        {
            var le = new LayeredEffect();
            var layer = new InterfaceModeLayer();

            Assert.True(le.Add(layer));
            Assert.False(le.Add(layer));
            Assert.Single(le.Layers);
            Assert.True(le.Remove(layer));
            Assert.False(le.Remove(layer));
        }

        [Fact]
        public void LayerRenderStateThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>("gameState", () => new LayerRenderState(null, new ChromaColors()));
            Assert.Throws<ArgumentNullException>("colors", () => new LayerRenderState(new GameState(), null));
        }

        [Fact]
        public async Task LayerBaseThrowsOnInvalidGameState()
        {
            var le = new LayeredEffect();
            le.Add(new DummyLayer(new NativeMethodsStub()));

            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            await Assert.ThrowsAsync<ArgumentNullException>(() => le.Render(chroma.Object, null)).ConfigureAwait(false);
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

            var hyperspaceLayer = new HyperspaceLayer() { NativeMethods = new NativeMethodsStub() };
            var le = new LayeredEffect();
            le.Add(hyperspaceLayer);

            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            CustomKeyboardEffect? keyboard = null;
            Mock.Get(chroma.Object.Keyboard)
                .Setup(x => x.SetCustomAsync(It.IsAny<CustomKeyboardEffect>()))
                .Callback((CustomKeyboardEffect c) => keyboard = c);

            var game = new GameState
            {
                FsdJumpType = StartJump.FsdJumpType.Hyperspace,
                FsdJumpStarClass = starClass,
                FsdJumpChange = DateTimeOffset.UtcNow,
                BindingPreset = binds,
                PressedModifiers = new DeviceKeySet(Enumerable.Empty<DeviceKey>()),
            };

            var state = new LayerRenderState(game, new ChromaColors());

            game.Now = DateTimeOffset.UtcNow;
            await le.Render(chroma.Object, state).ConfigureAwait(false);
            Assert.False(game.InWitchSpace);

            game.Now += GameState.JumpCountdownDelay;
            await le.Render(chroma.Object, state).ConfigureAwait(false);
            Assert.True(game.InWitchSpace);
#pragma warning disable CA1508
            Assert.Equal(colors[0], keyboard?[hyperJumpKey]);
#pragma warning restore CA1508

            foreach (var color in colors.Skip(1))
            {
                keyboard = null;
                game.Now += TimeSpan.FromSeconds(stepSeconds);
                await le.Render(chroma.Object, state).ConfigureAwait(false);
#pragma warning disable CA1508
                Assert.Equal(color, keyboard?[hyperJumpKey]);
#pragma warning restore CA1508
            }
        }

        [Theory]
        [InlineData(false, GameProcessState.InForeground, null, null)]
        [InlineData(false, GameProcessState.InBackground, 0x000000, 0xFF8800)]
        [InlineData(false, GameProcessState.NotRunning, null, null)]
        [InlineData(true, GameProcessState.NotRunning, null, null)]
        [InlineData(true, GameProcessState.InForeground, 0xFF8800, null)]
        public async Task GameInBackgroundLayerSetsAColorPerGameProcessState(bool wasInBackground, GameProcessState processState, int? startRgbColor, int? endRgbColor)
        {
            const double fadeDurationSeconds = 1;

            var expectedStartColor = startRgbColor.HasValue ? Color.FromRgb((uint)startRgbColor.Value) : (Color?)null;
            var expectedEndColor = endRgbColor.HasValue ? Color.FromRgb((uint)endRgbColor.Value) : (Color?)null;

            var graphicsConfig = GraphicsConfig.FromFile(_gif.GraphicsConfiguration.FullName);

            var bl = new GameInBackroundLayer();
            bl.SetPrivateField("_inBackground", wasInBackground);

            var le = new LayeredEffect();
            le.Add(bl);

            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            CustomKeyboardEffect? keyboard = null;
            Mock.Get(chroma.Object.Keyboard)
                .Setup(x => x.SetCustomAsync(It.IsAny<CustomKeyboardEffect>()))
                .Callback((CustomKeyboardEffect c) => keyboard = c);

            var game = new GameState
            {
                ProcessState = processState,
                GuiColour = graphicsConfig.GuiColour.Default,
            };

            var state = new LayerRenderState(game, new ChromaColors());

            game.Now = DateTimeOffset.UtcNow;
            await le.Render(chroma.Object, state).ConfigureAwait(false);
#pragma warning disable CA1508
            Assert.Equal(expectedStartColor, keyboard?[(Key)0]);
#pragma warning restore CA1508

            keyboard = null;
            game.Now += TimeSpan.FromSeconds(fadeDurationSeconds);
            await le.Render(chroma.Object, state).ConfigureAwait(false);
#pragma warning disable CA1508
            Assert.Equal(expectedEndColor, keyboard?[(Key)0]);
#pragma warning restore CA1508
        }

        private static Key GetKey(BindingPreset binds, string binding)
        {
            var bps = binds.Bindings[binding].Primary;
            return KeyMappings.TryGetKey(bps.Key, "en-US", false, out var key, new NativeMethodsStub()) ? key : 0;
        }

        private sealed class DummyLayer : LayerBase
        {
            public DummyLayer(INativeMethods nativeMethods)
            {
                NativeMethods = nativeMethods;
            }

            protected override void OnRender(ChromaCanvas canvas)
            {
            }
        }
    }
}
