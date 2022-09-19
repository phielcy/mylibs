/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 5/11/2022
 * Time: 6:45 PM
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
	/// Description of color_track.
	/// </summary>
	public class color_track : Control
	{
		
		// TODO: ...
		//event EventHandler ValueChanged = null;
		
		
		private Color color1;
		public Color Color1 {
			get {return this.color1;}
			set {
				this.color1 = value;
				this.Invalidate();
			}
		}
		
		private Color color2;
		public Color Color2 {
			get {return this.color2;}
			set {
				this.color2 = value;
				this.Invalidate();
			}
		}
		
		private int _value = 0;
		public int Value {
			get {return this._value;}
			set {
				this._value = value;
				if (this.num_ud != null) {
					this.num_ud_value_changed_event_fired_from_here = true;
					this.num_ud.Value = this._value;
				}
				this.Invalidate();
			}
		}
		
		private NumericUpDown num_ud = null;
		public NumericUpDown NumUpDown {
			get {return this.num_ud;}
			set {
				this.num_ud = value;
				this.num_ud.Minimum = 0;
				this.num_ud.Maximum = 255;
				this.num_ud.Value = this.Value;
				this.num_ud.ValueChanged += new EventHandler(nud_color_track_ValueChanged);
			}
		}
		
		bool num_ud_value_changed_event_fired_from_here = false;
		void nud_color_track_ValueChanged(object sender, EventArgs e)
		{
			if (!num_ud_value_changed_event_fired_from_here) {
				this._value = (int)num_ud.Value;
				this.Invalidate();
			}
			this.num_ud_value_changed_event_fired_from_here = false;
		}
		
		
		
		
		TextureBrush chessboard_texture_brush;
		
		public color_track() {
			// for form designer
			this.color1 = Color.Black;
			this.color2 = Color.Red;
			this.DoubleBuffered = true;
			this.chessboard_texture_brush = mygraph.make_chesserboard_texture_brush(4);
			this.Cursor = Cursors.Hand;
		}
		
		public color_track(Color color1, Color color2)
		{
			this.color1 = color1;
			this.color2 = color2;
			this.DoubleBuffered = true;
			this.chessboard_texture_brush = mygraph.make_chesserboard_texture_brush(4);
			this.Cursor = Cursors.Hand;
		}
		
		
		
		
		
		
		
		
		float rect_height_factor = 0.8f;
		float triangle_height_factor = 0.33f;
		
		float rect_width;
		float rect_height;
		float triangle_height;
		float triangle_width;
		
		protected override void OnPaint(PaintEventArgs e)
		{
			
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			
			float w = this.Width;
			float h = this.Height;
			
			rect_height = h * rect_height_factor;
			triangle_height = h * triangle_height_factor;
			triangle_width = triangle_height * 1.25f;
			rect_width = w - triangle_width;
			
			RectangleF rect = new RectangleF(triangle_width/2f, 0, rect_width, rect_height);
			
			g.FillRectangle(this.chessboard_texture_brush, rect);
			
			LinearGradientBrush lgbr = new LinearGradientBrush(
				new PointF(0, h/2f), new PointF(w, h/2f),
				this.color1, this.color2);
			
			
			g.FillRectangle(lgbr, rect);
			
			List<PointF> tri_pts = new List<PointF> ( );
			
			tri_pts.Add(new PointF(triangle_width/2f, h-triangle_height));
			tri_pts.Add(new PointF(0, h));
			tri_pts.Add(new PointF(triangle_width, h));
			
			float tri_move_x = rect_width * this._value / 255f;
			
			for (int i = 0; i < tri_pts.Count; i++)
				tri_pts[i] = new PointF(tri_pts[i].X + tri_move_x, tri_pts[i].Y);
			
			Brush sb_triangle = Brushes.DarkGray;
			if (this.mouse_is_over) sb_triangle = Brushes.LightYellow;
			g.FillPolygon(sb_triangle, tri_pts.ToArray());
			Pen pen_triangle = new Pen(Color.WhiteSmoke, 1.5f);
			g.DrawLines(pen_triangle, tri_pts.ToArray());
			
		}
		
		
		
		
		
		
		bool mouse_is_over = false;
		
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.mouse_is_over = true;
			this.Invalidate();
		}
		
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.mouse_is_over = false;
			this.Invalidate();
		}
		
		
		
		bool mouse_is_down = false;
		
		int mouse_pos_to_value(int x) {
			float val = 255f * ( (x - triangle_width/2f) / rect_width );
			if (val < 0) val = 0;
			if (val > 255) val = 255;
			return (int)val;
		}
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this.Enabled) return;
			this.mouse_is_down = true;
			if (e.Button == MouseButtons.Left) {
				this.Value = mouse_pos_to_value(e.Location.X);
			}
			base.OnMouseDown(e);
		}
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (mouse_is_down) {
				this.Value = mouse_pos_to_value(e.Location.X);
			}
			base.OnMouseMove(e);
		}
		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.mouse_is_down = false;
			base.OnMouseUp(e);
		}
		
		
	}
	
	
}
