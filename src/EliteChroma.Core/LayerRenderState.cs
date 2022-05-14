using EliteChroma.Elite;

namespace EliteChroma.Core
{
    public sealed class LayerRenderState
    {
        public LayerRenderState(GameState gameState, ChromaColors colors)
        {
            ArgumentNullException.ThrowIfNull(gameState);
            ArgumentNullException.ThrowIfNull(colors);

            GameState = gameState;
            Colors = colors;
        }

        public GameState GameState { get; }

        public ChromaColors Colors { get; }
    }
}
