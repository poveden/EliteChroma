using System;
using System.ComponentModel;
using System.Globalization;
using ChromaWrapper;
using EliteChroma.Internal.Json;
using EliteChroma.Properties;

namespace EliteChroma.Internal.UI
{
    internal sealed class ChromaColorConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // String -> Color
            if (!JsonChromaColorConverter.TryParseRgbString((string)value, out ChromaColor color))
            {
                throw new FormatException(Resources.FormatException_InvalidColorValue);
            }

            return color;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // Color -> String
            return JsonChromaColorConverter.ToRgbString((ChromaColor)value);
        }
    }
}
