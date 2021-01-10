using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using EliteChroma.Properties;

namespace EliteChroma.Internal.UI
{
    internal sealed class BrightnessConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // String -> Double
            var m = Regex.Match((string)value, @"^\s*0*?([0-9]|[1-9][0-9]|100)(?:\s*%)?\s*$");

            if (!m.Success)
            {
                throw new FormatException(Resources.FormatException_InvalidBrightnessValue);
            }

            return int.Parse(m.Groups[1].Value, NumberStyles.None, CultureInfo.InvariantCulture) / 100.0;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // Double -> String
            return ((double)value).ToString("P0", CultureInfo.InvariantCulture);
        }
    }
}
