using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EDFReader;
using BCIExplorer.Clustering;
using BCIExplorer.Geometry;
using BCIExplorer.Util;
using Settings;
using WeifenLuo.WinFormsUI.Docking;

namespace BCIExplorer.Forms
{
	public partial class Form_Sliders : DockContent
	{
		private bool mouseDownOnTrack;
		private int oldTrackBarValue;
		private double sigmaSmallStep = 0.05d;
		private double minSigma = 0.01d;
		private double maxSigma = 10;
		private bool canUpdateTrackbar;

		public Form_Sliders()
		{
			InitializeComponent();

			Reset();
			trackBar_sigma.Maximum = (int)( maxSigma / sigmaSmallStep );
			trackBar_sigma.Value = (int)( ClusterOptions.Default.Sigma / sigmaSmallStep );
			label_sigmaValue.Text = GetSigma().ToString();
			SharedForms.control.propertyGrid.SelectedObject = ClusterOptions.Default;
		}

		public Tree Tree { get; private set; }

		private bool IsRangeString( string str )
		{
			if( !str.Contains( '-' ) )
			{
				return false;
			}

			for( int i = str.Length; --i >= 0; )
			{
				char c = str[ i ];
				if( c == '-' )
				{
					continue;
				}

				int num;
				if( !int.TryParse( c.ToString(), out num ) )
				{
					return false;
				}
			}
			return true;
		}

