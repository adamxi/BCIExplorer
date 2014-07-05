using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDFReader;
using BCIExplorer.Clustering;
using BCIExplorer.Util;
using Settings;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDXForms;
using SharpDXForms.PrimitiveFramework;
using WeifenLuo.WinFormsUI.Docking;
using DXColor = SharpDX.Color;

namespace BCIExplorer.Forms
{
	public partial class Form_ChannelView : DockContent
	{
		public Camera cam;
		private Epoch[] epochs;
		private PRect[] clusterRects;
		private int samplesPerSec;
		private int[] indexToCluster;
		private DXColor[] clusterColors;
		private int epochCount;
		private int windowCount;
		private Vector2 cursorPos;
		private EDFDataRecord records;
		private List<EDFSignal> signals;
		private int epochSampleSize;
		private float windowSampleSize;
		private int sampleCount;
		private int signalCount;
		private float pixelsPerSample;
		private float epochUnitSize;
		private float windowUnitSize;
		private int clusterLineHeight = 2000;
		private int pointedCluster;
		private int oldPointedCluster;
		private int pointedWindow;
		private int startEpoch;
		private int endEpoch;
		private int startWindow;
		private int endWindow;
		private double startSec;
		private double endSec;
		private HashSet<int> windowLoadQueue;
		private bool clusterlock;

		public Form_ChannelView()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			KeyPreview = true;
			cam = new Camera();
			cam.Zoom = 0.05f;
			cam.Position = new Vector2( xnaPanel.Width, 0 );
			xnaPanel.EnableDebugDraw = true;
			MouseWheel += Form_ChannelView_MouseWheel;
			Project.FileFiltered += Project_FileFiltered;
			backgrounColor = DXColor.Gray;
		}

		#region Events
		private void Project_FileFiltered( object sender, EventArgs e )
		{
			EDFFile file = Project.FilteredFile;
			records = file.DataRecords;
			signals = file.Header.Signals;
			windowLoadQueue = new HashSet<int>();

			samplesPerSec = file.SamplesPerSecond;
			epochSampleSize = (int)( samplesPerSec * ClusterOptions.Default.EpochSec );
			windowSampleSize = epochSampleSize / (float)ClusterOptions.Default.WindowsPerEpoch;
			sampleCount = file.SampleCount;
			epochCount = (int)Math.Ceiling( sampleCount / (float)epochSampleSize );
			windowCount = (int)Math.Ceiling( sampleCount / (float)windowSampleSize );
			pixelsPerSample = file.SamplePeriod * ClusterOptions.Default.TimeScale;
			signalCount = signals.Count;
			epochUnitSize = pixelsPerSample * epochSampleSize;
			windowUnitSize = pixelsPerSample * windowSampleSize;

			epochs = new Epoch[ windowCount ];
			clusterRects = new PRect[ windowCount ];
			indexToCluster = Enumerable.Range( 0, windowCount ).ToArray();
			clusterColors = new DXColor[ windowCount ];
			for( int i = 0; i < windowCount; i++ )
			{
				clusterColors[ i ] = DXColor.Black;
			}

			UpdateViewportVariables();
			InvalidateWindow();
		}

		private void Form_ChannelView_MouseWheel( object sender, MouseEventArgs e )
		{
			cam.ZoomToPos( 20 * Sign( e.Delta ), e.Location.ToVector() );
			UpdateViewportVariables();
			InvalidateWindow();
		}

		private void Form_ChannelView_Resize( object sender, System.EventArgs e )
		{
			UpdateViewportVariables();
			InvalidateWindow();
		}

		private void Form_ChannelView_KeyPress( object sender, KeyPressEventArgs e )
		{
			switch( Char.ToLower( e.KeyChar ) )
			{
				case 'l':
					LockCluster( !clusterlock );
					InvalidateWindow();
					break;

				case 'q':
					cam.ZoomToPos( 20, xnaPanel.Size.ToVector() * 0.5f );
					UpdateViewportVariables();
					InvalidateWindow();
					break;

				case 'a':
					cam.ZoomToPos( -20, xnaPanel.Size.ToVector() * 0.5f );
					UpdateViewportVariables();
					InvalidateWindow();
					break;

				case '1':
					backgrounColor = DXColor.Gray;
					break;

				case '2':
					backgrounColor = DXColor.DarkGray;
					break;

				case '3':
					break;
			}
		}

		private DXColor backgrounColor;

