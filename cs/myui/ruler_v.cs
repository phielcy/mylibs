/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/24/2022
 * Time: 10:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using mytools;

namespace myui
{
	/// <summary>
	/// Description of v_ruler.
	/// </summary>
	public class ruler_v : ruler_base
	{
		
		
		public ruler_v()
		{
			this.Width = 18;
			this.Height = 180;
		}
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			
			base.OnPaint(e);
			Graphics g = e.Graphics;
			
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.High;
			g.CompositingQuality = CompositingQuality.HighQuality;
			
			int w = this.Width;
			int h = this.Height;
			
			
			if (this.selection != null) {
				
				int x = 0;
				int y = this.start_view_pix + this.canv_pix_to_vw_pix(this.selection.Y);
				int wd = this.Width;
				int height = this.selection.Height; if (height == 0) height = 1;
				int ht = this.canv_pix_to_vw_pix(height); if (ht < 2) ht = 2;
				Rectangle r = new Rectangle(x,y, wd, ht);
				SolidBrush b = new SolidBrush(Color.FromArgb(51, 147, 223)/*from paint.net*/);
				g.FillRectangle(b, r);
				
			}
			
			
			int big_width = (int)(w * big_height_factor);
			int mid_width = (int)(w * mid_height_factor);
			int lit_width = (int)(w * lit_height_factor);
			
			int digits_space_from_big_line = 2;
			
			if (this.selected_zoom_scale == null)
				this.selected_zoom_scale = this.find_best_zoom_scale_match();
			
			Pen pen = Pens.White;
			
			if (this.selected_zoom_scale.lit > 0) {
				
				for (int y = this.start_canv_pix; y < this.end_canv_pix; y += this.selected_zoom_scale.lit) {
					
					int view_y = canv_pix_to_vw_pix( y );
					int y2 = start_view_pix + view_y;
					g.DrawLine( pen, w - lit_width, y2, w, y2 );
					
				}
				
			}
			
			
			
			if (this.selected_zoom_scale.mid > 0 ) {
				
				for (int y = this.start_canv_pix; y < this.end_canv_pix; y += this.selected_zoom_scale.mid) {
					
					int view_y = canv_pix_to_vw_pix( y );
					int y2 = start_view_pix + view_y;
					g.DrawLine( pen, w - mid_width, y2, w, y2 );
					
				}
				
			}
			
			
			
			for (int y = this.start_canv_pix; y < this.end_canv_pix; y += this.selected_zoom_scale.big) {
				
				int view_y = canv_pix_to_vw_pix( y );
				int y2 = start_view_pix + view_y;
				g.DrawLine( pen, w - big_width, y2, w, y2 );
				string str = y + "";
				Font font = new Font("arial", 7f);
				SizeF sz = g.MeasureString(str, font);
				System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
				drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
				g.DrawString( str, font, Brushes.White, 0, y2 + digits_space_from_big_line, drawFormat );
				
			}
			
			
		}
		
		
	}
	
	
}
