/*
 * Created by SharpDevelop.
 * User: phielcy
 * Date: 5/6/2022
 * Time: 10:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using System.Windows.Forms;


namespace myll
{
	/// <summary>
	/// Description of mouse_hook.
	/// </summary>
	public static class mouse_hook
	{
		
		// shoja: my added codes:
		
		public class LLMouseEventArgs : EventArgs {
			public IntPtr wParam;
			public MSLLHOOKSTRUCT data;
		}
		
		public delegate void LLMouseEventHandler(object sender, LLMouseEventArgs e);
		
		
		
		
		// shoja url: https://stackoverflow.com/questions/11607133/global-mouse-event-handler
		
		public static event LLMouseEventHandler MouseAction = delegate { };

		public static void start()
		{
			_hookID = SetHook(_proc);
		}
		
		public static void stop()
		{
			UnhookWindowsHookEx(_hookID);
		}

		private static LowLevelMouseProc _proc = HookCallback;
		private static IntPtr _hookID = IntPtr.Zero;

		private static IntPtr SetHook(LowLevelMouseProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
				using (ProcessModule curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(WH_MOUSE_LL, proc,
				                        GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

		private static IntPtr HookCallback(
			int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 &&
			    ((MouseMessages)wParam) == MouseMessages.WM_LBUTTONDOWN ||
			    ((MouseMessages)wParam) == MouseMessages.WM_LBUTTONUP   ||
			    ((MouseMessages)wParam) == MouseMessages.WM_RBUTTONDOWN ||
			    ((MouseMessages)wParam) == MouseMessages.WM_RBUTTONUP   ||
			    ((MouseMessages)wParam) == MouseMessages.WM_MBUTTONDOWN ||
			    ((MouseMessages)wParam) == MouseMessages.WM_MBUTTONUP   ||
			    ((MouseMessages)wParam) == MouseMessages.WM_MOUSEMOVE   ||
			    ((MouseMessages)wParam) == MouseMessages.WM_MOUSEWHEEL
			   )
			{
				MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
				LLMouseEventArgs e = new mouse_hook.LLMouseEventArgs();
				e.wParam = wParam;
				e.data = hookStruct;
				MouseAction(null, e);
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		private const int WH_MOUSE_LL = 14;

		public enum MouseMessages
		{
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205,
			WM_MBUTTONDOWN = 0x0207,
			WM_MBUTTONUP = 0x0208,
			WM_MOUSEMOVE = 0x0200,
			WM_MOUSEWHEEL = 0x020A
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MSLLHOOKSTRUCT
		{
			public POINT pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
		                                              LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
		                                            IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);


	}
	
}
