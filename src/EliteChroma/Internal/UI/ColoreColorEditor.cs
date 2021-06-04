using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using EliteChroma.Controls;

namespace EliteChroma.Internal.UI
{
    [ExcludeFromCodeCoverage]
    internal sealed class ColoreColorEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var wfes = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (wfes == null)
            {
                return null;
            }

            using var cp = new RgbPicker
            {
                Color = FromColoreColor((Colore.Data.Color)value),
            };

            wfes.DropDownControl(cp);

            return ToColoreColor(cp.Color);
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            Color c = FromColoreColor((Colore.Data.Color)e.Value);
            using var b = new SolidBrush(c);
            e.Graphics.FillRectangle(b, e.Bounds);
        }

        private static Colore.Data.Color ToColoreColor(Color color)
        {
            return new Colore.Data.Color(color.R, color.G, color.B);
        }

        private static Color FromColoreColor(Colore.Data.Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }
    }
}
