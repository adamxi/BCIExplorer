using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using SharpDXForms.Helper;
using ShoNS.Array;

namespace EEGCluster.Clustering
{
	public class MetricTree
	{
		public class Anchor
		{
			private static readonly double Log2Pi = Math.Log( 2 * Math.PI );
			public ConcurrentDictionary<Anchor, double> cachedDistances;

			public Anchor()
			{
				Points = new List<Anchor>();
			}

			public Anchor( DoubleArray covarianceMatrix )
			{
				cachedDistances = new ConcurrentDictionary<Anchor, double>();
				CovarianceMatrix = covarianceMatrix;
				IsLeaf = true;
			}

			#region Properties
			/// <summary>
			/// 
			/// </summary>
			public DoubleArray CovarianceMatrix { get; set; }

			public double Radius { get; set; }

			public Anchor Pivot { get; set; }

			public Anchor LeftAnchor { get; private set; }

			public Anchor RightAnchor { get; private set; }

			public Anchor Parent { get; private set; }

			public List<Anchor> Points { get; private set; }

			public double DistanceToPivot { get; set; }

			public bool IsLeaf { get; set; }

			public int Index { get; set; }

			public int EpochIndex { get; set; }
			#endregion

			public void SetAnchorPair( Anchor left, Anchor right )
			{
				LeftAnchor = left;
				RightAnchor = right;
				LeftAnchor.Parent = this;
				RightAnchor.Parent = this;
				Points.Add( LeftAnchor );
				Points.Add( RightAnchor );
			}

			public double Distance( Anchor anchor )
			{
				if( anchor == this )
				{
					return 0;
				}

				double dist;
				if( !cachedDistances.TryGetValue( anchor, out dist ) )
				{
					dist = Math.Sqrt( RMath.SquaredDistance( anchor.CovarianceMatrix, CovarianceMatrix ) );
					cachedDistances.TryAdd( anchor, dist );
					anchor.cachedDistances.TryAdd( this, dist );
				}

				return dist;
			}

			public void SortPointsDescending()
			{
				Points.Sort( ( x, y ) => y.DistanceToPivot.CompareTo( x.DistanceToPivot ) );
			}

			public void Add( List<Anchor> points )
			{
				Points = points;
				Pivot = Points.RandomElement();

				Points.ForEach( p => p.DistanceToPivot = p.Distance( Pivot ) );
				SortPointsDescending();

				Radius = Points[ 0 ].DistanceToPivot;
			}

			public Anchor Pop()
			{
				int index = 0;// Points.Count - 1;
				Anchor a = Points[ index ];
				Points.RemoveAt( index );
				return a;
			}

			public IEnumerable<Anchor> Descendants()
			{
				if( IsLeaf )
				{
					yield return this;
				}
				else
				{
					foreach( Anchor point in Points )
					{
						if( point.IsLeaf )
						{
							yield return point;
						}
						else
						{
							foreach( Anchor subPoint in point.Descendants() )
							{
								yield return subPoint;
							}
						}
					}
				}
				yield break;
			}

			public override string ToString()
			{
				string txt = "D: " + DistanceToPivot + " R: " + Radius;
				if( Points != null )
				{
					txt += " C: " + Points.Count;
				}

				return txt;
			}

			public bool Overlap( Anchor anchor )
			{
				double pivotDistance = Pivot.Distance( anchor.Pivot );
				double anchorDistance = pivotDistance - Radius - anchor.Radius;
				return anchorDistance <= 0;
			}

			public double GaussianDensity( double distance, double variance )
			{
				return Math.Exp( -0.5 * ( CovarianceMatrix.Count * ( Log2Pi + Math.Log( variance ) ) + distance * distance / variance ) );
			}

			public bool HasMarkBelow { get; set; }
			public List<Anchor> MarkedNodes { get; set; }

			/// <summary>
			/// Travers path from node to root (or as far as necessary) and sets the 'HasMarkBelow' to true.
			/// </summary>
			public void SetMarkedBelow()
			{
				if( !HasMarkBelow )
				{
					HasMarkBelow = true;
					if( Parent != null )
					{
						Parent.SetMarkedBelow();
					}
				}
			}

