using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for the store camera.
    /// </summary>
    public static class StoreCamera
    {
        /// <summary>
        /// Gets the category of all <see cref="StoreCamera"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.GeneralControls;

#pragma warning disable 1591, SA1600
        public const string EnableRotation = "StoreEnableRotation";
        public const string PitchCamera = "StorePitchCamera";
        public const string YawCamera = "StoreYawCamera";
        public const string CamZoomIn = "StoreCamZoomIn";
        public const string CamZoomOut = "StoreCamZoomOut";
        public const string Toggle = "StoreToggle";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="StoreCamera"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(StoreCamera));
    }
}
