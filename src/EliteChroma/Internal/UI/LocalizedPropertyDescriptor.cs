using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
            _descriptor = baseDescriptor ?? throw new ArgumentNullException(nameof(baseDescriptor));
            var rm = Resources.ResourceManager;

            var baseName = $"{resourceNamePrefix}{_descriptor.Name}";

            _category = new Lazy<string>(() => rm.GetString(baseName + "_Category") ?? rm.GetString(base.Category));
            _description = new Lazy<string>(() => rm.GetString(baseName + "_Description") ?? rm.GetString(base.Description));
            _displayName = new Lazy<string>(() => rm.GetString(baseName) ?? rm.GetString(base.DisplayName));
        }

        public override string Category => _category.Value;

        public override string Description => _description.Value;

        public override string DisplayName => _displayName.Value;

        #region Base implementation

        [ExcludeFromCodeCoverage]
        public override Type ComponentType => _descriptor.ComponentType;

        [ExcludeFromCodeCoverage]
        public override bool IsReadOnly => _descriptor.IsReadOnly;

        [ExcludeFromCodeCoverage]
        public override Type PropertyType => _descriptor.PropertyType;

        [ExcludeFromCodeCoverage]
        public override bool CanResetValue(object component) => _descriptor.CanResetValue(component);

        [ExcludeFromCodeCoverage]
        public override object GetValue(object component) => _descriptor.GetValue(component);

        [ExcludeFromCodeCoverage]
        public override void ResetValue(object component) => _descriptor.ResetValue(component);

        [ExcludeFromCodeCoverage]
        public override void SetValue(object component, object value) => _descriptor.SetValue(component, value);

        [ExcludeFromCodeCoverage]
        public override bool ShouldSerializeValue(object component) => _descriptor.ShouldSerializeValue(component);

        #endregion
    }
}