		private void xnaPanel_MouseDown( object sender, MouseEventArgs e )
		{
			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.InitMovement();
					break;

				case MouseButtons.Right:
					if( epochs != null )
					{
						List<int> epochIndices = new List<int>();
						for( int i = indexToCluster.Length; --i >= 0; )
						{
							if( pointedCluster == indexToCluster[ i ] )
							{
								epochIndices.Add( i );
							}
						}
						SharedForms.clusterView.SetEpochIndices( epochIndices );
					}
					break;
			}
		}

		private void xnaPanel_MouseMove( object sender, MouseEventArgs e )
		{
			cursorPos = cam.GetCameraPos( e.Location.ToVector() );
			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.DoMovement();
					if( cam.Position.X < 0 )
					{
						cam.Position = new Vector2( 0, cam.Position.X );
					}
					break;
			}

			if( Project.FilteredFile != null )
			{
				UpdateViewportVariables();
				float sample = MathUtil.Clamp( cursorPos.X / pixelsPerSample, 0, sampleCount );
				float time = sample / samplesPerSec;

				xnaPanel.SetDebugObject( "Time (sec)", time.ToString() );
				xnaPanel.SetDebugObject( "Sample", ( (int)sample ).ToString() );

				if( !clusterlock )
				{
					oldPointedCluster = pointedCluster;
					if( pointedWindow >= 0 && pointedWindow < windowCount && cursorPos.Y > -clusterLineHeight && cursorPos.Y < clusterLineHeight )
					{
						xnaPanel.SetDebugObject( "Epoch index", pointedWindow );
						pointedCluster = indexToCluster[ pointedWindow ];
						xnaPanel.SetDebugObject( "Pointed cluster", pointedCluster );
					}
					else
					{
						pointedCluster = -1;
						xnaPanel.SetDebugObject( "Epoch index", "N/A" );
					}
					xnaPanel.SetDebugObject( "Epochs in cluster", indexToCluster.Count( item => item.Equals( pointedCluster ) ).ToString() );
				}

				if( pointedCluster != oldPointedCluster )
				{
					for( int i = startWindow; i < endWindow; i++ )
					{
						if( oldPointedCluster == indexToCluster[ i ] )
						{
							SetEpochColor( i, DXColor.Black );
							if( clusterRects[ i ] != null )
							{
								clusterRects[ i ].Color = clusterColors[ i ];
							}
						}
					}

					for( int i = startWindow; i < endWindow; i++ )
					{
						if( pointedCluster == indexToCluster[ i ] )
						{
							SetEpochColor( i, DXColor.White );
							if( clusterRects[ i ] != null )
							{
								clusterRects[ i ].Color = DXColor.White;
							}
						}
					}
				}
			}
			InvalidateWindow();
		}

		private void xnaPanel_Resize( object sender, EventArgs e )
		{
			cam.UpdateTransformations();
		}

		private void xnaPanel_Render( GraphicsDevice g, SpriteBatch s )
		{
			g.Clear( backgrounColor );
			s.Begin( SpriteSortMode.Deferred, g.BlendStates.AlphaBlend, null, null, null, null, cam.GetTransformation() );
			PrimitiveBatch.Begin( cam.GetOrthographicTransformation() );

			DrawSignals( s );

			PrimitiveBatch.End();
			s.End();
		}
		#endregion

		#region Methods
		public void InvalidateWindow()
		{
			xnaPanel.Invalidate();
		}

		private void LockCluster( bool locked )
		{
			clusterlock = locked;
			if( clusterlock )
			{
				xnaPanel.SetDebugObject( "<< CLUSTER LOCKED >>" );
			}
			else
			{
				xnaPanel.RemoveDebugObject( "<< CLUSTER LOCKED >>" );
			}
		}

		private int Sign( int v )
		{
			return 1 | ( v >> 31 );
		}

		private void UpdateViewportVariables()
		{
			float zoomWidth = ( xnaPanel.Width * 0.5f ) * cam.InverseZoom;
			float windowLeft = cam.Position.X - zoomWidth;
			float windowRight = cam.Position.X + zoomWidth;

			startEpoch = (int)Math.Max( windowLeft / epochUnitSize, 0 );
			endEpoch = (int)Math.Min( ( windowRight / epochUnitSize ) + 1, epochCount );
			startWindow = (int)Math.Max( windowLeft / windowUnitSize - ( ClusterOptions.Default.WindowsPerEpoch - 1 ), 0 );
			endWindow = (int)Math.Min( ( windowRight / windowUnitSize ) + 1, windowCount );
			startSec = ( windowLeft / pixelsPerSample ) / samplesPerSec;
			endSec = ( windowRight / pixelsPerSample ) / samplesPerSec;

			//int pointedEpoch = (int)Math.Floor( cursorPos.X / epochUnitSize );
			pointedWindow = (int)Math.Floor( cursorPos.X / windowUnitSize );
		}

		private void SetEpochColor( int windowIndex, DXColor color )
		{
			for( int i = ClusterOptions.Default.WindowsPerEpoch; --i >= 0; )
			{
				int index = windowIndex + i;
				if( index >= windowCount )
				{
					break;
				}

				Epoch epoch = epochs[ index ];
				if( epoch != null )
				{
					foreach( PShape shape in epoch.Shapes )
					{
						shape.Color = color;
					}
				}
			}
		}

		public void CreateClustersColors( Tree tree, List<Node> clusters )
		{
			double hueStep = 1d / windowCount * 240d;
			clusterColors = new DXColor[ windowCount ];
			indexToCluster = new int[ windowCount ];

			for( int i = clusters.Count; --i >= 0; )
			{
				Node node = clusters[ i ];
				int index = node.Index;
				DXColor color = ColorHelper.ColorFromHSV( index * hueStep, 1f, 255 );
				indexToCluster[ index ] = index;
				clusterColors[ index ] = color;

				foreach( Node descendant in node.Descendants() )
				{
					indexToCluster[ descendant.Index ] = index;
					clusterColors[ descendant.Index ] = color;
				}
			}

			for( int i = startWindow; i < endWindow; i++ )
			{
				if( clusterRects[ i ] != null )
				{
					clusterRects[ i ].Color = clusterColors[ i ];
				}
			}

			LockCluster( false );
			pointedCluster = -1;
			InvalidateWindow();
		}

		//public void CreateClustersColors( Tree tree, List<Node> clusters )
		//{
		//	double maxDensity = double.MinValue;
		//	double minDensity = double.MaxValue;
		//	foreach( Node node in clusters )
		//	{
		//		double density = node.Density;
		//		if( density < minDensity )
		//		{
		//			minDensity = density;
		//		}
		//		if( density > maxDensity )
		//		{
		//			maxDensity = density;
		//		}
		//	}

		//	double densityRange = maxDensity - minDensity;
		//	if( maxDensity == minDensity )
		//	{
		//		densityRange = 1;
		//	}

		//	clusterColors = new XNAColor[ windowCount ];
		//	indexToCluster = new int[ windowCount ];

		//	for( int i = clusters.Count; --i >= 0; )
		//	{
		//		Node node = clusters[ i ];
		//		int index = node.Index;
		//		//XNAColor color = ColorHelper.ColorFromHSV( index * hueStep, 1f, 255 );
		//		double hue = Lerp( 0, 240, ( node.Density - minDensity ) / densityRange );

		//		XNAColor color = ColorHelper.ColorFromHSV( hue, 1f, 255 );
		//		indexToCluster[ index ] = index;
		//		clusterColors[ index ] = color;

		//		foreach( Node descendant in node.Descendants() )
		//		{
		//			indexToCluster[ descendant.Index ] = index;
		//			clusterColors[ descendant.Index ] = color;
		//		}
		//	}

		//	LockCluster( false );
		//	pointedCluster = -1;
		//	InvalidateWindow();
		//}

		private double Lerp( double value1, double value2, double amount )
		{
			return ( value1 + ( ( value2 - value1 ) * amount ) );
		}

		private void dataLoader_DoWork( object sender, System.ComponentModel.DoWorkEventArgs e )
		{
			while( windowLoadQueue.Count > 0 )
			{
				int windowIndex = windowLoadQueue.First();
				windowLoadQueue.Remove( windowIndex );
				CreateWindow( windowIndex );
				CreateClusterBars( windowIndex );
				InvalidateWindow();
			}
		}

		public void LoadWindow( int windowIndex )
		{
			if( windowLoadQueue.Contains( windowIndex ) )
			{
				return;
			}

			windowLoadQueue.Add( windowIndex );
			if( !dataLoader.IsBusy )
			{
				dataLoader.RunWorkerAsync();
			}
		}

		private void CreateWindow( int windowIndex )
		{
			int startSample = (int)( windowIndex * windowSampleSize );
			int endSample = Math.Min( (int)( ( windowIndex + 1 ) * windowSampleSize + 1 ), sampleCount );
			float spacing = ClusterOptions.Default.ChannelSpacing;
			PShape[] shapes = new PShape[ signalCount ];

			for( int s = 0; s < signalCount; s++ )
			{
				double[] sampleData = records[ signals[ s ].IndexNumberWithLabel ];
				PShape shape = new PShape( false );
				shape.Color = DXColor.Black;
				shape.Position = new Vector2( 0, s * spacing );
				shapes[ s ] = shape;

				for( int j = startSample; j < endSample; j++ )
				{
					shape.AddVertex( new Vector2( pixelsPerSample * j, (float)sampleData[ j ] ) );
				}
				shape.InitializeForDrawing();
			}

			epochs[ windowIndex ] = new Epoch( shapes, windowIndex );
		}

		private void CreateClusterBars( int windowIndex )
		{
			float yOff = 100;
			float y = -1500 + ( windowIndex % ClusterOptions.Default.WindowsPerEpoch ) * ( yOff + 50 );
			float xOffset = ( (int)( windowIndex * windowSampleSize ) ) * pixelsPerSample;

			PRect r = new PRect( xOffset, y, epochUnitSize, yOff, true );
			clusterRects[ windowIndex ] = r;

			int clusterIndex = indexToCluster[ windowIndex ];
			if( pointedCluster == clusterIndex )
			{
				r.Color = DXColor.White;
			}
			else
			{
				r.Color = clusterColors[ windowIndex ];
			}

			r.InitializeForDrawing();
		}

		public bool TryGetEpoch( int i, out Epoch epoch )
		{
			i = (int)MathUtil.Clamp( i, 0, epochCount - 1 );
			epoch = epochs[ i ];
			if( epoch == null )
			{
				LoadWindow( i );
				return false;
			}
			return true;
		}

		private void DrawSignals( SpriteBatch s )
		{
			if( epochs != null )
			{
				float spacing = ClusterOptions.Default.ChannelSpacing;
				float strScale = ClusterOptions.Default.ClusterIdScale;
				bool drawClusterLines = ClusterOptions.Default.ShowClusterLines;
				bool drawIds = ClusterOptions.Default.ShowClusterIds;
				int windowsPerEpoc = ClusterOptions.Default.WindowsPerEpoch;
				int lastLineIndex = -1;
				int lastIdIndex = -1;

				for( int i = startWindow; i < endWindow; i++ )
				{
					Epoch epoch = epochs[ i ];
					if( epoch == null )
					{
						LoadWindow( i );
						continue;
					}

					foreach( PShape shape in epoch.Shapes )
					{
						shape.Draw();
					}

					int clusterIndex = indexToCluster[ i ];
					if( drawIds && lastIdIndex != clusterIndex )
					{
						lastIdIndex = clusterIndex;
						string clusterStr = indexToCluster[ epoch.Index ].ToString();
						Vector2 strSize = xnaPanel.DebugFont.MeasureString( clusterStr );
						Vector2 strPos = new Vector2();
						strPos.X = i * windowUnitSize + ( windowUnitSize * 0.5f ) - ( strSize.X * 0.5f * strScale ) * cam.InverseZoom;
						strPos.Y = -1000 - strSize.Y * cam.InverseZoom;
						s.DrawString( xnaPanel.DebugFont, clusterStr, strPos, DXColor.Black, 0f, Vector2.Zero, strScale * cam.InverseZoom, SpriteEffects.None, 0f );
					}

					if( drawClusterLines )
					{
						if( pointedCluster == clusterIndex && i > lastLineIndex )
						{
							float yOffset = signalCount * spacing;
							{
								float x = ( (int)( i * windowSampleSize ) ) * pixelsPerSample;
								PLine line = new PLine( x, -clusterLineHeight, x, yOffset + clusterLineHeight );
								line.Color = DXColor.Black;
								line.Draw();
								lastLineIndex = windowCount;
							}

							int count = 0;
							for( int j = i; j < endWindow; j++ )
							{
								if( pointedCluster == indexToCluster[ j ] )
								{
									count = 0;
								}
								else
								{
									count++;
									if( count == windowsPerEpoc )
									{
										float x = ( (int)( j * windowSampleSize ) ) * pixelsPerSample;
										PLine line = new PLine( x, -clusterLineHeight, x, yOffset + clusterLineHeight );
										line.Color = DXColor.Black;
										line.Draw();
										lastLineIndex = j;
										break;
									}
								}
							}
						}
					}

					//PRect r = clusterRects[ i ];
					//if( pointedCluster == clusterIndex )
					//{
					//	r.Color = DXColor.White;
					//}
					//else
					//{
					//	r.Color = clusterColors[ index ];
					//}
					clusterRects[ i ].Draw();
				}

				if( ClusterOptions.Default.DrawTranscriptions && Project.HasTranscriptions )
				{
					foreach( KeyValuePair<double, string> kvp in Project.Transcriptions.GetRange( startSec, endSec ) )
					{
						Vector2 pos = new Vector2( (float)( kvp.Key * ( samplesPerSec * pixelsPerSample ) ), 1500 );
						s.DrawString( xnaPanel.DebugFont, kvp.Value, pos, DXColor.White, MathUtil.DegreesToRadians( 90 ), Vector2.Zero, strScale * cam.InverseZoom, SpriteEffects.None, 0f );
					}
				}
			}
		}
		#endregion
	}

	public class Epoch
	{
		public Epoch( PShape[] shapes, int index )
		{
			this.Shapes = shapes;
			this.Index = index;
		}

		public Epoch( Epoch epoch )
		{
			int count = epoch.Shapes.Length;
			this.Shapes = new PShape[ count ];
			for( int i = 0; i < count; i++ )
			{
				this.Shapes[ i ] = new PShape( epoch.Shapes[ i ] );
			}
			this.Index = epoch.Index;
		}

		public PShape[] Shapes { get; private set; }
		public int Index { get; private set; }
	}
}