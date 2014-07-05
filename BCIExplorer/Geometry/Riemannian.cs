using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCIExplorer.Util;
using Settings;
using SharpDXForms.Helper;
using ShoNS.Array;

namespace BCIExplorer.Geometry
{
	public static class Riemannian
	{
		public static void CalculateDistances( List<RPoint> covMatrices, out double[][] distances, out int[][] indexToClosest, out double bandwidth )
		{
			int count = covMatrices.Count;
			int dim = covMatrices.First().Point.size0;
			int sampleSize = (int)( Project.FilteredFile.SamplesPerSecond * ClusterOptions.Default.EpochSec );
			int cubicCount = count * count;
			int naiveCount = count * ( count - 1 ) / 2;

			Logger.NewLine();
			string title = "### Clustering ############################";
			Logger.Log( "".PadLeft( title.Length, '#' ) );
			Logger.Log( title );
			Logger.Log( "".PadLeft( title.Length, '#' ) );
			Logger.NewLine();
			Logger.Log( "Matrix dimensions: " + dim + "x" + dim );
			Logger.Log( "Epoch size: " + ClusterOptions.Default.EpochMs + " ms - " + sampleSize + " samples" );
			Logger.Log( "N (Epochs) \t=  " + count );
			Logger.Log( "N^2 \t\t=  " + cubicCount );
			Logger.Log( "N(N-1) / 2 \t=  " + naiveCount );
			Logger.Log( "N^1.5 • LogN \t~= " + Math.Round( Math.Pow( count, 1.5 ) * Math.Log( count, 2 ), MidpointRounding.AwayFromZero ) );
			Logger.Log( "N • LogN \t~= " + Math.Round( count * Math.Log( count, 2 ), MidpointRounding.AwayFromZero ) );
			Logger.NewLine();

			Profiler p = Profiler.StartNew();
			double[][] m = new double[ count ][];
			Parallel.For( 0, count, r =>
			{
				DoubleArray cov = ( covMatrices[ r ] as RPoint ).Point;
				double[] row = new double[ count ];

				Parallel.For( r + 1, count, c =>
				{
					row[ c ] = Math.Sqrt( SquaredDistance( cov, ( covMatrices[ c ] as RPoint ).Point ) );
				} );

				m[ r ] = row;
			} );

			m.LowMemTranspose();
			bandwidth = ClusterOptions.Default.Sigma;
			distances = m;
			p.Stop();
			Logger.Log( "Method:\t" + "Naive" );
			Logger.Log( "Calculations:\t" + naiveCount + " in " + p );
			Logger.Log( "Factor:\t" + 1 );
			Logger.Log( "".PadLeft( title.Length, '#' ) );
			Logger.NewLine();


			indexToClosest = new int[ count ][];
			int[] range = Enumerable.Range( 0, count ).ToArray();
			for( int r = count; --r >= 0; )
			{
				indexToClosest[ r ] = new int[ count ];
				double[] row = new double[ count ];

				range.CopyTo( indexToClosest[ r ], 0 );
				distances[ r ].CopyTo( row, 0 );

				Array.Sort( row, indexToClosest[ r ] );
			}

			//ShoPlotHelper.ImageView( distances );
			//ShoPlotHelper.DGV( distances );
			//ShoPlotHelper.DGV( indexToClosest );
			//ShoPlotHelper.DGV( m.ToArray() );
		}

		///// <summary>
		///// Calculates the distances between all given covariance matrices.
		///// </summary>
		///// <param name="covMatrices">List of covariance matrices to calculate distances between.</param>
		///// <param name="distance">Matrix containing the distances between all given covariance matrices. E.g.: distance[0, 5] will give the distance between covariance matrices 0 and 5.</param>
		///// <param name="indexToClosest">
		///// 2D array mapping indices from the list of covariance matrices to the distance matrix.
		///// The first dimension contains covariance matrix indices from 0 to N.
		///// The second dimension maps other covariance indices to the distance matrix, listed from closest to furthest away.
		///// The first index of the second dimension, maps to the covariance index of the first dimension (it self), since this distance is always 0. 
		///// </param>
		//public static void CalculateDistances_Parallel( List<Matrix<double>> covMatrices, out Matrix<double> distance, out int[][] indexToClosest )
		//{
		//	Profiler p = Profiler.Start( "Distance calculations Fast" );
		//	int count = covMatrices.Count;
		//	//count = 10;
		//	int step = 0;
		//	object o = new object();
		//	Form_Progress f = Form_Progress.Create( SharedForms.main, count * ( count - 1 ) / 2 + count, "Riemannian Distance Calculations", form => form.SetValue( step ) );
		//	Matrix<double> m = new DenseMatrix( count, count );

