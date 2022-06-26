using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EliteChroma.Internal.UI
{
    internal sealed class LocalizedTypeDescriptor : CustomTypeDescriptor
    {
        private static readonly ConditionalWeakTable<PropertyDescriptorCollection, PropertyDescriptorCollection> _map = new ConditionalWeakTable<PropertyDescriptorCollection, PropertyDescriptorCollection>();

        private readonly string _resourceNamePrefix;

        public LocalizedTypeDescriptor(ICustomTypeDescriptor parent, string resourceNamePrefix)
            : base(parent)
        {
            _resourceNamePrefix = resourceNamePrefix;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
        {
            return _map.GetValue(base.GetProperties(attributes), Wrap);
        }

        private PropertyDescriptorCollection Wrap(PropertyDescriptorCollection properties)
        {
            var res = new PropertyDescriptorCollection(null);

            foreach (PropertyDescriptor property in properties)
            {
                _ = res.Add(new LocalizedPropertyDescriptor(property, _resourceNamePrefix));
            }

            return res;
        }
    }
}