		private void DoSelectChannels()
		{
			try
			{
				string[] chunks = ClusterOptions.Default.Channels.Split( new char[] { ',', ':', ';' }, StringSplitOptions.RemoveEmptyEntries );
				if( chunks.Length == 0 )
				{
					return;
				}

				List<int> ids = new List<int>();

				for( int i = 0; i < chunks.Length; i++ )
				{
					chunks[ i ] = chunks[ i ].Trim().ToLower();
					string chunk = chunks[ i ];

					if( IsRangeString( chunk ) )
					{
						string[] data = chunk.Split( new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries );
						int start = int.Parse( data[ 0 ] );
						int end = int.Parse( data[ 1 ] );

						for( int j = start; j <= end; j++ )
						{
							ids.Add( j );
						}
					}
					else
					{
						int channelId;
						if( int.TryParse( chunk, out channelId ) )
						{
							ids.Add( channelId );
						}
					}
				}

				if( ids.Count > 0 )
				{
					Project.FilteredFile = EEGUtil.SelectChannels( Project.LoadedFile, ids.ToArray() );
				}
				else if( !string.IsNullOrWhiteSpace( chunks[ 0 ] ) )
				{
					Project.FilteredFile = EEGUtil.SelectChannels( Project.LoadedFile, chunks );
				}
			}
			catch
			{
				MessageBox.Show( this, "Invalid channel format:\n\n\"" + ClusterOptions.Default.Channels + "\"", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void DoSelectRange()
		{
			EDFFile file = Project.FilteredFile;
			int sampleSize = file.SampleCount;
			int startSample = StringParse.ValueString<int>( ClusterOptions.Default.SignalRangeFrom, sampleSize, true );
			int endSample = StringParse.ValueString<int>( ClusterOptions.Default.SignalRangeTo, sampleSize, true );
			int length = endSample - startSample;

			if( length != sampleSize )
			{
				EDFDataRecord records = new EDFDataRecord();
				foreach( KeyValuePair<string, double[]> kvp in file.DataRecords )
				{
					double[] data = new double[ length ];
					Array.Copy( kvp.Value, startSample, data, 0, length );
					records.Add( kvp.Key, data );
				}

				file.DataRecords = records;
				file.SampleCount = length;
			}
		}

		private double GetSigma()
		{
			return Math.Max( trackBar_sigma.Value, minSigma ) * sigmaSmallStep;
		}

		private void Reset()
		{
			//Tree = null;
			label_maxDistance.Text = "0";
			label_maxClusterCount.Text = "0";
			label_distanceLevel.Text = "0";
			label_currentClusterCount.Text = "0";
			trackBar_distance.Value = 0;
			trackBar_distance.Maximum = 0;
		}

		public void EnableSigmaSlider( bool enabled )
		{
			trackBar_sigma.Enabled = enabled;
		}

		#region Form Events
		private void trackBar_sigma_Scroll( object sender, EventArgs e )
		{
			label_sigmaValue.Text = GetSigma().ToString();
			ClusterOptions.Default.Sigma = GetSigma();
			if( !mouseDownOnTrack )
			{
				DoSigmaUpdate();
			}
		}

		private void trackBar_distance_ValueChanged( object sender, EventArgs e )
		{
			if( canUpdateTrackbar && Tree != null )
			{
				label_distanceLevel.Text = Tree.DistanceLevels[ trackBar_distance.Value ].ToString();

				//if( !mouseDownOnTrack )
				{
					DoClusterUpdate();
				}
			}
		}

		private void DoSigmaUpdate()
		{
			if( Tree != null )
			{
				Tree.Create( ClusterOptions.Default.Sigma );
				trackBar_distance.Maximum = Tree.DistanceLevels.Length - 1;

				label_maxDistance.Text = Tree.MaxDistance.ToString();
				label_distanceLevel.Text = Tree.DistanceLevels[ Math.Max( trackBar_distance.Value, 0 ) ].ToString();
				label_maxClusterCount.Text = Tree.CovarianceMatrices.Count.ToString();

				if( !mouseDownOnTrack )
				{
					DoClusterUpdate();
				}
			}
		}

		private void DoClusterUpdate()
		{
			if( Tree != null )
			{
				List<Node> clusters = Tree.GetClusters( Tree.DistanceLevels[ trackBar_distance.Value ] );
				label_currentClusterCount.Text = clusters.Count.ToString();

				SharedForms.channelView.CreateClustersColors( Tree, clusters );
			}
		}

		private void trackBar_sigma_MouseDown( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Left )
			{
				mouseDownOnTrack = true;
				oldTrackBarValue = trackBar_sigma.Value;
			}
		}

		private void trackBar_sigma_MouseUp( object sender, MouseEventArgs e )
		{
			if( mouseDownOnTrack )
			{
				mouseDownOnTrack = false;
				if( trackBar_sigma.Value != oldTrackBarValue )
				{
					DoSigmaUpdate();
				}
			}
		}

		private void trackBar_clusters_MouseDown( object sender, MouseEventArgs e )
		{
			if( e.Button == MouseButtons.Left )
			{
				mouseDownOnTrack = true;
				oldTrackBarValue = trackBar_distance.Value;
			}
		}

		private void trackBar_clusters_MouseUp( object sender, MouseEventArgs e )
		{
			//if( mouseDownOnTrack )
			{
				mouseDownOnTrack = false;
				if( trackBar_distance.Value != oldTrackBarValue )
				{
					DoClusterUpdate();
				}
			}
		}

		private void button_runFiltering_Click( object sender, EventArgs e )
		{
			canUpdateTrackbar = false;
			Reset();
			SettingsController.Save();

			if( Project.LoadedFile == null )
			{
				return;
			}

			if( string.IsNullOrWhiteSpace( ClusterOptions.Default.Channels ) )
			{
				MessageBox.Show( this, "No channels selected", "Missing parameter", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}

			DoSelectChannels();
			DoSelectRange();

			if( ClusterOptions.Default.FilterSignal )
			{
				Project.FilteredFile = EEGUtil.FilterData( Project.FilteredFile, ClusterOptions.Default.FilterType, ClusterOptions.Default.FilterLowCutOff, ClusterOptions.Default.FilterHighCutOff, ClusterOptions.Default.FilterOrder );
			}

			Project.OnFileFiltered();
			canUpdateTrackbar = true;
		}

		private void button_run_Click( object sender, EventArgs e )
		{
			if( Project.FilteredFile == null )
			{
				return;
			}
			List<RPoint> covs = EEGUtil.GetCovarianceMatrices( Project.FilteredFile, ClusterOptions.Default.EpochMs, ClusterOptions.Default.WindowsPerEpoch );

			//Task.Run( delegate
			//{
			//double[][] distances;
			//int[][] indexToClosest;
			//EEGCluster.Geometry.Riemannian.CalculateDistances( covs, out distances, out indexToClosest );
			//} );
			Tree = new Tree( covs );
			Tree.Create( ClusterOptions.Default.Sigma );

			trackBar_distance.Maximum = Tree.DistanceLevels.Length - 1;
			label_sigmaValue.Text = Tree.Sigma.ToString();
			label_maxDistance.Text = Tree.MaxDistance.ToString();
			label_distanceLevel.Text = Tree.MaxDistance.ToString();
			label_maxClusterCount.Text = covs.Count.ToString();

			trackBar_distance.Value = Tree.DistanceLevels.Length - 1;
			trackBar_sigma.Value = (int)( Tree.Sigma / sigmaSmallStep );
			//DoClusterUpdate();
		}
		#endregion
	}
}