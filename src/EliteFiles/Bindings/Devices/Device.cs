namespace EliteFiles.Bindings.Devices
{
    /// <summary>
    /// Defines Elite:Dangerous device names for binding presets.
    /// </summary>
    /// <remarks>
    /// Reference: &lt;EliteRoot&gt;\Products\elite-dangerous-64\ControlSchemes\Help.txt.
    /// </remarks>
    /// <seealso cref="Devices.Keyboard"/>
    /// <seealso cref="Devices.Mouse"/>
    /// <seealso cref="Devices.XBox360Pad"/>
    /// <seealso cref="Devices.Joystick"/>
    public static class Device
    {
        /// <summary>
        /// Device name when no device is bound to a preset.
        /// </summary>
        public const string NoDevice = "{NoDevice}";

        /// <summary>
        /// Keyboard device.
        /// </summary>
        /// <seealso cref="Devices.Keyboard"/>
        public const string Keyboard = "Keyboard";

        /// <summary>
        /// Mouse device.
        /// </summary>
        /// <seealso cref="Devices.Mouse"/>
        public const string Mouse = "Mouse";

        /// <summary>
        /// XBox 360 Gamepad device.
        /// </summary>
        /// <seealso cref="Devices.XBox360Pad"/>
        public const string XBox360Pad = "XB360 Pad";
    }
}
