using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

public static class IOHelper
{
	public static bool Save<T>( string filePath, T o, Type[] extraTypes = null )
	{
		try
		{
			XmlWriterSettings ws = new XmlWriterSettings();
			ws.OmitXmlDeclaration = true;
			ws.IndentChars = "\t";
			ws.Indent = true;

			if( !Path.IsPathRooted( filePath ) )
			{
				filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), filePath );
			}

			if( !Directory.Exists( Path.GetDirectoryName( filePath ) ) )
			{
				Directory.CreateDirectory( Path.GetDirectoryName( filePath ) );
			}

			using( XmlWriter writer = XmlWriter.Create( filePath, ws ) )
			{
				XmlSerializer serializer = new XmlSerializer( typeof( T ), extraTypes );
				serializer.Serialize( writer, o );
			}
			return true;
		}
		catch( Exception ex )
		{
			Console.WriteLine( "Save exception:\n" + ex.ToString() );
		}

		return false;
	}

	public static bool Load<T>( string filePath, out T o, Type[] extraTypes = null )
	{
		try
		{
			if( !Path.IsPathRooted( filePath ) )
			{
				filePath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), filePath );
			}

			if( File.Exists( filePath ) )
			{
				using( XmlReader reader = XmlReader.Create( filePath ) )
				{
					XmlSerializer serializer = new XmlSerializer( typeof( T ), extraTypes );
					o = (T)serializer.Deserialize( reader );
				}
				return true;
			}
		}
		catch( Exception ex )
		{
			Console.WriteLine( "Load exception:\n" + ex.ToString() );
		}

		o = default( T );
		return false;
	}
}