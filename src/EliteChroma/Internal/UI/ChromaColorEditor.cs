using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using ChromaWrapper;
using EliteChroma.Controls;

namespace EliteChroma.Internal.UI
{
    [ExcludeFromCodeCoverage]
    internal sealed class ChromaColorEditor : UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object? EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var wfes = (IWindowsFormsEditorService?)provider.GetService(typeof(IWindowsFormsEditorService));

            if (wfes == null)
            {
                return null;
            }

            using var cp = new RgbPicker
            {
                Color = ((ChromaColor)value).ToColor(),
            };

            wfes.DropDownControl(cp);

            return ChromaColor.FromColor(cp.Color);
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            var c = ((ChromaColor)e.Value).ToColor();
            using var b = new SolidBrush(c);
            e.Graphics.FillRectangle(b, e.Bounds);
        }
    }
}
