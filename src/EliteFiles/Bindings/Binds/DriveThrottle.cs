using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for drive throttle.
    /// </summary>
    public static class DriveThrottle
    {
#pragma warning disable 1591, SA1600
        public const string DriveSpeedAxis = "DriveSpeedAxis";
        public const string ThrottleRange = "BuggyThrottleRange";
        public const string ToggleReverse = "BuggyToggleReverseThrottleInput";
        public const string Increment = "BuggyThrottleIncrement";
        public const string IncreaseSpeedMax = "IncreaseSpeedButtonMax";
        public const string DecreaseSpeedMax = "DecreaseSpeedButtonMax";
        public const string IncreaseSpeedPartial = "IncreaseSpeedButtonPartial";
        public const string DecreaseSpeedPartial = "DecreaseSpeedButtonPartial";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="DriveThrottle"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(DriveThrottle));
    }
}
