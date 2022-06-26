using ChromaWrapper.Sdk;
using EliteChroma.Core.Chroma;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class EffectExtensionsTests
    {
        [Fact]
        public void CombineKeyThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.CombineKey(null!, default));
        }

        [Fact]
        public void MaxAtThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.MaxAt(null!, 0, 0, default));
        }

        [Fact]
        public void CombineILedGridEffectThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.Combine((ILedGridEffect)null!, default));
        }

        [Fact]
        public void CombineILedArrayEffectThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.Combine((ILedArrayEffect)null!, default));
        }

        [Fact]
        public void MaxILedGridEffectThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.Max((ILedGridEffect)null!, default));
        }

        [Fact]
        public void MaxILedArrayEffectThrowsOnNullEffectArgument()
        {
            Assert.Throws<ArgumentNullException>("effect", () => EffectExtensions.Max((ILedArrayEffect)null!, default));
        }
    }
}
