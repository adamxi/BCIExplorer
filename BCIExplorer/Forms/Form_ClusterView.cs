using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDXForms;
using SharpDXForms.PrimitiveFramework;
using WeifenLuo.WinFormsUI.Docking;
using DXColor = SharpDX.Color;

namespace BCIExplorer.Forms
{
	public partial class Form_ClusterView : DockContent
	{
		public Camera cam;
		private Epoch[] epochs;

		public Form_ClusterView()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			cam = new Camera();
			cam.Zoom = 0.05f;

			cam.Position = new Vector2( 0, xnaPanel.Height * cam.InverseZoom * 0.5f );
			MouseWheel += Form_ClusterView_MouseWheel;
		}

		private int Sign( int v )
		{
			return 1 | ( v >> 31 );
		}

		public void InvalidateWindow()
		{
			xnaPanel.Invalidate();
		}

		public void SetEpochIndices( List<int> epochIndices )
		{
			epochs = new Epoch[ epochIndices.Count * ClusterOptions.Default.WindowsPerEpoch ];
			int index = 0;

			Task.Run( delegate
			{
				foreach( int i in epochIndices )
				{
					for( int j = 0; j < ClusterOptions.Default.WindowsPerEpoch; j++ )
					{
						int epochIndex = i + j;
						Epoch epoch;
						if( SharedForms.channelView.TryGetEpoch( epochIndex, out epoch ) )
						{
							Epoch e = new Epoch( epoch );
							foreach( PShape shape in e.Shapes )
							{
								shape.Color = Color.Black;
							}
							epochs[ index++ ] = e;
							InvalidateWindow();
						}
					}
				}
			} );
		}

		void Form_ClusterView_MouseWheel( object sender, MouseEventArgs e )
		{
			cam.ZoomToPos( 20 * Sign( e.Delta ), e.Location.ToVector() );
			InvalidateWindow();
		}

		private void xnaPanel_MouseDown( object sender, MouseEventArgs e )
		{
			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.InitMovement();
					break;
			}
		}

		private void xnaPanel_MouseMove( object sender, MouseEventArgs e )
		{
			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.DoMovement();
					break;
			}
			InvalidateWindow();
		}

		private void xnaPanel_Resize( object sender, EventArgs e )
		{
			cam.UpdateTransformations();
		}

		private void xnaPanel_Render( GraphicsDevice g, SpriteBatch s )
		{
			g.Clear( DXColor.CornflowerBlue );
			s.Begin( SpriteSortMode.Deferred, g.BlendStates.AlphaBlend, null, null, null, null, cam.GetTransformation() );
			PrimitiveBatch.Begin( cam.GetOrthographicTransformation() );

			DrawEpochs( s );

			PrimitiveBatch.End();
			s.End();
		}

		private void DrawEpochs( SpriteBatch s )
		{
			if( epochs != null )
			{
				float channelSpacing = ClusterOptions.Default.ChannelSpacing;
				float epochSpacing = 100;
				float pixelsPerSample = Project.FilteredFile.SamplePeriod * ClusterOptions.Default.TimeScale;
				float epochSampleSize = (int)( Project.FilteredFile.SamplesPerSecond * ClusterOptions.Default.EpochSec );
				float epochUnitSize = pixelsPerSample * epochSampleSize;
				float windowSampleSize = epochSampleSize / (float)ClusterOptions.Default.WindowsPerEpoch;
				float windowUnitSize = pixelsPerSample * windowSampleSize;

				for( int i = 0; i < epochs.Length; i++ )
				{
					Epoch epoch = epochs[ i ];
					if( epoch == null )
					{
						continue;
					}

					float yOffset = 0;
					foreach( PShape shape in epoch.Shapes )
					{
						Vector2 pos = new Vector2();
						pos.Y = yOffset;
						pos.X = -windowUnitSize * epoch.Index + ( windowUnitSize * ( i % ClusterOptions.Default.WindowsPerEpoch ) );
						shape.Position = pos;
						shape.Draw();
						yOffset += channelSpacing;
					}
					yOffset += epochSpacing;
				}
			}
		}
	}
}