/*
 * Modified version of the EDF project at https://edf.codeplex.com/.
*/

using System;
using System.Text;

namespace EDFReader
{
	[Serializable]
	public class EDFSignal
	{
		private StringBuilder _strSignal;
		private int _NumberOfSamplesPerDataRecord;

		public EDFSignal()
		{
			_strSignal = new StringBuilder( string.Empty );
		}

		public int IndexNumber { get; set; }
		public string IndexNumberWithLabel { get; set; }
		public string Label { get; set; }
		public string LabelType { get; set; }
		public string LabelSpecification { get; set; }
		public string TransducerType { get; set; } // equal to the lower and upper bounds
		public string PhysicalDimension { get; set; }
		public string PhysicalDimensionPrefix { get; set; }
		public string PhysicalDimensionBasic { get; set; }
		public float PhysicalMinimum { get; set; }
		public float PhysicalMaximum { get; set; }
		public float DigitalMinimum { get; set; }
		public float DigitalMaximum { get; set; }
		public string Prefiltering { get; set; }

		public int NumberOfSamplesPerDataRecord
		{
			get
			{
				if( _NumberOfSamplesPerDataRecord > 0 )
				{
					return _NumberOfSamplesPerDataRecord;
				}
				else
				{
					throw new InvalidOperationException( "Must provide the NumberOfSamplesPerDataRecord before accessing this Property" );
				}
			}
			set
			{
				if( value > 0 )
				{
					_NumberOfSamplesPerDataRecord = value;
				}
				else
				{
					throw new ArgumentException( "NumberOfSamplesPerDataRecord must be set to greater than 0" );
				}
			}
		}

		/// <summary>
		/// I don't understand the name of this parameter, yet. 
		/// It is used in getting the value out of the 2-byte integer, and was called "sense" in the C sample code I learned the format from.
		/// http://en.wikipedia.org/wiki/Gain 
		/// </summary>
		public float AmplifierGain { get; set; }

		/// <summary>
		/// This is used in getting the value of the sample out of the DataRecord.
		/// </summary>
		public float Offset { get; set; }

		public float SamplePeriodWithinDataRecord { get; set; }

		public override string ToString()
		{
			return IndexNumberWithLabel;
		}
	}
}