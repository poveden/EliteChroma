using System;
using System.Threading.Tasks;
using ChromaWrapper;
using ChromaWrapper.Data;
using ChromaWrapper.Sdk;

namespace EliteChroma.Core.Internal
{
    internal sealed class ChromaFactory : IChromaFactory
    {
        public ChromaAppInfo? ChromaAppInfo { get; set; }

        public TimeSpan WarmupDelay { get; } = TimeSpan.FromSeconds(1);

        public Task<IChromaSdk> CreateAsync()
        {
            return Task.FromResult<IChromaSdk>(new ChromaSdk(ChromaAppInfo, false));
        }
    }
}
