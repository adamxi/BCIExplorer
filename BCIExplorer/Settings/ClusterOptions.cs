using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using BCIExplorer.TypeDescriptors;
using MathNet.Numerics.Filtering;

namespace Settings
{
	public class ClusterOptions : ISettings
	{
		private static ClusterOptions instance = new ClusterOptions();
		public static ClusterOptions Default
		{
			get { return instance; }
		}

		void ISettings.SetDefault( ISettings o )
		{
			instance = o as ClusterOptions;
		}

		private float filterLowCutOff = 8;
		private float filterHighCutOff = 30;
		private int filterOrder = 4;
		private bool filterSignal = true;
		private string channels = "3-16";
		private float timeScale = 400f;
		private float channelSpacing = 100f;
		private int epochMs;
		private int epochIntervalMs;
		private int windowsPerEpoch;
		private FilterType filterType = FilterType.BandPass;
		private bool showClusterLines = false;
		private float clusterIdScale = 1f;
		private bool showClusterIds = false;
		private bool drawTranscriptions = true;
		private string signalRangeFrom;
		private string signalRangeTo;
		private double covarianceEpsilon;

		public ClusterOptions()
		{
			windowsPerEpoch = 2;
			EpochMs = 1000;
			Sigma = 1d;
			SignalRangeFrom = "0%";
			SignalRangeTo = "100%";
			CovarianceEpsilon = 1E-50;
		}

		[Browsable( false )]
		public double Sigma { get; set; }

		[Category( "Clustering" ), Description( "Size of an epoch (window) in milliseconds." )]
		public int EpochMs
		{
			get { return epochMs; }
			set
			{
				epochMs = Math.Max( value, 1 );
				EpochSec = epochMs * 0.001f;
				WindowsPerEpoch = WindowsPerEpoch; // Hack to refresh calculations done in the WindowsPerEpoch property.
			}
		}

		[Browsable( false ), XmlIgnore]
		public float EpochSec { get; private set; }

		[Category( "Clustering" ), Description( "Sliding windows per epoch. If 1, an epoch starts where the previous ended. If 2, an epoch starts after 50% of the epoch size, etc. Higher numbers yields more sophisticated clustering at the expense of computation time." )]
		public int WindowsPerEpoch
		{
			get { return windowsPerEpoch; }
			set
			{
				windowsPerEpoch = Math.Max( 1, value );
				EpochIntervalMs = EpochMs / windowsPerEpoch;
			}
		}

		[Browsable( false ), XmlIgnore]
		public int EpochIntervalMs
		{
			get { return epochIntervalMs; }
			set
			{
				if( value < 1 )
				{
					value = 1;
				}
				else if( value > epochMs )
				{
					value = epochMs;
				}
				epochIntervalMs = value;
				EpochIntervalSec = value * 0.001f;
			}
		}

		[Browsable( false ), XmlIgnore]
		public float EpochIntervalSec { get; private set; }

		[Category( "Visual" ), Description( "Horizontal (time) scale applied to each sample." )]
		public float TimeScale
		{
			get { return timeScale; }
			set { timeScale = Math.Max( value, 1 ); }
		}

		[Category( "Visual" ), Description( "Spacing between each channel. Set to '0' to stack the channels." )]
		public float ChannelSpacing
		{
			get { return channelSpacing; }
			set { channelSpacing = value; }
		}

		[Category( "Visual" ), Description( "True to show cluster lines." )]
		public bool ShowClusterLines
		{
			get { return showClusterLines; }
			set { showClusterLines = value; }
		}

		[Category( "Visual" ), Description( "If shown, show cluster ids with this scale." )]
		public float ClusterIdScale
		{
			get { return clusterIdScale; }
			set { clusterIdScale = value; }
		}

		[Category( "Visual" ), Description( "True to show cluster ids on the timeline." )]
		public bool ShowClusterIds
		{
			get { return showClusterIds; }
			set { showClusterIds = value; }
		}

		[Category( "Visual" ), Description( "True to show timeline transcriptions." )]
		public bool DrawTranscriptions
		{
			get { return drawTranscriptions; }
			set { drawTranscriptions = value; }
		}

		//[Category( "Visual" ), Description( "" )]
		//public bool RemoveMean
		//{
		//	get { return removeMean; }
		//	set { removeMean = value; }
		//}

		//[Category( "Visual" ), Description( "Amplitude scale of each channel." )]
		//public float AmplitudeScale
		//{
		//	get { return amplitudeScale; }
		//	set { amplitudeScale = value; }
		//}

		[Category( "Filtering" ), Description( "If true, signals will be filtered with selected filtering options." )]
		public bool FilterSignal
		{
			get { return filterSignal; }
			set { filterSignal = value; }
		}

		[Category( "Filtering" ), Description( "Lower frequency edge to attenuate. Lower edge for BandPass and HighPass filters." )]
		public float FilterLowCutOff
		{
			get { return filterLowCutOff; }
			set { filterLowCutOff = Math.Max( value, 0 ); }
		}

		[Category( "Filtering" ), Description( "Upper frequency edge to attenuate. Upper edge for BandPass and LowPass filters." )]
		public float FilterHighCutOff
		{
			get { return filterHighCutOff; }
			set { filterHighCutOff = value; }
		}

		[Category( "Filtering" ), Description( "Order used by a given filter." )]
		public int FilterOrder
		{
			get { return filterOrder; }
			set { filterOrder = Math.Max( value, 2 ); }
		}

		[Category( "Filtering" ), Description( "Channel selection. Format examples:\n\"3-16, 36\" \n\"7, 8\"\n \"AF3, AF4, T7, T8\"." ),
		Editor( typeof( DropdownCheckboxEditor ), typeof( UITypeEditor ) )]
		public string Channels
		{
			get { return channels; }
			set { channels = value; }
		}

		[Category( "Filtering" ), Description( "Inclusive start range to load a signal from." )]
		public string SignalRangeFrom
		{
			get { return signalRangeFrom; }
			set { signalRangeFrom = value; }
		}

		[Category( "Filtering" ), Description( "Inclusive end range to load a signal to." )]
		public string SignalRangeTo
		{
			get { return signalRangeTo; }
			set { signalRangeTo = value; }
		}

		[Category( "Filtering" ), Description( "Type of filter to process signal with." )]
		public FilterType FilterType
		{
			get { return filterType; }
			set { filterType = value; }
		}

		[Category( "Clustering" ), Description( "Epsilon value added to the eigenvalues of all covariance matrices computed from epochs. This ensures their computational stability for Generalized Eigenvalue Decomposition, when matrices have close to zero variance. Should ideally be as low as possible." )]
		public double CovarianceEpsilon
		{
			get { return covarianceEpsilon; }
			set { covarianceEpsilon = value; }
		}
	}
}