using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colore.Api;
using Colore.Data;
using EliteChroma.Core.Tests.Internal;
using EliteChroma.Core.Windows;
using EliteChroma.Core.Windows.Internal;
using Moq;
using Xunit;

namespace EliteChroma.Core.Tests
{
    public class WinChromaFactoryTests
    {
        [Fact]
        public async Task WaitsForChromaSdkDeviceAccessEvent()
        {
            using var cpl = ColoreProviderLock.GetLock();

            var chromaApi = new Mock<IChromaApi>() { DefaultValue = DefaultValue.Mock };

            using var cf = new WinChromaFactory
            {
                ChromaApi = chromaApi.Object,
                ChromaAppInfo = null,
            };

            Assert.Equal(TimeSpan.Zero, cf.WarmupDelay);

            var cw = cf.GetPrivateField<ChromaWindow>("_cw")!;

            var tcs = new TaskCompletionSource<IntPtr>();
            chromaApi
                .Setup(x => x.RegisterEventNotifications(It.IsAny<IntPtr>()))
                .Callback((IntPtr hWnd) => tcs.SetResult(hWnd));

            var tChroma = cf.CreateAsync();

            var hWnd = await tcs.Task.ConfigureAwait(false);
            Assert.Equal(cw.Handle, hWnd);

            // Reference: https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/_rz_chroma_s_d_k_8h.html#afc89b7127b37c6448277a2334b1e34db
            const int deviceAccess = 2;
            const int grantedAccess = 1;
            SendChromaEventMessage(cw, deviceAccess, grantedAccess);

            using var chroma = await tChroma.ConfigureAwait(false);
            Assert.NotNull(chroma);

            await chroma.UninitializeAsync().ConfigureAwait(false);
        }

        [Fact]
        public void DoesNotThrowWhenDisposingTwice()
        {
            var cf = new WinChromaFactory();
#pragma warning disable IDISP016, IDISP017
            cf.Dispose();
            cf.Dispose();
#pragma warning restore IDISP016, IDISP017
        }

        [Fact]
        public void ChromaWindowDoesNotThrowWhenDisposingTwice()
        {
            var cw = new ChromaWindow();
#pragma warning disable IDISP016, IDISP017
            cw.Dispose();
            cw.Dispose();
#pragma warning restore IDISP016, IDISP017
        }

        private static void SendChromaEventMessage(ChromaWindow cw, int wParam, int lParam)
        {
            var m = Message.Create(cw.Handle, (int)Constants.WmChromaEvent, new IntPtr(wParam), new IntPtr(lParam));

            cw.InvokePrivateMethod<object>("WndProc", m);
        }
    }
}
