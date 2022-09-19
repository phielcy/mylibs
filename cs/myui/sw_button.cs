/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 5/19/2022
 * Time: 11:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of sw_button.
	/// </summary>
	public class sw_button : ns_button
	{
		
		
		public sw_button()
		{
		}
		
		
		private bool _checked = false;
		public bool Checked {
			get {return this._checked;}
			set {
				this._checked = value;
				this.Invalidate();
			}
		}
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			
			if (this.Checked) {
				SolidBrush sb = new SolidBrush(Color.FromArgb(64, 0, 32, 128));
				g.FillRectangle(sb, this.ClientRectangle);
				Pen p = Pens.CadetBlue;
				g.DrawRectangle(p, this.ClientRectangle);
			}
		}
		
		
	}
	
	
}
