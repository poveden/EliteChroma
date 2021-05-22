using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for on foot mode switches.
    /// </summary>
    public static class OnFootModeSwitches
    {
#pragma warning disable 1591, SA1600
        public const string GalaxyMapOpen = "GalaxyMapOpen_Humanoid";
        public const string SystemMapOpen = "SystemMapOpen_Humanoid";
        public const string FocusCommsPanel = "FocusCommsPanel_Humanoid";
        public const string QuickCommsPanel = "QuickCommsPanel_Humanoid";
        public const string OpenAccessPanelButton = "HumanoidOpenAccessPanelButton";
        public const string ConflictContextualUIButton = "HumanoidConflictContextualUIButton";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="OnFootModeSwitches"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(OnFootModeSwitches));
    }
}
