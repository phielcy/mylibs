/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/24/2022
 * Time: 5:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;

namespace myui
{
	/// <summary>
	/// Description of vsep.
	/// </summary>
	public class sep_v : Control
	{
		public sep_v()
		{
			this.Width = 9;
			this.Height = 32;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			float w = this.Width;
			float h = this.Height;
			Pen pen = new Pen(Color.Gray, 1.5f);
			g.DrawLine(pen, w/2f, 2f, w/2f, h-2f);
			
		}
		
	}
}
