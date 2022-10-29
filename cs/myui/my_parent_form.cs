/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 4/20/2022
 * Time: 02:45 ب.ظ
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using myll;
using mytools;

namespace myui
{
	/// <summary>
	/// Description of myMdiParentForm.
	/// </summary>
	public class my_parent_form : Form
	{
		
		public List<my_form> child_forms = null;
		
		public Control doc_control = null;
		
		public int magnet_space = 5;
		public int magnet_space_effect_field = 20;
		
		public int magnet_margin_left;
		public int magnet_margin_top;
		public int magnet_margin_right;
		public int magnet_margin_bottom;
		
		
		
		
		public my_parent_form()
		{
			
			child_forms = new List<my_form> ( );
			
		}
		
		
		
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			mouse_hook.start();
			mouse_hook.MouseAction += new mouse_hook.LLMouseEventHandler(mouse_hook_MouseAction);
		}
		
		
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			mouse_hook.stop();
		}
		
		
		
		
		
		void mouse_hook_MouseAction(object sender, mouse_hook.LLMouseEventArgs e)
		{
			mouse_hook.MouseMessages param = (mouse_hook.MouseMessages)e.wParam;
			if (param == mouse_hook.MouseMessages.WM_MOUSEMOVE) {
				
				foreach (my_form form in this.child_forms) {
					if (form != null  &&  !form.IsDisposed  &&  form.intersects_with_doc_control()) {
						if (form.mouse_is_over_me()) {
							form.make_opaque();
						} else {
							form.make_transparent();
						}
					} else {
						form.make_opaque();
					}
				}
				
			}
		}
		
		
		
		
		
		
		public virtual void set_magnet_margins() {
			// shoja: should be overrided by developer, and cutomly set every time form window state or size changes
			foreach (my_form form in this.child_forms) {
				form.reposition_if_you_are_attached_to_parent();
			}
		}
		
		
		
		
		
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.set_magnet_margins();
		}
		
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			this.set_magnet_margins();
		}
		
		
		
		
		
	}
	
	
	
}
