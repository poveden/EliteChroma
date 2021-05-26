using System.Collections.Generic;

namespace EliteFiles.Bindings.Binds
{
    /// <summary>
    /// Defines bind names for mouse controls.
    /// </summary>
    public static class MouseControls
    {
        /// <summary>
        /// Gets the category of all <see cref="MouseControls"/> bind names.
        /// </summary>
        public const BindingCategory Category = BindingCategory.ShipControls;

#pragma warning disable 1591, SA1600
        public const string XMode = "MouseXMode";
        public const string XDecay = "MouseXDecay";
        public const string YMode = "MouseYMode";
        public const string YDecay = "MouseYDecay";
        public const string Reset = "MouseReset";
        public const string Sensitivity = "MouseSensitivity";
        public const string DecayRate = "MouseDecayRate";
        public const string Deadzone = "MouseDeadzone";
        public const string Linearity = "MouseLinearity";
        public const string GUI = "MouseGUI";
#pragma warning restore 1591, SA1600

        /// <summary>
        /// Gets the collection of all <see cref="MouseControls"/> bind names.
        /// </summary>
        public static IReadOnlyCollection<string> All { get; } = Binding.BuildGroup(typeof(MouseControls));
    }
}
