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
        private DeviceKey(string? device, string? key)
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
