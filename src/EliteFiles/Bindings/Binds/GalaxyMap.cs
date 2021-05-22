using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the galaxy map.
    /// </summary>
    public static class GalaxyMap
    {
#pragma warning disable 1591, SA1600
        public const string PitchAxis = "CamPitchAxis";
        public const string PitchUp = "CamPitchUp";
        public const string PitchDown = "CamPitchDown";
        public const string YawAxis = "CamYawAxis";
        public const string YawLeft = "CamYawLeft";
        public const string YawRight = "CamYawRight";
        public const string TranslateYAxis = "CamTranslateYAxis";
        public const string TranslateForward = "CamTranslateForward";
        public const string TranslateBackward = "CamTranslateBackward";
        public const string TranslateXAxis = "CamTranslateXAxis";
        public const string TranslateLeft = "CamTranslateLeft";
        public const string TranslateRight = "CamTranslateRight";
        public const string TranslateZAxis = "CamTranslateZAxis";
        public const string TranslateUp = "CamTranslateUp";
        public const string TranslateDown = "CamTranslateDown";
        public const string ZoomAxis = "CamZoomAxis";
        public const string ZoomIn = "CamZoomIn";
        public const string ZoomOut = "CamZoomOut";
        public const string TranslateZHold = "CamTranslateZHold";
        public const string GalaxyMapHome = "GalaxyMapHome";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="GalaxyMap"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(GalaxyMap));
    }
}
