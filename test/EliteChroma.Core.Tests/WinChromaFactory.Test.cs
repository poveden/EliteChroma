using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
    [SuppressMessage("DocumentationRules", "SA1649:File name should match first type name", Justification = "xUnit test class.")]
    public class WinChromaFactoryTest
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

            var cw = GetChromaWindowInstance(cf);

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

            var chroma = await tChroma.ConfigureAwait(false);
            Assert.NotNull(chroma);

            await chroma.UninitializeAsync().ConfigureAwait(false);
        }

        private static ChromaWindow GetChromaWindowInstance(WinChromaFactory cf)
        {
            return (ChromaWindow)cf.GetType()
                .GetField("_cw", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(cf);
        }

        private static void SendChromaEventMessage(ChromaWindow cw, int wParam, int lParam)
        {
            var miWndProc = cw.GetType().GetMethod("WndProc", BindingFlags.NonPublic | BindingFlags.Instance);

            var m = Message.Create(cw.Handle, (int)Constants.WmChromaEvent, new IntPtr(wParam), new IntPtr(lParam));
            miWndProc.Invoke(cw, new object[] { m });
        }
    }
}
