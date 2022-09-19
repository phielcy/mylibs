/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/24/2022
 * Time: 10:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of h_ruler.
	/// </summary>
	public class ruler_h : ruler_base
	{
		
		public ruler_h()
		{
			this.Width = 180;
			this.Height = 18;
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
				
				int x = this.start_view_pix + this.canv_pix_to_vw_pix(this.selection.X);
				int y = 0;
				int width = selection.Width; if (width == 0) width = 1;
				int wd = this.canv_pix_to_vw_pix(width); if (wd < 2) wd = 2;
				int ht = this.Height;
				Rectangle r = new Rectangle(x,y, wd, ht);
				SolidBrush b = new SolidBrush(Color.FromArgb(51, 147, 223)/*from paint.net*/);
				g.FillRectangle(b, r);
				
			}
			
			
			int big_height = (int)(h * big_height_factor);
			int mid_height = (int)(h * mid_height_factor);
			int lit_height = (int)(h * lit_height_factor);
			
			int digits_space_from_big_line = 2;
			
			if (this.selected_zoom_scale == null)
				this.selected_zoom_scale = this.find_best_zoom_scale_match();
			
			Pen pen = Pens.White;
			
			if (this.selected_zoom_scale.lit > 0) {
				
				for (int x = this.start_canv_pix; x < this.end_canv_pix; x += this.selected_zoom_scale.lit) {
					
					int view_x = canv_pix_to_vw_pix( x );
					int x2 = start_view_pix + view_x;
					g.DrawLine( pen, x2, h - lit_height, x2, h );
					
				}
				
			}
			
			
			
			if (this.selected_zoom_scale.mid > 0 ) {
				
				for (int x = this.start_canv_pix; x < this.end_canv_pix; x += this.selected_zoom_scale.mid) {
					
					int view_x = canv_pix_to_vw_pix( x );
					int x2 = start_view_pix + view_x;
					g.DrawLine( pen, x2, h - mid_height, x2, h );
					
				}
				
			}
			
			
			
			for (int x = this.start_canv_pix; x < this.end_canv_pix; x += this.selected_zoom_scale.big) {
				
				int view_x = canv_pix_to_vw_pix( x );
				int x2 = start_view_pix + view_x;
				g.DrawLine( pen, x2, h - big_height, x2, h );
				g.DrawString( x + "", new Font("arial", 7f), Brushes.White, x2 + digits_space_from_big_line, 0 );
			}
			
		
		}
		
		
	}
	
	
}
