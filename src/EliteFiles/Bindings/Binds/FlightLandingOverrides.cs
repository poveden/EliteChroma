using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight landing overrides.
    /// </summary>
    public static class FlightLandingOverrides
    {
        /// <summary>
        /// Gets the category of all <see cref="FlightLandingOverrides"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string YawAxis = "YawAxis_Landing";
        public const string YawLeft = "YawLeftButton_Landing";
        public const string YawRight = "YawRightButton_Landing";
        public const string YawToRollMode = "YawToRollMode_Landing";
        public const string PitchAxis = "PitchAxis_Landing";
        public const string PitchUp = "PitchUpButton_Landing";
        public const string PitchDown = "PitchDownButton_Landing";
        public const string RollAxis = "RollAxis_Landing";
        public const string RollLeft = "RollLeftButton_Landing";
        public const string RollRight = "RollRightButton_Landing";
        public const string LateralThrust = "LateralThrust_Landing";
        public const string LeftThrust = "LeftThrustButton_Landing";
        public const string RightThrust = "RightThrustButton_Landing";
        public const string VerticalThrust = "VerticalThrust_Landing";
        public const string UpThrust = "UpThrustButton_Landing";
        public const string DownThrust = "DownThrustButton_Landing";
        public const string AheadThrust = "AheadThrust_Landing";
        public const string ForwardThrust = "ForwardThrustButton_Landing";
        public const string BackwardThrust = "BackwardThrustButton_Landing";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FlightLandingOverrides"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FlightLandingOverrides));

        /// <summary>
        /// Gets the collection of all rotation-related <see cref="FlightLandingOverrides"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Rotation { get; } = new[]
        {
            YawAxis,
            YawLeft,
            YawRight,
            YawToRollMode,
            PitchAxis,
            PitchUp,
            PitchDown,
            RollAxis,
            RollLeft,
            RollRight,
        };

        /// <summary>
        /// Gets the collection of all thrust-related <see cref="FlightLandingOverrides"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> Thrust { get; } = new[]
        {
            LateralThrust,
            LeftThrust,
            RightThrust,
            VerticalThrust,
            UpThrust,
            DownThrust,
            AheadThrust,
            ForwardThrust,
            BackwardThrust,
        };
    }
}
