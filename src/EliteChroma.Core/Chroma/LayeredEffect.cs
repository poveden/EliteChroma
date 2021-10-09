using System;
using System.Collections.Generic;
using ChromaWrapper.Sdk;

namespace EliteChroma.Chroma
{
    public sealed class LayeredEffect
    {
        private static readonly LayerComparer _comparer = new LayerComparer();

        private readonly List<EffectLayer> _layers = new List<EffectLayer>();
        private readonly ChromaCanvas _canvas = new ChromaCanvas();

        private IReadOnlyCollection<Guid> _activeEffectIds = Array.Empty<Guid>();

        public IReadOnlyList<EffectLayer> Layers => _layers;

        public bool Add(EffectLayer layer)
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

        public bool Remove(EffectLayer layer)
        {
            return _layers.Remove(layer);
        }

        public void Render(IChromaSdk chroma, object state)
        {
            _canvas.ClearCanvas();

            for (int i = 0; i < _layers.Count; i++)
            {
                _layers[i].Render(_canvas, state);
            }

            IReadOnlyCollection<Guid> oldEffectIds = _activeEffectIds;
            _activeEffectIds = _canvas.SetEffect(chroma);

            foreach (Guid effectId in oldEffectIds)
            {
                chroma.DeleteEffect(effectId);
            }
        }

        private sealed class LayerComparer : Comparer<EffectLayer>
        {
            public override int Compare(EffectLayer? x, EffectLayer? y)
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
