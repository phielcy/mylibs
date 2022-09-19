/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 6/19/2022
 * Time: 9:55 AM
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
	/// Description of undo_box.
	/// </summary>
	public class undo_box : icon_list
	{
		
		
		
		Font activated_item_font, deactivated_item_font;
		
		
		
		public undo_box()
		{
			
			this.BackColor = System.Drawing.Color.DimGray;
			this.Cursor = Cursors.Hand;
			
			this.activated_item_font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
			this.deactivated_item_font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Italic);
			
		}
		
		
		
		private int selected_action_index = -1;
		
		protected override void OnSelectedIndexChanged(IconListEventArgs e)
		{
			
			base.OnSelectedIndexChanged(e);
			
			if (this.SelectedItemIndex < 0  ||  this.SelectedItemIndex > this.Items.Count-1) return;
			
			if (this.SelectedItemIndex > this.selected_action_index) {
				int start = this.selected_action_index;
				if (start < 0) start = 0;
				for (int i = start; i <= this.SelectedItemIndex; i++)
					this.make_an_item_normal_and_activated(i);
			}
			
			if (this.SelectedItemIndex < this.selected_action_index) {
				for (int i = this.selected_action_index; i > this.SelectedItemIndex; i--)
					this.make_an_item_italic_and_deactivated(i);
			}
			
			this.selected_action_index = this.SelectedItemIndex;
			
		}
		
		
		
		
		public override iconlist_item add_item(string image_key, string text) {
			
			int index = this.SelectedItemIndex;
			if (index != -1  &&  index < this.Items.Count-1) {
				int start = index + 1;
				int count = this.Items.Count - index - 1;
				this.remove_item_range(start, count);
			}
			
			return base.add_item(image_key, text);
			
		}
		
		
		
		
		
		#region decoration toolkit
		
		private string key_to_text(string key) {
			
			string[] splited = key.Split('_');
			string text = "";
			foreach (string word in splited) {
				if (text != "") text += " ";
				text += mystring.capitalize_first_letter(word);
			}
			
			return text;
			
		}
		
		
		public void make_an_item_normal_and_activated(int index) {
			
			if (index < 0  ||  index >= this.Items.Count) return;
			iconlist_item i = this.Items[index];
			i.BackColor = Color.DimGray;
			i.ForeColor = Color.White;
			i.label.Font = activated_item_font;
			
		}
		
		
		public void make_an_item_italic_and_deactivated(int index) {
			if (index < 0  ||  index >= this.Items.Count) return;
			iconlist_item i = this.Items[index];
			i.BackColor = Color.Gray;
			i.ForeColor = Color.Black;
			i.label.Font = deactivated_item_font;
		}
		
		#endregion
		
		
		
		
		
		
		#region main actions
		
		
		public void add_action(string key) {
			
			int index = this.SelectedItemIndex;
			if (index != -1/*  &&  index < this.Items.Count-1*/) {
				int start = index + 1;
				int count = this.Items.Count - index - 1;
				int end = this.Items.Count;
				for (int i = start; i < end; i++)
					this.Controls.Remove(this.Items[i]);
				this.Items.RemoveRange(start, count);
			}
			
			iconlist_item item = this.add_item(key, this.key_to_text(key));
			item.BackColor = Color.DimGray;
			item.ForeColor = Color.White;
			item.isSelected = true;
			
		}
		
		
		public void undo() {
			this.SelectedItemIndex--;
		}
		
		
		public void redo() {
			this.SelectedItemIndex++;
		}
		
		
		public void skip_to_index(int index) {
			
			if (index >= 0  &&  index < this.Items.Count)
				this.Items[index].isSelected = true;
			
		}
		
		
		#endregion
		
		
		
		
		
	}
	
	
	
}
