/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/28/2022
 * Time: 6:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace mytools
{
	/// <summary>
	/// Description of mycolor.
	/// </summary>
	public static class mycolor
	{
		
		public static Color mul(Color c, float v) {
			
//			float a = c.A * v;
			float r = c.R * v;
			float g = c.G * v;
			float b = c.B * v;
//			if (a < 0) a = 0;
			if (r < 0) r = 0;
			if (g < 0) g = 0;
			if (b < 0) b = 0;
//			if (a > 255) a = 255;
			if (r > 255) r = 255;
			if (g > 255) g = 255;
			if (b > 255) b = 255;
			
			return Color.FromArgb(c.A, (int)r,(int)g,(int)b);
		}
		
		
		
		
		public static Color color_circle_xy_to_color(PointF point, int width, int height, Color bk_color, bool keep_on_max_radius, out PointF point2) {
			
			
			// TODO: shoja: I should do it refrenced to science of it, not to my own creativity :)
			// https://stackoverflow.com/questions/23090019/fastest-formula-to-get-hue-from-rgb
			// https://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
			// https://www.rapidtables.com/convert/color/rgb-to-hsv.html
			
			int bm_w = width;
			int bm_h = height;
			
			point2 = point;
			
			PointF cartesian = mygeo.graphical_to_cartesian(point, bm_w, bm_h);
			mygeo.PolarF polar = mygeo.cartesian_to_polar(cartesian);
			
			float r = polar.r;
			float theta = polar.theta;
			
			float R = 0, G = 0, B = 0, A = 1f;
			
			float max_r = (float)bm_w / 2f;
			
			if (r > max_r  &&  keep_on_max_radius) {
				r = max_r;
				PointF cartesian2 = mygeo.polar_to_cartesian(new mygeo.PolarF(max_r, theta));
				point2 = mygeo.cartesian_to_graphical(cartesian2, width, height);
			}
			
			float near_to_center_factor = (max_r - r) / max_r;
			if (near_to_center_factor < 0f) near_to_center_factor = 0f;
			if (near_to_center_factor > 1f) near_to_center_factor = 1f;
			
			if (r <= max_r) {
				
				if (theta >= 0  &&  theta < 120) {
					R = (120f - theta) / 120f + near_to_center_factor;
					G = near_to_center_factor;
					B = (theta - 0f) / 120f + near_to_center_factor;
				}
				
				if (theta >= 120  &&  theta < 240) {
					R = near_to_center_factor;
					G = (theta - 120) / 120f + near_to_center_factor;
					B = (240f - theta) / 120f + near_to_center_factor;
				}
				
				if (theta >= 240  &&  theta < 360) {
					R = (theta - 240) / 120f + near_to_center_factor;
					G = (360f - theta) / 120f + near_to_center_factor;
					B = near_to_center_factor;
				}
				
				
			} else {
				
				return bk_color;
				
			}
			
			int AA = (int)(A * 255f);
			int RR = (int)(R * 255f);
			int GG = (int)(G * 255f);
			int BB = (int)(B * 255f);
			
			if (RR > 255) RR = 255;
			if (GG > 255) GG = 255;
			if (BB > 255) BB = 255;
			
			Color color = Color.FromArgb (AA, RR, GG, BB);
			
			return color;
			
		}
		
		
		
		
		public enum color_componenets {
			R,G,B
		}
		
		
		public enum color_areas {
			RG, GB , BR
		}
		
		
		
		
		public static PointF color_circle_color_to_xy(Color color, int width, int height) {
			
			
			// shoja urls:
			// https://stackoverflow.com/questions/23090019/fastest-formula-to-get-hue-from-rgb
			// https://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
			// https://www.rapidtables.com/convert/color/rgb-to-hsv.html
			
			
			//color_componenets comp_min = color_componenets.R;
			float val_min = color.R;
			if (color.G < val_min) {
				/*comp_min = color_componenets.G;*/ val_min = color.G; }
			if (color.B < val_min) {
				/*comp_min = color_componenets.B;*/ val_min = color.B; }
			
			color_componenets comp_max = color_componenets.R;
			float val_max = color.R;
			if (color.G > val_max) {
				comp_max = color_componenets.G; val_max = color.G; }
			if (color.B > val_max) {
				comp_max = color_componenets.B; val_max = color.B; }
						
			val_max /= 255f;
			val_min /= 255f;
			
			float theta = 0;
			
			float r2, g2, b2;
			r2 = color.R / 255f;
			g2 = color.G / 255f;
			b2 = color.B / 255f;
			float delta = val_max - val_min;
			
			if (comp_max == color_componenets.R) {
				theta =     0f +  60f * ( (g2-b2) / delta );
			}
			
			if (comp_max == color_componenets.G) {
				theta =   120f + 60f * ( (b2-r2) / delta );
			}
			
			if (comp_max == color_componenets.B) {
				theta =   240f + 60f * ( (r2-g2) / delta );
			}
			
			if (theta < 0) theta = 360f + theta;
			if (theta > 360f) theta = theta - 360f;
			theta = 360f - theta;
			
			float r = 0;
			float r_max = (float)width / 2f;
			if (val_max != 0) r = (delta / val_max) * r_max;
			if (r <     0) r = 0;
			if (r > r_max) r = r_max;
			
			if (color.R == color.G  &&  color.G == color.B) {
				r = 0;
				
				theta = 0;
			}
			
			PointF cart_xy = mygeo.polar_to_cartesian(new mygeo.PolarF(r, theta));
			PointF graph_xy = mygeo.cartesian_to_graphical(cart_xy, width, height);
			return graph_xy;
			
		}
		
		
		
		public static Bitmap create_color_circle(int width, int height, Color bk_color) {
			
			int bm_w = width;
			int bm_h = height;
			
			Bitmap bmp = new Bitmap(bm_w,bm_h, PixelFormat.Format32bppPArgb);
			
			for (int y = 0; y < bm_h; y++) {
				
				for (int x = 0; x < bm_w; x++) {
					
					PointF point2;
					Color color = color_circle_xy_to_color(new PointF(x, y), width, height, bk_color, false, out point2);
					bmp.SetPixel(x, y, color);
					
				}
				
			}
			
			
			return bmp;
			
		}
		
		
		
		
		
	}
	
}
