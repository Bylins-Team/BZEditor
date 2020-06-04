using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;

namespace ExtControls
{
    public partial class TouchScrollPanel : Panel
    {
        public enum TouchScrollPanelOrientaton
        {
            Vertical,
            Horizontal,
            Both
        }

        private Bitmap bmp;
        private TouchScrollPanelOrientaton orientation = TouchScrollPanelOrientaton.Vertical;
        private Point StartPoint = new Point(0, 0);
        private bool ButtonPushed = false;
        private int sensitivity = 5;

        public delegate void ScrollEvent(int val);
        public event ScrollEvent Scrolled;  

        #region Get/Set

        public int Sensitivity
        {
            get
            {
                return this.sensitivity;
            }
            set
            {
                this.sensitivity = value;
            }
        }

        public TouchScrollPanelOrientaton Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
                RedrawBitmap();
            }
        }

        #endregion

        public TouchScrollPanel()
        {
            this.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
            bmp = new Bitmap(this.Width, this.Height);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            /*base.OnMouseHover(e);
            this.BackColor = Color.FromKnownColor(KnownColor.ControlLight);*/
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            /*base.OnMouseLeave(e);
            this.BackColor = Color.FromKnownColor(KnownColor.ControlDark);*/
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ButtonPushed = true;
            RedrawBitmap();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ButtonPushed = false;
            RedrawBitmap();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ButtonPushed)
            {
                if (orientation == TouchScrollPanelOrientaton.Horizontal)
                {
                    if (Math.Abs(e.X - StartPoint.X) >= sensitivity)
                    {
                        int res = Math.Sign(e.X - StartPoint.X);
                        if (Scrolled != null)
                            Scrolled(res);
                        StartPoint.X = e.X;
                    }
                }
                else
                {
                    if (Math.Abs(e.Y - StartPoint.Y) >= sensitivity)
                    {
                        int res = Math.Sign(e.Y - StartPoint.Y);
                        if (Scrolled != null)
                            Scrolled(res);
                        StartPoint.Y = e.Y;
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(bmp, -1, -1);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Height <= 0 || this.Width <= 0) return;
            bmp = new Bitmap(this.Width+2, this.Height+2);
            RedrawBitmap();
        }

        public void RedrawBitmap()
        {
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            LinearGradientBrush br;
            if (this.orientation == TouchScrollPanelOrientaton.Horizontal)
                if (ButtonPushed)
                    br = new LinearGradientBrush(new Rectangle(0, 0, bmp.Width, bmp.Height), SystemColors.ControlDark, SystemColors.ControlLight, 270F, false);
                else
                    br = new LinearGradientBrush(new Rectangle(0, 0, bmp.Width, bmp.Height), SystemColors.ControlDark, SystemColors.ControlLight, 90F, false);
            else
                if (ButtonPushed)
                    br = new LinearGradientBrush(new Rectangle(0, 0, bmp.Width, bmp.Height), SystemColors.ControlDark, SystemColors.ControlLight, 180F, false);
                else
                    br = new LinearGradientBrush(new Rectangle(0, 0, bmp.Width, bmp.Height), SystemColors.ControlDark, SystemColors.ControlLight, 0F, false);
            gr.FillRectangle(br, 0, 0, bmp.Width, bmp.Height);            
            gr.Dispose();
            this.Invalidate();
        }
    }
}
