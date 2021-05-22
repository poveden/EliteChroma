using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight miscellaneous.
    /// </summary>
    public static class FlightMiscellaneous
    {
        /// <summary>
        /// Gets the category of all <see cref="FlightMiscellaneous"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string ToggleFlightAssist = "ToggleFlightAssist";
        public const string UseBoostJuice = "UseBoostJuice";
        public const string HyperSuperCombination = "HyperSuperCombination";
        public const string Supercruise = "Supercruise";
        public const string Hyperspace = "Hyperspace";
        public const string DisableRotationCorrectToggle = "DisableRotationCorrectToggle";
        public const string OrbitLinesToggle = "OrbitLinesToggle";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FlightMiscellaneous"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FlightMiscellaneous));
    }
}
