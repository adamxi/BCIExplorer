using System;
using System.Runtime.InteropServices;

namespace BCIExplorer
{
	public static class ArrayEx
	{
		public static T[] Flatten<T>( this T[ , ] a )
		{
			int len = a.GetLength( 0 ) * a.GetLength( 1 );
			T[] array = new T[ len ];
			Buffer.BlockCopy( a, 0, array, 0, Marshal.SizeOf( typeof( T ) ) * len );

			return array;
		}

		public static T[][] CopyArrayBuiltIn<T>( this T[][] source )
		{
			var len = source.Length;
			var dest = new T[ len ][];

			for( var x = 0; x < len; x++ )
			{
				var inner = source[ x ];
				var ilen = inner.Length;
				var newer = new T[ ilen ];
				Array.Copy( inner, newer, ilen );
				dest[ x ] = newer;
			}

			return dest;
		}

		public static void PrintMat( double[][] a, int decimals = 2 )
		{
			int rowLength = a.Length;
			int colLength = a[ 0 ].Length;

			for( int i = 0; i < rowLength; i++ )
			{
				for( int j = 0; j < colLength; j++ )
				{
					Console.Write( string.Format( "{0," + ( decimals + 3 ) + "} | ", Math.Round( a[ i ][ j ], decimals ) ) );
				}
				Console.Write( Environment.NewLine );
			}
		}
	}
}