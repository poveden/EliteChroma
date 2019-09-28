using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight thrust.
    /// </summary>
    public static class FlightThrust
    {
#pragma warning disable 1591, SA1600
        public const string LateralRaw = "LateralThrustRaw";
        public const string Left = "LeftThrustButton";
        public const string Right = "RightThrustButton";
        public const string VerticalRaw = "VerticalThrustRaw";
        public const string Up = "UpThrustButton";
        public const string Down = "DownThrustButton";
        public const string Ahead = "AheadThrust";
        public const string Forward = "ForwardThrustButton";
        public const string Backward = "BackwardThrustButton";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FlightThrust"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FlightThrust));
    }
}
