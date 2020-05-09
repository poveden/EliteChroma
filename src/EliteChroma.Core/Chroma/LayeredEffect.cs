using System.Collections.Generic;
using System.Threading.Tasks;
using Colore;

namespace EliteChroma.Chroma
{
    public sealed class LayeredEffect
    {
        private static readonly LayerComparer _comparer = new LayerComparer();

        private readonly List<EffectLayer> _layers = new List<EffectLayer>();

        public IReadOnlyList<EffectLayer> Layers => _layers;

        public bool Add(EffectLayer layer)
        {
            var i = _layers.BinarySearch(layer, _comparer);

            if (i >= 0)
            {
                return false;
            }

            _layers.Insert(~i, layer);
            return true;
        }

        public void Clear() => _layers.Clear();

        public bool Remove(EffectLayer layer) => _layers.Remove(layer);

        public async Task Render(IChroma chroma, object state)
        {
            var canvas = new ChromaCanvas();

            for (var i = 0; i < _layers.Count; i++)
            {
                _layers[i].Render(canvas, state);
            }

            await canvas.SetEffect(chroma).ConfigureAwait(false);
        }

        private sealed class LayerComparer : Comparer<EffectLayer>
        {
            public override int Compare(EffectLayer x, EffectLayer y)
            {
                if (x == y)
                {
                    return 0;
                }

                var ord = x.Order.CompareTo(y.Order);

                return (ord != 0) ? ord : x.GetHashCode().CompareTo(y.GetHashCode());
            }
        }
    }
}
