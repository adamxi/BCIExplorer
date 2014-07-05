using System;
using System.IO;
using System.Windows.Forms;
using EDFReader;

namespace BCIExplorer
{
	public static class Project
	{
		public static event EventHandler FileFiltered = delegate { };

		public static EDFFile LoadedFile { get; private set; }

		public static EDFFile FilteredFile { get; set; }

		private static Transcriptions transcriptions;

		public static Transcriptions Transcriptions
		{
			get { return transcriptions; }
		}

		public static bool HasTranscriptions { get; private set; }

		public static bool LoadFile( string filePath )
		{
			if( File.Exists( filePath ) )
			{
				LoadedFile = new EDFFile();
				LoadedFile.readFile( filePath );
				FilteredFile = LoadedFile.Copy();
				OnFileFiltered();
				return true;
			}
			return false;
		}

		public static bool LoadTranscriptions( string filePath )
		{
			if( File.Exists( filePath ) )
			{
				HasTranscriptions = IOHelper.Load( filePath, out transcriptions );
				return HasTranscriptions;
			}
			return false;
		}

		public static void OnFileFiltered()
		{
			FileFiltered.Invoke( null, null );
		}
	}
}