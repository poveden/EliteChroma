namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for mode switches.
    /// </summary>
    public static class ModeSwitches
    {
        /// <summary>
        /// Gets the category of all <see cref="ModeSwitches"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string UIFocus = "UIFocus";
        public const string UIFocusMode = "UIFocusMode";
        public const string FocusLeftPanel = "FocusLeftPanel";
        public const string FocusCommsPanel = "FocusCommsPanel";
        public const string FocusOnTextEntryField = "FocusOnTextEntryField";
        public const string QuickCommsPanel = "QuickCommsPanel";
        public const string FocusRadarPanel = "FocusRadarPanel";
        public const string FocusRightPanel = "FocusRightPanel";
        public const string LeftPanelFocusOptions = "LeftPanelFocusOptions";
        public const string CommsPanelFocusOptions = "CommsPanelFocusOptions";
        public const string RolePanelFocusOptions = "RolePanelFocusOptions";
        public const string RightPanelFocusOptions = "RightPanelFocusOptions";
        public const string EnableCameraLockOn = "EnableCameraLockOn";
        public const string GalaxyMapOpen = "GalaxyMapOpen";
        public const string SystemMapOpen = "SystemMapOpen";
        public const string ShowPGScoreSummaryInput = "ShowPGScoreSummaryInput";
        public const string HeadLookToggle = "HeadLookToggle";
        public const string Pause = "Pause";
        public const string FriendsMenu = "FriendsMenu";
        public const string OpenCodexGoToDiscovery = "OpenCodexGoToDiscovery";
        public const string PlayerHUDModeToggle = "PlayerHUDModeToggle";
        public const string ExplorationFssEnter = "ExplorationFSSEnter";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="ModeSwitches"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(ModeSwitches));
    }
}
