/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 5/28/2022
 * Time: 2:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using mytools;

namespace myui
{
	
	/// <summary>
	/// Description of layerbox_layer.
	/// </summary>
	public class layerbox_item : Control
	{
		
		
		public Bitmap bitmap = null;
		
		
		private bool is_background = false;
		public bool IsBackground {
			get {return this.is_background;}
			set {
				this.is_background = value;
			}
		}
		
		
		private bool is_selected = false;
		public bool isSelected {
			get { return this.is_selected; }
			set {
				this.is_selected = value;
				if (value == true) {
					this.parent_layerbox.deselect_others_but_this_layer( this );
					this.parent_layerbox.SelectedLayer = this;
				}
				this.Invalidate();
			}
		}
		
		
		public layer_box parent_layerbox = null;
		public CheckBox checkbox = null;
		public PictureBox picbox = null;
		public Label label = null;
		public Point animate_final_pos;
		
		public object bounded_object;
		
		
		public layerbox_item(layer_box parent_layerbox, Bitmap bitmap) {
			
//			this.generate_bitmap(bmp_width, bmp_height, color);
			this.bitmap = bitmap;
			
			this.DoubleBuffered = true;
			this.parent_layerbox = parent_layerbox;
			
			this.Left = 0;
			this.Top = 0; // will be reset by parent_layerbox.rearrange_all_layers()
			this.Width = parent_layerbox.Width;
			this.Height = parent_layerbox.default_layer_height;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
			
			int border = 2;
			
			this.checkbox = new CheckBox();
			this.checkbox.AutoSize = true;
			this.checkbox.Text = "";
			int scrollbar_width = 0;
			if (this.parent_layerbox.VerticalScroll.Visible) scrollbar_width = SystemInformation.VerticalScrollBarWidth;
			this.checkbox.Left = this.Width - this.checkbox.Size.Width - border  -  scrollbar_width;
			this.checkbox.Top = this.Height / 2  -  this.checkbox.Size.Height / 3;
			this.checkbox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.checkbox.Cursor = Cursors.Hand;
			this.checkbox.Checked = true;
			this.checkbox.CheckedChanged += new EventHandler(layerbox_layer_CheckedChanged);
			this.Controls.Add( this.checkbox );
			
			this.picbox = new PictureBox();
			this.picbox.Left = border;
			this.picbox.Top = border;
			this.picbox.Height = this.Height - border * 2;
			this.picbox.Width = this.picbox.Height * 3 / 2; // TODO: ...
			this.picbox.BackColor = Color.LightGray; // TODO: ...
			this.picbox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.picbox.Cursor = Cursors.Default;
			this.picbox.Visible = false;
			this.Controls.Add( this.picbox );
			
			this.label = new Label();
			this.label.AutoSize = true;
			this.label.Text = "Layer-1"; // TODO: ...
			this.label.Left = this.picbox.Right + border;
			this.label.Top = this.Height / 2  -  this.label.Size.Height / 3;
			this.label.ForeColor = Color.WhiteSmoke;
			this.label.Font = new Font("arial", 7f);
			this.label.Visible = false;
			this.Controls.Add( this.label );
			
			this.Cursor = Cursors.Hand;
			
			this.parent_layerbox.Controls.Add ( this );
			
			this.MouseDown += new MouseEventHandler(layerbox_layer_MouseDown);
			this.MouseUp += new MouseEventHandler(layerbox_layer_MouseUp);
			this.MouseMove += new MouseEventHandler(layerbox_layer_MouseMove);
			
		}
		
		
		
		
		
		
		void layerbox_layer_CheckedChanged(object sender, EventArgs e)
		{
			
			if (this.parent_layerbox != null  &&  this.parent_layerbox.events_enabled) {
				
				LayerBoxEventArgs args = new LayerBoxEventArgs();
				args.layerbox_item = this;
				this.parent_layerbox.__invoke_layer_checkbox_changed_event(this, args);
				
			}
			
		}
		
		
		
		
		
		
		public void set_image(Bitmap image) {
			
			this.picbox.Image = image;
			this.Invalidate();
			
		}
		
		
		
		
		
		public void animate_to_pos(Point p, bool start_immediately) {
			
			this.animate_final_pos = new Point(p.X, p.Y);
			parent_layerbox.add_to_animation_list(this, start_immediately);
			
		}
		
		
		
		
		private bool is_mouse_down = false;
		private Point mouse_down_point;
		private Point mouse_down_location;
		private Point desired_pos_after_mouse_up;
		void layerbox_layer_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) {
				this.is_mouse_down = true;
				this.mouse_down_point = Cursor.Position;
				this.mouse_down_location = this.Location;
				this.isSelected = true;
				this.BringToFront();
				this.desired_pos_after_mouse_up = this.Location;
			}
		}
		
		
		void layerbox_layer_MouseMove(object sender, MouseEventArgs e)
		{
			if ( this.is_mouse_down) {
				
				Point diff = mygeo.point_diff(Cursor.Position, this.mouse_down_point);
				this.Location = new Point(this.Location.X, this.mouse_down_location.Y + diff.Y);
				
				layerbox_item near_layer = parent_layerbox.find_near_layer_to_this(this);
				
				if (near_layer != null) {
					
					Point near_layer_loc = near_layer.Location;
					near_layer.animate_to_pos(this.desired_pos_after_mouse_up, true);
					this.desired_pos_after_mouse_up = near_layer_loc;
					this.parent_layerbox.swap_layers_in_list(near_layer, this);
					
				}
				
			}
		}
		
		
		void layerbox_layer_MouseUp(object sender, MouseEventArgs e)
		{
			this.is_mouse_down = false;
			this.animate_to_pos(this.desired_pos_after_mouse_up, true);
		}
		
		
		
		
		
		
		private bool is_mouse_overed = false;
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.is_mouse_overed = true;
			this.Invalidate();
		}
		
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.is_mouse_overed = false;
			this.Invalidate();
		}
		
		
		
		
		
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			Graphics g = e.Graphics;
			int w = this.Width;
			int h = this.Height;
			
			Rectangle rp = new Rectangle(this.picbox.Left, this.picbox.Top, this.picbox.Width, this.picbox.Height);
			g.FillRectangle(this.parent_layerbox.chessboard_texture_brush, rp);
			if (this.picbox.Image != null) {
//				Rectangle r0 = this.picbox.RectangleToClient(this.picbox.ClientRectangle);
				Rectangle r0 = new Rectangle(picbox.Left, picbox.Top, picbox.Width, picbox.Height);
				g.DrawImage(this.picbox.Image, r0);
			}
			mygraph.render_control_to_graphics(g, this.label);
			
			Rectangle r = new Rectangle(0,0  ,  w-1, h-1);
			Color bk_color = Color.FromArgb(64, 10, 80, 120);
			if (this.is_selected) bk_color = Color.FromArgb(64, 10, 20, 120);
			if (this.is_selected  ||  this.is_mouse_overed) {
				SolidBrush sb = new SolidBrush(bk_color);
				g.FillRectangle(sb, r);
				g.DrawRectangle(Pens.DeepSkyBlue, r);
			}
			
		}
		
		
		
	}
	
	
	
}
