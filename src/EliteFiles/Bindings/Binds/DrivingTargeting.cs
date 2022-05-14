namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving targeting.
    /// </summary>
    public static class DrivingTargeting
    {
        /// <summary>
        /// Gets the category of all <see cref="DrivingTargeting"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.SrvControls;

#pragma warning disable 1591, SA1600
        public const string SelectTarget = "SelectTarget_Buggy";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DrivingTargeting"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DrivingTargeting));
    }
}
