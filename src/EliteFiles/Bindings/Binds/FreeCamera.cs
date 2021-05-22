using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the free camera.
    /// </summary>
    public static class FreeCamera
    {
        /// <summary>
        /// Gets the category of all <see cref="FreeCamera"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.GeneralControls;

#pragma warning disable 1591, SA1600
        public const string ToggleHUD = "FreeCamToggleHUD";
        public const string SpeedInc = "FreeCamSpeedInc";
        public const string SpeedDec = "FreeCamSpeedDec";
        public const string MoveY = "MoveFreeCamY";
        public const string ThrottleRange = "ThrottleRangeFreeCam";
        public const string ToggleReverseThrottleInput = "ToggleReverseThrottleInputFreeCam";
        public const string MoveForward = "MoveFreeCamForward";
        public const string MoveBackwards = "MoveFreeCamBackwards";
        public const string MoveX = "MoveFreeCamX";
        public const string MoveRight = "MoveFreeCamRight";
        public const string MoveLeft = "MoveFreeCamLeft";
        public const string MoveZ = "MoveFreeCamZ";
        public const string MoveUpAxis = "MoveFreeCamUpAxis";
        public const string MoveDownAxis = "MoveFreeCamDownAxis";
        public const string MoveUp = "MoveFreeCamUp";
        public const string MoveDown = "MoveFreeCamDown";
        public const string PitchMouse = "PitchCameraMouse";
        public const string YawMouse = "YawCameraMouse";
        public const string Pitch = "PitchCamera";
        public const string MouseSensitivity = "FreeCamMouseSensitivity";
        public const string MouseYDecay = "FreeCamMouseYDecay";
        public const string PitchUp = "PitchCameraUp";
        public const string PitchDown = "PitchCameraDown";
        public const string Yaw = "YawCamera";
        public const string MouseXDecay = "FreeCamMouseXDecay";
        public const string YawLeft = "YawCameraLeft";
        public const string YawRight = "YawCameraRight";
        public const string Roll = "RollCamera";
        public const string RollLeft = "RollCameraLeft";
        public const string RollRight = "RollCameraRight";
        public const string ToggleRotationLock = "ToggleRotationLock";
        public const string FixRelativeToggle = "FixCameraRelativeToggle";
        public const string FixWorldToggle = "FixCameraWorldToggle";
        public const string Quit = "QuitCamera";
        public const string ToggleAdvanceMode = "ToggleAdvanceMode";
        public const string ZoomIn = "FreeCamZoomIn";
        public const string ZoomOut = "FreeCamZoomOut";
        public const string FStopDec = "FStopDec";
        public const string FStopInc = "FStopInc";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FreeCamera"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FreeCamera));
    }
}
