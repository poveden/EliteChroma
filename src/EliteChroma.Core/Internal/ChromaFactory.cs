using System;
using System.Threading.Tasks;
using Colore;
using Colore.Api;
using Colore.Data;
using Colore.Native;

namespace EliteChroma.Core.Internal
{
    internal sealed class ChromaFactory : IChromaFactory
    {
        public IChromaApi ChromaApi { get; set; }

        public AppInfo ChromaAppInfo { get; set; }

        public TimeSpan WarmupDelay { get; } = TimeSpan.FromSeconds(1);

        public async Task<IChroma> CreateAsync()
        {
            var chromaApi = ChromaApi ?? new NativeApi();
            var res = await ColoreProvider.CreateAsync(ChromaAppInfo, chromaApi).ConfigureAwait(false);
            return res;
        }
    }
}
