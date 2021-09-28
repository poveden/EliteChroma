using System.Collections.Generic;
using ChromaWrapper;
using EliteChroma.Elite;

namespace EliteChroma.Core.Layers
{
    internal partial class HyperspaceLayer
    {
        private sealed class ParticlePopulation
        {
            public static readonly IReadOnlyCollection<ParticlePopulation> Supercruise = new[]
            {
                new ParticlePopulation(JumpPhase.PreJump, 20, -20, GameColors.SupercruiseTunnelParticle),
                new ParticlePopulation(JumpPhase.Tunnel, 20, -20, GameColors.SupercruiseTunnelParticle),
                new ParticlePopulation(JumpPhase.Jump, 20, -20, GameColors.SupercruiseTunnelParticle),
            };

            public static readonly IReadOnlyCollection<ParticlePopulation> Hyperspace = new[]
            {
                new ParticlePopulation(JumpPhase.PreJump, 20, -20, GameColors.SupercruiseTunnelParticle),
                new ParticlePopulation(JumpPhase.Tunnel, 100, -80, GameColors.HyperspaceTunnelParticle),
                new ParticlePopulation(JumpPhase.Jump, 10, -20, GameColors.HyperspaceDimStar),
                new ParticlePopulation(JumpPhase.Jump, 5, -10, GameColors.HyperspaceBrightStar),
            };

            private ParticlePopulation(JumpPhase jumpPhase, double spawnsPerSecond, double zSpeedPerSecond, ChromaColor color)
            {
                JumpPhase = jumpPhase;
                SpawnsPerSecond = spawnsPerSecond;
                ZSpeedPerSecond = zSpeedPerSecond;
                Color = color;
            }

            public JumpPhase JumpPhase { get; }

            public double SpawnsPerSecond { get; }

            public double ZSpeedPerSecond { get; }

            public ChromaColor Color { get; }
        }
    }
}
