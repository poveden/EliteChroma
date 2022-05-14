using System.Collections.ObjectModel;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving.
    /// </summary>
    public static class Driving
    {
        /// <summary>
        /// Gets the category of all <see cref="Driving"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.SrvControls;

#pragma warning disable 1591, SA1600
        public const string ToggleDriveAssist = "ToggleDriveAssist";
        public const string DriveAssistDefault = "DriveAssistDefault";
        public const string MouseSteeringXMode = "MouseBuggySteeringXMode";
        public const string MouseSteeringXDecay = "MouseBuggySteeringXDecay";
        public const string MouseRollingXMode = "MouseBuggyRollingXMode";
        public const string MouseRollingXDecay = "MouseBuggyRollingXDecay";
        public const string MouseYMode = "MouseBuggyYMode";
        public const string MouseYDecay = "MouseBuggyYDecay";
        public const string SteeringAxis = "SteeringAxis";
        public const string SteerLeft = "SteerLeftButton";
        public const string SteerRight = "SteerRightButton";
        public const string RollAxisRaw = "BuggyRollAxisRaw";
        public const string RollLeft = "BuggyRollLeftButton";
        public const string RollRight = "BuggyRollRightButton";
        public const string PitchAxis = "BuggyPitchAxis";
        public const string PitchUp = "BuggyPitchUpButton";
        public const string PitchDown = "BuggyPitchDownButton";
        public const string VerticalThrusters = "VerticalThrustersButton";
        public const string PrimaryFire = "BuggyPrimaryFireButton";
        public const string SecondaryFire = "BuggySecondaryFireButton";
        public const string AutoBreak = "AutoBreakBuggyButton";
        public const string Headlights = "HeadlightsBuggyButton";
        public const string ToggleTurret = "ToggleBuggyTurretButton";
        public const string CycleFireGroupNext = "BuggyCycleFireGroupNext";
        public const string CycleFireGroupPrevious = "BuggyCycleFireGroupPrevious";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Driving"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Driving));

        /// <summary>
        /// Gets the collection of all rotation-related <see cref="Driving"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Rotation { get; } = new ReadOnlyCollection<string>(new[]
        {
            SteeringAxis,
            SteerLeft,
            SteerRight,
            RollAxisRaw,
            RollLeft,
            RollRight,
            PitchAxis,
            PitchUp,
            PitchDown,
        });

        /// <summary>
        /// Gets the collection of all thrust-related <see cref="Driving"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Thrust { get; } = new ReadOnlyCollection<string>(new[]
        {
            VerticalThrusters,
        });

        /// <summary>
        /// Gets the collection of all weapon-related <see cref="Driving"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Weapons { get; } = new ReadOnlyCollection<string>(new[]
        {
            PrimaryFire,
            SecondaryFire,
            CycleFireGroupNext,
            CycleFireGroupPrevious,
            ToggleTurret,
        });
    }
}
