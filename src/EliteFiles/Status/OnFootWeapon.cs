using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EliteFiles.Status
{
    /// <summary>
    /// Provides methods to work with weapons.
    /// </summary>
    public static class OnFootWeapon
    {
#pragma warning disable 1591, SA1600
        public const string Unarmed = "humanoid_fists";

        public const string Energylink = "humanoid_rechargetool";
        public const string ProfileAnalyser = "humanoid_companalyser";
        public const string GeneticSampler = "humanoid_sampletool";
        public const string ArcCutter = "humanoid_repairtool";

        public const string KarmaAR50 = "wpn_m_assaultrifle_kinetic_fauto";
        public const string KarmaC44 = "wpn_m_submachinegun_kinetic_fauto";
        public const string KarmaL6 = "wpn_m_launcher_rocket_sauto";
        public const string KarmaP15 = "wpn_s_pistol_kinetic_sauto";

        public const string ManticoreExecutioner = "wpn_m_sniper_plasma_charged";
        public const string ManticoreIntimidator = "wpn_m_shotgun_plasma_doublebarrel";
        public const string ManticoreOppressor = "wpn_m_assaultrifle_plasma_fauto";
        public const string ManticoreTormentor = "wpn_s_pistol_plasma_charged";

        public const string TKAphelion = "wpn_m_assaultrifle_laser_fauto";
        public const string TKEclipse = "wpn_m_submachinegun_laser_fauto";
        public const string TKZenith = "wpn_s_pistol_laser_sauto";
#pragma warning restore 1591, SA1600

        private static readonly IReadOnlyList<(Kind, Regex)> _rx = new List<(Kind, Regex)>
        {
            (Kind.Unarmed, BuildRegexFromLiteral(Unarmed)),
            (Kind.Energylink, BuildRegexFromLiteral(Energylink)),
            (Kind.ProfileAnalyser, BuildRegexFromLiteral(ProfileAnalyser)),
            (Kind.GeneticSampler, BuildRegexFromLiteral(GeneticSampler)),
            (Kind.ArcCutter, BuildRegexFromLiteral(ArcCutter)),
            (Kind.Weapon, BuildRegexFromExpression(@"wpn_\w+")),
        };

        /// <summary>
        /// Specifies weapon kinds.
        /// </summary>
        public enum Kind
        {
            /// <summary>Unknown kind.</summary>
            Unknown = 0,

            /// <summary>Unarmed.</summary>
            Unarmed,

            /// <summary>Energylink.</summary>
            Energylink,

            /// <summary>Profile analyser.</summary>
            ProfileAnalyser,

            /// <summary>Genetic sampler.</summary>
            GeneticSampler,

            /// <summary>Arc cutter.</summary>
            ArcCutter,

            /// <summary>Weapon.</summary>
            Weapon,
        }

        /// <summary>
        /// Obtains the kind of a weapon.
        /// </summary>
        /// <param name="weaponName">The weapon name.</param>
        /// <returns>The weapon kind.</returns>
        public static Kind GetKind(string? weaponName)
        {
            if (string.IsNullOrEmpty(weaponName))
            {
                return Kind.Unknown;
            }

            foreach ((Kind kind, Regex rx) in _rx)
            {
                if (rx.IsMatch(weaponName))
                {
                    return kind;
                }
            }

            return Kind.Unknown;
        }

        private static Regex BuildRegexFromLiteral(string str)
        {
            string escaped = Regex.Escape(str);

            return new Regex($"^{escaped}$|^\\${escaped}_name;$", RegexOptions.IgnoreCase);
        }

        private static Regex BuildRegexFromExpression(string regexExpr)
        {
            return new Regex($"^{regexExpr}$|^\\${regexExpr}_name;$", RegexOptions.IgnoreCase);
        }
    }
}
