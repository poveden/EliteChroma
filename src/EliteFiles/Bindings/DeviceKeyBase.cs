using System;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a device key.
    /// </summary>
    public abstract class DeviceKeyBase : IEquatable<DeviceKeyBase>
    {
        private protected DeviceKeyBase(string? device, string? key)
        {
            Device = device;
            Key = key;
        }

        /// <summary>
        /// Gets the device for the key.
        /// </summary>
        /// <see cref="Devices.Device"/>
        public string? Device { get; }

        /// <summary>
        /// Gets the key bound to the device.
        /// </summary>
        /// <see cref="Devices"/>
        public string? Key { get; }

        /// <summary>
        /// Determines wheter this instance and the given device key instance represent the same device key.
        /// </summary>
        /// <param name="other">The device key to compare against this instance.</param>
        /// <returns><c>true</c> if <paramref name="other"/> represents the same device key as this instance; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(DeviceKeyBase? other)
        {
            return other != null
                && Device == other.Device
                && Key == other.Key;
        }

        /// <summary>
        /// Determines wheter this instance and the given object represent the same device key.
        /// </summary>
        /// <param name="obj">The object to compare against this instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> represents the same device key as this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is DeviceKeyBase other && Equals(other);
        }

        /// <summary>
        /// Calculates the hash code for the current <see cref="DeviceKeyBase"/> instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Device, Key);
        }
    }
}
