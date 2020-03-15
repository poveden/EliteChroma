using System;
using System.Threading;
using System.Threading.Tasks;
using Colore;
using Colore.Api;
using Colore.Data;
using Colore.Events;
using Colore.Native;
using EliteChroma.Core.Windows.Internal;

namespace EliteChroma.Core.Windows
{
    public sealed class WinChromaFactory : IChromaFactory, IDisposable
    {
        private const int _accessGrantedTimeout = 2000;

        private readonly ChromaWindow _cw;

        public WinChromaFactory()
        {
            _cw = new ChromaWindow();
        }

        public IChromaApi ChromaApi { get; set; }

        public AppInfo ChromaAppInfo { get; set; }

        public TimeSpan WarmupDelay => TimeSpan.Zero;

        public async Task<IChroma> CreateAsync()
        {
            var chromaApi = ChromaApi ?? new NativeApi();
            var res = await ColoreProvider.CreateAsync(ChromaAppInfo, chromaApi).ConfigureAwait(false);
            await WaitForAccessGranted(res).ConfigureAwait(false);
            return res;
        }

        public void Dispose()
        {
            _cw.Dispose();
        }

        private async Task<bool> WaitForAccessGranted(IChroma chroma)
        {
            var tcs = new TaskCompletionSource<bool>();

            var callback = new EventHandler<DeviceAccessEventArgs>((s, e) =>
            {
                tcs.SetResult(e.Granted);
            });

            _cw.Chroma = chroma;
            chroma.Register(_cw.Handle);
            chroma.DeviceAccess += callback;

            using var ctsTimeout = new CancellationTokenSource();
            var tTimeout = Task.Delay(_accessGrantedTimeout, ctsTimeout.Token)
                .ContinueWith(t => false, TaskScheduler.Default);

            var tRes = await Task.WhenAny(tTimeout, tcs.Task).ConfigureAwait(false);

            if (tRes == tcs.Task)
            {
                ctsTimeout.Cancel();
            }

            chroma.DeviceAccess -= callback;
            chroma.Unregister();
            _cw.Chroma = null;

            return await tRes.ConfigureAwait(false);
        }
    }
}
