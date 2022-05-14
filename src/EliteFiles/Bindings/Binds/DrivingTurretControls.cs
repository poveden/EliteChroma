namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving turret controls.
    /// </summary>
    public static class DrivingTurretControls
    {
        /// <summary>
        /// Gets the category of all <see cref="DrivingTurretControls"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.SrvControls;

#pragma warning disable 1591, SA1600
        public const string MouseXMode = "MouseTurretXMode";
        public const string MouseXDecay = "MouseTurretXDecay";
        public const string MouseYMode = "MouseTurretYMode";
        public const string MouseYDecay = "MouseTurretYDecay";
        public const string YawAxisRaw = "BuggyTurretYawAxisRaw";
        public const string YawLeft = "BuggyTurretYawLeftButton";
        public const string YawRight = "BuggyTurretYawRightButton";
        public const string PitchAxisRaw = "BuggyTurretPitchAxisRaw";
        public const string PitchUp = "BuggyTurretPitchUpButton";
        public const string PitchDown = "BuggyTurretPitchDownButton";
        public const string MouseSensitivity = "BuggyTurretMouseSensitivity";
        public const string MouseDeadzone = "BuggyTurretMouseDeadzone";
        public const string MouseLinearity = "BuggyTurretMouseLinearity";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DrivingTurretControls"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DrivingTurretControls));
    }
}
