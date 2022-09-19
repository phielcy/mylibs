/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 6/17/2022
 * Time: 1:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using mytools;

namespace myui
{
	/// <summary>
	/// Description of iconlist_item.
	/// </summary>
	public class iconlist_item : Control
	{
		
		
		
		private bool is_selected = false;
		public bool isSelected {
			get { return this.is_selected; }
			set {
				this.is_selected = value;
				if (value == true) {
					this.parent_iconlist.SelectedItem = this;
					this.parent_iconlist.deselect_others_but_this_layer( this );
				}
				this.Refresh();
			}
		}
		
		
		
		private bool is_mouse_overed = false;
		public bool isMouseOvered {
			get { return this.is_mouse_overed; }
			set {
				this.is_mouse_overed = value;
				if (value == true) {
					this.parent_iconlist.dehighlight_others_but_this_layer( this );
				}
				this.Refresh();
			}
		}
		
		
		
		public icon_list parent_iconlist = null;
		public PictureBox picbox = null;
		public Label label = null;
		public string image_key;
		
		
		public iconlist_item(icon_list parent_iconlist, string image_key, string text)
		{
			
			this.DoubleBuffered = true;
			
			this.parent_iconlist = parent_iconlist;
			this.image_key = image_key;
			
			this.Left = 0;
			this.Top = 0; // will be reset by parent_iconlist.rearrange_all_layers()
			this.Width = this.parent_iconlist.Width;
			this.Height = parent_iconlist.default_layer_height;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;


			
			int border = 0;
			int space = 2;
			
			this.picbox = new PictureBox();
			this.picbox.Image = this.parent_iconlist.imageList.Images[image_key];
			this.picbox.Left = border;
			this.picbox.Top = border;
			this.picbox.Height = this.Height - border * 2;
			this.picbox.Width = this.picbox.Height;
			this.picbox.BackColor = Color.LightGray; // TODO: ...
			this.picbox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.picbox.Cursor = Cursors.Default;
			this.picbox.Visible = false;
			this.Controls.Add( this.picbox );
			
			this.label = new Label();
			this.label.AutoSize = true;
			this.label.Text = text;
			this.label.Left = this.picbox.Right + space;
			this.label.Top = this.Height / 2  -  this.label.Size.Height / 3; // TODO: ...
			this.label.BackColor = Color.Transparent;
			this.label.ForeColor = Color.White;
			this.label.Font = this.parent_iconlist.font;
			this.label.Visible = false;
			this.Controls.Add( this.label );
			
			this.Cursor = Cursors.Hand;
			
			this.parent_iconlist.Controls.Add ( this );
			
			this.MouseDown += new MouseEventHandler(iconlist_item_MouseDown);
			
			
		}

		
		
		void iconlist_item_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				this.isSelected = true;
			}
		}
		
		
		
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.isMouseOvered = true;
			this.Invalidate();
		}
		
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.is_mouse_overed = false;
			this.Invalidate();
//			this.isMouseOvered = false;
		}
		
		
		
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			int w = this.Width;
			int h = this.Height;
			
			Rectangle rp = new Rectangle(this.picbox.Left, this.picbox.Top, this.picbox.Width, this.picbox.Height);
			g.DrawImage(picbox.Image, rp);
			//g.FillRectangle(this.parent_layerbox.chessboard_texture_brush, rp);
			
			//mygraph.render_control_to_graphics(g, this.label);
			Font lf = this.label.Font;
			Font font = new Font(lf.FontFamily, lf.Size, lf.Style);
			g.DrawString( this.label.Text, font, new SolidBrush(this.label.ForeColor), new Rectangle(this.label.Location, this.label.ClientRectangle.Size) );
			
			Rectangle r = new Rectangle(0,0  ,  w-1, h-1);
			Color bk_color = Color.FromArgb(64, 10, 80, 120); // mouse overed (high lighted) back color
			if (this.is_selected) bk_color = Color.FromArgb(64, 10, 20, 120); // seleted back color
			if (this.is_selected  ||  this.is_mouse_overed) {
				SolidBrush sb = new SolidBrush(bk_color);
				g.FillRectangle(sb, r);
				g.DrawRectangle(Pens.DeepSkyBlue, r);
			}
			
		}
		
		
		
	}
	
	
}
