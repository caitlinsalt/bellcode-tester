using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BellTest
{
    /// <summary>
    /// A Button that is circular, has no label, and does not respond to the keyboard.
    /// </summary>
    public partial class PlungerButton : Button
    {
        private bool MouseOver { get; set; }
        private bool ButtonDown { get; set; }

        public PlungerButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int actualBorderSize = FlatAppearance.BorderSize * (Focused ? 2 : 1);
            Size circleSize = new Size(Size.Width - actualBorderSize * 2, Size.Height - actualBorderSize * 2);
            Brush plungerBrush;
            if (ButtonDown)
            {
                plungerBrush = new SolidBrush(FlatAppearance.MouseDownBackColor);
            }
            else if (MouseOver)
            {
                plungerBrush = new SolidBrush(FlatAppearance.MouseOverBackColor);
            }
            else
            {
                plungerBrush = new SolidBrush(BackColor);
            }
            Brush bgBrush = new SolidBrush(Parent.BackColor);
            pevent.Graphics.FillRectangle(bgBrush, new Rectangle(new Point(0,0), Size));
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.FillEllipse(plungerBrush, new Rectangle(new Point(actualBorderSize, actualBorderSize), circleSize));
            Pen plungerPen = new Pen(FlatAppearance.BorderColor, FlatAppearance.BorderSize * (Focused ? 2 : 1));
            pevent.Graphics.DrawEllipse(plungerPen, new Rectangle(new Point(actualBorderSize, actualBorderSize), circleSize));
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            ButtonDown = true;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            ButtonDown = false;
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            MouseOver = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            MouseOver = false;
            base.OnMouseLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                ButtonDown = true;
            }
            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                ButtonDown = false;
            }
            base.OnKeyUp(kevent);
        }
    }
}
