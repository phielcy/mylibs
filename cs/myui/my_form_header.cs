/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/11/2022
 * Time: 10:41 PM
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
	/// Description of my_form_header.
	/// </summary>
	public class my_form_header : Panel
	{
		
//		public Color back_color = Color.DimGray;
//		public int height = 20;
		public my_form cform = null;
//		public bool close_button = true;
//		public bool max_button = true;
//		public bool min_button = true;
		public Button close_button = null;
		public Label text = null;
		
		
		public my_form_header(my_form container)
		{
			
			this.cform = container;
			this.cform.SuspendLayout();
			
			this.Height = 20;
			this.Width = this.cform.Width;
			this.BackColor = Color.DimGray;
			
			this.close_button = new ns_button();
			this.close_button.FlatAppearance.BorderSize = 0;
			this.close_button.FlatStyle = FlatStyle.Flat;
			this.close_button.BackColor = Color.OrangeRed;
			this.close_button.Size = new Size(20, 20);
			this.close_button.Location = new Point(this.Width-this.close_button.Width, 0);
			this.close_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.close_button.Click += new EventHandler(formheader_btn_close_Click);
			this.Controls.Add(this.close_button);
			
			this.text = new Label();
			this.text.Location = new Point(0,0);
			this.text.AutoSize = true;
			this.text.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.text.Font = cform.Font;
			this.text.Font = new Font(cform.Font.FontFamily, cform.Font.Size, FontStyle.Bold);
			this.text.Text = cform.Text;
			this.text.ForeColor = Color.LightGray;
			this.Controls.Add(this.text);
			
			this.Top = 0;
			
			this.Left = 0;
			this.Width = cform.Width;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
			
			this.cform.Controls.Add(this);
			this.cform.ResumeLayout();
			
			this.MouseDown += new MouseEventHandler(formheader_MouseDown);
			this.MouseUp   += new MouseEventHandler(formheader_MouseUp);
			this.MouseMove += new MouseEventHandler(formheader_MouseMove);
			
		}
		
		
		
		public void set_text(string _text) {
			this.text.Text = _text;
			rearrange_label();
		}
		
		
		void rearrange_label() {
			this.text.Top = (this.Height / 2) - (this.text.Height / 2);
			this.text.Left = 2;
		}
		
		
		
		void formheader_btn_close_Click(object sender, EventArgs e)
		{
			
			if (cform.close_behavior == close_button_behavior.close)
				this.cform.Close();
			
			if (cform.close_behavior == close_button_behavior.hide)
				this.cform.Hide();
			
		}
		
		
		
		
		
		
		void formheader_MouseMove(object sender, MouseEventArgs e)
		{
			
			if (this.mouse_is_down) {
				
				Point diff = mygeo.point_diff(e.Location, this.mouse_down_point);
				
				Point new_loc = mygeo.point_add(this.cform.Location, diff);
				
				
				cform.detach_from_my_left();
				cform.detach_from_my_top();
				cform.detach_from_my_right();
				cform.detach_from_my_bottom();
				
				
				// magnet to other forms
				
				my_form form_near_my_left = cform.find_form_near_my_left(diff);
				if (form_near_my_left != null) {
					new_loc.X = form_near_my_left.Right + cform.parent_form.magnet_space;
					cform.attach_to_my_left(form_near_my_left);
				}
				
				my_form form_near_my_right = cform.find_form_near_my_right(diff);
				if (form_near_my_right != null) {
					new_loc.X = form_near_my_right.Left - cform.Width - cform.parent_form.magnet_space;
					cform.attach_to_my_right(form_near_my_right);
				}
				
				my_form form_near_my_top = cform.find_form_near_my_top(diff);
				if (form_near_my_top != null) {
					new_loc.Y = form_near_my_top.Bottom + cform.parent_form.magnet_space;
					cform.attach_to_my_top(form_near_my_top);
				}
				
				my_form form_near_my_bottom = cform.find_form_near_my_bottom(diff);
				if (form_near_my_bottom != null) {
					new_loc.Y = form_near_my_bottom.Top - cform.Height - cform.parent_form.magnet_space;
					cform.attach_to_my_bottom(form_near_my_bottom);
				}
				
				
				
				// magnet to parent form
				
				my_parent_form parent_near_my_left = cform.parent_magnet_margin_near_my_left(diff);
				if (parent_near_my_left != null) {
					new_loc.X = parent_near_my_left.magnet_margin_left + cform.parent_form.magnet_space;
					cform.attach_to_my_left(parent_near_my_left);
				}
				
				my_parent_form parent_near_my_right = cform.parent_magnet_margin_near_my_right(diff);
				if (parent_near_my_right != null) {
					new_loc.X = parent_near_my_right.magnet_margin_right - cform.Width - cform.parent_form.magnet_space;
					cform.attach_to_my_right(parent_near_my_right);
				}
				
				my_parent_form parent_near_my_top = cform.parent_magnet_margin_near_my_top(diff);
				if (parent_near_my_top != null) {
					new_loc.Y = parent_near_my_top.magnet_margin_top + cform.parent_form.magnet_space;
					cform.attach_to_my_top(parent_near_my_top);
				}
				
				my_parent_form parent_near_my_bottom = cform.parent_magnet_margin_near_my_bottom(diff);
				if (parent_near_my_bottom != null) {
					new_loc.Y = parent_near_my_bottom.magnet_margin_bottom - cform.Height - cform.parent_form.magnet_space;
					cform.attach_to_my_bottom(parent_near_my_bottom);
				}
				
				
				
				// magnet to attached forms edges
				
				if (cform.attached_to_my_top != null  &&  cform.attached_to_my_top is my_form  &&  mymath.is_in_rangei(cform.Left + diff.X, cform.attached_to_my_top.Left - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_top.Left + cform.parent_form.magnet_space_effect_field))
					new_loc.X = cform.attached_to_my_top.Left;
				
				if (cform.attached_to_my_top != null  &&  cform.attached_to_my_top is my_form  &&  mymath.is_in_rangei(cform.Right + diff.X, cform.attached_to_my_top.Right - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_top.Right + cform.parent_form.magnet_space_effect_field))
					new_loc.X = cform.attached_to_my_top.Right - cform.Width;
				
				if (cform.attached_to_my_bottom != null  &&  cform.attached_to_my_bottom is my_form  &&  mymath.is_in_rangei(cform.Left + diff.X, cform.attached_to_my_bottom.Left - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_bottom.Left + cform.parent_form.magnet_space_effect_field))
					new_loc.X = cform.attached_to_my_bottom.Left;
				
				if (cform.attached_to_my_bottom != null  &&  cform.attached_to_my_bottom is my_form  &&  mymath.is_in_rangei(cform.Right + diff.X, cform.attached_to_my_bottom.Right - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_bottom.Right + cform.parent_form.magnet_space_effect_field))
					new_loc.X = cform.attached_to_my_bottom.Right - cform.Width;


				
				if (cform.attached_to_my_left != null  &&  cform.attached_to_my_left is my_form  &&  mymath.is_in_rangei(cform.Top + diff.Y, cform.attached_to_my_left.Top - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_left.Top + cform.parent_form.magnet_space_effect_field))
					new_loc.Y = cform.attached_to_my_left.Top;
				
				if (cform.attached_to_my_left != null  &&  cform.attached_to_my_left is my_form  &&  mymath.is_in_rangei(cform.Bottom + diff.Y, cform.attached_to_my_left.Bottom - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_left.Bottom + cform.parent_form.magnet_space_effect_field))
					new_loc.Y = cform.attached_to_my_left.Bottom - cform.Height;
				
				if (cform.attached_to_my_right != null  &&  cform.attached_to_my_right is my_form  &&  mymath.is_in_rangei(cform.Top + diff.Y, cform.attached_to_my_right.Top - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_right.Top + cform.parent_form.magnet_space_effect_field))
					new_loc.Y = cform.attached_to_my_right.Top;
				
				if (cform.attached_to_my_right != null  &&  cform.attached_to_my_right is my_form  &&  mymath.is_in_rangei(cform.Bottom + diff.Y, cform.attached_to_my_right.Bottom - cform.parent_form.magnet_space_effect_field, cform.attached_to_my_right.Bottom + cform.parent_form.magnet_space_effect_field))
					new_loc.Y = cform.attached_to_my_right.Bottom - cform.Height;
				
				
				this.cform.Location = new_loc;
				
			}
		}
		
		
		void formheader_MouseUp(object sender, MouseEventArgs e)
		{
			this.mouse_is_down = false;
		}
		
		
		bool mouse_is_down = false;
		Point mouse_down_point = new Point(0,0);
		void formheader_MouseDown(object sender, MouseEventArgs e)
		{
			this.mouse_is_down = true;
			this.mouse_down_point = e.Location;
		}
		
		
		
		
	}
	
	
}
