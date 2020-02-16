using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EliteChroma.Core.Tests.Internal
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

        public T Wait(Action trigger, int timeout = Timeout.Infinite)
        {
            T res = default;

            using (var ss = new SemaphoreSlim(0, 1))
            {
                void Handler(object sender, T e)
                {
                    res = e;
                    ss.Release();
                }

                _attach(Handler);
                trigger();
                ss.Wait(timeout);
                _detach(Handler);
            }

            return res;
        }

        public IList<T> Wait(int count, Action trigger, int timeout = Timeout.Infinite)
        {
            var res = new List<T>();

            using (var ce = new CountdownEvent(count))
            {
                void Handler(object sender, T e)
                {
                    res.Add(e);
                    ce.Signal();
                }

                _attach(Handler);
                trigger();
                ce.Wait(timeout);
                _detach(Handler);
            }

            return res;
        }
    }
}
