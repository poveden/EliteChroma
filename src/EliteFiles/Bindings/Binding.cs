using System.Reflection;
using System.Xml.Linq;

namespace EliteFiles.Bindings
{
    /// <summary>
    /// Represents a binding.
    /// </summary>
    public sealed class Binding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Binding"/> class
        /// with the provided name and key combinations.
        /// </summary>
        /// <param name="name">The name of the binding.</param>
        /// <param name="primary">The primary device key combination that triggers this binding.</param>
        /// <param name="secondary">The secondary device key combination that triggers this binding.</param>
        public Binding(string name, DeviceKeyCombination? primary, DeviceKeyCombination? secondary)
        {
            Name = name;
            Primary = primary ?? DeviceKeyCombination.Undefined;
            Secondary = secondary ?? DeviceKeyCombination.Undefined;
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

        internal static Binding? FromXml(XElement xml)
        {
            var primary = DeviceKeyCombination.FromXml(xml.Element("Primary"));
            var secondary = DeviceKeyCombination.FromXml(xml.Element("Secondary"));

            if (primary == null && secondary == null)
            {
                return null;
            }

            return new Binding(xml.Name.LocalName, primary, secondary);
        }

        internal static IReadOnlyCollection<string> BuildGroup(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetValue(null)!)
                .ToList()
                .AsReadOnly();
        }
    }
}
