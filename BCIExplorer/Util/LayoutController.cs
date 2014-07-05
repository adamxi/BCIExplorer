using System;
using System.IO;
using System.Reflection;
using WeifenLuo.WinFormsUI.Docking;

namespace BCIExplorer
{
	public static class LayoutController
	{
		private static string layoutFile = "Layout.xml";
		private static DeserializeDockContent deserializeDockContent;
		private static string folderPath = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ) + Path.DirectorySeparatorChar + "Settings" + Path.DirectorySeparatorChar;

		public static string ConfigFile
		{
			get { return Path.Combine( folderPath, layoutFile ); }
		}

		public static bool LoadLayout( DockPanel dockPanel )
		{
			deserializeDockContent = new DeserializeDockContent( GetContentFromPersistString );

			if( File.Exists( ConfigFile ) )
			{
				dockPanel.LoadFromXml( ConfigFile, deserializeDockContent );
				return true;
			}

			return false;
		}

		public static void SaveLayout( DockPanel dockPanel )
		{
			if( !Directory.Exists( folderPath ) )
			{
				Directory.CreateDirectory( folderPath );
			}
			dockPanel.SaveAsXml( ConfigFile );
		}

		private static IDockContent GetContentFromPersistString( string persistString )
		{
			foreach( DockContent dockingForm in SharedForms.dockingForms )
			{
				Type t = dockingForm.GetType();

				if( persistString == t.ToString() )
				{
					return dockingForm;
				}
			}

			return null;
		}
	}
}