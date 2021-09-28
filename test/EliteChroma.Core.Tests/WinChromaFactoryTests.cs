using System;
using System.Reflection;
using System.Threading.Tasks;
using ChromaWrapper.Events;
using ChromaWrapper.Sdk;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Core.Windows;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class WinChromaFactoryTests
    {
        [Fact]
        public async Task WaitsForChromaSdkDeviceAccessEvent()
        {
            var cf = new Factory();

            Assert.Equal(TimeSpan.Zero, cf.WarmupDelay);

            var tChroma = cf.CreateAsync();

            using var chroma = await tChroma.ConfigureAwait(false);
            Assert.NotNull(chroma);
        }

        private sealed class Factory : WinChromaFactory
        {
            protected override IChromaSdk Create()
            {
                var sdk = ChromaMockFactory.Create();

                Task.Delay(100).ContinueWith(
                    x =>
                    {
                        var ci = typeof(ChromaDeviceAccessEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(bool) }, null)!;
                        var e = (ChromaDeviceAccessEventArgs)ci.Invoke(new object[] { true })!;
                        sdk.Raise(x => x.DeviceAccess += null, e);
                    },
                    TaskScheduler.Default);

                return sdk.Object;
            }
        }
    }
}
