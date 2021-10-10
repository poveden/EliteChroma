using System;
using ChromaWrapper;
using ChromaWrapper.Sdk;

namespace EliteChroma.Chroma
{
    public static class EffectExtensions
    {
        public static IKeyGridEffect CombineKey(this IKeyGridEffect effect, ChromaColor c, double cPct = 0.5)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Key.Count; i++)
            {
                effect.Key[i] = ((ChromaColor)effect.Key[i]).Combine(c, cPct);
            }

            return effect;
        }

        public static IKeyGridEffect MaxKey(this IKeyGridEffect effect, ChromaColor c)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Key.Count; i++)
            {
                effect.Key[i] = ((ChromaColor)effect.Key[i]).Max(c);
            }

            return effect;
        }

        public static IKeyGridEffect MaxAt(this IKeyGridEffect effect, int row, int column, ChromaColor c)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            effect.Key[row, column] = ((ChromaColor)effect.Key[row, column]).Max(c);

            return effect;
        }

        public static ILedGridEffect Combine(this ILedGridEffect effect, ChromaColor c, double cPct = 0.5)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Color.Count; i++)
            {
                effect.Color[i] = effect.Color[i].Combine(c, cPct);
            }

            return effect;
        }

        public static ILedGridEffect Max(this ILedGridEffect effect, ChromaColor c)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Color.Count; i++)
            {
                effect.Color[i] = effect.Color[i].Max(c);
            }

            return effect;
        }

        public static ILedArrayEffect Combine(this ILedArrayEffect effect, ChromaColor c, double cPct = 0.5)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Color.Count; i++)
            {
                effect.Color[i] = effect.Color[i].Combine(c, cPct);
            }

            return effect;
        }

        public static ILedArrayEffect Max(this ILedArrayEffect effect, ChromaColor c)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            for (int i = 0; i < effect.Color.Count; i++)
            {
                effect.Color[i] = effect.Color[i].Max(c);
            }

            return effect;
        }
    }
}