			// Adds the kernel node to the set of marked nodes for this data node in the MPT.
			// Also makes sure that the 'HasMarkBelow' is true for every nodes on the path from here to the root.
			public void AddMarked( Anchor kernelNode )
			{
				if( MarkedNodes == null )
				{
					MarkedNodes = new List<Anchor>();
					if( Parent != null )
					{
						Parent.SetMarkedBelow();
					}
				}
				MarkedNodes.Add( kernelNode );
			}

			public void GetLeaves( List<Anchor> leaves )
			{
				if( IsLeaf )
				{
					leaves.Add( this );
					return;
				}

				if( LeftAnchor != null )
				{
					LeftAnchor.GetLeaves( leaves );
				}
				if( RightAnchor != null )
				{
					RightAnchor.GetLeaves( leaves );
				}
			}

			public void CollectDistances( double[][] distances )
			{
				if( LeftAnchor != null )
				{
					LeftAnchor.CollectDistances( distances );
				}
				if( RightAnchor != null )
				{
					RightAnchor.CollectDistances( distances );
				}

				if( MarkedNodes != null )
				{
					List<Anchor> leaves = new List<Anchor>();
					GetLeaves( leaves );
					foreach( Anchor marked in MarkedNodes )
					{
						int count = 0;
						List<Anchor> markedLeaves = new List<Anchor>();
						marked.GetLeaves( markedLeaves );

						double distance = Pivot.Distance( marked.Pivot );

						foreach( Anchor leaf in leaves )
						{
							foreach( Anchor markedLeaf in markedLeaves )
							{
								foreach( Anchor item in leaf.Points )
								{
									foreach( Anchor itemMarked in markedLeaf.Points )
									{
										++count;
										distances[ item.Index ][ itemMarked.Index ] = distance;
									}
								}
							}
						}
					}
				}
			}
		}

		public class NodePair : PriorityQueueNode
		{
			public NodePair( Anchor dataNode, Anchor kernelNode )
			{
				this.DataNode = dataNode;
				this.KernelNode = kernelNode;
			}

			public Anchor DataNode { get; private set; }
			public Anchor KernelNode { get; private set; }
		}

		public MetricTree()
		{
		}

		public double[][] ComputeDistances( List<DoubleArray> covarianceMatrices, int targetBlocks )
		{
			int count = covarianceMatrices.Count;
			targetBlocks = Math.Min( targetBlocks, count * count );

			bandwidth = 0.5d; //kernelTree.AutoBandwidth( data );
			variance = bandwidth * bandwidth;
			kernelPart = covarianceMatrices[ 0 ].Count * ( Math.Log( 2 * Math.PI ) + Math.Log( variance ) );
			expansionCandidates = new HeapPriorityQueue<NodePair>( 10 * count );

			Anchor kernelRoot = BuildTree( covarianceMatrices );
			Anchor dataRoot = kernelRoot;
			CreatePartitionBlocks( dataRoot, kernelRoot );

			double[][] distances = new double[ count ][];
			for( int i = count; --i >= 0; )
			{
				distances[ i ] = new double[ count ];
			}

			dataRoot.CollectDistances( distances );
			return distances;
		}

		#region Dual-Tree
		protected double bandwidth;
		protected double variance;
		protected double kernelPart;
		private HeapPriorityQueue<NodePair> expansionCandidates;

		private void CreatePartitionBlocks( Anchor dataNode, Anchor kernelNode )
		{
			if( !dataNode.Overlap( kernelNode ) )
			{
				dataNode.AddMarked( kernelNode );
				AddExpansionCandidate( dataNode, kernelNode );
			}
			else if( dataNode.IsLeaf && kernelNode.IsLeaf )
			{
				dataNode.AddMarked( kernelNode );
			}
			else
			{
				if( dataNode.IsLeaf )
				{
					CreatePartitionBlocks( dataNode, kernelNode.LeftAnchor );
					CreatePartitionBlocks( dataNode, kernelNode.RightAnchor );
				}
				else if( kernelNode.IsLeaf )
				{
					CreatePartitionBlocks( dataNode.LeftAnchor, kernelNode );
					CreatePartitionBlocks( dataNode.RightAnchor, kernelNode );
				}
				else if( dataNode.Radius > kernelNode.Radius )
				{
					CreatePartitionBlocks( dataNode.LeftAnchor, kernelNode );
					CreatePartitionBlocks( dataNode.RightAnchor, kernelNode );
				}
				else
				{
					CreatePartitionBlocks( dataNode, kernelNode.LeftAnchor );
					CreatePartitionBlocks( dataNode, kernelNode.RightAnchor );
				}
			}
		}

