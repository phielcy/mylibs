/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 5/17/2022
 * Time: 6:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of dropdown_button.
	/// </summary>
	public class dd_button : Button
	{
		
		
		
		private Image icon_image = null;
		public Image IconImage {
			get { return icon_image; }
			set { this.icon_image = value; this.Invalidate(); }
		}
		
		private int icon_image_width;
		public int IconImageWidth {
			get { return icon_image_width; }
			set { this.icon_image_width = value; this.Invalidate(); }
		}
		
		
		
		
		
		public dd_button() {
			
			this.TextAlign = ContentAlignment.MiddleLeft;
			
		}
		
		
		
		
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			
			base.OnPaint(e);
			Graphics g = e.Graphics;
			int w = e.ClipRectangle.Width;
			int h = e.ClipRectangle.Height;
			
			// triangle:
			float taw = 9f;
			float tah = 5f;
			float brdr = 4f;
			PointF[] taps = new PointF[] { new PointF(w - brdr, h/2f - brdr/2f), new PointF(w - taw - brdr, h/2f - brdr/2f), new PointF(w - taw/2f - brdr, h/2f + tah - brdr/2f) };
			g.FillPolygon(Brushes.Black, taps);
			
			if (this.icon_image != null) {
				
				float x = w - brdr - taw - brdr - this.icon_image_width;
				float y = (h - this.icon_image_width) / 2f;
				g.DrawImage(this.icon_image, x, y,  this.icon_image_width, this.icon_image_width);
				
			}
			
		}
		
		
	}
	
	
}
