/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/30/2022
 * Time: 5:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using mytools;

namespace myui
{
	/// <summary>
	/// Description of my_form.
	/// </summary>
	partial class my_form
	{
		
		public Form attached_to_my_left   = null;
		public Form attached_to_my_top    = null;
		public Form attached_to_my_right  = null;
		public Form attached_to_my_bottom = null;
		
		
		
		#region magnet effect check near parent form magnet margins
		
		
		public my_parent_form parent_magnet_margin_near_my_left(Point diff) {
			
			int new_left = this.Left + diff.X;
			
			my_parent_form prnt = this.parent_form;
			
			if (mymath.ranges_are_intersectedi(prnt.magnet_margin_top, prnt.magnet_margin_bottom, this.Top, this.Bottom)  &&
			    mymath.is_in_rangei(new_left, prnt.magnet_margin_left + parent_form.magnet_space - prnt.magnet_space_effect_field, prnt.magnet_margin_left + parent_form.magnet_space + prnt.magnet_space_effect_field)) {
				return prnt;
			}
			
			return null;
			
		}
		
		
		public my_parent_form parent_magnet_margin_near_my_top(Point diff) {
			
			int new_top = this.Top + diff.Y;
			
			my_parent_form prnt = this.parent_form;
			
			if (mymath.ranges_are_intersectedi(prnt.magnet_margin_left, prnt.magnet_margin_right, this.Left, this.Right)  &&
			    mymath.is_in_rangei(new_top, prnt.magnet_margin_top + parent_form.magnet_space - prnt.magnet_space_effect_field, prnt.magnet_margin_top + parent_form.magnet_space + prnt.magnet_space_effect_field)) {
				return prnt;
			}
			
			return null;
			
		}
		
		
		public my_parent_form parent_magnet_margin_near_my_right(Point diff) {
			
			int new_right = this.Right + diff.X;
			
			my_parent_form prnt = this.parent_form;
			
			if (mymath.ranges_are_intersectedi(prnt.magnet_margin_top, prnt.magnet_margin_bottom, this.Top, this.Bottom)  &&
			    mymath.is_in_rangei(new_right, prnt.magnet_margin_right - parent_form.magnet_space - prnt.magnet_space_effect_field, prnt.magnet_margin_right - parent_form.magnet_space + prnt.magnet_space_effect_field)) {
				return prnt;
			}
			
			return null;
			
		}
		
		
		public my_parent_form parent_magnet_margin_near_my_bottom(Point diff) {
			
			int new_bottom = this.Bottom + diff.Y;
			
			my_parent_form prnt = this.parent_form;
			
			if (mymath.ranges_are_intersectedi(prnt.magnet_margin_left, prnt.magnet_margin_right, this.Left, this.Right)  &&
			    mymath.is_in_rangei(new_bottom, prnt.magnet_margin_bottom - parent_form.magnet_space - prnt.magnet_space_effect_field, prnt.magnet_margin_bottom - parent_form.magnet_space + prnt.magnet_space_effect_field)) {
				return prnt;
			}
			
			return null;
			
		}
		
		
		#endregion
		
		
		#region magnet effect check near other forms methods
		
