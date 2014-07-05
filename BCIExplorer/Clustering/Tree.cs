using System.Collections.Generic;
using BCIExplorer.Geometry;
using Settings;
using ShoNS.Array;

namespace BCIExplorer.Clustering
{
	public class Tree
	{
		private List<RPoint> covarianceMatrices;
		private double[][] distances;
		private int[][] indexToClosest;
		private double sigma;

		/// <summary>
		/// Creates a tree based on a set of covariance matrices.
		/// </summary>
		/// <param name="covarianceMatrices">Covariance matrices to used.</param>
		public Tree( List<RPoint> covarianceMatrices )
		{
			this.Sigma = -1;
			this.covarianceMatrices = covarianceMatrices;
			this.EpochCount = covarianceMatrices.Count;
		}

		/// <summary>
		/// Covariance matrices the tree was created with.
		/// </summary>
		public List<RPoint> CovarianceMatrices
		{
			get { return covarianceMatrices; }
		}

		/// <summary>
		/// Distance matrix between all covariance matrices.
		/// </summary>
		public double[][] Distances
		{
			get { return distances; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int[][] IndexToClosest
		{
			get { return indexToClosest; }
		}

		/// <summary>
		/// Covariance densities.
		/// </summary>
		public double[] Densities { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public double Sigma
		{
			get { return sigma; }
			set { sigma = value; }
		}

		/// <summary>
		/// Maximum cluster distance in this tree.
		/// </summary>
		public double MaxDistance { get; set; }

		public double[] DistanceLevels { get; set; }

		/// <summary>
		/// Number of epochs/covariance matrices used to create the tree.
		/// </summary>
		public int EpochCount { get; private set; }

		/// <summary>
		/// Root node after clustering.
		/// </summary>
		public Node Root { get; private set; }

		#region Methods
		/// <summary>
		/// Calculates the distances between all covariance matrices using Riemannian Geometry.
		/// NOTE: These calculations are heavy and time-consuming,
		/// </summary>
		public void CalculateDistances()
		{
			double b;
			Riemannian.CalculateDistances( covarianceMatrices, out distances, out indexToClosest, out b );
			//if( ClusterOptions.Default.ApproximationType != BallTreeType.Naive )
			//{
			sigma = b;
			//}
		}

		/// <summary>
		/// Creates the tree structure using QuickShift based on the distances between each covariance matrix.
		/// </summary>
		/// <param name="sigma">Sigma value to create tree with.</param>
		public void Create( double sigma = 1 )
		{
			if( Distances == null )
			{
				CalculateDistances();
			}

			if( this.Sigma != sigma )
			{
				this.Sigma = sigma;
			}
			Densities = QuickShift.ComputeDensities( this );

			QuickShift.CreateTree( this );
		}

		public void SetRoot( Node node )
		{
			this.Root = node;
		}

		public List<Node> GetClusters( double maxDistance )
		{
			Node rootClone = Cloner.DeepClone( Root );
			List<Node> clusters = new List<Node>();
			clusters.Add( rootClone );

			foreach( Node node in rootClone.Descendants() )
			{
				if( node.Distance > maxDistance )
				{
					clusters.Add( node );
				}
			}

			clusters.ForEach( n => n.Remove() );
			return clusters;
		}

		///// <summary>
		///// Finds and returns the max node level in the tree.
		///// </summary>
		//public int MaxLevel()
		//{
		//	int maxLevel = 0;
		//	foreach( Node descendant in Root.Descendants() )
		//	{
		//		if( descendant.Level > maxLevel )
		//		{
		//			maxLevel = descendant.Level;
		//		}
		//	}
		//	return maxLevel;
		//}
		#endregion
	}
}