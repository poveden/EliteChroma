using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using EliteChroma.Properties;

namespace EliteChroma.Internal.UI
{
    internal sealed class LocalizedPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _descriptor;
        private readonly Lazy<string> _category;
        private readonly Lazy<string> _description;
        private readonly Lazy<string> _displayName;

        public LocalizedPropertyDescriptor(PropertyDescriptor baseDescriptor, string resourceNamePrefix)
            : base(baseDescriptor)
        {
            _descriptor = baseDescriptor;
            ResourceManager rm = Resources.ResourceManager;
            CultureInfo ci = Resources.Culture;

            string baseName = $"{resourceNamePrefix}{_descriptor.Name}";

            _category = new Lazy<string>(() => rm.GetString(baseName + "_Category", ci) ?? rm.GetString(_descriptor.Category, ci) ?? _descriptor.Category);
            _description = new Lazy<string>(() => rm.GetString(baseName + "_Description", ci) ?? rm.GetString(_descriptor.Description, ci) ?? _descriptor.Description);
            _displayName = new Lazy<string>(() => rm.GetString(baseName, ci) ?? rm.GetString(_descriptor.DisplayName, ci) ?? _descriptor.DisplayName);
        }

        public override string Category => _category.Value;

        public override string Description => _description.Value;

        public override string DisplayName => _displayName.Value;

        [ExcludeFromCodeCoverage]
        public override Type ComponentType => _descriptor.ComponentType;

        [ExcludeFromCodeCoverage]
        public override bool IsReadOnly => _descriptor.IsReadOnly;

        [ExcludeFromCodeCoverage]
        public override Type PropertyType => _descriptor.PropertyType;

        [ExcludeFromCodeCoverage]
        public override bool CanResetValue(object component)
        {
            return _descriptor.CanResetValue(component);
        }

        [ExcludeFromCodeCoverage]
        public override object GetValue(object component)
        {
            return _descriptor.GetValue(component);
        }

        [ExcludeFromCodeCoverage]
        public override void ResetValue(object component)
        {
            _descriptor.ResetValue(component);
        }

        [ExcludeFromCodeCoverage]
        public override void SetValue(object component, object value)
        {
            _descriptor.SetValue(component, value);
        }

        [ExcludeFromCodeCoverage]
        public override bool ShouldSerializeValue(object component)
        {
            return _descriptor.ShouldSerializeValue(component);
        }
    }
}
