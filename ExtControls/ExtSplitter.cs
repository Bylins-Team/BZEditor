using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ExtControls
{
	public class ExtSplitter : Splitter
	{
		private bool IIsClosed  = true;
		
		public ExtSplitter()
		{
		}

		#region Get/Set

		[DefaultValue(true)]
		[Description("Состояние дополнительной панели, для управления которой предусмотрен сплиттер.")]
		[Category("Appearance")]
		public bool IsClosed
		{
			get
			{
				return IIsClosed;
			}
			set
			{
				IIsClosed = value;
				this.Invalidate();
			}
		}

		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if (Size.Height > 6 && Size.Width > 6)DrawArrows(e.Graphics);
		}

		private void DrawArrows(Graphics g)
		{
			g.SmoothingMode = SmoothingMode.AntiAlias;
			if (this.Dock == DockStyle.Bottom)
			{
				if (IIsClosed)
				{					
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(3), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(3), FillMode.Alternate);
				}
				else
				{
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(2), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(2), FillMode.Alternate);
				}
			}else if (this.Dock == DockStyle.Left)
			{
				if (IIsClosed)
				{					
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(0), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(0), FillMode.Alternate);
				}
				else
				{
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(1), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(1), FillMode.Alternate);
				}
			}
			else if (this.Dock == DockStyle.Right)
			{
				if (IIsClosed)
				{					
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(1), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(1), FillMode.Alternate);
				}
				else
				{
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(0), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(0), FillMode.Alternate);
				}
			}
			else if (this.Dock == DockStyle.Top)
			{
				if (IIsClosed)
				{					
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(2), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(2), FillMode.Alternate);
				}
				else
				{
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetFirstTriangle(3), FillMode.Alternate);
					g.FillPolygon(new SolidBrush(Color.FromKnownColor(KnownColor.ActiveCaptionText)), GetSecondTriangle(3), FillMode.Alternate);
				}
			}
		}

		private Point[] GetFirstTriangle(int type)
		{
			Point[] pArr = new Point[3];
			switch (type)
			{
				case 0:
					pArr[0] = new Point(1,this.Height/2-25-1);
					pArr[1] = new Point(4,this.Height/2-25-3);
					pArr[2] = new Point(1,this.Height/2-25-5);
					break;
				case 1:
					pArr[0] = new Point(4,this.Height/2-25-1);
					pArr[1] = new Point(1,this.Height/2-25-3);
					pArr[2] = new Point(4,this.Height/2-25-5);
					break;	 
				case 2:	  
					pArr[0] = new Point(this.Width/2-25-1,1);
					pArr[1] = new Point(this.Width/2-25-3,4);
					pArr[2] = new Point(this.Width/2-25-5,1);
					break;
				case 3:
					pArr[0] = new Point(this.Width/2-25-1,4);
					pArr[1] = new Point(this.Width/2-25-3,1);
					pArr[2] = new Point(this.Width/2-25-5,4);
					break;
			}
			return pArr;
		}

		private Point[] GetSecondTriangle(int type)
		{
			Point[] pArr = new Point[3];
			switch (type)
			{
				case 0:
					pArr[0] = new Point(1,this.Height/2+25+1);
					pArr[1] = new Point(4,this.Height/2+25+3);
					pArr[2] = new Point(1,this.Height/2+25+5);
					break;
				case 1:
					pArr[0] = new Point(4,this.Height/2+25+1);
					pArr[1] = new Point(1,this.Height/2+25+3);
					pArr[2] = new Point(4,this.Height/2+25+5);
					break;
				case 2:	  
					pArr[0] = new Point(this.Width/2+25+1,1);
					pArr[1] = new Point(this.Width/2+25+3,4);
					pArr[2] = new Point(this.Width/2+25+5,1);
					break;
				case 3:
					pArr[0] = new Point(this.Width/2+25+1,4);
					pArr[1] = new Point(this.Width/2+25+3,1);
					pArr[2] = new Point(this.Width/2+25+5,4);
					break;
			}
			return pArr;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Invalidate();
		}

	}
}
