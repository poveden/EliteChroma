using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using EliteChroma.Properties;
using System.Diagnostics.CodeAnalysis;

namespace EliteChroma.Controls
{
    [ExcludeFromCodeCoverage]
    public partial class RgbPicker : UserControl
    {
        // Picker image is 7×7 pixels
        private const int _halfPickerWidth = 3;
        private const int _halfPickerHeight = 3;

        private bool _loading;
        private PictureBox _hoverSlide;

        public RgbPicker()
        {
            InitializeComponent();
            
            pbR.Tag = nudR;
            pbG.Tag = nudG;
            pbB.Tag = nudB;
        }

        public Color Color { get; set; }

        private void RefreshColors()
        {
            pbPicked.Refresh();
            pbR.Refresh();
            pbG.Refresh();
            pbB.Refresh();
        }

        private void RgbPicker_Load(object sender, EventArgs e)
        {
            _loading = true;
            nudR.Value = Color.R;
            nudG.Value = Color.G;
            nudB.Value = Color.B;
            _loading = false;

            RefreshColors();
        }

        private void PbSlide_MouseDownMove(object sender, MouseEventArgs e)
        {
            if (!e.Button.HasFlag(MouseButtons.Left))
            {
                return;
            }

            var pb = (PictureBox)sender;
            var nud = (NumericUpDown)pb.Tag;

            var maxX = pb.Width - (_halfPickerWidth * 2) - 1;

            var x = Math.Clamp(e.X - _halfPickerWidth, 0, maxX);

            var newValue = 255m * x / maxX;
            nud.Value = newValue;
        }

        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }

            Color = Color.FromArgb((int)nudR.Value, (int)nudG.Value, (int)nudB.Value);
            RefreshColors();
        }

        private void PbSlide_MouseEnter(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            _hoverSlide = pb;
            pb.Refresh();
        }

        private void PbSlide_MouseLeave(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            _hoverSlide = null;
            pb.Refresh();
        }

        private void PbSlide_Paint(object sender, PaintEventArgs e)
        {
            var pb = (PictureBox)sender;
            var nud = (NumericUpDown)pb.Tag;

            e.Graphics.FillRectangle(new SolidBrush(tlpRgb.BackColor), e.ClipRectangle);
            
            var r = e.ClipRectangle;
            r.Inflate(-_halfPickerWidth, -_halfPickerHeight);

            var pcArgb = Color.ToArgb();
            var slArgb = pb.BackColor.ToArgb();

            var minColor = Color.FromArgb(unchecked((pcArgb & ~slArgb) | (int)0xFF000000));
            var maxColor = Color.FromArgb(unchecked((pcArgb | slArgb) | (int)0xFF000000));

            var b = new LinearGradientBrush(r, minColor, maxColor, LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle(b, r);

            var pct = nud.Value / 255m;
            var x = (int)((r.Width - 1) * pct);

            var picker = (pb == _hoverSlide) ? Resources.Picker1 : Resources.Picker0;

            r = new Rectangle(new Point(x, e.ClipRectangle.Bottom - picker.Height), picker.Size);
            e.Graphics.DrawImageUnscaledAndClipped(picker, r);
        }

        private void PbPicked_Paint(object sender, PaintEventArgs e)
        {
            var b = new SolidBrush(Color);
            e.Graphics.FillRectangle(b, e.ClipRectangle);
        }
    }
}
