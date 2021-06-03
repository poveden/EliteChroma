using System;

namespace EliteFiles.Status
{
    /// <summary>
    /// Represents additional game status flags.
    /// </summary>
    [Flags]
    public enum Flags2 : long
    {
        /// <summary>No flags are set (e.g. when the game is not running).</summary>
        None = 0,

        /// <summary>On foot.</summary>
        OnFoot = 1,

        /// <summary>In taxi.</summary>
        InTaxi = 1 << 1,

        /// <summary>In multi-crew.</summary>
        InMultiCrew = 1 << 2,

        /// <summary>On foot in station.</summary>
        OnFootInStation = 1 << 3,

        /// <summary>On foot on planet.</summary>
        OnFootOnPlanet = 1 << 4,

        /// <summary>Aiming down weapon sights.</summary>
        AimDownSight = 1 << 5,

        /// <summary>Low oxygen.</summary>
        LowOxygen = 1 << 6,

        /// <summary>Low health.</summary>
        LowHealth = 1 << 7,

        /// <summary>Cold ambient temperature.</summary>
        Cold = 1 << 8,

        /// <summary>Hot ambient temperature.</summary>
        Hot = 1 << 9,

        /// <summary>Very cold ambient temperature.</summary>
        VeryCold = 1 << 10,

        /// <summary>Very hot ambient temperature.</summary>
        VeryHot = 1 << 11,

        /// <summary>Glide mode.</summary>
        GlideMode = 1 << 12,

        /// <summary>On foot in hangar.</summary>
        OnFootInHangar = 1 << 13,

        /// <summary>On foot in social space.</summary>
        OnFootSocialSpace = 1 << 14,

        /// <summary>On foot on exterior.</summary>
        OnFootExterior = 1 << 15,

        /// <summary>Breathable atmosphere.</summary>
        BreathableAtmosphere = 1 << 16,
    }
}
