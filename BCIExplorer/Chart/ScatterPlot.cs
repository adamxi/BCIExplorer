using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDXForms;
using SharpDXForms.PrimitiveFramework;
using WeifenLuo.WinFormsUI.Docking;
using DXColor = SharpDX.Color;

namespace BCIExplorer.Chart
{
	public partial class ScatterPlot : DockContent
	{
		private static List<ScatterPlot> plots = new List<ScatterPlot>();

		public static void CloseAllPlots()
		{
			for( int i = 0; i < plots.Count; i++ )
			{
				Form plot = plots[ i ];
				plot.UIThread( delegate
				{
					plot.Close();
					plot.Dispose();
				} );
			}
			plots.Clear();
		}

		private Camera cam;
		private Vector2 cursorPos;
		private List<Primitive> primitives;
		private List<Primitive> drawings;
		private List<string> labels;
		private List<Vector2> points;
		private List<DXColor> pointColors;
		private float leftMargin = 50;
		private float bottomMargin = 50;
		private Primitive selectedPrimitive;
		private Color defaultPointColor;
		private Color highlightColor;
		private ActionMode mode;
		private Primitive currentDrawingPrimitive;
		private bool measure;
		private Vector2 mouseDownPos;

		public ScatterPlot( string title = null )
		{
			KeyPreview = true;
			InitializeComponent();
			plots.Add( this );

			if( title == null )
			{
				title = "Scatter plot " + plots.Count.ToString();
			}
			this.Text = title;

			primitives = new List<Primitive>();
			drawings = new List<Primitive>();
			labels = new List<string>();
			points = new List<Vector2>();
			pointColors = new List<DXColor>();
			cam = new Camera();
			cam.Zoom = 1f;
			cam.Position = new Vector2( 0, 0 );
			defaultPointColor = DXColor.Red;
			highlightColor = DXColor.Magenta;
			mode = ActionMode.DrawLine;
			measure = true;

			xnaPanel.EnableDebugDraw = true;
			xnaPanel.FPSCounter.DrawFPS = false;
			MouseWheel += ScatterPlot_MouseWheel;
		}

		public void Draw( double[][] data )
		{
			for( int i = 0; i < data.Length; i++ )
			{
				AddPoint( data[ i ] );
			}
			Show();
		}

		public void AddPoint( double[] point, string label = null )
		{
			AddPoint( point[ 0 ], point[ 1 ], defaultPointColor, label );
		}

		public void AddPoint( double[] point, DXColor color )
		{
			AddPoint( point[ 0 ], point[ 1 ], color, null );
		}

		public void AddPoint( double[] point, DXColor color, string label = null )
		{
			AddPoint( point[ 0 ], point[ 1 ], color, label );
		}

		public void AddPoint( double x, double y )
		{
			AddPoint( x, y, defaultPointColor, null );
		}

		public void AddPoint( double x, double y, string label = null )
		{
			AddPoint( x, y, defaultPointColor, label );
		}

		public void AddPoint( double x, double y, DXColor color, string label = null )
		{
			float scale = (float)numericUpDown_scale.Value;
			Vector2 pos = new Vector2( (float)x, (float)y );
			PCircle p = new PCircle( pos * scale, 2f, checkBox_filled.Checked );
			p.Color = color;

			primitives.Add( p );
			labels.Add( label );
			points.Add( pos );
			pointColors.Add( color );
		}

		private int Sign( int v )
		{
			return 1 | ( v >> 31 );
		}

		public void SearchPoints( string label )
		{
			if( selectedPrimitive != null )
			{
				selectedPrimitive.Color = pointColors[ primitives.IndexOf( selectedPrimitive ) ];
				selectedPrimitive = null;
			}

			for( int i = 0; i < primitives.Count; i++ )
			{
				string pLabel = labels[ i ];
				if( pLabel != null && pLabel.ToLower().Equals( label.ToLower() ) )
				{
					SelectPoint( i );
					break;
				}
			}
		}

		private void SelectPoint( Primitive p )
		{
			SelectPoint( primitives.IndexOf( p ) );
		}

