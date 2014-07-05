using System;
using System.Drawing;
using System.Windows.Forms;

namespace BCIExplorer.Forms
{
	public partial class Form_Progress : Form
	{
		private DateTime startTime;
		private TimeSpan duration;
		private Action<Form_Progress> onTickAction;

		public Form_Progress( Form parent, int max, string caption, Action<Form_Progress> onTickAction )
		{
			InitializeComponent();
			Location = new Point( parent.Location.X + ( parent.Width - this.Width ) / 2, parent.Location.Y + ( parent.Height - this.Height ) / 2 );
			Size = new Size( 340, 92 );
			Text = caption;
			this.onTickAction = onTickAction;
			SetMax( max );
		}

		public void SetMax( int max )
		{
			progressBar.Maximum = max;
		}

		public void Step()
		{
			progressBar.PerformStep();
		}

		public void SetValue( int value )
		{
			progressBar.Value = value;
		}

		public void SetOnTickAction( Action<Form_Progress> onTickAction )
		{
			this.onTickAction = onTickAction;
		}

		public static Form_Progress Create( Form parent, int max = 0, string caption = "Progress", Action<Form_Progress> onTickAction = null )
		{
			Form_Progress f = new Form_Progress( parent, max, caption, onTickAction );
			f.Show();
			f.BeginTimer();
			return f;
		}

		private void BeginTimer()
		{
			startTime = DateTime.Now;
			timer.Start();
		}

		private void timer_Tick( object sender, EventArgs e )
		{
			duration = DateTime.Now - startTime;
			label_time.Text = duration.ToString( @"hh\:mm\:ss\.ff" );
			if( onTickAction != null )
			{
				onTickAction.Invoke( this );
			}
			if( progressBar.Value >= progressBar.Maximum )
			{
				Dispose();
			}
		}
	}
}