		//	List<double[ , ]> covData = new List<double[ , ]>( covMatrices.Count );
		//	covMatrices.ForEach( c => covData.Add( c.ToArray() ) );

		//	Parallel.For( 0, count, r =>
		//	{
		//		double[ , ] cov = covData[ r ];

		//		Parallel.For( r + 1, count, c =>
		//		{
		//			m[ r, c ] = SquaredDistance3( cov, covData[ c ] );
		//		} );

		//		lock( o )
		//		{
		//			step += count - r - 1;
		//			Application.DoEvents();
		//		}
		//	} );

		//	f.SetOnTickAction( null );
		//	f.SetValue( step );

		//	m.LowMemTranspose();
		//	distance = m;

		//	indexToClosest = new int[ count ][];
		//	for( int r = 0; r < count; r++ )
		//	{
		//		double[] row = m.Row( r ).ToArray();
		//		indexToClosest[ r ] = Enumerable.Range( 0, count ).ToArray();
		//		Array.Sort( row, indexToClosest[ r ] );
		//		f.Step();
		//	}

		//	p.Stop( true );
		//}

		#region Distance Methods
		public static double SquaredDistance( DoubleArray P )
		{
			EigenValsSym eigenValues = new EigenValsSym( P );
			DoubleArray D = eigenValues.D;

			double sum = 0;
			for( int i = D.Count; --i >= 0; )
			{
				if( D[ i ] > 0 )
				{
					double num = Math.Log( D[ i ] );
					sum += num * num;
				}
			}

			return sum;
		}

		public static double SquaredDistance( DoubleArray P1, DoubleArray P2 )
		{
			try
			{
				EigenValsSym eigenValues = new EigenValsSym( P1, P2 );
				DoubleArray D = eigenValues.D;

				double sum = 0;
				for( int i = D.Count; --i >= 0; )
				{
					if( D[ i ] > 0 )
					{
						double num = Math.Log( D[ i ] );
						sum += num * num;
					}
				}

				return sum;
			}
			catch( Exception ex )
			{
				double det1 = P1.Det();
				double det2 = P2.Det();
				Console.WriteLine( det1 + " " + det2 );
				throw ex;
			}
		}

		public static void Distance( DoubleArray P, out double[] point, out double distance )
		{
			EigenValsSym eigenValues = new EigenValsSym( P );
			DoubleArray D = eigenValues.D;
			point = new double[ D.Count ];

			double sum = 0;
			for( int i = D.Count; --i >= 0; )
			{
				if( D[ i ] > 0 )
				{
					double num = Math.Log( D[ i ] );
					point[ i ] = num;
					sum += num * num;
				}
			}

			distance = Math.Sqrt( sum );
		}

		public static double EuclidianDistance( DoubleArray S )
		{
			EigenValsSym eigenValues = new EigenValsSym( S );
			DoubleArray D = eigenValues.D;

			double sum = 0;
			for( int i = D.Count; --i >= 0; )
			{
				double num = D[ i ];
				sum += num * num;
			}

			return Math.Sqrt( sum );
		}

		public static void EuclidianDistance( DoubleArray S, out double[] point, out double distance )
		{
			EigenSym eigenValues = new EigenSym( S );
			DoubleArray D = eigenValues.D;
			DoubleArray diag = eigenValues.V.Diagonal;
			point = new double[ D.Count ];

			double sum = 0;
			for( int i = D.Count; --i >= 0; )
			{
				double num = D[ i ];
				point[ i ] = num * diag[ i ];
				sum += num * num;
			}

			distance = Math.Sqrt( sum );
		}

