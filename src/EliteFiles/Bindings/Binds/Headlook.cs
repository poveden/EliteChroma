using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for headlook.
    /// </summary>
    public static class Headlook
    {
        /// <summary>
        /// Gets the category of all <see cref="Headlook"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string Mouse = "MouseHeadlook";
        public const string MouseInvert = "MouseHeadlookInvert";
        public const string MouseSensitivity = "MouseHeadlookSensitivity";
        public const string Default = "HeadlookDefault";
        public const string Increment = "HeadlookIncrement";
        public const string Mode = "HeadlookMode";
        public const string ResetOnToggle = "HeadlookResetOnToggle";
        public const string Sensitivity = "HeadlookSensitivity";
        public const string Smoothing = "HeadlookSmoothing";
        public const string Reset = "HeadLookReset";
        public const string PitchUp = "HeadLookPitchUp";
        public const string PitchDown = "HeadLookPitchDown";
        public const string PitchAxisRaw = "HeadLookPitchAxisRaw";
        public const string YawLeft = "HeadLookYawLeft";
        public const string YawRight = "HeadLookYawRight";
        public const string YawAxis = "HeadLookYawAxis";
        public const string Motion = "MotionHeadlook";
        public const string MotionSensitivity = "HeadlookMotionSensitivity";
        public const string YawRotate = "yawRotateHeadlook";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="Headlook"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(Headlook));
    }
}
