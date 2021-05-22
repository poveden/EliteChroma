using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the camera suite.
    /// </summary>
    public static class CameraSuite
    {
#pragma warning disable 1591, SA1600
        public const string Toggle = "PhotoCameraToggle";
        public const string BuggyToggle = "PhotoCameraToggle_Buggy";
        public const string HumanoidToggle = "PhotoCameraToggle_Humanoid";
        public const string VanityCameraScrollLeft = "VanityCameraScrollLeft";
        public const string VanityCameraScrollRight = "VanityCameraScrollRight";
        public const string ToggleFreeCam = "ToggleFreeCam";
        public const string VanityCamera1 = "VanityCameraOne";
        public const string VanityCamera2 = "VanityCameraTwo";
        public const string VanityCamera3 = "VanityCameraThree";
        public const string VanityCamera4 = "VanityCameraFour";
        public const string VanityCamera5 = "VanityCameraFive";
        public const string VanityCamera6 = "VanityCameraSix";
        public const string VanityCamera7 = "VanityCameraSeven";
        public const string VanityCamera8 = "VanityCameraEight";
        public const string VanityCamera9 = "VanityCameraNine";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="CameraSuite"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(CameraSuite));
    }
}
