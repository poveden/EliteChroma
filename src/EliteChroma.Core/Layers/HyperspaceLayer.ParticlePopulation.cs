using System.Collections.Generic;
using Colore.Data;

namespace EliteChroma.Core.Layers
{
    internal partial class HyperspaceLayer
    {
        private sealed class ParticlePopulation
        {
            public static readonly IReadOnlyCollection<ParticlePopulation> Supercruise = new[]
            {
                new ParticlePopulation(JumpPhase.PreJump, 20, -20, Color.Blue),
                new ParticlePopulation(JumpPhase.Tunnel, 20, -20, Color.Blue),
                new ParticlePopulation(JumpPhase.Jump, 20, -20, Color.Blue),
            };

            public static readonly IReadOnlyCollection<ParticlePopulation> Hyperspace = new[]
            {
                new ParticlePopulation(JumpPhase.PreJump, 20, -20, Color.Blue),
                new ParticlePopulation(JumpPhase.Tunnel, 100, -80, Color.White),
                new ParticlePopulation(JumpPhase.Jump, 10, -20, Color.Blue),
                new ParticlePopulation(JumpPhase.Jump, 5, -10, new Color(160, 255, 255)),
            };

            private ParticlePopulation(JumpPhase jumpPhase, double spawnsPerSecond, double zSpeedPerSecond, Color color)
            {
                JumpPhase = jumpPhase;
                SpawnsPerSecond = spawnsPerSecond;
                ZSpeedPerSecond = zSpeedPerSecond;
                Color = color;
            }

            public JumpPhase JumpPhase { get; }

            public double SpawnsPerSecond { get; }

            public double ZSpeedPerSecond { get; }

            public Color Color { get; }
        }
    }
}
