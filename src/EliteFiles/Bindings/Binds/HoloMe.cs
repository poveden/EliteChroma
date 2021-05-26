using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for Holo-Me.
    /// </summary>
    public static class HoloMe
    {
        /// <summary>
        /// Gets the category of all <see cref="HoloMe"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.GeneralControls;

#pragma warning disable 1591, SA1600
        public const string Undo = "CommanderCreator_Undo";
        public const string Redo = "CommanderCreator_Redo";
        public const string RotationMouseToggle = "CommanderCreator_Rotation_MouseToggle";
        public const string Rotation = "CommanderCreator_Rotation";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="HoloMe"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(HoloMe));
    }
}
