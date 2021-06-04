using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EliteChroma.Internal.UI
{
    [ExcludeFromCodeCoverage]
    internal sealed class BrightnessEditor : UITypeEditor
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

            using var tb = new BrightnessTrackBar
            {
                Minimum = 0,
                Maximum = 100,
                SmallChange = 5,
                LargeChange = 10,
                TickStyle = TickStyle.None,
                Value = (int)((double)value * 100),
            };

            wfes.DropDownControl(tb);

            return tb.Value / 100.0;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            int v255 = (int)((double)e.Value * 255);
            using var b = new SolidBrush(Color.FromArgb(v255, v255, v255));
            e.Graphics.FillRectangle(b, e.Bounds);
        }

        private sealed class BrightnessTrackBar : TrackBar
        {
#pragma warning disable SA1310
            private const int WM_PAINT = 0x0F;
#pragma warning restore SA1310

            public BrightnessTrackBar()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg != WM_PAINT)
                {
                    return;
                }

                using var g = Graphics.FromHwndInternal(m.HWnd);
                using var e = new PaintEventArgs(g, ClientRectangle);
                OnPaintOverlay(e);
            }

            private static void OnPaintOverlay(PaintEventArgs e)
            {
                Rectangle r = e.ClipRectangle;
                r.Height /= 2;
                r.Y = r.Height;
                r.Inflate(-17, -3);

                using var b = new LinearGradientBrush(r, Color.Black, Color.White, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(b, r);
            }
        }
    }
}
