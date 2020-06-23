using System;
using EliteChroma.Elite;

namespace EliteChroma.Core
{
    public sealed class LayerRenderState
    {
        public LayerRenderState(GameState gameState, ChromaColors colors)
        {
            GameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
            Colors = colors ?? throw new ArgumentNullException(nameof(colors));
        }

        public GameState GameState { get; }

        public ChromaColors Colors { get; }
    }
}
