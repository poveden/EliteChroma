using System.Diagnostics;
using System.Xml.Linq;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a device key.
    /// </summary>
    [DebuggerDisplay("{Device}:{Key}")]
    public sealed class DeviceKey : DeviceKeyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKey"/> class.
        /// </summary>
        /// <param name="device">The device for the key.</param>
        /// <param name="key">The key bound to the device.</param>
        public DeviceKey(string? device, string? key)
            : base(device, key)
        {
        }

        internal static DeviceKey FromXml(XElement xElement)
        {
            string? device = xElement.Attribute("Device")?.Value;
            string? key = xElement.Attribute("Key")?.Value;

            return new DeviceKey(device, key);
        }
    }
}