		public static void Distance( DoubleArray P, out double[] point, out double distance, out double[] eig )
		{
			EigenValsSym eigenValues = new EigenValsSym( P );
			DoubleArray D = eigenValues.D;
			point = new double[ D.Count ];
			eig = new double[ D.Count ];

			double sum = 0;
			for( int i = D.Count; --i >= 0; )
			{
				if( D[ i ] > 0 )
				{
					eig[ i ] = D[ i ];
					double num = Math.Log( D[ i ] );
					point[ i ] = num;
					sum += num * num;
				}
			}

			distance = Math.Sqrt( sum );
		}
		#endregion

		#region Math Operations
		public static DoubleArray Pow( DoubleArray P, double power )
		{
			return Pow( new EigenSym( P ), power );
		}

		public static DoubleArray Exp( DoubleArray P )
		{
			return Exp( new EigenSym( P ) );
		}

		public static DoubleArray Log( DoubleArray P )
		{
			return Log( new EigenSym( P ) );
		}

		public static DoubleArray Pow( EigenSym eigen, double power )
		{
			DoubleArray D = eigen.D;
			DoubleArray U = eigen.V;
			DoubleArray UT = U.TransposeDeep();
			int len = D.Count;

			DoubleArray diag = new DoubleArray( len, len );
			for( int i = len; --i >= 0; )
			{
				diag[ i, i ] = Math.Pow( D[ i ], power );
			}

			return U * diag * U.T;
		}

		public static DoubleArray Exp( EigenSym eigen )
		{
			DoubleArray D = eigen.D;
			DoubleArray U = eigen.V;
			DoubleArray UT = U.TransposeDeep();
			int len = D.Count;

			DoubleArray diag = new DoubleArray( len, len );
			for( int i = len; --i >= 0; )
			{
				diag[ i, i ] = Math.Exp( D[ i ] );
			}

			return U * diag * U.T;
		}

		public static DoubleArray Log( EigenSym eigen )
		{
			DoubleArray D = eigen.D;
			DoubleArray U = eigen.V;
			DoubleArray UT = U.TransposeDeep();
			int len = D.Count;

			DoubleArray diag = new DoubleArray( len, len );
			for( int i = len; --i >= 0; )
			{
				diag[ i, i ] = Math.Log( D[ i ] );
			}

			return U * diag * U.T;
		}

		public static DoubleArray Log( DoubleArray P, DoubleArray Pi )
		{
			return Log( new EigenSym( P ), Pi );
		}

		public static DoubleArray Exp( DoubleArray P, DoubleArray Si )
		{
			return Exp( new EigenSym( P ), Si );
		}

		public static DoubleArray Log( EigenSym eigenP, DoubleArray Pi )
		{
			DoubleArray PExpHalf = Pow( eigenP, 0.5d );
			DoubleArray PExpHalfNegative = Pow( eigenP, -0.5d );
			return PExpHalf * Log( PExpHalfNegative * Pi * PExpHalfNegative ) * PExpHalf;
		}

		public static DoubleArray Exp( EigenSym eigenP, DoubleArray Si )
		{
			DoubleArray PExpHalf = Pow( eigenP, 0.5d );
			DoubleArray PExpHalfNeg = Pow( eigenP, -0.5d );
			return PExpHalf * Exp( PExpHalfNeg * Si * PExpHalfNeg ) * PExpHalf;
		}
		#endregion

		#region Interpolation
		public static DoubleArray Interpolate( DoubleArray from, DoubleArray to, double amount )
		{
			if( amount <= 0 )
			{
				return from;
			}
			else if( amount >= 1 )
			{
				return to;
			}
			return Exp( from, amount * Log( from, to ) );
		}

		public static DoubleArray Interpolate( EigenSym from, DoubleArray to, double amount )
		{
			return Exp( from, amount * Log( from, to ) );
		}
		#endregion
	}
}