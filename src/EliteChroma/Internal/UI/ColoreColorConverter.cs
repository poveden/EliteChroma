using System;
using System.ComponentModel;
using System.Globalization;
using Colore.Data;
using EliteChroma.Internal.Json;
using EliteChroma.Properties;

namespace EliteChroma.Internal.UI
{
    internal sealed class ColoreColorConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // String -> Color
            if (!JsonColoreColorConverter.TryParseRgbString((string)value, out var color))
            {
                throw new FormatException(Resources.FormatException_InvalidColorValue);
            }

            return color;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // Color -> String
            return JsonColoreColorConverter.ToRgbString((Color)value);
        }
    }
}
