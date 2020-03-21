using System;

namespace EliteChroma.Chroma
{
    public abstract class EffectLayer
    {
        public virtual int Order => 500;

        internal void Render(ChromaCanvas canvas, object state) => OnRender(canvas, state);

        protected abstract void OnRender(ChromaCanvas canvas, object state);
    }
}
