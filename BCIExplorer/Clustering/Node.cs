using System;
using System.Collections.Generic;

namespace BCIExplorer.Clustering
{
	[Serializable]
	public class Node
	{
		public Node( int index, double density )
		{
			this.Index = index;
			this.Density = density;
			this.Childs = new HashSet<Node>();
		}

		#region Properties
		public Node Parent { get; private set; }

		/// <summary>
		/// Node children.
		/// </summary>
		public HashSet<Node> Childs { get; private set; }

		/// <summary>
		/// Index corresponding to a covariance/epoc index of the signal.
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Density of the covariance matrix represented by this node.
		/// </summary>
		public double Density { get; set; }

		/// <summary>
		/// Node level.
		/// </summary>
		public int Level { get; private set; }

		/// <summary>
		/// Distance to parent.
		/// </summary>
		public double Distance { get; private set; }
		#endregion

		#region Methods
		/// <summary>
		/// Adds a child to this node.
		/// </summary>
		/// <param name="child">Child to add.</param>
		public void AddChild( Node child )
		{
			child.Parent = this;
			Childs.Add( child );
		}

		/// <summary>
		/// Removes this node as a child from its parent.
		/// </summary>
		public void Remove()
		{
			if( Parent != null )
			{
				Parent.Childs.Remove( this );
				Parent = null;
			}
		}

		/// <summary>
		/// Sets the distance to parent.
		/// </summary>
		/// <param name="distance">Distance value.</param>
		public void SetDistance( double distance )
		{
			this.Distance = distance;
		}

		/// <summary>
		/// Sets the level of this node and update child nodes according to this.
		/// </summary>
		/// <param name="level">Node level.</param>
		public void SetLevel( int level )
		{
			this.Level = level;

			foreach( Node child in Childs )
			{
				child.SetLevel( level + 1 );
			}
		}

		/// <summary>
		/// Enumerates through all descendants of this node.
		/// </summary>
		public IEnumerable<Node> Descendants()
		{
			foreach( Node child in Childs )
			{
				yield return child;

				foreach( Node subChild in child.Descendants() )
				{
					yield return subChild;
				}
			}
			yield break;
		}

		/// <summary>
		/// Enumerates through all descendants of this node up until a target level.
		/// </summary>
		/// <param name="targetLevel">Target level to reach.</param>
		/// <param name="includeSelf">True to unclude this node in the collection</param>
		public IEnumerable<Node> Descendants( int targetLevel, bool includeSelf = true )
		{
			if( includeSelf && Level <= targetLevel )
			{
				yield return this;
			}

			foreach( Node descendant in Descendants() )
			{
				if( descendant.Level <= targetLevel )
				{
					yield return descendant;
				}
			}
			yield break;
		}

		public IEnumerable<Node> DescendantsAfter( int targetLevel )
		{
			foreach( Node descendant in Descendants() )
			{
				if( descendant.Level > targetLevel )
				{
					yield return descendant;
				}
			}
			yield break;
		}

		/// <summary>
		/// Counts all descendants including self.
		/// </summary>
		public int CountDescendants()
		{
			int count = 1;
			foreach( Node descendant in Descendants() )
			{
				count++;
			}
			return count;
		}

		/// <summary>
		/// Sequentially prints all descendants indices including self, to the console.
		/// </summary>
		public void PrintDescendants()
		{
			Console.WriteLine( Index );
			foreach( Node descendant in Descendants() )
			{
				Console.Write( ", " + descendant.Index );
			}
		}
		#endregion

		public override string ToString()
		{
			return Index + ": L[" + Level + "] C[" + Childs.Count + "] De[" + Density + "] Di[" + Distance + "]";
		}
	}
}