/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 4/11/2022
 * Time: 10:50 PM
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
	
	public enum close_button_behavior {
		close,
		hide
	}
	
	/// <summary>
	/// Description of myForm.
	/// </summary>
	public partial class my_form : Form
	{
		
		public close_button_behavior close_behavior = close_button_behavior.hide;
		public my_form_header header = null;
		public my_parent_form parent_form = null;
		
		public bool auto_opacity = false;
		public double auto_opacity_min_value = 0.6;
		private Timer timer_opacity = null;
		
		
		public my_form() {
			
			// shoja: just for designer
			this.ShowInTaskbar = false;
			this.header = new my_form_header(this);
			
			this.FormBorderStyle = FormBorderStyle.None; // no borders
			this.DoubleBuffered = true;
			this.AutoScaleMode = AutoScaleMode.None;
			this.MinimumSize = new Size(1,1);
			//this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts
			
		}
		
		
		public my_form(my_parent_form parent_form)
		{
			
			this.parent_form = parent_form;
			this.parent_form.child_forms.Add(this);
			this.Owner = parent_form;
			
			this.ShowInTaskbar = false;
			this.header = new my_form_header(this);
			
			this.FormBorderStyle = FormBorderStyle.None; // no borders
			this.DoubleBuffered = true;
			//this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts
			this.AutoScaleMode = AutoScaleMode.None;
			this.MinimumSize = new Size(1,1);
			
			this.timer_opacity = new Timer();
			this.timer_opacity.Interval = 25; // ms
			this.timer_opacity.Tick += new EventHandler(my_form_Tick);
			
		}
		
		
		
		protected override void OnTextChanged(EventArgs e)
		{
			this.header.set_text(this.Text);
			base.OnTextChanged(e);
		}
		
		
		
		// FOR DROP SHADOW: shoja url: https://stackoverflow.com/questions/16493698/drop-shadow-on-a-borderless-winform
		protected override CreateParams CreateParams {
			get {
				const int CS_DROPSHADOW = 0x20000;
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CS_DROPSHADOW;
				return cp;
			}
		}
		
		
		
		
		
		
//		TODO: write by my own...

//		// shoja from url: https://stackoverflow.com/questions/2575216/how-to-move-and-resize-a-form-without-a-border
//
//		private const int
//			HTLEFT = 10,
//		HTRIGHT = 11,
//		HTTOP = 12,
//		HTTOPLEFT = 13,
//		HTTOPRIGHT = 14,
//		HTBOTTOM = 15,
//		HTBOTTOMLEFT = 16,
//		HTBOTTOMRIGHT = 17;
//
//		const int _ = 10; // you can rename this variable if you like
//
//		Rectangle _Top { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
//		Rectangle _Left { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
//		Rectangle _Bottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
//		Rectangle _Right { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }
//
//		Rectangle TopLeft { get { return new Rectangle(0, 0, _, _); } }
//		Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, _); } }
//		Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - _, _, _); } }
//		Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _); } }
//
//
//		protected override void WndProc(ref Message message)
//		{
//
//			base.WndProc(ref message);
//
//			if (message.Msg == 0x84) // WM_NCHITTEST
//			{
//				var cursor = this.PointToClient(Cursor.Position);
//
//				if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
//				else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
//				else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
//				else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;
//
//				else if (_Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
//				else if (_Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
//				else if (_Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
//				else if (_Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
//			}
//		}
		
		
		
		
		
		
		
		
		
	}
	
	
}