		public void SelectPoint( int epochIndex )
		{
			if( selectedPrimitive != null )
			{
				selectedPrimitive.Color = pointColors[ primitives.IndexOf( selectedPrimitive ) ];
			}
			selectedPrimitive = primitives[ epochIndex ];
			selectedPrimitive.Color = highlightColor;
			InvalidateWindow();
		}

		public void InvalidateWindow()
		{
			xnaPanel.Invalidate();
		}

		private void ScatterPlot_MouseWheel( object sender, MouseEventArgs e )
		{
			cam.ZoomToPos( 20 * Sign( e.Delta ), e.Location.ToVector() );
			InvalidateWindow();
		}

		private void ScatterPlot_FormClosed( object sender, FormClosedEventArgs e )
		{
			plots.Remove( this );
		}

		private void textBox1_TextChanged( object sender, EventArgs e )
		{
			SearchPoints( ( sender as TextBox ).Text );
		}

		private void button_goTo_Click( object sender, EventArgs e )
		{
			if( selectedPrimitive != null )
			{
				cam.CenterToPosition( selectedPrimitive.Position );
				InvalidateWindow();
			}
		}

		private void checkBox_toggleGrid_CheckedChanged( object sender, EventArgs e )
		{
			InvalidateWindow();
		}

		private void checkBox_labels_CheckedChanged( object sender, EventArgs e )
		{
			InvalidateWindow();
		}

		private void checkBox_filled_CheckedChanged( object sender, EventArgs e )
		{
			foreach( Primitive p in primitives )
			{
				p.Filled = checkBox_filled.Checked;
			}
			InvalidateWindow();
		}

		private void button_clear_Click( object sender, EventArgs e )
		{
			drawings.Clear();
			InvalidateWindow();
		}

		private void numericUpDown_scale_ValueChanged( object sender, EventArgs e )
		{
			float scale = (float)numericUpDown_scale.Value;
			for( int i = 0; i < primitives.Count; i++ )
			{
				Primitive p = primitives[ i ];
				p.Position = points[ i ] * scale;
			}
			InvalidateWindow();
		}

		private void ScatterPlot_Resize( object sender, System.EventArgs e )
		{
			InvalidateWindow();
		}

		private void ScatterPlot_KeyPress( object sender, KeyPressEventArgs e )
		{
			switch( e.KeyChar )
			{
				case '1':
					measure = !measure;
					break;

				case '2':
					mode = ActionMode.DrawLine;
					break;

				case '3':
					mode = ActionMode.DrawCircle;
					break;

				case 'r':
					foreach( Primitive p in drawings )
					{
						if( p.Intersects( cursorPos ) )
						{
							drawings.Remove( p );
							InvalidateWindow();
							break;
						}
					}
					break;
			}
		}

		private void xnaPanel_MouseDown( object sender, MouseEventArgs e )
		{
			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.InitMovement();
					break;

				case MouseButtons.Right:
					if( currentDrawingPrimitive == null )
					{
						mouseDownPos = cam.GetCameraPos( e.Location.ToVector() );
						foreach( Primitive p in primitives )
						{
							if( p.Intersects( mouseDownPos ) )
							{
								mouseDownPos = p.Position;
							}
						}

						switch( mode )
						{
							case ActionMode.DrawLine:
								currentDrawingPrimitive = new PLine( mouseDownPos, mouseDownPos );
								break;

							case ActionMode.DrawCircle:
								currentDrawingPrimitive = new PCircle( mouseDownPos, 2.002f, 1.001f );
								break;
						}

						if( currentDrawingPrimitive != null )
						{
							currentDrawingPrimitive.Color = measure ? DXColor.Green : DXColor.Blue;
							drawings.Add( currentDrawingPrimitive );
						}
					}
					break;
			}
		}

		private void xnaPanel_MouseUp( object sender, MouseEventArgs e )
		{
			if( measure )
			{
				drawings.Remove( currentDrawingPrimitive );
			}
			xnaPanel.RemoveDebugObject( "Distance" );
			xnaPanel.RemoveDebugObject( "Radius" );
			currentDrawingPrimitive = null;
		}

