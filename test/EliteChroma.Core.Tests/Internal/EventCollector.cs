using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EliteChroma.Core.Tests.Internal
{
    internal sealed class EventCollector<T>
    {
        private readonly Action<EventHandler<T>> _attach;
        private readonly Action<EventHandler<T>> _detach;
        private readonly string _name;

        public EventCollector(Action<EventHandler<T>> attach, Action<EventHandler<T>> detach, string name)
        {
            _attach = attach;
            _detach = detach;
            _name = name;
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
                bool ok = ss.Wait(timeout);
                _detach(Handler);

                if (!ok)
                {
                    throw new TimeoutException();
                }
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

                    if (ce.IsSet)
                    {
                        string list = string.Join(',', res.Select(x => $"{x}"));
                        throw new InvalidOperationException($"More than {count} events received in collector '{_name}': {list}.");
                    }

                    ce.Signal();
                }

                _attach(Handler);
                trigger();
                bool ok = ce.Wait(timeout);
                _detach(Handler);

                if (!ok)
                {
                    string list = string.Join(',', res.Select(x => $"{x}"));
                    throw new TimeoutException($"Timeout in collector '{_name}' after receiving the following events: {list}.");
                }
            }

            return res;
        }
    }
}
