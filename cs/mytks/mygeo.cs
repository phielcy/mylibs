/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 4/27/2022
 * Time: 3:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace mytools
{
	/// <summary>
	/// Description of mygeo.
	/// </summary>
	public static class mygeo
	{
		
		
		public static Point point_add(Point p1, Point p2) {
			return new Point( p1.X + p2.X,   p1.Y + p2.Y );
		}
		
		public static Point point_diff(Point p1, Point p2) {
			return new Point( p1.X - p2.X,   p1.Y - p2.Y );
		}
		
		
		public static Point point_abs(Point p) {
			return new Point( mymath.absi(p.X),   mymath.absi(p.Y) );
		}
		
		
		public static bool points_are_same(Point p1, Point p2) {
			if ( p1.X == p2.X  &&  p1.Y == p2.Y )
				return true;
			return false;
		}
		
		
		
		
		public static Rectangle rect_make_positive(Rectangle r) {
			
			Point p = r.Location;
			Size s = r.Size;
			
			if (s.Width < 0) {
				p.X = p.X + s.Width - 1;
				s.Width = -s.Width + 2;
			}
			
			if (s.Height < 0) {
				p.Y = p.Y + s.Height - 1;
				s.Height = -s.Height + 2;
			}
			
			if (s.Width == 0 ) {
				s.Width = 1;
			}
			
			if (s.Height == 0 ) {
				s.Height = 1;
			}
			
			Rectangle r2 = new Rectangle(p, s);
			return r2;
			
		}
		
		
		
		
		public static PointF cartesian_to_graphical(PointF pc, float width, float height) {
			float gx = (width  / 2f) + pc.X;
			float gy = (height / 2f) - pc.Y;
			return new PointF(gx, gy);
		}
		
		public static PointF graphical_to_cartesian(PointF pg, float width, float height) {
			float cx = +pg.X - (width  / 2f);
			float cy = -pg.Y + (height / 2f);
			return new PointF(cx, cy);
		}
		

		
		
		
		
		public class PolarF {
			public float r;
			public float theta;
			public PolarF(float r, float tetha) {
				this.r = r;
				this.theta = tetha;
			}
		}
		
		public static PolarF cartesian_to_polar(PointF p) {
			float r = mymath.sqrt( (p.X*p.X) + (p.Y*p.Y) );
			float tetha = mymath.atan2 ( p.Y, p.X );
			if (tetha < 0) tetha = 360 + tetha;
			return new PolarF(r, tetha);
		}
		
		public static PointF polar_to_cartesian(PolarF pol) {
			float x = pol.r * mymath.cos(pol.theta);
			float y = pol.r * mymath.sin(pol.theta);
			return new PointF(x, y);
		}
		
		
	}
}
