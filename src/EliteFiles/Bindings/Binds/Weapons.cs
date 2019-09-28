using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for weapons.
    /// </summary>
    public static class Weapons
    {
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
