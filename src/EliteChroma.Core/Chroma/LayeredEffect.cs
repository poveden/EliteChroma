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

        public async Task Render(IChroma chroma)
        {
            var canvas = new ChromaCanvas();

            foreach (var layer in Layers)
            {
                layer.Render(canvas);
            }

            await canvas.SetEffect(chroma).ConfigureAwait(false);
        }

        private sealed class LayerComparer : Comparer<EffectLayer>
        {
            public override int Compare(EffectLayer x, EffectLayer y)
            {
                var ord = x.Order.CompareTo(y.Order);

                return (ord != 0) ? ord : 1;
            }
        }
    }
}
