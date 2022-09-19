/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/25/2022
 * Time: 7:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace mytools
{
	/// <summary>
	/// Description of mymath.
	/// </summary>
	public static class mymath
	{
		
		public static float powf(float a, float b) {
			return (float)Math.Pow(a, b);
		}
		
		public static float logf(float a, float _base) {
			return (float)Math.Log(a, _base);
		}
		
		public static float decipartf(float a) {
			return a - truncf(a);
		}
		
		public static float truncf(float a) {
			return (float)Math.Truncate(a);
		}
		
		public static float ceilf(float a) {
			return (float)Math.Ceiling(a);
		}
		
		public static float floorf(float a) {
			return (float)Math.Floor(a);
		}
		
		public static float roundf(float a) {
			return (float)Math.Round(a);
		}
		
		public static float absf(float a) {
			return (float)Math.Abs(a);
		}
		
		public static int absi(int a) {
			return (int)Math.Round((float)Math.Abs(a));
		}
		
		
		
		
		
		
		public static float sin(float tetha) {
			return (float)Math.Sin(tetha * Math.PI / 180f);
		}
		
		public static float cos(float tetha) {
			return (float)Math.Cos(tetha * Math.PI / 180f);
		}
		
		public static float tan(float tetha) {
			return (float)Math.Tan(tetha * Math.PI / 180f);
		}
		
		public static float atan(float y_dived_by_x) {
			return (float) ( Math.Atan(y_dived_by_x) * 180f / Math.PI );
		}
		
		public static float atan2(float y, float x) {
			return (float) ( Math.Atan2(y, x) * 180f / Math.PI );
		}
		
		
		
		public static float sqrt(float a) {
			return (float)Math.Sqrt(a);
		}
		
		public static float pow(float x, float y) {
			return (float)Math.Pow(x, y);
		}
		
		
		
		public static bool is_in_rangei(int val, int min, int max) {
			if (val >= min  && val <= max)
				return true;
			return false;
		}
		
		public static bool is_in_rangef(float val, float min, float max) {
			if (val >= min  && val <= max)
				return true;
			return false;
		}
		
		public static bool ranges_are_intersectedi(int min1, int max1, int min2, int max2) {
			if ( is_in_rangei(min1, min2, max2)  ||  is_in_rangei(max1, min2, max2)  || 
			     is_in_rangei(min2, min1, max1)  ||  is_in_rangei(max2, min1, max1) )
				return true;
			return false;
		}
		
		
		
	}
}
