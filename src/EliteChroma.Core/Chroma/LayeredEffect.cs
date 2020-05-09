using System.Collections.Generic;
using System.Threading.Tasks;
using Colore;

namespace EliteChroma.Chroma
{
    public sealed class LayeredEffect
    {
        private static readonly LayerComparer _comparer = new LayerComparer();

        public LayeredEffect()
        {
            Layers = new SortedSet<EffectLayer>(_comparer);
        }

        public SortedSet<EffectLayer> Layers { get; }

        public async Task Render(IChroma chroma, object state)
        {
            var canvas = new ChromaCanvas();

            foreach (var layer in Layers)
            {
                layer.Render(canvas, state);
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