		private void xnaPanel_MouseMove( object sender, MouseEventArgs e )
		{
			cursorPos = cam.GetCameraPos( e.Location.ToVector() );

			switch( e.Button )
			{
				case MouseButtons.Left:
					cam.DoMovement();
					break;

				case MouseButtons.Right:
					if( currentDrawingPrimitive != null )
					{
						foreach( Primitive p in primitives )
						{
							if( p.Intersects( cursorPos ) )
							{
								cursorPos = p.Position;
							}
						}
						float mouseDist = Vector2.Distance( cursorPos, mouseDownPos );
						float scaleDist = mouseDist / (float)numericUpDown_scale.Value;

						switch( mode )
						{
							case ActionMode.DrawLine:
								PLine line = currentDrawingPrimitive as PLine;
								line.SetEnd( cursorPos );
								xnaPanel.SetDebugObject( "Distance", scaleDist.ToString() );
								break;

							case ActionMode.DrawCircle:
								PCircle circle = currentDrawingPrimitive as PCircle;
								if( scaleDist > circle.Thickness * 2 / (float)numericUpDown_scale.Value )
								{
									circle.Radius = mouseDist;
									xnaPanel.SetDebugObject( "Radius", scaleDist.ToString() );
								}
								break;
						}
					}
					break;

				default:
					if( selectedPrimitive != null && !selectedPrimitive.Intersects( cursorPos ) )
					{
						selectedPrimitive.Color = pointColors[ primitives.IndexOf( selectedPrimitive ) ];
					}

					int pointedIndex = -1;
					for( int i = 0; i < primitives.Count; i++ )
					{
						Primitive p = primitives[ i ];
						if( p.Intersects( cursorPos ) )
						{
							pointedIndex = i;
							SelectPoint( i );
							break;
						}
					}

					xnaPanel.SetDebugObject( "Pointed Epoch", pointedIndex == -1 ? "N/A" : pointedIndex.ToString() );
					break;
			}

			Vector2 pos = cursorPos / (float)numericUpDown_scale.Value;

			//foreach( Primitive p in primitives )
			//{
			//	p.Color = DXColor.Red;
			//}
		
			//nearestNeighbours = kdTree.NearestNeighbors( new double[] { pos.X, pos.Y }, 100, 0.2 );
			//while( nearestNeighbours.MoveNext() )
			//{
			//	nearestNeighbours.Current.Color = highlightColor;
			//}

			xnaPanel.SetDebugObject( "Coordinat", pos.ToString() );
			InvalidateWindow();
		}

		private void xnaPanel_MouseEnter( object sender, EventArgs e )
		{
			xnaPanel.Focus();
		}

		private void xnaPanel_Resize( object sender, EventArgs e )
		{
			cam.UpdateTransformations();
		}

		private void xnaPanel_Render( GraphicsDevice g, SpriteBatch s )
		{
			g.Clear( DXColor.CornflowerBlue );
			DrawPlot( s );
			DrawScale( s );
		}

		private void DrawPlot( SpriteBatch s )
		{
			s.Begin( SpriteSortMode.Deferred, s.GraphicsDevice.BlendStates.AlphaBlend, null, null, null, null, cam.GetTransformation() );
			PrimitiveBatch.Begin( cam.GetOrthographicTransformation() );
			bool drawLabels = checkBox_labels.Checked;

			for( int i = 0; i < primitives.Count; i++ )
			{
				Primitive p = primitives[ i ];
				p.Draw();

				if( drawLabels )
				{
					string label = labels[ i ];
					if( label != null )
					{
						Vector2 fSize = xnaPanel.DebugFont.MeasureString( label );
						s.DrawString( xnaPanel.DebugFont, label, p.Position, DXColor.Yellow, 0, fSize * 0.5f, 0.2f, SpriteEffects.None, 0f );
					}
				}
			}

			foreach( Primitive p in drawings )
			{
				p.Draw();
			}
			PrimitiveBatch.End();
			s.End();
		}

