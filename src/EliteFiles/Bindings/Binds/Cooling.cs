namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for cooling.
    /// </summary>
    public static class Cooling
    {
        /// <summary>
        /// Gets the category of all <see cref="Cooling"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string ToggleButtonUpInput = "ToggleButtonUpInput";
        public const string DeployHeatSink = "DeployHeatSink";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Cooling"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Cooling));
    }
}
