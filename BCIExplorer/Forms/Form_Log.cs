using System;
using BCIExplorer.Util;
using WeifenLuo.WinFormsUI.Docking;

namespace BCIExplorer.Forms
{
	public partial class Form_Log : DockContent
	{
		public Form_Log()
		{
			InitializeComponent();
			PopulateLevelDropdown();
		}

		private void PopulateLevelDropdown()
		{
			foreach( Logger.Level level in Enum.GetValues( typeof( Logger.Level ) ) )
			{
				comboBox_levels.Items.Add( level );
			}
			comboBox_levels.SelectedIndex = 0;
		}

		public void WriteToLog( string msg, Logger.Level level )
		{
			this.UIThread( delegate
			{
				Logger.Level currentLevel = (Logger.Level)comboBox_levels.SelectedItem;
				if( currentLevel.CompareTo( level ) >= 0 )
				{
					textBox_log.AppendText( msg + Environment.NewLine );
					textBox_log.Select( textBox_log.Text.Length, 0 );
				}
			} );
		}

		private void comboBox_levels_SelectedIndexChanged( object sender, EventArgs e )
		{
			textBox_log.Focus();
			textBox_log.Text = Logger.GetLog( (Logger.Level)comboBox_levels.SelectedItem );
			textBox_log.Select( textBox_log.Text.Length, 0 );
			textBox_log.ScrollToCaret();
		}

		private void button_clear_Click( object sender, EventArgs e )
		{
			Logger.Clear();
			textBox_log.Clear();
		}
	}
}