		public my_form find_form_near_my_left(Point diff) {
			
			int new_left = this.Left + diff.X;
			
			foreach (my_form frm in this.parent_form.child_forms)
				
				if ( frm.Visible  &&
				    mymath.ranges_are_intersectedi(frm.Top, frm.Bottom, this.Top, this.Bottom)  &&
				    mymath.is_in_rangei(new_left, frm.Right + parent_form.magnet_space - parent_form.magnet_space_effect_field, frm.Right + parent_form.magnet_space + parent_form.magnet_space_effect_field) )
					
					return frm;
			
			return null;
			
		}
		
		
		public my_form find_form_near_my_top(Point diff) {
			
			int new_top = this.Top + diff.Y;
			
			foreach (my_form frm in this.parent_form.child_forms)
				
				if ( frm.Visible  &&
				    mymath.ranges_are_intersectedi(frm.Left, frm.Right, this.Left, this.Right)  &&
				    mymath.is_in_rangei(new_top, frm.Bottom + parent_form.magnet_space - parent_form.magnet_space_effect_field, frm.Bottom + parent_form.magnet_space + parent_form.magnet_space_effect_field) )
					
					return frm;
			
			return null;
			
		}
		
		
		public my_form find_form_near_my_right(Point diff) {
			
			int new_right = this.Right + diff.X;
			
			foreach (my_form frm in this.parent_form.child_forms)
				
				if ( frm.Visible  &&
				    mymath.ranges_are_intersectedi(frm.Top, frm.Bottom, this.Top, this.Bottom)  &&
				    mymath.is_in_rangei(new_right, frm.Left - parent_form.magnet_space - parent_form.magnet_space_effect_field, frm.Left - parent_form.magnet_space + parent_form.magnet_space_effect_field) )
					
					return frm;
			
			return null;
			
		}
		
		
		public my_form find_form_near_my_bottom(Point diff) {
			
			int new_bottom = this.Bottom + diff.Y;
			
			foreach (my_form frm in this.parent_form.child_forms)
				
				if ( frm.Visible  &&
				    mymath.ranges_are_intersectedi(frm.Left, frm.Right, this.Left, this.Right)  &&
				    mymath.is_in_rangei(new_bottom, frm.Top - parent_form.magnet_space - parent_form.magnet_space_effect_field, frm.Top - parent_form.magnet_space + parent_form.magnet_space_effect_field) )
					
					
					return frm;
			
			return null;
			
		}
		
		#endregion
		
		
		#region attach/detach to forms methods
		
		
		int last_parent_left_margin;
		int last_parent_top_margin;
		int last_parent_right_margin;
		int last_parent_bottom_margin;
		
		void save_parent_margins() {
			this.last_parent_left_margin = parent_form.magnet_margin_left;
			this.last_parent_top_margin = parent_form.magnet_margin_top;
			this.last_parent_right_margin = parent_form.magnet_margin_right;
			this.last_parent_bottom_margin = parent_form.magnet_margin_bottom;
		}
		
		// LEFT:
		
		public void attach_to_my_left(Form target_form) {
			
			if (target_form == null) return;
			
			this.attached_to_my_left = target_form;
			
			if (target_form is my_form)
				((my_form)target_form).attached_to_my_right = this;
			
			save_parent_margins();
		}
		
		
		public void detach_from_my_left() {
			
			if (this.attached_to_my_left == null) return;
			
			if (this.attached_to_my_left is my_form)
				((my_form)this.attached_to_my_left).attached_to_my_right = null;
			
			this.attached_to_my_left = null;
			
		}
		
		
		
		// RIGHT:
		
		public void attach_to_my_right(Form target_form) {
			
			if (target_form == null) return;
			
			this.attached_to_my_right = target_form;
			
			if (target_form is my_form)
				((my_form)target_form).attached_to_my_left = this;
			
			save_parent_margins();
		}
		
		
		public void detach_from_my_right() {
			
			if (this.attached_to_my_right == null) return;
			
			if (this.attached_to_my_right is my_form)
				((my_form)this.attached_to_my_right).attached_to_my_left = null;
			
			this.attached_to_my_right = null;
			
		}
		
		
		
		// TOP:
		
		public void attach_to_my_top(Form target_form) {
			
			if (target_form == null) return;
			
			this.attached_to_my_top = target_form;
			
			if (target_form is my_form)
				((my_form)target_form).attached_to_my_bottom = this;
			
			save_parent_margins();
		}
		
		
		public void detach_from_my_top() {
			
			if (this.attached_to_my_top == null) return;
			
			if (this.attached_to_my_top is my_form)
				((my_form)this.attached_to_my_top).attached_to_my_bottom = null;
			
			this.attached_to_my_top = null;
			
		}
		
		
		
		// BOTTOM:
		
		public void attach_to_my_bottom(Form target_form) {
			
			if (target_form == null) return;
			
			this.attached_to_my_bottom = target_form;
			
			if (target_form is my_form)
				((my_form)target_form).attached_to_my_top = this;
			
			save_parent_margins();
		}
		
		
		public void detach_from_my_bottom() {
			
			if (this.attached_to_my_bottom == null) return;
			
			if (this.attached_to_my_bottom is my_form)
				((my_form)this.attached_to_my_bottom).attached_to_my_top = null;
			
			this.attached_to_my_bottom = null;
			
		}
		
