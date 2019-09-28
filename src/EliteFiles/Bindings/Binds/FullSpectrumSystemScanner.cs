using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the full spectrum system scanner.
    /// </summary>
    public static class FullSpectrumSystemScanner
    {
#pragma warning disable 1591, SA1600
        public const string Enter = "ExplorationFSSEnter";
        public const string CameraPitch = "ExplorationFSSCameraPitch";
        public const string CameraPitchIncrease = "ExplorationFSSCameraPitchIncreaseButton";
        public const string CameraPitchDecrease = "ExplorationFSSCameraPitchDecreaseButton";
        public const string CameraYaw = "ExplorationFSSCameraYaw";
        public const string CameraYawIncrease = "ExplorationFSSCameraYawIncreaseButton";
        public const string CameraYawDecrease = "ExplorationFSSCameraYawDecreaseButton";
        public const string ZoomIn = "ExplorationFSSZoomIn";
        public const string ZoomOut = "ExplorationFSSZoomOut";
        public const string MiniZoomIn = "ExplorationFSSMiniZoomIn";
        public const string MiniZoomOut = "ExplorationFSSMiniZoomOut";
        public const string RadioTuningXRaw = "ExplorationFSSRadioTuningX_Raw";
        public const string RadioTuningXIncrease = "ExplorationFSSRadioTuningX_Increase";
        public const string RadioTuningXDecrease = "ExplorationFSSRadioTuningX_Decrease";
        public const string RadioTuningAbsoluteX = "ExplorationFSSRadioTuningAbsoluteX";
        public const string TuningSensitivity = "FSSTuningSensitivity";
        public const string DiscoveryScan = "ExplorationFSSDiscoveryScan";
        public const string Quit = "ExplorationFSSQuit";
        public const string MouseXMode = "FSSMouseXMode";
        public const string MouseXDecay = "FSSMouseXDecay";
        public const string MouseYMode = "FSSMouseYMode";
        public const string MouseYDecay = "FSSMouseYDecay";
        public const string MouseSensitivity = "FSSMouseSensitivity";
        public const string MouseDeadzone = "FSSMouseDeadzone";
        public const string MouseLinearity = "FSSMouseLinearity";
        public const string Target = "ExplorationFSSTarget";
        public const string ShowHelp = "ExplorationFSSShowHelp";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="FullSpectrumSystemScanner"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(FullSpectrumSystemScanner));
    }
}
