using System.Drawing;
using SharpDX;
using Rectangle = System.Drawing.Rectangle;

public class ImageHelper
{
	/// <summary>
	/// Resizes an image to fit within another size while still keeping its aspect ratio.
	/// http://www.codeproject.com/KB/GDI-plus/imageresize.aspx
	/// </summary>
	/// <param name="boundingSize">The size to make the image fit within.</param>
	/// <param name="imageSize">Size of the image.</param>
	/// <param name="scaleUpToFit">If true, the image will be scaled up to fit the bounding size if the image is smaller. If false, the image will only be scaled if its bigger than the bounding size.</param>
	public static Rectangle ResizeToFit( Size boundingSize, Size imageSize, bool scaleUpToFit = false )
	{
		int posX = 0;
		int posY = 0;
		int width = 0;
		int height = 0;

		//if the image is too big for the boundingSize or simply scaling the image up
		if( scaleUpToFit || imageSize.Width > boundingSize.Width || imageSize.Height > boundingSize.Height )
		{
			float ratio = 0;
			float ratioWidth = ( (float)boundingSize.Width / (float)imageSize.Width );
			float ratioHeight = ( (float)boundingSize.Height / (float)imageSize.Height );

			if( ratioHeight < ratioWidth )
			{
				ratio = ratioHeight;
				posX = (int)( boundingSize.Width - ( imageSize.Width * ratio ) ) >> 1;
			}
			else
			{
				ratio = ratioWidth;
				posY = (int)( boundingSize.Height - ( imageSize.Height * ratio ) ) >> 1;
			}

			width = (int)( imageSize.Width * ratio );
			height = (int)( imageSize.Height * ratio );

		}
		else
		{ //else center image in the middle on X and Y.
			posX = ( boundingSize.Width - imageSize.Width ) >> 1;
			posY = ( boundingSize.Height - imageSize.Height ) >> 1;
			width = imageSize.Width;
			height = imageSize.Height;
		}

		return new Rectangle( posX, posY, width, height );
	}

	public static Vector2 ClampSize( Vector2 boundingSize, Vector2 imageSize, bool scaleUpToFit = false )
	{
		Vector2 size = Vector2.Zero;

		if( scaleUpToFit || imageSize.X > boundingSize.X || imageSize.Y > boundingSize.Y )
		{
			float ratio = 0;
			float ratioWidth = boundingSize.X / imageSize.X;
			float ratioHeight = boundingSize.Y / imageSize.Y;

			if( ratioHeight < ratioWidth )
			{
				ratio = ratioHeight;
			}
			else
			{
				ratio = ratioWidth;
			}

			size.X = imageSize.X * ratio;
			size.Y = imageSize.Y * ratio;

		}
		else
		{
			size.X = imageSize.X;
			size.Y = imageSize.Y;
		}

		return size;
	}

	public static Size ClampSize( Size boundingSize, Size imageSize, bool scaleUpToFit = false )
	{
		Size size = Size.Empty;

		if( scaleUpToFit || imageSize.Width > boundingSize.Width || imageSize.Height > boundingSize.Height )
		{
			float ratio = 0;
			float ratioWidth = boundingSize.Width / (float)imageSize.Width;
			float ratioHeight = boundingSize.Height / (float)imageSize.Height;

			if( ratioHeight < ratioWidth )
			{
				ratio = ratioHeight;
			}
			else
			{
				ratio = ratioWidth;
			}

			size.Width = (int)( imageSize.Width * ratio );
			size.Height = (int)( imageSize.Height * ratio );

		}
		else
		{
			size.Width = imageSize.Width;
			size.Height = imageSize.Height;
		}

		return size;
	}
}