using System;
using System.Threading.Tasks;
using Colore;

namespace EliteChroma.Core
{
    public interface IChromaFactory
    {
        TimeSpan WarmupDelay { get; }

        Task<IChroma> CreateAsync();
    }
}
