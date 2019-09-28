using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for multi-crew.
    /// </summary>
    public static class MultiCrew
    {
#pragma warning disable 1591, SA1600
        public const string ToggleMode = "MultiCrewToggleMode";
        public const string PrimaryFire = "MultiCrewPrimaryFire";
        public const string SecondaryFire = "MultiCrewSecondaryFire";
        public const string PrimaryUtilityFire = "MultiCrewPrimaryUtilityFire";
        public const string SecondaryUtilityFire = "MultiCrewSecondaryUtilityFire";
        public const string ThirdPersonMouseXMode = "MultiCrewThirdPersonMouseXMode";
        public const string ThirdPersonMouseXDecay = "MultiCrewThirdPersonMouseXDecay";
        public const string ThirdPersonMouseYMode = "MultiCrewThirdPersonMouseYMode";
        public const string ThirdPersonMouseYDecay = "MultiCrewThirdPersonMouseYDecay";
        public const string ThirdPersonYawAxisRaw = "MultiCrewThirdPersonYawAxisRaw";
        public const string ThirdPersonYawLeft = "MultiCrewThirdPersonYawLeftButton";
        public const string ThirdPersonYawRight = "MultiCrewThirdPersonYawRightButton";
        public const string ThirdPersonPitchAxisRaw = "MultiCrewThirdPersonPitchAxisRaw";
        public const string ThirdPersonPitchUp = "MultiCrewThirdPersonPitchUpButton";
        public const string ThirdPersonPitchDown = "MultiCrewThirdPersonPitchDownButton";
        public const string ThirdPersonMouseSensitivity = "MultiCrewThirdPersonMouseSensitivity";
        public const string ThirdPersonFovAxisRaw = "MultiCrewThirdPersonFovAxisRaw";
        public const string ThirdPersonFovOut = "MultiCrewThirdPersonFovOutButton";
        public const string ThirdPersonFovIn = "MultiCrewThirdPersonFovInButton";
        public const string CockpitUICycleForward = "MultiCrewCockpitUICycleForward";
        public const string CockpitUICycleBackward = "MultiCrewCockpitUICycleBackward";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="MultiCrew"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(MultiCrew));
    }
}
