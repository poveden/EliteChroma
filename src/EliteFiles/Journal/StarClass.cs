using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EliteFiles.Journal
{
    /// <summary>
    /// Provides methods to work with star classes.
    /// </summary>
    public static class StarClass
    {
#pragma warning disable 1591, SA1600
        // Kind.MainSequence
        public const string O = "O";
        public const string B = "B";
        public const string A = "A";
        public const string F = "F";
        public const string G = "G";
        public const string K = "K";
        public const string M = "M";

        // Kind.BrownDwarf
        public const string L = "L";
        public const string T = "T";
        public const string Y = "Y";

        // Kind.Protostar
        public const string HerbigAeBe = "AeBe";
        public const string TTauri = "TTS";

        // Kind.Carbon
        public const string C = "C";
        public const string MS = "MS";
        public const string S = "S";

        // Kind.WolfRayet
        public const string W = "W";

        // Kind.BlackHole
        public const string BlackHole = "H";

        // Kind.Neutron
        public const string Neutron = "N";

        // Kind.WhiteDwarf
        public const string D = "D";

        // Kind.Other
        public const string Exotic = "X";
        public const string RoguePlanet = "RoguePlanet";
        public const string Nebula = "Nebula";
        public const string StellarRemnantNebula = "StellarRemnantNebula";
#pragma warning restore 1591, SA1600

        private static readonly IReadOnlyList<(Kind, Regex)> _rx = new List<(Kind, Regex)>
        {
            (Kind.MainSequence, new Regex("^([OBAFGKM])(?:_[A-Za-z]+)?$")),
            (Kind.BrownDwarf, new Regex("^([LTY])$")),
            (Kind.Protostar, new Regex("^(AeBe|TTS)$")),
            (Kind.Carbon, new Regex("^(C|MS|S)$")),
            (Kind.Carbon, new Regex("^(C)[A-Z][a-z]?$")),
            (Kind.WolfRayet, new Regex("^(W)$")),
            (Kind.WolfRayet, new Regex("^(W)[A-Z][A-Z]?$")),
            (Kind.BlackHole, new Regex("^(H)$")),
            (Kind.BlackHole, new Regex("^SupermassiveBlack(H)ole$")),
            (Kind.Neutron, new Regex("^(N)$")),
            (Kind.WhiteDwarf, new Regex("^(D)$")),
            (Kind.WhiteDwarf, new Regex("^(D)[A-Z][A-Z]?$")),
            (Kind.Other, new Regex("^(X|RoguePlanet|Nebula|StellarRemnantNebula)$")),
        };

        /// <summary>
        /// Specifies star class kinds.
        /// </summary>
        public enum Kind
        {
            /// <summary>Unknown kind.</summary>
            Unknown = 0,

            /// <summary>Main sequence star.</summary>
            MainSequence,

            /// <summary>Brown dwarf.</summary>
            BrownDwarf,

            /// <summary>Protostar.</summary>
            Protostar,

            /// <summary>Carbon star.</summary>
            Carbon,

            /// <summary>Wolf-Rayet star.</summary>
            WolfRayet,

            /// <summary>Black hole.</summary>
            BlackHole,

            /// <summary>Neutron star.</summary>
            Neutron,

            /// <summary>White dwarf.</summary>
            WhiteDwarf,

            /// <summary>Other type.</summary>
            Other,
        }

        /// <summary>
        /// Obtains the kind of a star class.
        /// </summary>
        /// <param name="starClass">The star class.</param>
        /// <param name="baseClass">The base star class, or <c>null</c> is the kind is unknown.</param>
        /// <returns>The star class kind.</returns>
        public static Kind GetKind(string? starClass, out string? baseClass)
        {
            if (string.IsNullOrEmpty(starClass))
            {
                baseClass = null;
                return Kind.Unknown;
            }

            foreach ((Kind kind, Regex rx) in _rx)
            {
                Match m = rx.Match(starClass);

                if (m.Success)
                {
                    baseClass = m.Groups[1].Value;
                    return kind;
                }
            }

            baseClass = null;
            return Kind.Unknown;
        }
    }
}
