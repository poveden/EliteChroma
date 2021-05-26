using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving mode switches.
    /// </summary>
    public static class DrivingModeSwitches
    {
        /// <summary>
        /// Gets the category of all <see cref="DrivingModeSwitches"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.SrvControls;

#pragma warning disable 1591, SA1600
        public const string UIFocus = "UIFocus_Buggy";
        public const string FocusLeftPanel = "FocusLeftPanel_Buggy";
        public const string FocusCommsPanel = "FocusCommsPanel_Buggy";
        public const string QuickCommsPanel = "QuickCommsPanel_Buggy";
        public const string FocusRadarPanel = "FocusRadarPanel_Buggy";
        public const string FocusRightPanel = "FocusRightPanel_Buggy";
        public const string GalaxyMapOpen = "GalaxyMapOpen_Buggy";
        public const string SystemMapOpen = "SystemMapOpen_Buggy";
        public const string OpenCodexGoToDiscovery = "OpenCodexGoToDiscovery_Buggy";
        public const string PlayerHUDModeToggle = "PlayerHUDModeToggle_Buggy";
        public const string HeadLookToggle = "HeadLookToggle_Buggy";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DrivingModeSwitches"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DrivingModeSwitches));
    }
}
