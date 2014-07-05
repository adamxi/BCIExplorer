using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class FormEx
{
	// offset of window style value
	public const int GWL_STYLE = -16;

	// window style constants for scrollbars
	public const int WS_VSCROLL = 0x00200000;
	public const int WS_HSCROLL = 0x00100000;

	[DllImport( "user32.dll", SetLastError = true )]
	public static extern int GetWindowLong( IntPtr hWnd, int nIndex );

	public static ScrollBars GetVisibleScrollbars( Control ctl )
	{
		int wndStyle = GetWindowLong( ctl.Handle, GWL_STYLE );
		bool hsVisible = ( wndStyle & WS_HSCROLL ) != 0;
		bool vsVisible = ( wndStyle & WS_VSCROLL ) != 0;

		if( hsVisible )
		{
			return vsVisible ? ScrollBars.Both : ScrollBars.Horizontal;
		}
		else
		{
			return vsVisible ? ScrollBars.Vertical : ScrollBars.None;
		}
	}

	static public void UIThread( this Form form, MethodInvoker code )
	{
		if( form.InvokeRequired )
		{
			form.Invoke( code );
			return;
		}
		code.Invoke();
	}

	static public void UIThread( this Control control, MethodInvoker code )
	{
		if( control.InvokeRequired )
		{
			control.Invoke( code );
			return;
		}
		code.Invoke();
	}
}