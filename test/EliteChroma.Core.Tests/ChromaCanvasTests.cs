using System;
using System.Threading.Tasks;
using Colore;
using Colore.Data;
using Colore.Effects.ChromaLink;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;
using EliteChroma.Chroma;
using Moq;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ChromaCanvasTests
    {
        [Fact]
        public async Task SetEffectOnlyAppliesAccessedEffects()
        {
            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            var cc = new ChromaCanvas();
            cc.Keyboard.Set(Color.Blue);

            await cc.SetEffect(chroma.Object).ConfigureAwait(false);

            Mock.Get(chroma.Object.Keyboard)
                .Verify(x => x.SetCustomAsync(It.Is<CustomKeyboardEffect>(y => y[0] == Color.Blue)), Times.Once);
            Mock.Get(chroma.Object.Mouse)
                .Verify(x => x.SetGridAsync(It.IsAny<CustomMouseEffect>()), Times.Never);
            Mock.Get(chroma.Object.Headset)
                .Verify(x => x.SetCustomAsync(It.IsAny<CustomHeadsetEffect>()), Times.Never);
            Mock.Get(chroma.Object.Mousepad)
                .Verify(x => x.SetCustomAsync(It.IsAny<CustomMousepadEffect>()), Times.Never);
            Mock.Get(chroma.Object.Keypad)
                .Verify(x => x.SetCustomAsync(It.IsAny<CustomKeypadEffect>()), Times.Never);
            Mock.Get(chroma.Object.ChromaLink)
                .Verify(x => x.SetCustomAsync(It.IsAny<CustomChromaLinkEffect>()), Times.Never);
        }

        [Fact]
        public async Task RazerEffectsAreAccessible()
        {
            var chroma = new Mock<IChroma> { DefaultValue = DefaultValue.Mock };

            var cc = new ChromaCanvas();
            cc.Keyboard.Set(Color.Blue);
            cc.Mouse.Set(Color.Green);
            cc.Headset.Set(Color.HotPink);
            cc.Mousepad.Set(Color.Orange);
            cc.Keypad.Set(Color.Pink);
            cc.ChromaLink.Set(Color.Purple);

            await cc.SetEffect(chroma.Object).ConfigureAwait(false);

            Mock.Get(chroma.Object.Keyboard)
                .Verify(x => x.SetCustomAsync(It.Is<CustomKeyboardEffect>(y => y[0] == Color.Blue)));
            Mock.Get(chroma.Object.Mouse)
                .Verify(x => x.SetGridAsync(It.Is<CustomMouseEffect>(y => y[0] == Color.Green)));
            Mock.Get(chroma.Object.Headset)
                .Verify(x => x.SetCustomAsync(It.Is<CustomHeadsetEffect>(y => y[0] == Color.HotPink)));
            Mock.Get(chroma.Object.Mousepad)
                .Verify(x => x.SetCustomAsync(It.Is<CustomMousepadEffect>(y => y[0] == Color.Orange)));
            Mock.Get(chroma.Object.Keypad)
                .Verify(x => x.SetCustomAsync(It.Is<CustomKeypadEffect>(y => y[0] == Color.Pink)));
            Mock.Get(chroma.Object.ChromaLink)
                .Verify(x => x.SetCustomAsync(It.Is<CustomChromaLinkEffect>(y => y[0] == Color.Purple)));
        }

        [Fact]
        public void SetEffectThrowsOnNullChromaObject()
        {
            var cc = new ChromaCanvas();
            Assert.ThrowsAsync<ArgumentNullException>("chroma", () => cc.SetEffect(null));
        }
    }
}