		private void DrawScale( SpriteBatch s )
		{
			int interval = 10;
			float camX = cam.Position.X;
			float camY = cam.Position.Y;
			float zoomWidth = ( xnaPanel.Width * 0.5f ) * cam.InverseZoom;
			float zoomHeight = ( xnaPanel.Height * 0.5f ) * cam.InverseZoom;
			float windowLeft = camX - zoomWidth;
			float windowRight = camX + zoomWidth;
			float windowTop = camY - zoomHeight;
			float windowBottom = camY + zoomHeight;
			float windowWidth = windowRight - windowLeft;
			float windowHeight = windowBottom - windowTop;
			float intervalPixelStepX = windowWidth / interval;
			float intervalPixelStepY = windowHeight / interval;
			float intervalWidth = ( xnaPanel.Width * cam.InverseZoom ) / (float)interval;
			float intervalHeight = ( xnaPanel.Height * cam.InverseZoom ) / (float)interval;
			int startX = (int)( windowLeft / intervalWidth ) - 2;
			int endX = (int)( windowRight / intervalWidth ) + 2;
			int startY = (int)( windowTop / intervalHeight ) - 1;
			int endY = (int)( windowBottom / intervalHeight ) + 2;
			bool drawGrid = checkBox_toggleGrid.Checked;
			float scale = (float)numericUpDown_scale.Value;

			//Console.WriteLine("IntervalSize: " + intervalSize);
			//Console.WriteLine( "Window: " + windowLeft + " " + windowRight );
			//Console.WriteLine( "Intervals: " + startInterval + " " + endInterval );
			//Console.WriteLine( "Step: " + intervalPixelStep );
			//Console.WriteLine( cam.Position );

			s.Begin( SpriteSortMode.Deferred, s.GraphicsDevice.BlendStates.AlphaBlend );
			PrimitiveBatch.Begin();

			PRect coverLeft = new PRect( 0, 0, leftMargin, xnaPanel.Height, true );
			coverLeft.Color = DXColor.CornflowerBlue;
			coverLeft.Draw();

			PRect coverBottom = new PRect( 0, xnaPanel.Height - bottomMargin, xnaPanel.Width, bottomMargin, true );
			coverBottom.Color = DXColor.CornflowerBlue;
			coverBottom.Draw();

			float granularityX = intervalPixelStepX / scale;
			float granularityY = intervalPixelStepY / scale;
			int decimalsX = 0;
			int decimalsY = 0;

			if( granularityX < 0.5f )
			{
				decimalsX = 2;
			}
			else if( granularityX < 10 )
			{
				decimalsX = 1;
			}
			if( granularityY < 0.5f )
			{
				decimalsY = 2;
			}
			else if( granularityY < 10 )
			{
				decimalsY = 1;
			}

			for( int i = startX; i < endX; i++ )
			{
				float val = i * intervalPixelStepX;
				string txt = Math.Round( val / scale, decimalsX, MidpointRounding.AwayFromZero ).ToString();

				Vector2 fSize = xnaPanel.DebugFont.MeasureString( txt );
				Vector2 pos = new Vector2();
				pos.X = ( -camX + zoomWidth + val ) * cam.Zoom;
				pos.Y = xnaPanel.Height - 10;

				if( drawGrid && pos.X > leftMargin )
				{
					PLine line = new PLine( pos.X, 0, pos.X, xnaPanel.Height - bottomMargin );
					line.Draw();
				}
				s.DrawString( xnaPanel.DebugFont, txt, pos, DXColor.Black, 0, fSize * 0.5f, 1f, SpriteEffects.None, 0f );
			}

			for( int i = startY; i < endY; i++ )
			{
				float val = i * intervalPixelStepY;
				string txt = Math.Round( val / scale, decimalsY, MidpointRounding.AwayFromZero ).ToString();

				Vector2 fSize = xnaPanel.DebugFont.MeasureString( txt );
				Vector2 pos = new Vector2();
				pos.X = 10;
				pos.Y = ( -camY + zoomHeight + val ) * cam.Zoom;

				if( drawGrid && pos.Y < xnaPanel.Height - bottomMargin )
				{
					PLine line = new PLine( leftMargin, pos.Y, xnaPanel.Width, pos.Y );
					line.Draw();
				}
				s.DrawString( xnaPanel.DebugFont, txt, pos, DXColor.Black, 0, new Vector2( 0, fSize.Y * 0.5f ), 1f, SpriteEffects.None, 0f );
			}

			PrimitiveBatch.End();
			s.End();
		}

		private enum ActionMode
		{
			DrawLine,
			DrawCircle,
		}
	}
}