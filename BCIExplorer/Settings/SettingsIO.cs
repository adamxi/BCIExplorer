using System.Collections.Specialized;
using System.ComponentModel;

namespace Settings
{
	public class SettingsIO : ISettings
	{
		private static SettingsIO instance = new SettingsIO();
		public static SettingsIO Default
		{
			get { return instance; }
		}

		void ISettings.SetDefault( ISettings o )
		{
			instance = o as SettingsIO;
		}


		private StringCollection recentFiles = new StringCollection();

		[Browsable( false )]
		public StringCollection RecentFiles
		{
			get { return recentFiles; }
			set { recentFiles = value; }
		}
	}
}