/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/28/2022
 * Time: 4:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of sep_h.
	/// </summary>
	public class sep_h : Control
	{
		
		public sep_h()
		{
			this.Width = 32;
			this.Height = 9;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			float w = this.Width;
			float h = this.Height;
			Pen pen = new Pen(Color.Gray, 1.5f);
			g.DrawLine(pen, 2f, h/2f, w-2f, h/2f);
			
		}
		
	}
	
}
