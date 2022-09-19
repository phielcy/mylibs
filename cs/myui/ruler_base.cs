/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/25/2022
 * Time: 10:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using mytools;

namespace myui
{
	/// <summary>
	/// Description of ruler_base.
	/// </summary>
	public abstract class ruler_base : Control
	{
		
		public int start_view_pix;
		public int end_view_pix;
		public int start_canv_pix = 0;
		public int end_canv_pix = 100;
		
		
		public float big_height_factor = 1.00f;
		public float mid_height_factor = 0.66f;
		public float lit_height_factor = 0.33f;
		
		public Rectangle selection;
		
		
		public ruler_base()
		{
			this.DoubleBuffered = true;
			this.BackColor = Color.DimGray;
		}

		
		public void set_props(int start_view_pix, int end_view_pix, int start_canv_pix, int end_canv_pix, zoom_scale force_zs) {
			
			this.start_view_pix = start_view_pix;
			this.end_view_pix = end_view_pix;
			this.start_canv_pix = start_canv_pix;
			this.end_canv_pix = end_canv_pix;
			
			if (force_zs == null)
				this.selected_zoom_scale = find_best_zoom_scale_match();
			else
				this.selected_zoom_scale = force_zs;
			
			this.Invalidate();
			
		}
		
		
		
		public void set_selection(Rectangle selection) {
			this.selection = selection;
			this.Invalidate();
		}
		
		
		
		
		
		public class zoom_scale {
			
			public int big;
			public int mid;
			public int lit;
			
			public zoom_scale(int big, int mid, int lit) {
				this.big = big;
				this.mid = mid;
				this.lit = lit;
			}
			
		}
		
		
		zoom_scale[] zoom_scales = new zoom_scale[]
		{
			new zoom_scale(1	,0		,0),
			new zoom_scale(2	,1		,0),
			new zoom_scale(5	,0		,0),
			new zoom_scale(10	,5		,1),
			new zoom_scale(10	,0		,2),
			new zoom_scale(20	,10		,2),
			new zoom_scale(20	,10		,5),
			new zoom_scale(50	,10		,5),
			new zoom_scale(50	,10		,0),
			new zoom_scale(100	,50		,10),
			new zoom_scale(100	,00		,20),
			new zoom_scale(200	,100	,20),
			new zoom_scale(200	,100	,50),
			new zoom_scale(500	,100	,50),
			new zoom_scale(500	,000	,100),
			new zoom_scale(1000	,500	,100),
			new zoom_scale(1000	,000	,200),
			new zoom_scale(2000	,1000	,200),
			new zoom_scale(2000	,1000	,500),
			new zoom_scale(5000	,1000	,250),
			new zoom_scale(5000	,1000	,500),
			new zoom_scale(5000	,0000	,1000),
			new zoom_scale(10000,5000	,1000),
		};
		
		
		public int ideal_big_scales_pixel_space = 50; // pixels
		
		public zoom_scale selected_zoom_scale = null;
		
		
		int calc_big_scale_pixel_space(zoom_scale zs) {
			
			int total_view_pixels = mymath.absi(this.end_view_pix - this.start_view_pix);
			int total_canvas_pixels = mymath.absi(this.end_canv_pix - this.start_canv_pix);
			int scale_pixel_width_in_view = total_view_pixels * zs.big  /  total_canvas_pixels;
			
			return scale_pixel_width_in_view;
		}
		
		
		public zoom_scale find_best_zoom_scale_match() {
			
			int min_space_diff = int.MaxValue;
			zoom_scale best_zs = null;
			foreach (zoom_scale zs in zoom_scales) {
				int big_scale_pixel_space = calc_big_scale_pixel_space(zs);
				int diff = mymath.absi(big_scale_pixel_space - ideal_big_scales_pixel_space);
				if ( diff < min_space_diff ) {
					best_zs = zs;
					min_space_diff = diff;
				}
			}
			return best_zs;
		}
		
		
		
		
		
		
		public int canv_pix_to_vw_pix(int canv_pix) {
			
			int total_view_pixels = mymath.absi(this.end_view_pix - this.start_view_pix);
			int total_canvas_pixels = mymath.absi(this.end_canv_pix - this.start_canv_pix);
			int vw_pix = canv_pix * total_view_pixels / total_canvas_pixels;
			
			return vw_pix;
		}
		
		
		public int vw_pix_to_canv_pix(int vw_pix) {
			
			int total_view_pixels = mymath.absi(this.end_view_pix - this.start_view_pix);
			int total_canvas_pixels = mymath.absi(this.end_canv_pix - this.start_canv_pix);
			int canv_pix = vw_pix * total_canvas_pixels / total_view_pixels;
			
			return canv_pix;
		}
		
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			//e.Graphics.DrawRectangle(Pens.White, 0, 0, this.Width-1, this.Height-1);
		}
		
		
	}
}
