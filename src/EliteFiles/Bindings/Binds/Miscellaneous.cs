using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for miscellaneous.
    /// </summary>
    public static class Miscellaneous
    {
#pragma warning disable 1591, SA1600
        public const string ShipSpotLightToggle = "ShipSpotLightToggle";
        public const string RadarRangeAxis = "RadarRangeAxis";
        public const string RadarIncreaseRange = "RadarIncreaseRange";
        public const string RadarDecreaseRange = "RadarDecreaseRange";
        public const string IncreaseEnginesPower = "IncreaseEnginesPower";
        public const string IncreaseWeaponsPower = "IncreaseWeaponsPower";
        public const string IncreaseSystemsPower = "IncreaseSystemsPower";
        public const string ResetPowerDistribution = "ResetPowerDistribution";
        public const string HMDReset = "HMDReset";
        public const string ToggleCargoScoop = "ToggleCargoScoop";
        public const string EjectAllCargo = "EjectAllCargo";
        public const string LandingGearToggle = "LandingGearToggle";
        public const string MicrophoneMute = "MicrophoneMute";
        public const string MuteButtonMode = "MuteButtonMode";
        public const string CqcMuteButtonMode = "CqcMuteButtonMode";
        public const string UseShieldCell = "UseShieldCell";
        public const string FireChaffLauncher = "FireChaffLauncher";
        public const string ChargeECM = "ChargeECM";
        public const string EnableRumbleTrigger = "EnableRumbleTrigger";
        public const string EnableMenuGroups = "EnableMenuGroups";
        public const string WeaponColourToggle = "WeaponColourToggle";
        public const string EngineColourToggle = "EngineColourToggle";
        public const string NightVisionToggle = "NightVisionToggle";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Miscellaneous"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Miscellaneous));
    }
}
