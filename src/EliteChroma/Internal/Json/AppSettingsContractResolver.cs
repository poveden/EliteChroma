using System;
using System.Reflection;
using Colore.Data;
using EliteChroma.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EliteChroma.Internal.Json
{
    internal sealed class AppSettingsContractResolver : DefaultContractResolver
    {
        private static readonly JsonColoreColorConverter _colorConverter = new JsonColoreColorConverter();
        private static readonly JsonBrightnessConverter _brightnessConverter = new JsonBrightnessConverter();

        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(Color))
            {
                contract.Converter = _colorConverter;
            }

            return contract;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType != typeof(ChromaColors))
            {
                return property;
            }

            switch (property.PropertyName)
            {
                case nameof(ChromaColors.DeviceDimBrightness):
                case nameof(ChromaColors.KeyboardDimBrightness):
                case nameof(ChromaColors.SecondaryBindingBrightness):
                    property.Converter = _brightnessConverter;
                    break;

                case nameof(ChromaColors.PowerDistributorScale):
                    property.Ignored = true;
                    break;

                default:
                    break;
            }

            return property;
        }
    }
}
