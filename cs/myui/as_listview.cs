/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 5/29/2022
 * Time: 3:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of as_listview: Always Seleted ListView
	/// </summary>
	public class as_listview : ListView
	{
		
		public as_listview()
		{
			this.HideSelection = false;
		}
		
		
		protected override void DefWndProc(ref Message msg)
		{
			if (msg.Msg != 8)
				base.DefWndProc(ref msg);
		}
		
	}
	
}
