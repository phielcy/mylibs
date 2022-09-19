/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 21/03/1401
 * Time: 03:36 ب.ظ
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace mytools
{
	/// <summary>
	/// Description of mystring.
	/// </summary>
	public static class mystring
	{
		
		public static string capitalize_first_letter(string s) {
			
			if (s.Length == 0) return s;
			if (s.Length == 1) return s.ToUpper();
			return char.ToUpper(s[0]) + s.Substring(1);
			
		}
		
	}
	
}