		/// <summary>
		/// Adds to the priority queue of expansion candidates
		/// </summary>
		private void AddExpansionCandidate( Anchor dataNode, Anchor kernelNode )
		{
			if( !dataNode.IsLeaf || !kernelNode.IsLeaf )
			{
				if( expansionCandidates.MaxSize == expansionCandidates.Count )
				{
					ResizePQ();
				}

				expansionCandidates.Enqueue( new NodePair( dataNode, kernelNode ), CandidateScore( dataNode, kernelNode ) );
			}
		}

		private double CandidateScore( Anchor dataNode, Anchor kernelNode )
		{
			double score;
			double pivotDistance = dataNode.Pivot.Distance( kernelNode.Pivot );
			double minDistance = pivotDistance - dataNode.Radius - kernelNode.Radius;
			double maxDistance = pivotDistance + dataNode.Radius + kernelNode.Radius;

			if( minDistance < 0.0 )
			{
				score = Double.MaxValue;
			}
			else
			{
				double maxKernel = dataNode.Pivot.GaussianDensity( minDistance, variance );
				double minKernel = dataNode.Pivot.GaussianDensity( maxDistance, variance );
				score = maxKernel - minKernel;
			}

			return score;
		}

		private void ResizePQ()
		{
			HeapPriorityQueue<NodePair> tmp = new HeapPriorityQueue<NodePair>( expansionCandidates.MaxSize * 10 );

			foreach( NodePair candidate in expansionCandidates )
			{
				tmp.Enqueue( candidate, candidate.Priority );
			}

			expansionCandidates = tmp;
		}
		#endregion

		#region Construction
		public Anchor BuildTree( List<DoubleArray> covarianceMatrices )
		{
			List<Anchor> anchors = new List<Anchor>();
			for( int i = 0; i < covarianceMatrices.Count; i++ )
			{
				Anchor anchor = new Anchor( covarianceMatrices[ i ] );
				anchor.Index = i;
				anchors.Add( anchor );
			}

			Anchor seedAnchor = new Anchor();
			seedAnchor.Add( anchors );
			seedAnchor = BuildAnchors( seedAnchor );

			return seedAnchor;
		}

		private Anchor BuildAnchors( Anchor seedAnchor )
		{
			if( seedAnchor.Points.Count <= 1 )
			{
				return seedAnchor;
			}

			List<Anchor> anchors = new List<Anchor>();
			anchors.Add( seedAnchor );
			int R = Math.Max( 2, (int)Math.Sqrt( seedAnchor.Points.Count ) );

			while( anchors.Count < R )
			{
				anchors.Add( NextAnchor( anchors ) );
			}

			for( int i = 0; i < anchors.Count; i++ )
			{
				Anchor anchor = anchors[ i ];
				if( anchor.Points.Count == 0 )
				{
					anchor.Radius = anchor.DistanceToPivot;
				}
				else
				{
					anchor.Radius = anchor.Points[ 0 ].DistanceToPivot;
				}
				anchor = BuildAnchors( anchor );
			}

			return Agglomerate( anchors );
		}

