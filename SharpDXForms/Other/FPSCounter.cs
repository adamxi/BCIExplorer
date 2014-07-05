using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDXForms.Helper;

namespace SharpDXForms
{
	/// <summary>
	/// Based on: http://blogs.msdn.com/b/shawnhar/archive/2007/06/08/displaying-the-framerate.aspx?CommentPosted=true
	/// </summary>
	public class FPSCounter
	{
		private SpriteFont font;
		private Vector2 position;
		private Vector2 shadePosition;
		private Color color;
		private int frameCounter;
		private int frameRate;
		private double timer;
		private string drawString;

		public FPSCounter( Vector2 position, SpriteFont font, Color color )
		{
			this.font = font;
			this.position = position;
			this.shadePosition = position + Vector2.One;
			this.color = color;

			this.frameCounter = -1; // Hack to make the first frame display "0"
			DrawFPS = true;
			
		}

		public FPSCounter( Vector2 position, SpriteFont font )
			: this( position, font, Color.White )
		{
		}

		/// <summary>
		/// The Frames Per Second measured by this FPSCounter.
		/// </summary>
		public int FPS
		{
			get { return frameRate; }
		}

		/// <summary>
		/// True to draw the measured fps on the screen. The fps value can also be retrieved with the "FPS" property.
		/// </summary>
		public bool DrawFPS { get; set; }

		public void Update( SpriteBatch spriteBatch )
		{
			frameCounter++;

			if( TickCounter.Milliseconds - timer >= 1000 )
			{
				timer = TickCounter.LastTime;
				frameRate = frameCounter;
				frameCounter = 0;
				drawString = "FPS: " + frameRate.ToString();
			}

			if( DrawFPS )
			{
				spriteBatch.DrawString( font, drawString, shadePosition, Color.Black );
				spriteBatch.DrawString( font, drawString, position, color );
			}
		}
	}
}