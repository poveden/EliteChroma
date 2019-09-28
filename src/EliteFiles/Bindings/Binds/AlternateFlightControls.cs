using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for alternate flight controls.
    /// </summary>
    public static class AlternateFlightControls
    {
#pragma warning disable 1591, SA1600
        public const string Toggle = "UseAlternateFlightValuesToggle";
        public const string YawAxis = "YawAxisAlternate";
        public const string RollAxis = "RollAxisAlternate";
        public const string PitchAxis = "PitchAxisAlternate";
        public const string LateralThrust = "LateralThrustAlternate";
        public const string VerticalThrust = "VerticalThrustAlternate";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Cooling"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(AlternateFlightControls));
    }
}
