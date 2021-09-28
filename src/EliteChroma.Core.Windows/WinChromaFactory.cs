using System;
using System.Threading;
using System.Threading.Tasks;
using ChromaWrapper;
using ChromaWrapper.Data;
using ChromaWrapper.Events;
using ChromaWrapper.Sdk;

namespace EliteChroma.Core.Windows
{
    public class WinChromaFactory : IChromaFactory
    {
        private const int _accessGrantedTimeout = 2000;

        public ChromaAppInfo? ChromaAppInfo { get; set; }

        public TimeSpan WarmupDelay => TimeSpan.Zero;

        public async Task<IChromaSdk> CreateAsync()
        {
            IChromaSdk res = Create();
            _ = await WaitForAccessGranted(res).ConfigureAwait(false);
            return res;
        }

        protected virtual IChromaSdk Create()
        {
            return new ChromaSdk(ChromaAppInfo, false);
        }

        private static async Task<bool> WaitForAccessGranted(IChromaSdk chroma)
        {
            var tcs = new TaskCompletionSource<bool>();

            var callback = new EventHandler<ChromaDeviceAccessEventArgs>((s, e) =>
            {
                tcs.SetResult(e.AccessGranted);
            });

            chroma.DeviceAccess += callback;

            using var ctsTimeout = new CancellationTokenSource();
            Task<bool> tTimeout = Task.Delay(_accessGrantedTimeout, ctsTimeout.Token)
                .ContinueWith(t => false, TaskScheduler.Default);

            Task<bool> tRes = await Task.WhenAny(tTimeout, tcs.Task).ConfigureAwait(false);

            if (tRes == tcs.Task)
            {
                ctsTimeout.Cancel();
            }

            chroma.DeviceAccess -= callback;

            return await tRes.ConfigureAwait(false);
        }
    }
}
