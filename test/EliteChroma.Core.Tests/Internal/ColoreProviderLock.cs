using System;
using System.Threading;

namespace EliteChroma.Core.Tests.Internal
{
    // Colore's ColoreProvider class has a static variable that keeps the
    // currently initialized IChroma instance. This class synchronizes access
    // so no xUnit tests try to initialize instances at the same time.
    internal static class ColoreProviderLock
    {
        private static readonly SemaphoreSlim _chromaLock = new SemaphoreSlim(1, 1);

        public static IDisposable GetLock()
        {
            if (!_chromaLock.Wait(5000))
            {
                throw new TimeoutException();
            }

            return new Lock();
        }

        private sealed class Lock : IDisposable
        {
            private bool _disposed;

            public void Dispose()
            {
                if (_disposed)
                {
                    return;
                }

                _chromaLock.Release();
                _disposed = true;
            }
        }
    }
}
