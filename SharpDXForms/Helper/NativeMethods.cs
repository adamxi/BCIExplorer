using System;
using System.Runtime.InteropServices;

namespace SharpDXForms.Helper
{
	public class NativeDrawMethods
	{
		/// <summary>
		/// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
		/// </summary>
		/// <param name="hdcDest">A handle to the destination device context.</param>
		/// <param name="nXDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="nYDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="nWidth">The width, in logical units, of the source and destination rectangles.</param>
		/// <param name="nHeight">The height, in logical units, of the source and the destination rectangles.</param>
		/// <param name="hdcSrc">A handle to the source device context.</param>
		/// <param name="nXSrc">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="nYSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="dwRop">A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
		[DllImport( "GDI32.dll" )]
		public static extern bool BitBlt( IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop );

		/// <summary>
		/// The StretchBlt function copies a bitmap from a source rectangle into a destination rectangle, stretching or compressing the bitmap to fit the dimensions of the destination rectangle, if necessary. The system stretches or compresses the bitmap according to the stretching mode currently set in the destination device context.
		/// </summary>
		/// <param name="hdcDest">A handle to the destination device context.</param>
		/// <param name="nXOriginDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="nYOriginDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
		/// <param name="nWidthDest">The width, in logical units, of the destination rectangle.</param>
		/// <param name="nHeightDest">The height, in logical units, of the destination rectangle.</param>
		/// <param name="hdcSrc">A handle to the source device context.</param>
		/// <param name="nXOriginSrc">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="nYOriginSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
		/// <param name="nWidthSrc">The width, in logical units, of the source rectangle.</param>
		/// <param name="nHeightSrc">The height, in logical units, of the source rectangle.</param>
		/// <param name="dwRop">The raster operation to be performed. Raster operation codes define how the system combines colors in output operations that involve a brush, a source bitmap, and a destination bitmap. See BitBlt for a list of common raster operation codes (ROPs). Note that the CAPTUREBLT ROP generally cannot be used for printing device contexts.</param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
		[DllImport( "GDI32.dll" )]
		public static extern bool StretchBlt( IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop );

		[DllImport( "GDI32.dll" )]
		public static extern IntPtr CreateCompatibleDC( IntPtr hdc );

		[DllImport( "GDI32.dll" )]
		public static extern bool DeleteDC( IntPtr hdc );

		[DllImport( "GDI32.dll" )]
		public static extern bool DeleteObject( IntPtr hObject );

		[DllImport( "GDI32.dll" )]
		public static extern int SelectObject( IntPtr hdc, IntPtr hgdiobj );

		[DllImport( "GDI32.dll" )]
		public static extern int SetStretchBltMode( IntPtr hdc, StretchBltMode stretchMode );

		[DllImport( "gdi32.dll" )]
		public static extern bool SetBrushOrgEx( IntPtr hdc, int nXOrg, int nYOrg, IntPtr lppt );

		public enum StretchBltMode : int
		{
			STRETCH_ANDSCANS = 1,
			STRETCH_ORSCANS = 2,
			STRETCH_DELETESCANS = 3,
			STRETCH_HALFTONE = 4,
		}
	}
}