/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 5/6/2022
 * Time: 1:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of my_form_Control.
	/// </summary>
	partial class my_form
	{
		
		
		
		public bool mouse_is_over_me() {
			return this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
		}
		
		
		
		public bool intersects_with_doc_control() {
			if (this.parent_form.doc_control == null) return false;
			Rectangle rc = this.parent_form.doc_control.RectangleToScreen(this.parent_form.doc_control.ClientRectangle);
			Rectangle rf = this.RectangleToScreen(this.ClientRectangle);
			return rf.IntersectsWith(rc);
		}
		
		
		
		float opacity_step_abs_value = 0.05f;
		float opacity_step = 0.0f;
		
		
		public void make_opaque() {
			if (this.Opacity == 1.0) return;
			if (this.timer_opacity == null) return;
			this.opacity_step = +opacity_step_abs_value;
			this.timer_opacity.Start();
		}
		
		
		
		public void make_transparent() {
			if (this.Opacity == this.auto_opacity_min_value) return;
			if (this.timer_opacity == null) return;
			this.opacity_step = -opacity_step_abs_value;
			this.timer_opacity.Start();
		}
		
		
		
		void my_form_Tick(object sender, EventArgs e)
		{
			
			this.Opacity += this.opacity_step;
			
			if (this.opacity_step > 0  &&  this.Opacity >= 1.0f) {
				this.timer_opacity.Stop();
				this.Opacity = 1.0f;
			}
			
			if (this.opacity_step < 0  &&  this.Opacity <= this.auto_opacity_min_value) {
				this.timer_opacity.Stop();
				this.Opacity = this.auto_opacity_min_value;
			}
			
		}
		
	}
	
	
}
