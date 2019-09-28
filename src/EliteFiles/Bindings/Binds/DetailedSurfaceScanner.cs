using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the detailed surface scanner.
    /// </summary>
    public static class DetailedSurfaceScanner
    {
#pragma warning disable 1591, SA1600
        public const string ChangeScannedAreaViewToggle = "ExplorationSAAChangeScannedAreaViewToggle";
        public const string Exit = "ExplorationSAAExitThirdPerson";
        public const string MouseXMode = "SAAThirdPersonMouseXMode";
        public const string MouseXDecay = "SAAThirdPersonMouseXDecay";
        public const string MouseYMode = "SAAThirdPersonMouseYMode";
        public const string MouseYDecay = "SAAThirdPersonMouseYDecay";
        public const string MouseSensitivity = "SAAThirdPersonMouseSensitivity";
        public const string YawAxisRaw = "SAAThirdPersonYawAxisRaw";
        public const string YawLeft = "SAAThirdPersonYawLeftButton";
        public const string YawRight = "SAAThirdPersonYawRightButton";
        public const string PitchAxisRaw = "SAAThirdPersonPitchAxisRaw";
        public const string PitchUp = "SAAThirdPersonPitchUpButton";
        public const string PitchDown = "SAAThirdPersonPitchDownButton";
        public const string FovAxisRaw = "SAAThirdPersonFovAxisRaw";
        public const string FovOut = "SAAThirdPersonFovOutButton";
        public const string FovIn = "SAAThirdPersonFovInButton";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DetailedSurfaceScanner"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DetailedSurfaceScanner));
    }
}
