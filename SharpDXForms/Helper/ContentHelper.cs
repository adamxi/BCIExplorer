using System.Collections.Generic;
using System.IO;
using SharpDX.Toolkit.Graphics;
using SharpDXForms.Helper;

namespace SharpDXForms
{
	public static class ContentHelper
	{
		private static Dictionary<string, Texture2D> assets;
		private static GraphicsDevice graphics;

		public static Dictionary<string, Texture2D> Assets
		{
			get { return assets; }
		}

		static ContentHelper()
		{
			assets = new Dictionary<string, Texture2D>();
			graphics = GraphicsDeviceService.GetGraphics();
		}

		/// <summary>
		/// Clears the content cache forcing an asset to be reloaded next time 'Load()' is called.
		/// </summary>
		public static void ClearContentCache()
		{
			assets.Clear();
		}

		/// <summary>
		/// Loads a Texture2D from an assetPath. The texture will be cached for future references so if the texture has previously been loaded the old loaded texture will be returned.
		/// </summary>
		public static Texture2D Load( string assetPath )
		{
			Texture2D tex;

			if( assets.TryGetValue( assetPath, out tex ) )
			{
				return tex;
			}

			if( File.Exists( assetPath ) )
			{
				using( FileStream stream = new FileStream( assetPath, FileMode.Open, FileAccess.Read ) )
				{
					tex = Texture2D.Load( graphics, stream );
					tex.Tag = assetPath;
				}
				assets.Add( assetPath, tex );
			}

			return tex;
		}

		/// <summary>
		/// Loads a Texture2D from an assetPath. Textures will not be cached for future references.
		/// </summary>
		public static Texture2D Reload( string assetPath )
		{
			if( File.Exists( assetPath ) )
			{
				using( FileStream stream = new FileStream( assetPath, FileMode.Open, FileAccess.Read ) )
				{
					Texture2D tex = Texture2D.Load( graphics, stream );
					tex.Tag = assetPath;
					return tex;
				}
			}
			return null;
		}
	}
}