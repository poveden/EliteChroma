using System;
using System.Threading.Tasks;
using ChromaWrapper.Sdk;

namespace EliteChroma.Core
{
    public interface IChromaFactory
    {
        TimeSpan WarmupDelay { get; }

        Task<IChromaSdk> CreateAsync();
    }
}
