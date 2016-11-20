// Downloaded from Visual C# Kicks - http://www.vcskicks.com/
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharpDXForms.Helper
{
	unsafe public class FastBitmap
	{
		private struct PixelData
		{
			public byte blue;
			public byte green;
			public byte red;
			public byte alpha;

			public override string ToString()
			{
				return "(" + alpha.ToString() + ", " + red.ToString() + ", " + green.ToString() + ", " + blue.ToString() + ")";
			}
		}

		private Bitmap workingBitmap = null;
		private BitmapData bitmapData = null;
		private int width = 0;
		private Byte* pBase = null;

		public FastBitmap( Bitmap inputBitmap )
		{
			workingBitmap = inputBitmap;
		}

		public void LockImage()
		{
			Rectangle bounds = new Rectangle( Point.Empty, workingBitmap.Size );

			width = (int)( bounds.Width * sizeof( PixelData ) );
			if( width % 4 != 0 ) width = 4 * ( width / 4 + 1 );

			//Lock Image
			bitmapData = workingBitmap.LockBits( bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb );
			pBase = (Byte*)bitmapData.Scan0.ToPointer();
		}

		private PixelData* pixelData = null;

		public Color GetPixel( int x, int y )
		{
			pixelData = (PixelData*)( pBase + y * width + x * sizeof( PixelData ) );
			return Color.FromArgb( pixelData->alpha, pixelData->red, pixelData->green, pixelData->blue );
		}

		public Color GetPixelNext()
		{
			pixelData++;
			return Color.FromArgb( pixelData->alpha, pixelData->red, pixelData->green, pixelData->blue );
		}

		public void SetPixel( int x, int y, Color color )
		{
			PixelData* data = (PixelData*)( pBase + y * width + x * sizeof( PixelData ) );
			data->alpha = color.A;
			data->red = color.R;
			data->green = color.G;
			data->blue = color.B;
		}

		public void GetColors( ref Color[] buffer, Rectangle r )
		{
			int i = 0;
			int w = r.Width;
			int h = r.Height;

			for( int y = r.X; y < h; y++ )
			{
				for( int x = r.Y; x < w; x++ )
				{
					buffer[ i++ ] = GetPixel( x, y );
				}
			}
		}

		public void UnlockImage()
		{
			workingBitmap.UnlockBits( bitmapData );
			bitmapData = null;
			pBase = null;
		}
	}
}