using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChromaWrapper;
using ChromaWrapper.Keyboard;
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
using TestUtils;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class EffectLayerTests
    {
        private const string _gameRootFolder = @"TestFiles\GameRoot";

        private const string _mainFile = @"ControlSchemes\KeyboardMouseOnly.binds";

        private readonly GameInstallFolder _gif;

        public EffectLayerTests()
        {
            _gif = new GameInstallFolder(_gameRootFolder);
        }

        [Fact]
        public void LayerComparerComparesCorrectly()
        {
            var comparer = typeof(LayeredEffect).GetPrivateStaticField<IComparer<EffectLayer>>("_comparer")!;

            var l1 = new Mock<EffectLayer>();
            l1.Setup(x => x.Order).Returns(500);

            var l2 = new Mock<EffectLayer>();
            l2.Setup(x => x.Order).Returns(500);

            Assert.Equal(0, comparer.Compare(l1.Object, l1.Object));

            int h1 = l1.Object.GetHashCode();
            int h2 = l2.Object.GetHashCode();

            Assert.Equal(h1.CompareTo(h2), comparer.Compare(l1.Object, l2.Object));
            Assert.Equal(h2.CompareTo(h1), comparer.Compare(l2.Object, l1.Object));

            Assert.Equal(-1, comparer.Compare(null, l1.Object));
            Assert.Equal(1, comparer.Compare(l1.Object, null));
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
            Assert.Throws<ArgumentNullException>("gameState", () => new LayerRenderState(null!, new ChromaColors()));
            Assert.Throws<ArgumentNullException>("colors", () => new LayerRenderState(new GameState(), null!));
        }

        [Fact]
        public void LayerBaseThrowsOnInvalidGameState()
        {
            var le = new LayeredEffect();
            le.Add(new DummyLayer(new NativeMethodsStub()));

            var chroma = ChromaMockFactory.Create();

            Assert.Throws<ArgumentNullException>(() => le.Render(chroma.Object, null!));
        }

        [Theory]
        [InlineData(StarClass.O, 0.25, new[] { 0x000000, 0x007F00, 0x00FF00, 0x007F00, 0x000000 })]
        [InlineData(StarClass.HerbigAeBe, 0.25, new[] { 0xFFFF00, 0xFFFF00, 0x000000, 0x000000 })]
        [InlineData(StarClass.Neutron, 0.1675, new[] { 0xFF0000, 0x000000, 0x000000, 0xFF0000 })]
        public void HyperspaceLayerPulsesJumpKeyBasedOnHazardLevel(string starClass, double stepSeconds, int[] rgbColors)
        {
            var colors = rgbColors.Select(x => ChromaColor.FromRgb(x)).ToList();

            var binds = BindingPreset.FromFile(Path.Combine(_gif.FullName, _mainFile))!;
            var hyperJumpKey = GetKey(binds, FlightMiscellaneous.HyperSuperCombination);

            var hyperspaceLayer = new HyperspaceLayer() { NativeMethods = new NativeMethodsStub() };
            var le = new LayeredEffect();
            le.Add(hyperspaceLayer);

            var chroma = ChromaMockFactory.Create();

            CustomKeyKeyboardEffect? keyboard = null;
            Mock.Get(chroma.Object)
                .Setup(x => x.CreateEffect(It.IsAny<IKeyboardEffect>()))
                .Callback((IKeyboardEffect c) => keyboard = (CustomKeyKeyboardEffect)c);

            var game = new GameState
            {
                FsdJumpType = StartJump.FsdJumpType.Hyperspace,
                FsdJumpStarClass = starClass,
                FsdJumpChange = DateTimeOffset.UtcNow,
                Bindings = new GameBindings(binds),
                PressedModifiers = new DeviceKeySet(Enumerable.Empty<DeviceKey>()),
            };

            var state = new LayerRenderState(game, new ChromaColors());

            game.Now = DateTimeOffset.UtcNow;
            le.Render(chroma.Object, state);
            Assert.False(game.InWitchSpace);

            game.Now += GameState.JumpCountdownDelay;
            le.Render(chroma.Object, state);
            Assert.True(game.InWitchSpace);
#pragma warning disable CA1508
            Assert.Equal(colors[0], keyboard?.Key[hyperJumpKey]);
#pragma warning restore CA1508

            foreach (var color in colors.Skip(1))
            {
                keyboard = null;
                game.Now += TimeSpan.FromSeconds(stepSeconds);
                le.Render(chroma.Object, state);
#pragma warning disable CA1508
                Assert.Equal(color, keyboard?.Key[hyperJumpKey]);
#pragma warning restore CA1508
            }
        }

        [Theory]
        [InlineData(false, GameProcessState.InForeground, null, null)]
        [InlineData(false, GameProcessState.InBackground, 0x000000, 0xFF8800)]
        [InlineData(false, GameProcessState.NotRunning, null, null)]
        [InlineData(true, GameProcessState.NotRunning, null, null)]
        [InlineData(true, GameProcessState.InForeground, 0xFF8800, null)]
        public void GameInBackgroundLayerSetsAColorPerGameProcessState(bool wasInBackground, GameProcessState processState, int? startRgbColor, int? endRgbColor)
        {
            const double fadeDurationSeconds = 1;

            var expectedStartColor = startRgbColor.HasValue ? ChromaColor.FromRgb(startRgbColor.Value) : (ChromaColor?)null;
            var expectedEndColor = endRgbColor.HasValue ? ChromaColor.FromRgb(endRgbColor.Value) : (ChromaColor?)null;

            var graphicsConfig = GraphicsConfig.FromFile(_gif.GraphicsConfiguration.FullName)!;

            var bl = new GameInBackroundLayer();
            bl.SetPrivateField("_inBackground", wasInBackground);

            var le = new LayeredEffect();
            le.Add(bl);

            var chroma = ChromaMockFactory.Create();

            CustomKeyKeyboardEffect? keyboard = null;
            Mock.Get(chroma.Object)
                .Setup(x => x.CreateEffect(It.IsAny<IKeyboardEffect>()))
                .Callback((IKeyboardEffect c) => keyboard = (CustomKeyKeyboardEffect)c);

            var game = new GameState
            {
                ProcessState = processState,
                GuiColour = graphicsConfig.GuiColour!.Default!,
            };

            var state = new LayerRenderState(game, new ChromaColors());

            game.Now = DateTimeOffset.UtcNow;
            le.Render(chroma.Object, state);
#pragma warning disable CA1508
            Assert.Equal(expectedStartColor, keyboard?.Key[0]);
#pragma warning restore CA1508

            keyboard = null;
            game.Now += TimeSpan.FromSeconds(fadeDurationSeconds);
            le.Render(chroma.Object, state);
#pragma warning disable CA1508
            Assert.Equal(expectedEndColor, keyboard?.Key[0]);
#pragma warning restore CA1508
        }

        private static KeyboardKey GetKey(BindingPreset binds, string binding)
        {
            var bps = binds.Bindings[binding].Primary;
            return KeyMappings.TryGetKey(bps.Key!, "en-US", false, out var key, new NativeMethodsStub()) ? key : 0;
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
