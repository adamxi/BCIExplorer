using System;
using DXColor = SharpDX.Color;

namespace BCIExplorer.Util
{
	public static class ColorHelper
	{
		private static double inv60 = 1d / 60f;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hue">[0..360]</param>
		/// <param name="saturation">[0..1]</param>
		/// <param name="value">[0..255]</param>
		public static void ColorFromHSV( double hue, float saturation, int value, ref DXColor color )
		{
			double num = hue * inv60;
			int floorNum = (int)num;
			double f = num - floorNum;
			int p = (int)( value * ( 1 - saturation ) );

			switch( floorNum )
			{
				case 0:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						color = DXColor.FromRgba( (uint)( value | t << 8 | p << 16 | 0xFF000000 ) );
						return;
					}

				case 1:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						color = DXColor.FromRgba( (uint)( q | value << 8 | p << 16 | 0xFF000000 ) );
						return;
					}

				case 2:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						color = DXColor.FromRgba( (uint)( p | value << 8 | t << 16 | 0xFF000000 ) );
						return;
					}

				case 3:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						color = DXColor.FromRgba( (uint)( p | q << 8 | value << 16 | 0xFF000000 ) );
						return;
					}

				case 4:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						color = DXColor.FromRgba( (uint)( t | p << 8 | value << 16 | 0xFF000000 ) );
						return;
					}

				default:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						color = DXColor.FromRgba( (uint)( value | p << 8 | q << 16 | 0xFF000000 ) );
						return;
					}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hue">[0..360]</param>
		/// <param name="saturation">[0..1]</param>
		/// <param name="value">[0..255]</param>
		public static DXColor ColorFromHSV( double hue, float saturation, int value )
		{
			double num = hue * inv60;
			int floorNum = (int)num;
			double f = num - floorNum;
			int p = (int)( value * ( 1 - saturation ) );

			switch( floorNum )
			{
				case 0:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						return DXColor.FromRgba( (uint)( value | t << 8 | p << 16 | 0xFF000000 ) );
					}

				case 1:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						return DXColor.FromRgba( (uint)( q | value << 8 | p << 16 | 0xFF000000 ) );
					}

				case 2:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						return DXColor.FromRgba( (uint)( p | value << 8 | t << 16 | 0xFF000000 ) );
					}

				case 3:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						return DXColor.FromRgba( (uint)( p | q << 8 | value << 16 | 0xFF000000 ) );
					}

				case 4:
					{
						int t = (int)( value * ( 1 - ( 1 - f ) * saturation ) );
						return DXColor.FromRgba( (uint)( t | p << 8 | value << 16 | 0xFF000000 ) );
					}

				default:
					{
						int q = (int)( value * ( 1 - f * saturation ) );
						return DXColor.FromRgba( (uint)( value | p << 8 | q << 16 | 0xFF000000 ) );
					}
			}
		}

		public static DXColor ColorFromHSV_Old( float hue, float saturation, int value )
		{
			int hi = Convert.ToInt32( Math.Floor( hue / 60 ) ) % 6;
			double f = hue / 60 - Math.Floor( hue / 60 );

			int v = Convert.ToInt32( value );
			int p = Convert.ToInt32( value * ( 1 - saturation ) );
			int q = Convert.ToInt32( value * ( 1 - f * saturation ) );
			int t = Convert.ToInt32( value * ( 1 - ( 1 - f ) * saturation ) );

			if( hi == 0 )
				return new DXColor( value, t, p );
			else if( hi == 1 )
				return new DXColor( q, value, p );
			else if( hi == 2 )
				return new DXColor( p, value, t );
			else if( hi == 3 )
				return new DXColor( p, q, value );
			else if( hi == 4 )
				return new DXColor( t, p, value );
			else
				return new DXColor( value, p, q );
		}
	}
}