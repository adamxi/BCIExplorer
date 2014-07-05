using System;
using System.Linq;
using System.Text;
using ShoNS.Array;

namespace BCIExplorer
{
	public static class MatrixEx
	{
		public static double maxMinEigenValue = 100;

		/// <summary>
		/// Returns the Sample Covariance Matrix.
		/// NOTE:
		/// a) Observations must be arranged as columns. If not, transpose the matrix first.
		/// b) Observations must be centered around 0. If not, use RemoveMean() on the matrix first.
		/// </summary>
		/// <remarks>
		/// Based on paper: "Multiclass Brain-Computer Interface Classification by Riemannian Geometry", Barachant, Alexandre and Bonnet, Stéphane and Congedo, Marco and Jutten, Christian
		/// </remarks>
		public static DoubleArray Covariance( this DoubleArray a )
		{
			if( a.size1 == 1 )
			{
				throw new Exception( "Cannot calculate covariance from a row vector." );
			}

			return ( 1d / ( a.size1 - 1 ) ) * ( a * a.T );
		}

		public static DoubleArray StableCovariance( this DoubleArray a, double epsilon )
		{
			if( a.size1 == 1 )
			{
				throw new Exception( "Cannot calculate covariance from a row vector." );
			}

			DoubleArray cov = a * a.T;						// Compute covariance matrix.
			EigenSym eigen = new EigenSym( cov );			// Compute eigenvalue decomposition.
			DoubleArray D = eigen.D;						// Cache D as eigenvalues.
			DoubleArray Q = eigen.V;						// Cache Q as eigenvectors.

			int len = D.Count;
			DoubleArray A = new DoubleArray( len, len );	// Use A as an eigenvalue matrix.
			for( int i = len; --i >= 0; )
			{
				// Fill the diagonal of A with the decomposed eigenvalues. Adding epsilon to each eigenvalue ensures their computational stability.
				A[ i, i ] = D[ i ] + epsilon;
			}

			// Use the eigenvectos and the diagonal matrix to construct an almost identical version of our covariance matrix which is positive definite and computationally stable.
			// Naming conversion can be seen on: http://en.wikipedia.org/wiki/Eigendecomposition_of_a_matrix#Real_symmetric_matrices
			cov = Q * A * Q.T;

			return ( 1d / ( a.size1 - 1 ) ) * cov;			// Return the Sample Covariance Matrix.
		}

		/// <summary>
		/// Removes the Matrix row means.
		/// </summary>
		public static void RemoveMean( this DoubleArray a )
		{
			int columns = a.size1;
			decimal invColumns = 1m / columns;

			for( int r = a.size0; --r >= 0; )
			{
				decimal rowSum = 0m;
				for( int c = columns; --c >= 0; )
				{
					rowSum += (decimal)a[ r, c ];
				}

				double rowMean = (double)( rowSum * invColumns );
				for( int c = columns; --c >= 0; )
				{
					a[ r, c ] -= rowMean;
				}
			}
		}

		public static double[] RemoveMean( this double[] a )
		{
			double[] array = new double[ a.Length ];
			double average = a.Average();

			for( int i = a.Length; --i >= 0; )
			{
				array[ i ] = a[ i ] - average;
			}

			return array;
		}

		public static void LowMemTranspose( this double[][] m )
		{
			for( int r = m.Length; --r >= 0; )
			{
				for( int c = m[ r ].Length; --c >= 0; )
				{
					m[ r ][ c ] = m[ c ][ r ];
				}
			}
		}

		public static void Print( this DoubleArray da, int maxRows = 10, int maxColumns = 10, int padding = 12 )
		{
			string str = string.Concat( ToTypeString( da ), Environment.NewLine, ToString( da, maxRows, maxColumns, padding, "G6" ) );
			Console.WriteLine( str );
		}

		private static string ToTypeString( DoubleArray da )
		{
			return string.Format( "{0} {1}x{2}", da.GetType().Name, da.size0, da.size1 );
		}

		private static string ToString( DoubleArray da, int maxRows, int maxColumns, int padding, string format = null, IFormatProvider provider = null )
		{
			int RowCount = da.size0;
			int ColumnCount = da.size1;

			int rowN = RowCount <= maxRows ? RowCount : maxRows < 3 ? maxRows : maxRows - 1;
			bool rowDots = maxRows < RowCount;
			bool rowLast = rowDots && maxRows > 2;

			int colN = ColumnCount <= maxColumns ? ColumnCount : maxColumns < 3 ? maxColumns : maxColumns - 1;
			bool colDots = maxColumns < ColumnCount;
			bool colLast = colDots && maxColumns > 2;

			const string separator = " ";
			const string dots = "...";
			string pdots = "...".PadLeft( padding );

			if( format == null )
			{
				format = "G8";
			}

			var stringBuilder = new StringBuilder();

			for( var row = 0; row < rowN; row++ )
			{
				if( row > 0 )
				{
					stringBuilder.Append( Environment.NewLine );
				}
				stringBuilder.Append( da[ row, 0 ].ToString( format, provider ).PadLeft( padding ) );
				for( var column = 1; column < colN; column++ )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( da[ row, column ].ToString( format, provider ).PadLeft( padding ) );
				}
				if( colDots )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( dots );
					if( colLast )
					{
						stringBuilder.Append( separator );
						stringBuilder.Append( da[ row, ColumnCount - 1 ].ToString( format, provider ).PadLeft( 12 ) );
					}
				}
			}

			if( rowDots )
			{
				stringBuilder.Append( Environment.NewLine );
				stringBuilder.Append( pdots );
				for( var column = 1; column < colN; column++ )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( pdots );
				}
				if( colDots )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( dots );
					if( colLast )
					{
						stringBuilder.Append( separator );
						stringBuilder.Append( pdots );
					}
				}
			}

			if( rowLast )
			{
				stringBuilder.Append( Environment.NewLine );
				stringBuilder.Append( da[ RowCount - 1, 0 ].ToString( format, provider ).PadLeft( padding ) );
				for( var column = 1; column < colN; column++ )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( da[ RowCount - 1, column ].ToString( format, provider ).PadLeft( padding ) );
				}
				if( colDots )
				{
					stringBuilder.Append( separator );
					stringBuilder.Append( dots );
					if( colLast )
					{
						stringBuilder.Append( separator );
						stringBuilder.Append( da[ RowCount - 1, ColumnCount - 1 ].ToString( format, provider ).PadLeft( padding ) );
					}
				}
			}

			return stringBuilder.ToString();
		}
	}
}