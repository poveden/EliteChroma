using System.Runtime.CompilerServices;
using ChromaWrapper.Sdk;

namespace EliteChroma.Core.Chroma
{
    public class ChromaEffect<TState>
    {
        private static readonly Comparer<ChromaEffectLayer<TState>> _comparer = new LayerComparer();

        private readonly List<ChromaEffectLayer<TState>> _layers = new List<ChromaEffectLayer<TState>>();
        private readonly ChromaCanvas _canvas = new ChromaCanvas();

        private readonly ConditionalWeakTable<IChromaSdk, IReadOnlyCollection<Guid>> _activeEffectIds = new ConditionalWeakTable<IChromaSdk, IReadOnlyCollection<Guid>>();

        public IReadOnlyList<ChromaEffectLayer<TState>> Layers => _layers;

        public bool Add(ChromaEffectLayer<TState> layer)
        {
            int i = _layers.BinarySearch(layer, _comparer);

            if (i >= 0)
            {
                return false;
            }

            _layers.Insert(~i, layer);
            return true;
        }

        public void Clear()
        {
            _layers.Clear();
        }

        public bool Remove(ChromaEffectLayer<TState> layer)
        {
            return _layers.Remove(layer);
        }

        public void Render(IChromaSdk chroma, TState state)
        {
            _canvas.ClearCanvas();

            for (int i = 0; i < _layers.Count; i++)
            {
                _layers[i].Render(_canvas, state);
            }

            _ = _activeEffectIds.TryGetValue(chroma, out IReadOnlyCollection<Guid>? oldEffectIds);
            _activeEffectIds.AddOrUpdate(chroma, _canvas.SetEffect(chroma));

            if (oldEffectIds != null)
            {
                foreach (Guid effectId in oldEffectIds)
                {
                    chroma.DeleteEffect(effectId);
                }
            }
        }

        private sealed class LayerComparer : Comparer<ChromaEffectLayer<TState>>
        {
            public override int Compare(ChromaEffectLayer<TState>? x, ChromaEffectLayer<TState>? y)
            {
                if (x == y)
                {
                    return 0;
                }

                if (x == null)
                {
                    return -1;
                }

                if (y == null)
                {
                    return 1;
                }

                int ord = x.Order.CompareTo(y.Order);

                return (ord != 0) ? ord : x.GetHashCode().CompareTo(y.GetHashCode());
            }
        }
    }
}
