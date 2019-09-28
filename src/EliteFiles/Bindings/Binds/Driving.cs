using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for driving.
    /// </summary>
    public static class Driving
    {
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
    }
}
