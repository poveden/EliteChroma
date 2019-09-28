using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EliteFiles.Tests.Internal
{
    internal sealed class EventCollector<T>
    {
        private readonly Action<EventHandler<T>> _attach;
        private readonly Action<EventHandler<T>> _detach;

        public EventCollector(Action<EventHandler<T>> attach, Action<EventHandler<T>> detach)
        {
            _attach = attach;
            _detach = detach;
        }

        public async Task<T> WaitAsync(Action trigger, int timeout = Timeout.Infinite)
        {
            T res = default;

            using (var ss = new SemaphoreSlim(0, 1))
            {
                void handler(object _, T e)
                {
                    res = e;
                    ss.Release();
                }

                _attach(handler);
                trigger();
                await ss.WaitAsync(timeout);
                _detach(handler);
            }

            return res;
        }

        public async Task<IList<T>> WaitAsync(int count, Action trigger, int timeout = Timeout.Infinite)
        {
            var res = new List<T>();

            using (var ce = new CountdownEvent(count))
            {
                void handler(object _, T e)
                {
                    res.Add(e);
                    ce.Signal();
                }

                _attach(handler);
                trigger();
                await Task.Run(() => ce.Wait(timeout));
                _detach(handler);
            }

            return res;
        }
    }
}
