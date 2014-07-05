using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Toolkit.Graphics;
using Color = System.Drawing.Color;
using DXColor = SharpDX.Color;
using DXRectangle = SharpDX.Rectangle;
using MSBrush = System.Drawing.Brush;

namespace SharpDXForms
{
	[DefaultEvent( "Render" )]
	public abstract class GraphicsDeviceControl : Control
	{
		public delegate void RenderHandler( GraphicsDevice g, SpriteBatch s );

		[Description( "Occurs when the XNAPanel needs to be rendered" ), Category( "CatBehavior" )]
		public event RenderHandler Render = delegate { };

		// However many GraphicsDeviceControl instances you have, they all share the same underlying GraphicsDevice, managed by this helper service.
		protected GraphicsDeviceService graphicsDeviceService;
		protected SpriteBatch spriteBatch;
		protected ServiceContainer services;
		protected ViewportF viewport;
		private DXRectangle surfaceRectangle;
		protected bool canUpdate;
		private bool fixedViewport;
		private PresentationParameters parameters;
		private SwapChainGraphicsPresenter presenter;

		public GraphicsDeviceControl()
		{
			BackColor = Color.CornflowerBlue;
			this.SizeChanged += new EventHandler( GraphicsDeviceControl_SizeChanged );

			// Hack to trigger the OnCreateControl() method. From here we can detect if the application is in design mode.
			// Design mode cannot be detected from a constructor. Donno how to implement a proper architecture for this atm.
			this.Handle.ToString();
		}

		#region Properties
		public bool EnableDebugDraw { get; set; }

		public GraphicsDevice GraphicsDevice
		{
			get { return graphicsDeviceService.GraphicsDevice; }
		}

		public SpriteBatch SpriteBatch
		{
			get { return spriteBatch; }
		}

		/// <summary>
		/// Gets an IServiceProvider containing our IGraphicsDeviceService. This can be used with components such as the ContentManager, which use this service to look up the GraphicsDevice.
		/// </summary>
		public ServiceContainer Services
		{
			get { return services; }
		}

		public ViewportF Viewport
		{
			get { return viewport; }
		}
		#endregion

		#region Events & Drawing logic
		protected override void OnCreateControl()
		{
			if( !DesignMode )
			{
				services = new ServiceContainer();
				graphicsDeviceService = GraphicsDeviceService.AddRef();
				services.AddService<IGraphicsDeviceService>( graphicsDeviceService ); // Register the service, so components like ContentManager can find it.
				spriteBatch = new SpriteBatch( GraphicsDevice );
				viewport = new Viewport( 0, 0, 0, 0, 0, 1 );
				canUpdate = true;

				Initialize();
			}
			base.OnCreateControl();
		}

		protected override void OnPaintBackground( PaintEventArgs pevent )
		{
		}

		protected override void OnPaint( PaintEventArgs e )
		{
			if( !DesignMode )
			{
				if( canUpdate )
				{
					GraphicsDevice.SetRenderTargets( presenter.DepthStencilBuffer, presenter.BackBuffer );
					OnDraw();
					presenter.Present();
				}
				else
				{
					e.Graphics.Clear( Color.Black );
				}
			}
			else
			{
				PaintUsingSystemDrawing( e.Graphics, Text + "\n\n" + GetType() );
			}
		}

		protected virtual void OnDraw()
		{
			Render( GraphicsDevice, spriteBatch );
			if( EnableDebugDraw )
			{
				DebugDraw();
			}
		}

		/// <summary>
		/// Captures the screen onto a Texture2D.
		/// Note: This is not a copy of the current backbuffer (displayed screen image).
		/// This is instead an entirely new rendering of the screen onto a Texture2D which is relativly slow. So performance wise, it's not something you'll want to do each frame.
		/// </summary>
		public Texture2D CaptureScreen()
		{
			bool debugDraw = EnableDebugDraw;
			EnableDebugDraw = false;

			GraphicsDevice.SetRenderTargets( presenter.DepthStencilBuffer, presenter.BackBuffer );
			OnDraw();
			EnableDebugDraw = debugDraw;

			// Set the copied color data to the new Texture2D.
			// This makes sure that the screenshot doesn't get lost with the rendertarget.
			// This also prevents memory leaks as the rendertarget can now safely be disposed.
			Texture2D texture = Texture2D.New( GraphicsDevice, presenter.BackBuffer.Width, presenter.BackBuffer.Height, presenter.BackBuffer.Format );

			DXColor[] colors = new DXColor[ Width * Height ];
			presenter.BackBuffer.GetData( colors );
			texture.SetData( colors );

			return texture;
		}

		/// <summary>
		/// If we do not have a valid graphics device (for instance if the device is lost,
		/// or if we are running inside the Form designer), we must use regular System.Drawing method to display a status message.
		/// </summary>
		protected virtual void PaintUsingSystemDrawing( Graphics graphics, string text )
		{
			graphics.Clear( BackColor );

			using( MSBrush brush = new SolidBrush( Color.Black ) )
			{
				using( StringFormat format = new StringFormat() )
				{
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;
					graphics.DrawString( text, Font, brush, ClientRectangle, format );
				}
			}
		}

		private void GraphicsDeviceControl_SizeChanged( object sender, EventArgs e )
		{
			viewport.Width = Width;
			viewport.Height = Height;
			presenter.Resize( Width, Height, Format.B8G8R8A8_UNorm );
			surfaceRectangle = new DXRectangle( 0, 0, Width, Height );
		}
		#endregion

		#region Methods
		/// <summary>
		/// Sets the viewport.
		/// </summary>
		public void SetViewport( Viewport v )
		{
			viewport = v;
			fixedViewport = true;
		}

		/// <summary>
		/// Sets the viewport.
		/// </summary>
		public void SetViewport( int x, int y, int width, int height )
		{
			viewport.X = x;
			viewport.Y = y;
			viewport.Width = width;
			viewport.Height = height;
			fixedViewport = true;
		}

		/// <summary>
		/// Sets the viewport to always be the same as the size of the panel.
		/// </summary>
		public void SetViewportOff()
		{
			fixedViewport = false;
		}

		/// <summary>
		/// Disables redrawing of the control.
		/// </summary>
		public void BeginUpdate()
		{
			canUpdate = false;
		}

		/// <summary>
		/// Enables redrawing of the control.
		/// </summary>
		public void EndUpdate()
		{
			canUpdate = true;
		}

		protected override void Dispose( bool disposing )
		{
			if( graphicsDeviceService != null )
			{
				graphicsDeviceService.Release( disposing );
				graphicsDeviceService = null;
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Virtual methods
		/// <summary>
		/// Called after the GraphicsDeviceControl is done initializing. Deriving classes can override this to initialize their own code.
		/// </summary>
		protected virtual void Initialize()
		{
			parameters = new PresentationParameters();
			parameters.BackBufferFormat = Format.B8G8R8A8_UNorm;
			parameters.DepthStencilFormat = DepthFormat.None;
			parameters.PresentationInterval = PresentInterval.Immediate;
			parameters.IsFullScreen = false;
			parameters.DeviceWindowHandle = this.Handle;

			presenter = new SwapChainGraphicsPresenter( GraphicsDevice, parameters );
		}

		protected virtual void DebugDraw()
		{
		}
		#endregion
	}
}