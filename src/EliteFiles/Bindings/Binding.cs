using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a binding.
    /// </summary>
    public sealed class Binding
    {
        private Binding()
        {
        }

        /// <summary>
        /// Gets the name of the binding.
        /// </summary>
        /// <seealso cref="Binds"/>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the primary device key combination that triggers this binding.
        /// </summary>
        public DeviceKeyCombination Primary { get; private set; }

        /// <summary>
        /// Gets the secondary device key combination that triggers this binding.
        /// </summary>
        public DeviceKeyCombination Secondary { get; private set; }

        internal static Binding FromXml(XElement xml)
        {
            var primary = DeviceKeyCombination.FromXml(xml.Element("Primary"));
            var secondary = DeviceKeyCombination.FromXml(xml.Element("Secondary"));

            if (primary == null && secondary == null)
            {
                return null;
            }

            return new Binding
            {
                Name = xml.Name.LocalName,
                Primary = primary ?? DeviceKeyCombination.Undefined,
                Secondary = secondary ?? DeviceKeyCombination.Undefined,
            };
        }

        internal static IReadOnlyCollection<string> BuildGroup(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(fi => (string)fi.GetValue(null))
                .ToList()
                .AsReadOnly();
        }
    }
}
