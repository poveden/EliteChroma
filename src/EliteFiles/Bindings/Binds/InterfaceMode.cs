namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the interface mode.
    /// </summary>
    public static class InterfaceMode
    {
        /// <summary>
        /// Gets the category of all <see cref="InterfaceMode"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.GeneralControls;

#pragma warning disable 1591, SA1600
        public const string Up = "UI_Up";
        public const string Down = "UI_Down";
        public const string Left = "UI_Left";
        public const string Right = "UI_Right";
        public const string Select = "UI_Select";
        public const string Back = "UI_Back";
        public const string Toggle = "UI_Toggle";
        public const string CycleNextPanel = "CycleNextPanel";
        public const string CyclePreviousPanel = "CyclePreviousPanel";
        public const string CycleNextPage = "CycleNextPage";
        public const string CyclePreviousPage = "CyclePreviousPage";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="InterfaceMode"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(InterfaceMode));
    }
}
