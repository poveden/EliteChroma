using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a combination of device keys.
    /// </summary>
    public sealed class DeviceKeyCombination : DeviceKeyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeyCombination"/> class.
        /// </summary>
        /// <param name="device">The device for the key.</param>
        /// <param name="key">The key bound to the device.</param>
        /// <param name="modifiers">The collection of device key modifiers.</param>
        public DeviceKeyCombination(string? device, string? key, IEnumerable<DeviceKey> modifiers)
            : base(device, key)
        {
            Modifiers = new DeviceKeySet(modifiers);
        }

        /// <summary>
        /// Gets the collection of device key modifiers.
        /// </summary>
        public DeviceKeySet Modifiers { get; }

        internal static DeviceKeyCombination Undefined { get; } =
            new DeviceKeyCombination(null, null, Enumerable.Empty<DeviceKey>());

        internal static DeviceKeyCombination? FromXml(XElement xml)
        {
            if (xml == null)
            {
                return null;
            }

            string? device = xml.Attribute("Device")?.Value;
            string? key = xml.Attribute("Key")?.Value;
            IEnumerable<DeviceKey> modifiers = xml.Elements("Modifier").Select(DeviceKey.FromXml);

            return new DeviceKeyCombination(device, key, modifiers);
        }
    }
}