		#endregion
		
		
		#region expand to attach form
		
		
		public void expand_bottom_to_attach_form(my_form target_form) {
			
			if (target_form.Top < this.Top) return;
			this.Height = target_form.Top - this.Top - parent_form.magnet_space;
			this.attach_to_my_bottom(target_form);
			
		}
		
		
		public void expand_right_to_attach_form(my_form target_form) {
			
			if (target_form.Left < this.Left) return;
			this.Width = target_form.Left - this.Left - parent_form.magnet_space;
			this.attach_to_my_right(target_form);
			
		}
		
		
		public void expand_top_to_attach_form(my_form target_form) {
			
			if (target_form.Top > this.Top) return;
			this.Height = this.Bottom - target_form.Bottom - parent_form.magnet_space;
			this.Top = target_form.Bottom + parent_form.magnet_space;
			this.attach_to_my_top(target_form);
			
		}
		
		
		public void expand_left_to_attach_form(my_form target_form) {
			
			if (target_form.Left > this.Left) return;
			this.Width = this.Right - target_form.Right - parent_form.magnet_space;
			this.Left = target_form.Right + parent_form.magnet_space;
			this.attach_to_my_left(target_form);
			
		}
		
		
		#endregion
		
		
		#region stick to parent's sides
		
		public void stick_to_parents_left() {
			this.Left = parent_form.magnet_margin_left + parent_form.magnet_space;
			this.attach_to_my_left(parent_form);
		}
		
		public void stick_to_parents_top() {
			this.Top = parent_form.magnet_margin_top + parent_form.magnet_space;
			this.attach_to_my_top(parent_form);
		}
		
		public void stick_to_parents_right() {
			this.Left = parent_form.magnet_margin_right - parent_form.magnet_space - this.Width;
			this.attach_to_my_right(parent_form);
		}
		
		public void stick_to_parents_bottom() {
			this.Top = parent_form.magnet_margin_bottom - parent_form.magnet_space - this.Height;
			this.attach_to_my_bottom(parent_form);
		}
		
		#endregion
		
		
		#region stick to sides
		
		public void stick_to_forms_left(my_form form) {
			this.Left = form.Left - this.Width - parent_form.magnet_space;
			this.attach_to_my_right(form);
		}
		
		public void stick_to_forms_top(my_form form) {
			this.Top = form.Top - this.Height - parent_form.magnet_space;
			this.attach_to_my_bottom(form);
		}
		
		public void stick_to_forms_right(my_form form) {
			this.Left = form.Right + parent_form.magnet_space;
			this.attach_to_my_left(form);
		}
		
		public void stick_to_forms_bottom(my_form form) {
			this.Top = form.Bottom + parent_form.magnet_space;
			this.attach_to_my_bottom(parent_form);
		}
		
		#endregion
		
		
		#region reposition to arrange
		
		public void reposition_if_you_are_attached_to_parent() {
			
			bool attached_horizontally = false;
			bool attached_vertically = false;
			
			Point new_loc = this.Location;
			my_parent_form prnt = this.parent_form;
			
			
			// horzontal check:
			
			if (this.attached_to_my_left is my_parent_form) {
				attached_horizontally = true;
				new_loc.X = prnt.magnet_margin_left + prnt.magnet_space;
			}
			
			if (!attached_horizontally  &&  this.attached_to_my_right is my_parent_form) {
				attached_horizontally = true;
				new_loc.X = prnt.magnet_margin_right - prnt.magnet_space - this.Width;
			}
			
			
			// vertical check:
			
			if (this.attached_to_my_top is my_parent_form) {
				attached_vertically = true;
				new_loc.Y = prnt.magnet_margin_top + prnt.magnet_space;
			}
			
			if (!attached_vertically  &&  this.attached_to_my_bottom is my_parent_form) {
				attached_vertically = true;
				new_loc.Y = prnt.magnet_margin_bottom - prnt.magnet_space - this.Height;
			}
			
			
			// calc for not attached sides
			
			if (attached_horizontally  &&  !attached_vertically) {
				new_loc.Y += parent_form.magnet_margin_top - last_parent_top_margin;
			}
			
			if (attached_vertically  &&  !attached_horizontally) {
				new_loc.X += parent_form.magnet_margin_left - last_parent_left_margin;
			}
			
			
			save_parent_margins();
			
			if (attached_horizontally  ||  attached_vertically)
				this.Location = new_loc;
			
		}
		
		#endregion
		
		
		
	}
	
	
}
