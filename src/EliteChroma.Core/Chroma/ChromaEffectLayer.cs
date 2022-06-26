namespace EliteChroma.Core.Chroma
{
    public abstract class ChromaEffectLayer<TState>
    {
        public virtual int Order => 500;

        internal void Render(ChromaCanvas canvas, TState state)
        {
            OnRender(canvas, state);
        }

        protected abstract void OnRender(ChromaCanvas canvas, TState state);
    }
}
