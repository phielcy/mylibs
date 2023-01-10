/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 5/27/2022
 * Time: 8:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using mytools;

namespace myui
{
	
	
	public class LayerBoxEventArgs : EventArgs {
		public layerbox_item layerbox_item;
	}
	
	public class LayerBoxSwapLayersEventArgs : EventArgs {
		public layerbox_item layer1, layer2;
	}
	
	public delegate void LayerBoxEventHandler(object sender, LayerBoxEventArgs e);
	public delegate void LayerBoxSwapEventHandler(object sender, LayerBoxSwapLayersEventArgs e);
	
	
	
	
	/// <summary>
	/// Description of layer_box.
	/// </summary>
	public class layer_box : Panel
	{
		
		
		public TextureBrush chessboard_texture_brush;
		
		
		public List<layerbox_item> items = null;
		public int default_layer_height = 45;
		
		private bool never_remove_background_layer = false;
		public bool NeverRemoveBackgroundLayer {
			get {return this.never_remove_background_layer;}
			set {
				this.never_remove_background_layer = value;
			}
		}
		
		private Timer timer_animation = null;
		private List<layerbox_item> to_be_animated = null;
		private int animation_step = 5;
		
		private layerbox_item _selected_layer = null;
		public layerbox_item SelectedLayer {
			get {return this._selected_layer;}
			set {this._selected_layer = value;
				//Debug.WriteLine("layer_box.cs: selected_layer_index = " + this.SelectedLayerIndex);
			}
		}
		
		public int SelectedLayerIndex {
			get {
				
				if (this._selected_layer == null)
					return -1;
				else
					return this.items.IndexOf(this._selected_layer);
				
			}
		}
		
		
		
		
		
		
		
		
		public event LayerBoxEventHandler     LayerAdded = null;
		public event LayerBoxEventHandler     LayerGoingToRemove = null;
		public event LayerBoxEventHandler     LayerRemoved = null;
		public event LayerBoxSwapEventHandler LayersSwapped = null;
		public event LayerBoxEventHandler     LayerVisibleCheckboxCheckedChanged = null;
		
		
		public bool events_enabled = true;
		
		
		
		
		
		
		
		public layer_box()
		{
			
			this.chessboard_texture_brush = mygraph.make_chesserboard_texture_brush(4);
			this.DoubleBuffered = true;
			this.items = new List<layerbox_item> ( );
			this.AutoScroll = true;
			this.to_be_animated = new List<layerbox_item> ( );
			this.timer_animation = new Timer();
			this.timer_animation.Interval = 1;
			this.timer_animation.Tick += new EventHandler(layer_box_Tick);
			
		}
		
		
		
		
		
		public void add_to_animation_list(layerbox_item l, bool start_immediately) {
			
			this.to_be_animated.Add(l);
			if (start_immediately  &&  !this.timer_animation.Enabled)
				this.timer_animation.Start();
			
		}
		
