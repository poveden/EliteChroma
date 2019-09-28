using System;

namespace EliteChroma.Chroma
{
    public abstract class EffectLayer
    {
        public virtual int Order => 500;

        internal void Render(ChromaCanvas canvas) => OnRender(canvas);

        protected abstract void OnRender(ChromaCanvas canvas);
    }
}
