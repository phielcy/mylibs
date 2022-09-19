/*
 * Created by SharpDevelop.
 * User: a_shojaeddin
 * Date: 4/21/2022
 * Time: 01:18 ب.ظ
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace myui
{
	/// <summary>
	/// Description of nsButton.
	/// </summary>
	public class ns_button : Button
	{
		public ns_button()
		{
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}
