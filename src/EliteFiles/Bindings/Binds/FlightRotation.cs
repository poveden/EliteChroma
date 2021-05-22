using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for flight rotation.
    /// </summary>
    public static class FlightRotation
    {
        /// <summary>
        /// Gets the category of all <see cref="FlightRotation"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string YawAxisRaw = "YawAxisRaw";
        public const string YawLeft = "YawLeftButton";
        public const string YawRight = "YawRightButton";
        public const string YawToRollMode = "YawToRollMode";
        public const string YawToRollSensitivity = "YawToRollSensitivity";
        public const string YawToRollModeFAOff = "YawToRollMode_FAOff";
        public const string YawToRoll = "YawToRollButton";
        public const string RollAxisRaw = "RollAxisRaw";
        public const string RollLeft = "RollLeftButton";
        public const string RollRight = "RollRightButton";
        public const string PitchAxisRaw = "PitchAxisRaw";
        public const string PitchUp = "PitchUpButton";
        public const string PitchDown = "PitchDownButton";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FlightRotation"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FlightRotation));
    }
}