		private Anchor NextAnchor( List<Anchor> anchors )
		{
			Anchor A = new Anchor();
			A.Pivot = MaxAnchorRadius( anchors ).Pop();
			A.Pivot.DistanceToPivot = 0;
			A.Points.Add( A.Pivot );

			foreach( Anchor anchor in anchors )
			{
				double threshold = anchor.Pivot.Distance( A.Pivot ) * 0.5d;

				for( int i = 0; i < anchor.Points.Count; i++ )
				{
					Anchor point = anchor.Points[ i ];

					double distanceToCurrent = point.DistanceToPivot; // point.Distance( anchor.Pivot );
					if( distanceToCurrent <= threshold )
					{
						break;
					}

					double distanceToNew = point.Distance( A.Pivot );
					if( point.DistanceToPivot != point.Distance( anchor.Pivot ) )
					{
						"".ToArray();
					}

					if( distanceToNew < distanceToCurrent )
					{
						if( point == anchor.Pivot )
						{
							"".ToArray();
						}
						anchor.Points.Remove( point );
						A.Points.Add( point );
						point.DistanceToPivot = distanceToNew;
						i--;
					}
				}
			}

			if( A.Points.Count == 0 )
			{
				"".ToArray();
			}

			A.SortPointsDescending();
			return A;
		}

		private Anchor Agglomerate( List<Anchor> anchors )
		{
			while( anchors.Count > 1 )
			{
				Anchor A;
				Anchor B;
				Anchor C = new Anchor();

				SmallestPair( anchors, out A, out B );
				C.SetAnchorPair( A, B );
				C.Pivot = GetPivot( A, B );
				C.Radius = GetRadius( C, A, B );

				anchors.Remove( A );
				anchors.Remove( B );
				anchors.Add( C );
			}

			return anchors[ 0 ];
		}

		private Anchor MaxAnchorRadius( List<Anchor> anchors )
		{
			double maxRadius = -1;
			Anchor a = null;

			foreach( Anchor anchor in anchors )
			{
				if( anchor.Radius > maxRadius )
				{
					maxRadius = anchor.Radius;
					a = anchor;
				}
			}

			if( maxRadius == -1 )
			{
				"".ToArray();
			}

			return a;
		}

		private void SmallestPair( List<Anchor> anchors, out Anchor A, out Anchor B )
		{
			int len = anchors.Count;
			int indexA = -1;
			int indexB = -1;
			double minRadius = double.MaxValue;

			for( int i = 0; i < len - 1; i++ )
			{
				Anchor a = anchors[ i ];

				for( int j = i + 1; j < len; j++ )
				{
					Anchor b = anchors[ j ];

					double distance = a.Pivot.Distance( b.Pivot );
					double radA = a.Radius;
					double radB = b.Radius;
					double radius = -1;

					if( radA + distance <= radB )		// A inside B
					{
						radius = radB;
					}
					else if( radB + distance <= radA )	// B inside A
					{
						radius = radA;
					}
					else								// overlap and at least one of A and B have smaller radius than the distance between them
					{
						radius = distance + Math.Max( radA, radB );
					}

					if( radius < minRadius )
					{
						minRadius = radius;
						indexA = i;
						indexB = j;
					}
				}
			}

			A = anchors[ indexA ];
			B = anchors[ indexB ];
		}

		private Anchor GetPivot( Anchor A, Anchor B )
		{
			Anchor pivot = null;
			double minDistance = double.MaxValue;

			foreach( Anchor point in EnumeratePair( A, B ) )
			{
				double distance = Math.Max(
					A.Radius + A.Pivot.Distance( point ),
					B.Radius + B.Pivot.Distance( point ) );

				if( distance < minDistance )
				{
					minDistance = distance;
					pivot = point;
				}
			}

			return pivot;
		}

		private double GetRadius( Anchor C, Anchor A, Anchor B )
		{
			double maxRadius = -1;
			int count = 0;

			foreach( Anchor point in EnumeratePair( A, B ) )
			{
				double radius = C.Pivot.Distance( point );
				count++;
				if( radius > maxRadius )
				{
					maxRadius = radius;
				}
			}

			if( maxRadius == -1 )
			{
				"".ToArray();
			}

			return maxRadius;
		}

		public IEnumerable<Anchor> EnumeratePair( Anchor A, Anchor B )
		{
			foreach( Anchor point in A.Descendants() )
			{
				yield return point;
			}
			foreach( Anchor point in B.Descendants() )
			{
				yield return point;
			}
			yield break;
		}
		#endregion
	}
}