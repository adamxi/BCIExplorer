using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCIExplorer.Util;
using SharpDXForms.Helper;

namespace BCIExplorer.Clustering
{
	public static class QuickShift
	{
		public static void CreateTree( Tree tree )
		{
			Profiler p = Profiler.StartNew( "Quick Shift tree creation" );
			double[] densities = tree.Densities;
			double[][] distances = tree.Distances;
			int[][] indexToClosest = tree.IndexToClosest;
			int count = distances.Length;
			Node[] nodes = new Node[ count ];
			HashSet<double> distanceLevels = new HashSet<double>();
			distanceLevels.Add( 0 );
			tree.SetRoot( null );
			int rootIndex = -1;

			for( int i = count; --i >= 0; )
			{
				int index = tree.CovarianceMatrices[ i ].EpochIndex;
				nodes[ i ] = new Node( index, densities[ i ] );
			}

			for( int x = 0; x < count; x++ )
			{
				int bestIndex = -1;
				double densityX = densities[ x ];
				int[] rowIndexToClosest = indexToClosest[ x ];
				double dist = 0;

				for( int c = 1; c < count; c++ ) // Start iteration at 1 since the first element in the rowIndexToClosest array will always point to itself because this distance is 0.
				{
					int n = rowIndexToClosest[ c ];
					if( densities[ n ] > densityX )
					{
						bestIndex = n;
						dist = distances[ x ][ n ];
						distanceLevels.Add( dist );
						break;
					}
				}

				if( bestIndex > -1 )
				{
					Node child = nodes[ x ];
					child.SetDistance( dist );
					nodes[ bestIndex ].AddChild( child );
				}
				else
				{
					if( tree.Root != null )
					{
						Node child = nodes[ x ];
						if( child.Density != tree.Root.Density )
						{
							throw new Exception( "Quick Shift density error. Root densities must be equal" );
						}
						distanceLevels.Add( distances[ x ][ rootIndex ] );
						child.SetDistance( distances[ x ][ rootIndex ] );
						tree.Root.AddChild( child );
					}
					else
					{
						rootIndex = x;
						tree.SetRoot( nodes[ x ] );
					}
				}
			}

			double[] d = distanceLevels.ToArray();
			Array.Sort( d );
			tree.DistanceLevels = d;
			tree.MaxDistance = d.LastOrDefault();
			tree.Root.SetLevel( 0 );
			p.Stop();
			Logger.Log( p.ToString(), Logger.Level.Level_1 );
		}

		public static double[] ComputeDensities( Tree tree )
		{
			Profiler p = Profiler.StartNew( "Density calculations" );
			double[][] distances = tree.Distances;
			int[][] indexToClosest = tree.IndexToClosest;
			double sigma = tree.Sigma;
			int count = distances.Length;
			double invCount = 1d / count;
			double[] densityEstimate = new double[ count ];

			Parallel.For( 0, count, r =>
			{
				double kernalSum = 0;
				int[] rowIndexToClosest = indexToClosest[ r ];

				for( int c = count; --c >= 0; )
				{
					double dist = distances[ r ][ rowIndexToClosest[ c ] ];
					if( dist > -1 )
					{
						kernalSum += NormalDistribution( dist, sigma );
					}
				}
				densityEstimate[ r ] = invCount * kernalSum;
			} );

			p.Stop();
			Logger.Log( p.ToString(), Logger.Level.Level_1 );
			return densityEstimate;
		}

		/// <summary>
		/// http://en.wikipedia.org/wiki/Normal_distribution
		/// </summary>
		/// <param name="dist"></param>
		/// <param name="sigma"></param>
		public static double NormalDistribution( double dist, double sigma )
		{
			return ( 1d / ( sigma * Math.Sqrt( 2 * Math.PI ) ) ) * Math.Exp( -( ( dist * dist ) / ( 2d * ( sigma * sigma ) ) ) );
		}
	}
}