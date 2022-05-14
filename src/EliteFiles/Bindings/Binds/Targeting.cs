namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for targeting.
    /// </summary>
    public static class Targeting
    {
        /// <summary>
        /// Gets the category of all <see cref="Targeting"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string SelectTarget = "SelectTarget";
        public const string CycleNextTarget = "CycleNextTarget";
        public const string CyclePreviousTarget = "CyclePreviousTarget";
        public const string SelectHighestThreat = "SelectHighestThreat";
        public const string CycleNextHostileTarget = "CycleNextHostileTarget";
        public const string CyclePreviousHostileTarget = "CyclePreviousHostileTarget";
        public const string TargetWingman0 = "TargetWingman0";
        public const string TargetWingman1 = "TargetWingman1";
        public const string TargetWingman2 = "TargetWingman2";
        public const string SelectTargetsTarget = "SelectTargetsTarget";
        public const string WingNavLock = "WingNavLock";
        public const string CycleNextSubsystem = "CycleNextSubsystem";
        public const string CyclePreviousSubsystem = "CyclePreviousSubsystem";
        public const string TargetNextRouteSystem = "TargetNextRouteSystem";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Targeting"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Targeting));
    }
}
