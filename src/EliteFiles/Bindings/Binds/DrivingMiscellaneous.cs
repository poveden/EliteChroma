using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving miscellaneous.
    /// </summary>
    public static class DrivingMiscellaneous
    {
        /// <summary>
        /// Gets the category of all <see cref="DrivingMiscellaneous"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.SrvControls;

#pragma warning disable 1591, SA1600
        public const string IncreaseEnginesPower = "IncreaseEnginesPower_Buggy";
        public const string IncreaseWeaponsPower = "IncreaseWeaponsPower_Buggy";
        public const string IncreaseSystemsPower = "IncreaseSystemsPower_Buggy";
        public const string ResetPowerDistribution = "ResetPowerDistribution_Buggy";
        public const string ToggleCargoScoop = "ToggleCargoScoop_Buggy";
        public const string EjectAllCargo = "EjectAllCargo_Buggy";
        public const string RecallDismissShip = "RecallDismissShip";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DrivingMiscellaneous"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DrivingMiscellaneous));
    }
}
