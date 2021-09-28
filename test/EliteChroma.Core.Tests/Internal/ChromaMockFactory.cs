using System;
using System.Threading.Tasks;
using ChromaWrapper.Sdk;
using Moq;

namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class ChromaMockFactory : IChromaFactory
    {
        public Mock<IChromaSdk> Mock { get; } = Create();

        public TimeSpan WarmupDelay { get; } = TimeSpan.FromSeconds(1);

        public static Mock<IChromaSdk> Create()
        {
            return new Mock<IChromaSdk>() { DefaultValue = DefaultValue.Mock };
        }

        public Task<IChromaSdk> CreateAsync()
        {
            return Task.FromResult(Mock.Object);
        }
    }
}
