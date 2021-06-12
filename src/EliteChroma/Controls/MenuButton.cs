using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace EliteChroma.Controls
{
    // Reference: https://stackoverflow.com/questions/10803184/windows-forms-button-with-drop-down-menu/24087828#24087828
    [ExcludeFromCodeCoverage]
    internal class MenuButton : Button
    {
        public enum HorizontalDirection
        {
            Right = 0,
            Left = 1,
        }

        [DefaultValue(null)]
        public ContextMenuStrip? Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }

        [DefaultValue(HorizontalDirection.Right)]
        public HorizontalDirection MenuHorizontalDirection { get; set; }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (Menu == null || mevent.Button != MouseButtons.Left)
            {
                return;
            }

            ToolStripDropDownDirection ddd = MenuHorizontalDirection == HorizontalDirection.Left
                ? ToolStripDropDownDirection.Left
                : ToolStripDropDownDirection.Right;

            Point menuLocation;

            if (ShowMenuUnderCursor)
            {
                menuLocation = mevent.Location;
            }
            else
            {
                int x = MenuHorizontalDirection == HorizontalDirection.Left ? Width : 0;
                menuLocation = new Point(x, Height);
            }

            Menu.Show(this, menuLocation, ddd);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            if (Menu != null)
            {
                int arrowX = ClientRectangle.Width - 14;
                int arrowY = (ClientRectangle.Height / 2) - 1;

                Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
                var arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
                pevent.Graphics.FillPolygon(brush, arrows);
            }
        }
    }
}
