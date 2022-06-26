using System.Collections;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a collection of unique device keys.
    /// </summary>
    public sealed class DeviceKeySet : IReadOnlyCollection<DeviceKey>, IEquatable<DeviceKeySet>
    {
        private static readonly IEqualityComparer<HashSet<DeviceKey>> _setComparer = HashSet<DeviceKey>.CreateSetComparer();

        private readonly HashSet<DeviceKey> _modifiers;
        private readonly int _hashCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeySet"/> class,
        /// with the given collection of device keys.
        /// </summary>
        /// <param name="modifiers">The collection of device keys.</param>
        public DeviceKeySet(IEnumerable<DeviceKey> modifiers)
        {
            ArgumentNullException.ThrowIfNull(modifiers);
            _modifiers = new HashSet<DeviceKey>(modifiers);
            _hashCode = _setComparer.GetHashCode(_modifiers);
        }

        /// <summary>
        /// Gets the number of device keys in this collection.
        /// </summary>
        public int Count => _modifiers.Count;

        /// <summary>
        /// Determines wheter this instance and the given device key collection instance represent the same set of device keys.
        /// </summary>
        /// <param name="other">The device key collection to compare against this instance.</param>
        /// <returns><c>true</c> if <paramref name="other"/> represents the same set of device key as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(DeviceKeySet? other)
        {
            return other != null && _modifiers.SetEquals(other._modifiers);
        }

        /// <summary>
        /// Determines wheter this instance and the given object represent the same set of device keys.
        /// </summary>
        /// <param name="obj">The obj to compare against this instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> represents the same set of device key as this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is DeviceKeySet other && Equals(other);
        }

        /// <summary>
        /// Calculates the hash code for the current <see cref="DeviceKeySet"/> instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return _hashCode;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the set of device keys.
        /// </summary>
        /// <returns>An enumerator for this object.</returns>
        public IEnumerator<DeviceKey> GetEnumerator()
        {
            return _modifiers.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the set of device keys.
        /// </summary>
        /// <returns>An enumerator for this object.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modifiers.GetEnumerator();
        }
    }
}
