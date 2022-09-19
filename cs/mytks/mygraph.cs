/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 5/12/2022
 * Time: 7:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace mytools
{
	/// <summary>
	/// Description of mygraph.
	/// </summary>
	public static class mygraph
	{
		
		
		
		public static TextureBrush make_chesserboard_texture_brush(int sw /*single square width*/) {
			
			Bitmap tbm = new Bitmap(sw*2, sw*2, PixelFormat.Format32bppPArgb);
			Graphics gbm = Graphics.FromImage(tbm);
			for (int x = 0; x < 2; x++) {
				for (int y = 0; y < 2; y++) {
					SolidBrush rbr = new SolidBrush(Color.White);
					if ((x + y) % 2 == 1) rbr = new SolidBrush(Color.LightGray);
					gbm.FillRectangle(rbr, x*sw, y*sw, sw, sw);
				}
			}
			
			return new TextureBrush(tbm);
			
		}
		
		
		
		public static void render_control_to_graphics(Graphics g, Control c) {
			
			Bitmap b = new Bitmap(c.Width, c.Height, PixelFormat.Format32bppArgb);
			c.DrawToBitmap(b, c.ClientRectangle);
			g.DrawImage(b, c.Location);
			
		}
		
		
		
		public static Bitmap pickup_part_of_bitmap(Bitmap src, Rectangle src_rect) {
			
			Bitmap dest = new Bitmap(src_rect.Width, src_rect.Height, PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(dest);
			Rectangle dest_rect = new Rectangle(0,0, src_rect.Width, src_rect.Height);
			g.DrawImage(src, dest_rect, src_rect, GraphicsUnit.Pixel);
			
			return dest;
			
		}
		
		
		
	}
	
}
