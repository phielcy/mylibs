/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/30/2022
 * Time: 9:19 PM
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
	/// Sizablity of my_form.
	/// </summary>
	partial class my_form : Form
	{
		
		public int sizable_margin = 10;
		
		private bool is_sizable = false;
		public bool isSizable {
			get {return this.is_sizable;}
			set {
				this.is_sizable = value;
				if (this.is_sizable)
					this.header.Top = 2;
				else
					this.header.Top = 0;
				this.Refresh();
			}
		}
		
		Rectangle rect_sizable_top;
		Rectangle rect_sizable_right;
		Rectangle rect_sizable_bottom;
		Rectangle rect_sizable_left;
		
		public void calc_size_boxes() {
			
			rect_sizable_top    = new Rectangle(0,0, this.Width, sizable_margin);
			rect_sizable_right  = new Rectangle(this.Width-sizable_margin,0, sizable_margin, this.Height);
			rect_sizable_bottom = new Rectangle(0,this.Height-sizable_margin, this.Width, sizable_margin);
			rect_sizable_left   = new Rectangle(0,sizable_margin, sizable_margin, this.Height);
		}
		
		
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.calc_size_boxes();
		}
		
		
		
		bool mouse_is_in_top_resize_area = false;
		bool mouse_is_in_right_resize_area = false;
		bool mouse_is_in_bottom_resize_area = false;
		bool mouse_is_in_left_resize_area = false;
		
		
		Form should_attached_to_my_left_after_mouse_up   = null;
		Form should_attached_to_my_top_after_mouse_up    = null;
		Form should_attached_to_my_right_after_mouse_up  = null;
		Form should_attached_to_my_bottom_after_mouse_up = null;

		
		
		protected override void OnMouseMove(MouseEventArgs e)
		{
			
			base.OnMouseMove(e);
			
			if (!is_sizable) return;
			
			if (mouse_is_down_for_resize) {
				
				Point diff = mygeo.point_diff(Cursor.Position, resizing_mouse_down_xy);
				Point e_location = e.Location;
				
				
				
				// RIGHT:
				
				if (mouse_is_in_right_resize_area) {
					
					int new_width = this.Width;
					
					bool continue_normal_resizing_right = false;
					
					if (this.attached_to_my_right is my_form) {
						
						continue_normal_resizing_right = true;
						
					} else {
						
						this.should_attached_to_my_right_after_mouse_up = null;
						this.detach_from_my_right();
						
						if (this.attached_to_my_top != null  &&  this.attached_to_my_top is my_form  &&  mymath.is_in_rangei(this.Right + diff.X, this.attached_to_my_top.Right - parent_form.magnet_space_effect_field, this.attached_to_my_top.Right + parent_form.magnet_space_effect_field)) {
							
							new_width = this.attached_to_my_top.Width - (this.Left - this.attached_to_my_top.Left);
							
						} else {
							
							if (this.attached_to_my_bottom != null  &&  this.attached_to_my_bottom is my_form  &&  mymath.is_in_rangei(this.Right + diff.X, this.attached_to_my_bottom.Right - parent_form.magnet_space_effect_field, this.attached_to_my_bottom.Right + parent_form.magnet_space_effect_field)) {
								
								new_width = this.attached_to_my_bottom.Width - (this.Left - this.attached_to_my_bottom.Left);
								
							} else {
								
								my_form form_near_my_right = this.find_form_near_my_right(diff);
								if (form_near_my_right != null) {
									
									new_width = form_near_my_right.Left - this.Left - this.parent_form.magnet_space;
									this.should_attached_to_my_right_after_mouse_up = form_near_my_right;
									
								} else {
									
									my_parent_form edge_near_my_right = this.parent_magnet_margin_near_my_right(diff);
									if (edge_near_my_right != null) {
										
										new_width = parent_form.magnet_margin_right - this.Left - this.parent_form.magnet_space;
										
									} else {
										
										continue_normal_resizing_right = true;
										
									}
								}
								
							}
							
						}
						
					}
					
					
					if (continue_normal_resizing_right) {
						
						new_width += diff.X;
						resizing_mouse_down_xy = Cursor.Position;
						
					}
					
					this.Width = new_width;
					
					if (this.attached_to_my_right != null  &&  this.attached_to_my_right is my_form)
						if (((my_form)this.attached_to_my_right).isSizable)
							((my_form)this.attached_to_my_right).expand_left_to_attach_form(this);
						else
							this.detach_from_my_right();
					
				}
				
				
				
				
				// BOTTOM
				
				if (mouse_is_in_bottom_resize_area) {
					
					int new_height = this.Height;
					
					bool continue_normal_resizing_bottom = false;
					
					if (this.attached_to_my_bottom is my_form) {
						
						continue_normal_resizing_bottom = true;
						
					} else {
						
						this.should_attached_to_my_bottom_after_mouse_up = null;
						this.detach_from_my_bottom();
						
						if (this.attached_to_my_left != null  &&  this.attached_to_my_left is my_form  &&  mymath.is_in_rangei(this.Bottom + diff.Y, this.attached_to_my_left.Bottom - parent_form.magnet_space_effect_field, this.attached_to_my_left.Bottom + parent_form.magnet_space_effect_field)) {
							
							new_height = this.attached_to_my_left.Height - ( this.Top - this.attached_to_my_left.Top );
							
						} else {
							
							if (this.attached_to_my_right != null  &&  this.attached_to_my_right is my_form  &&  mymath.is_in_rangei(this.Bottom + diff.Y, this.attached_to_my_right.Bottom - parent_form.magnet_space_effect_field, this.attached_to_my_right.Bottom + parent_form.magnet_space_effect_field)) {
								
								new_height = this.attached_to_my_right.Height - ( this.Top - this.attached_to_my_right.Top );;
								
							} else {
								
								my_form form_near_my_bottom = this.find_form_near_my_bottom(diff);
								if (form_near_my_bottom != null) {
									
									new_height = form_near_my_bottom.Top - this.Top - this.parent_form.magnet_space;
									this.should_attached_to_my_bottom_after_mouse_up = form_near_my_bottom;
									
								} else {
									
									my_parent_form edge_near_my_bottom = this.parent_magnet_margin_near_my_bottom(diff);
									if (edge_near_my_bottom != null) {
										
										new_height = parent_form.magnet_margin_bottom - this.Top - this.parent_form.magnet_space;
										
									} else {
										
										continue_normal_resizing_bottom = true;
										
									}
									
								}
								
							}
							
						}
						
					}
					
					
					if (continue_normal_resizing_bottom) {
						
						new_height += diff.Y;
						resizing_mouse_down_xy = Cursor.Position;
						
					}
					
					
					this.Height = new_height;
					
					if (this.attached_to_my_bottom != null  &&  this.attached_to_my_bottom is my_form)
						if (((my_form)this.attached_to_my_bottom).isSizable)
							((my_form)this.attached_to_my_bottom).expand_top_to_attach_form(this);
						else
							this.detach_from_my_bottom();
					
				}
				
				
				
				
				// LEFT
				
				if (mouse_is_in_left_resize_area) {
					
					int new_width = this.Width;
					int new_left = this.Left;
					
					bool continue_normal_resizing_left = false;
					
					if (this.attached_to_my_left is my_form) {
						
						continue_normal_resizing_left = true;
						
					} else {
						
						this.should_attached_to_my_left_after_mouse_up = null;
						this.detach_from_my_left();
						
						if (this.attached_to_my_top != null  &&  this.attached_to_my_top is my_form  &&  mymath.is_in_rangei(this.Left + diff.X, this.attached_to_my_top.Left - parent_form.magnet_space_effect_field, this.attached_to_my_top.Left + parent_form.magnet_space_effect_field)) {
							
							new_width = this.Right - this.attached_to_my_top.Left;
							new_left = this.attached_to_my_top.Left;
							
						} else {
							
							if (this.attached_to_my_bottom != null  &&  this.attached_to_my_bottom is my_form  &&  mymath.is_in_rangei(this.Left + diff.X, this.attached_to_my_bottom.Left - parent_form.magnet_space_effect_field, this.attached_to_my_bottom.Left + parent_form.magnet_space_effect_field)) {
								
								new_width = this.Right - this.attached_to_my_bottom.Left;
								new_left = this.attached_to_my_bottom.Left;
								
							} else {
								
								my_form form_near_my_left = this.find_form_near_my_left(diff);
								if (form_near_my_left != null) {
									
									new_width = this.Right - form_near_my_left.Right - this.parent_form.magnet_space;
									new_left = form_near_my_left.Right + this.parent_form.magnet_space;
									this.should_attached_to_my_left_after_mouse_up = form_near_my_left;
									
								} else {
									
									my_parent_form edge_near_my_left = this.parent_magnet_margin_near_my_left(diff);
									if (edge_near_my_left != null) {
										
										new_width = this.Right - parent_form.magnet_margin_left - this.parent_form.magnet_space;
										new_left = parent_form.magnet_margin_left + this.parent_form.magnet_space;
										
									} else {
										
										continue_normal_resizing_left = true;
										
									}
								}
								
							}
							
						}
						
					}
					
					
					if (continue_normal_resizing_left) {
						
						new_width -= diff.X;
						new_left += diff.X;
						resizing_mouse_down_xy = Cursor.Position;
						
					}
					
					this.Width = new_width;
					this.Left = new_left;
					
					if (this.attached_to_my_left != null  &&  this.attached_to_my_left is my_form)
						if (((my_form)this.attached_to_my_left).isSizable)
							((my_form)this.attached_to_my_left).expand_right_to_attach_form(this);
						else
							this.detach_from_my_left();
					
					
				}
				
				
				
				
				// TOP
				
				if (mouse_is_in_top_resize_area) {
					
					int new_height = this.Height;
					int new_top = this.Top;
					
					bool continue_normal_resizing_top = false;
					
					if (this.attached_to_my_top is my_form) {
						
						continue_normal_resizing_top = true;
						
					} else {
						
						this.should_attached_to_my_top_after_mouse_up = null;
						this.detach_from_my_top();
						
						if (this.attached_to_my_left != null  &&  this.attached_to_my_left is my_form  &&  mymath.is_in_rangei(this.Top + diff.Y, this.attached_to_my_left.Top - parent_form.magnet_space_effect_field, this.attached_to_my_left.Top + parent_form.magnet_space_effect_field)) {
							
							new_height = this.Bottom - this.attached_to_my_left.Top;
							new_top = this.attached_to_my_left.Top;
							
						} else {
							
							if (this.attached_to_my_right != null  &&  this.attached_to_my_right is my_form  &&  mymath.is_in_rangei(this.Top + diff.Y, this.attached_to_my_right.Top - parent_form.magnet_space_effect_field, this.attached_to_my_right.Top + parent_form.magnet_space_effect_field)) {
								
								new_height = this.Bottom - this.attached_to_my_right.Top;
								new_top = this.attached_to_my_right.Top;
								
							} else {
								
								my_form form_near_my_top = this.find_form_near_my_top(diff);
								if (form_near_my_top != null) {
									
									new_height = this.Bottom - form_near_my_top.Bottom - this.parent_form.magnet_space;
									new_top = form_near_my_top.Bottom + this.parent_form.magnet_space;
									this.should_attached_to_my_top_after_mouse_up = form_near_my_top;
									
								} else {
									
									my_parent_form edge_near_my_top = this.parent_magnet_margin_near_my_top(diff);
									if (edge_near_my_top != null) {
										
										new_height = this.Bottom - parent_form.magnet_margin_top - this.parent_form.magnet_space;
										new_top = parent_form.magnet_margin_top + parent_form.magnet_space;
										
									} else {
										
										continue_normal_resizing_top = true;
										
									}
									
								}
								
							}
							
						}
						
					}
					
					
					if (continue_normal_resizing_top) {
						
						new_height -= diff.Y;
						new_top += diff.Y;
						resizing_mouse_down_xy = Cursor.Position;
						
					}
					
					this.Height = new_height;
					this.Top = new_top;
					
					if (this.attached_to_my_top != null  &&  this.attached_to_my_top is my_form)
						if (((my_form)this.attached_to_my_top).isSizable)
							((my_form)this.attached_to_my_top).expand_bottom_to_attach_form(this);
						else
							this.detach_from_my_top();
					
					
				}
				
				
				
				
			} else {
				
				
				
				this.Cursor = Cursors.Arrow;
				this.header.Cursor = Cursors.Arrow;
				mouse_is_in_top_resize_area = false;
				mouse_is_in_right_resize_area = false;
				mouse_is_in_bottom_resize_area = false;
				mouse_is_in_left_resize_area = false;
				
				if (rect_sizable_top.Contains(e.Location)) {
					this.Cursor = Cursors.SizeNS;
					mouse_is_in_top_resize_area = true;
				}
				
				if (rect_sizable_bottom.Contains(e.Location)) {
					this.Cursor = Cursors.SizeNS;
					mouse_is_in_bottom_resize_area = true;
				}
				
				if (rect_sizable_left.Contains(e.Location)) {
					this.Cursor = Cursors.SizeWE;
					mouse_is_in_left_resize_area = true;
				}
				
				if (rect_sizable_right.Contains(e.Location)) {
					this.Cursor = Cursors.SizeWE;
					mouse_is_in_right_resize_area = true;
				}
				
				
				if ((mouse_is_in_left_resize_area && mouse_is_in_top_resize_area) || (mouse_is_in_right_resize_area && mouse_is_in_bottom_resize_area))
					this.Cursor = Cursors.SizeNWSE;
				
				if ((mouse_is_in_right_resize_area && mouse_is_in_top_resize_area) || (mouse_is_in_left_resize_area && mouse_is_in_bottom_resize_area))
					this.Cursor = Cursors.SizeNESW;
				
			}
			
		}
		
		
		
		
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Cursor = Cursors.Arrow;
		}
		
		
		
		
		bool mouse_is_down_for_resize = false;
		Point resizing_mouse_down_xy;
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (mouse_is_in_top_resize_area || mouse_is_in_right_resize_area || mouse_is_in_bottom_resize_area || mouse_is_in_left_resize_area) {
				mouse_is_down_for_resize = true;
//				resizing_mouse_down_xy = e.Location;
				resizing_mouse_down_xy = Cursor.Position;
			}
		}
		
		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			mouse_is_down_for_resize = false;
			if (this.should_attached_to_my_bottom_after_mouse_up != null) this.attach_to_my_bottom(this.should_attached_to_my_bottom_after_mouse_up);
			if (this.should_attached_to_my_right_after_mouse_up != null) this.attach_to_my_right(this.should_attached_to_my_right_after_mouse_up);
			if (this.should_attached_to_my_top_after_mouse_up != null) this.attach_to_my_top(this.should_attached_to_my_top_after_mouse_up);
			if (this.should_attached_to_my_left_after_mouse_up != null) this.attach_to_my_left(this.should_attached_to_my_left_after_mouse_up);
		}
		
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
//			base.OnPaint(e);
//			Graphics g = e.Graphics;
//			g.DrawRectangle(Pens.Red, rect_sizable_top);
//			g.DrawRectangle(Pens.Green, rect_sizable_right);
//			g.DrawRectangle(Pens.Blue, rect_sizable_bottom);
//			g.DrawRectangle(Pens.Yellow, rect_sizable_left);
		}
		
		
	}
	
}
