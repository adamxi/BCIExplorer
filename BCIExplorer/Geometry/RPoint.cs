using ShoNS.Array;

namespace BCIExplorer.Geometry
{
	public class RPoint
	{
		private DoubleArray point;
		private int epochIndex;

		public RPoint( DoubleArray point, int epochIndex )
		{
			this.point = point;
			this.epochIndex = epochIndex;
		}

		public DoubleArray Point
		{
			get { return point; }
		}

		public int EpochIndex
		{
			get { return epochIndex; }
		}
	}
}