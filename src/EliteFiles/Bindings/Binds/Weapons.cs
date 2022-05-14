namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for weapons.
    /// </summary>
    public static class Weapons
    {
        /// <summary>
        /// Gets the category of all <see cref="Weapons"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string PrimaryFire = "PrimaryFire";
        public const string SecondaryFire = "SecondaryFire";
        public const string CycleFireGroupNext = "CycleFireGroupNext";
        public const string CycleFireGroupPrevious = "CycleFireGroupPrevious";
        public const string DeployHardpointToggle = "DeployHardpointToggle";
        public const string DeployHardpointsOnFire = "DeployHardpointsOnFire";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Weapons"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Weapons));
    }
}
