using System;
using System.Drawing;
using ChromaWrapper;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Headset;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using EliteChroma.Chroma;
using EliteChroma.Core.Tests.Internal;
using Moq;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class ChromaCanvasTests
    {
        [Fact]
        public void SetEffectOnlyAppliesAccessedEffects()
        {
            var chroma = ChromaMockFactory.Create();

            var cc = new ChromaCanvas();
            cc.Keyboard.Color.Fill(ChromaColor.Blue);

            Assert.Single(cc.SetEffect(chroma.Object));

            chroma.Verify(x => x.CreateEffect(It.Is<CustomKeyKeyboardEffect>(y => y.Color[0] == ChromaColor.Blue)), Times.Once);
            chroma.Verify(x => x.CreateEffect(It.IsAny<CustomMouseEffect2>()), Times.Never);
            chroma.Verify(x => x.CreateEffect(It.IsAny<CustomHeadsetEffect>()), Times.Never);
            chroma.Verify(x => x.CreateEffect(It.IsAny<CustomMousepadEffect>()), Times.Never);
            chroma.Verify(x => x.CreateEffect(It.IsAny<CustomKeypadEffect>()), Times.Never);
            chroma.Verify(x => x.CreateEffect(It.IsAny<CustomChromaLinkEffect>()), Times.Never);
        }

        [Fact]
        public void RazerEffectsAreAccessible()
        {
            var chroma = ChromaMockFactory.Create();

            var cc = new ChromaCanvas();
            cc.Keyboard.Color.Fill(ChromaColor.Blue);
            cc.Mouse.Color.Fill(ChromaColor.Green);
            cc.Headset.Color.Fill(ChromaColor.FromColor(Color.HotPink));
            cc.Mousepad.Color.Fill(ChromaColor.FromColor(Color.Orange));
            cc.Keypad.Color.Fill(ChromaColor.FromColor(Color.Pink));
            cc.ChromaLink.Color.Fill(ChromaColor.FromColor(Color.Purple));

            cc.SetEffect(chroma.Object);

            chroma.Verify(x => x.CreateEffect(It.Is<CustomKeyKeyboardEffect>(y => y.Color[0] == ChromaColor.Blue)));
            chroma.Verify(x => x.CreateEffect(It.Is<CustomMouseEffect2>(y => y.Color[0] == ChromaColor.Green)));
            chroma.Verify(x => x.CreateEffect(It.Is<CustomHeadsetEffect>(y => y.Color[0] == ChromaColor.FromColor(Color.HotPink))));
            chroma.Verify(x => x.CreateEffect(It.Is<CustomMousepadEffect>(y => y.Color[0] == ChromaColor.FromColor(Color.Orange))));
            chroma.Verify(x => x.CreateEffect(It.Is<CustomKeypadEffect>(y => y.Color[0] == ChromaColor.FromColor(Color.Pink))));
            chroma.Verify(x => x.CreateEffect(It.Is<CustomChromaLinkEffect>(y => y.Color[0] == ChromaColor.FromColor(Color.Purple))));
        }

        [Fact]
        public void SetEffectThrowsOnNullChromaObject()
        {
            var cc = new ChromaCanvas();
            Assert.Throws<ArgumentNullException>("chroma", () => cc.SetEffect(null!));
        }
    }
}