		void layer_box_Tick(object sender, EventArgs e)
		{
			
			if (this.to_be_animated.Count == 0) {
				
				this.timer_animation.Stop();
				return;
				
			} else {
				
				List<layerbox_item> to_be_removed = new List<layerbox_item> ( );
				
				foreach (layerbox_item l in this.to_be_animated) {
					
					Point diff = mygeo.point_diff(l.animate_final_pos, l.Location);
					int diff_y_sign = +1;
					if (diff.Y != 0) diff_y_sign = diff.Y / mymath.absi(diff.Y);
					l.Location = mygeo.point_add( l.Location,  new Point(0, (diff_y_sign * animation_step)) );
					Point diff2 = mygeo.point_diff(l.Location, l.animate_final_pos);
					if (mymath.absi(diff2.Y) < animation_step) {
						l.Location = l.animate_final_pos;
						to_be_removed.Add(l);
					}
					
				}
				
				foreach (layerbox_item l in to_be_removed)
					this.to_be_animated.Remove(l);
				
			}
			
		}
		
		
		
		
		public layerbox_item add_layer_at(int pos, string title, Bitmap bmp, object bounded_object) {
			
			layerbox_item l = new layerbox_item(this, bmp);
			l.bounded_object = bounded_object; // shoja: must be here, not after this.LayerAdded.Invoke in next two ifs
			l.label.Text = title;
			this.items.Insert(pos, l);
			if (this.items.Count > 1)
				l.Top = this.items[pos + 1].Top;
			l.isSelected = true;
			this.rearrange_all_layers();
			if (this.items.Count == 1)
				l.isSelected = true;
			
			if (this.events_enabled  &&  this.LayerAdded != null) {
				LayerBoxEventArgs args = new LayerBoxEventArgs();
				args.layerbox_item = l;
				this.LayerAdded.Invoke(this, args);
			}
			
			this.events_enabled = true;
			
			return l;
			
		}
		
		
		public void remove_layer(layerbox_item l, bool enforce_to_remove) {
			
			if (this.NeverRemoveBackgroundLayer  &&  l.IsBackground  &&  !enforce_to_remove) {
				// TODO: do something to warn developer or user
				return;
			}
			
			if (this.events_enabled  &&  this.LayerGoingToRemove != null) {
				LayerBoxEventArgs args = new LayerBoxEventArgs();
				args.layerbox_item = l;
				this.LayerGoingToRemove.Invoke(this, args);
			}
			
			if (l == null) return;
			int seleted_layer_index = -1;
			if (l == this.SelectedLayer)
				seleted_layer_index = this.items.IndexOf(l);
			this.Controls.Remove(l);
			this.items.Remove(l);
			
			if (seleted_layer_index >= 0  &&  this.items.Count > 0) {
				if (seleted_layer_index > this.items.Count-1)
					seleted_layer_index = this.items.Count-1;
				this.items[seleted_layer_index].isSelected = true;
				this.rearrange_all_layers();
			}
			else
				this.SelectedLayer = null;
			
			if (this.events_enabled  &&  this.LayerRemoved != null) {
				LayerBoxEventArgs args = new LayerBoxEventArgs();
				args.layerbox_item = l;
				this.LayerRemoved.Invoke(this, args);
			}
			
			this.events_enabled = true;
			
		}
		
		
		
		public void remove_all_layers() {
			
			while (this.items.Count > 0) {
				this.remove_layer(this.items[0], true);
			}
			
		}
		
		
		
		public void swap_layers_in_list(layerbox_item l1, layerbox_item l2) {
			
			int i1 = this.items.IndexOf(l1);
			int i2 = this.items.IndexOf(l2);
			layerbox_item swap_temp = this.items[i1];
			this.items[i1] = this.items[i2];
			this.items[i2] = swap_temp;
			
			if (this.events_enabled  &&  this.LayersSwapped != null) {
				LayerBoxSwapLayersEventArgs args = new LayerBoxSwapLayersEventArgs();
				args.layer1 = l1;
				args.layer2 = l2;
				this.LayersSwapped.Invoke(this, args);
			}
			
			this.events_enabled = true;
			
		}
		
		
		
		public void __invoke_layer_checkbox_changed_event(layerbox_item layer, LayerBoxEventArgs args) {
			
			if ( this.events_enabled  &&  this.LayerVisibleCheckboxCheckedChanged != null )
				this.LayerVisibleCheckboxCheckedChanged.Invoke(layer, args);
			
		}
		
		
		
		public void rearrange_all_layers() {
			
			int top = 0;
			foreach (layerbox_item l in this.items) {
				l.animate_to_pos(new Point(l.Location.X, top), false);
				top += default_layer_height;
			}
			
			this.timer_animation.Start();
			
		}
		
		
		
		
		private bool is_in_deselect_all_process = false;
		public void deselect_others_but_this_layer(layerbox_item layer) {
			
			if (this.is_in_deselect_all_process) return;
			this.is_in_deselect_all_process = true;
			foreach (layerbox_item l in this.items)
				if (l != layer) l.isSelected = false;
			this.is_in_deselect_all_process = false;
			
		}
		
		
		
		public layerbox_item find_near_layer_to_this(layerbox_item l) {
			
			foreach (layerbox_item l2 in this.items) {
				if (!this.to_be_animated.Contains(l2)  &&  l2 != l  &&  mymath.absi(l2.Location.Y-l.Location.Y) < this.default_layer_height / 3)
					return l2;
			}
			
			return null;
			
		}
		
		
		
	}
	
	

	
}
