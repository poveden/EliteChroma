using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight throttle.
    /// </summary>
    public static class FlightThrottle
    {
#pragma warning disable 1591, SA1600
        public const string Axis = "ThrottleAxis";
        public const string Range = "ThrottleRange";
        public const string ToggleReverse = "ToggleReverseThrottleInput";
        public const string Forward = "ForwardKey";
        public const string Backward = "BackwardKey";
        public const string Increment = "ThrottleIncrement";
        public const string SpeedMinus100 = "SetSpeedMinus100";
        public const string SpeedMinus75 = "SetSpeedMinus75";
        public const string SpeedMinus50 = "SetSpeedMinus50";
        public const string SpeedMinus25 = "SetSpeedMinus25";
        public const string SpeedZero = "SetSpeedZero";
        public const string Speed25 = "SetSpeed25";
        public const string Speed50 = "SetSpeed50";
        public const string Speed75 = "SetSpeed75";
        public const string Speed100 = "SetSpeed100";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FlightThrottle"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FlightThrottle));
    }
}
