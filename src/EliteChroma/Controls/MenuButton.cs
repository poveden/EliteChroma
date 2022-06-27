using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace EliteChroma.Controls
{
    // Reference: https://stackoverflow.com/questions/10803184/windows-forms-button-with-drop-down-menu/24087828#24087828
    [ExcludeFromCodeCoverage]
    internal class MenuButton : Button
    {
        private const char _whiteSpace = '\u2003';

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

        // We just insert an em space to the end so the rendered text gets shifted to the left, and avoid rewriting the full render code.
        public new string Text
        {
            get => base.Text.EndsWith(_whiteSpace) ? base.Text[..^1] : base.Text;
            set => base.Text = $"{value}{_whiteSpace}";
        }

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

                int dividerX = arrowX - 6;
                int dividerY0 = 4;
                int dividerY1 = ClientRectangle.Height - 5;

                Pen pen = SystemPens.ControlLight;
                pevent.Graphics.DrawLine(pen, new Point(dividerX, dividerY0), new Point(dividerX, dividerY1));
            }
        }
    }
}
