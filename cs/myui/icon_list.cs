/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 6/17/2022
 * Time: 12:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace myui
{
	
	
	
	public class IconListEventArgs : EventArgs {
		public iconlist_item item;
	}
	
	
	public delegate void IconListEventHandler(object sender, IconListEventArgs e);
	
	
	
	
	/// <summary>
	/// Description of icon_list.
	/// </summary>
	public class icon_list : Panel
	{
		
		
		public ImageList imageList {get;set;}
		public int default_layer_height = 16;
		public Font font {get; set;}
		
		
		public List<iconlist_item> Items;
		
		private iconlist_item _selected_item = null;
		public iconlist_item SelectedItem {
			
			get {return this._selected_item;}
			
			set {
				
				this._selected_item = value;
				
				IconListEventArgs args = new IconListEventArgs();
				args.item = this._selected_item;
				this.OnSelectedIndexChanged(args);
				
			}
			
		}
		
		
		
		public int SelectedItemIndex {
			get {
				
				if (this._selected_item == null)
					return -1;
				else
					return this.Items.IndexOf(this._selected_item);
				
			}
			
			set {
				
				if (value >= 0  &&  value < this.Items.Count)
					this.Items[value].isSelected = true;
				
			}
			
		}
		
		
		
		public virtual event IconListEventHandler SelectedIndexChanged = null;
		
		protected virtual void OnSelectedIndexChanged(IconListEventArgs e) {
			
			if (this.SelectedIndexChanged != null)
				this.SelectedIndexChanged(this, e);
			
		}
		
		
		
		public icon_list()
		{
			this.font = new Font("Microsoft Sans Serif", 8.25f);
			this.Items = new List<iconlist_item> ( );
			this.DoubleBuffered = true;
			this.AutoScroll = true;
		}
		
		
		public void rearrange_all_items() {
			
			int top = 0;
			foreach (iconlist_item i in this.Items) {
				i.Top = top;
				top += default_layer_height + 0;
			}
			
		}
		
		
		private bool is_in_deselect_all_process = false;
		public void deselect_others_but_this_layer(iconlist_item item) {
			
			if (this.is_in_deselect_all_process) return;
			this.is_in_deselect_all_process = true;
			foreach (iconlist_item i in this.Items)
				if (i != item) i.isSelected = false;
			this.is_in_deselect_all_process = false;
			
//			// TODO: may be better place to call this event
//			IconListEventArgs args = new IconListEventArgs();
//			args.item = item;
			
		}
		
		
		private bool is_in_dehighlight_all_process = false;
		public void dehighlight_others_but_this_layer(iconlist_item item) {
			
			if (this.is_in_dehighlight_all_process) return;
			this.is_in_dehighlight_all_process = true;
			foreach (iconlist_item i in this.Items)
				if (i != item) i.isMouseOvered = false;
			this.is_in_dehighlight_all_process = false;
			
//			// TODO: may be better place to call this event
//			IconListEventArgs args = new IconListEventArgs();
//			args.item = item;
			
		}
		
		
		
		
		public virtual iconlist_item add_item(string image_key, string text) {
			
			iconlist_item item = new iconlist_item(this, image_key, text);
			this.Items.Add(item);
			this.rearrange_all_items();
			return item;
			
		}
		
		
		public iconlist_item add_item_at(string image_key, string text, int index) {
			
			iconlist_item item = new iconlist_item(this, image_key, text);
			this.Items.Insert(index, item);
			this.rearrange_all_items();
			return item;
			
		}
		
		
		public void remove_item_at(int index) {
			
			iconlist_item itm = this.Items[index];
			this.Controls.Remove(itm);
			this.Items.RemoveAt(index);
			this.rearrange_all_items();
			
		}
		
		
		public void remove_item_range(int start, int count) {
			
			for (int i = start; i < start + count; i++) {
				iconlist_item itm = this.Items[start];
				this.Controls.Remove(itm);
				this.Items.RemoveAt(start);
			}
			
			this.rearrange_all_items();
			
		}
		
		
	}
	
	
}
