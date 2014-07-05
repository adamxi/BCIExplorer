using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BCIExplorer.Chart;
using BCIExplorer.Clustering;
using BCIExplorer.Geometry;
using BCIExplorer.Util;
using MathNet.Numerics.Filtering;
using MathNet.Numerics.Filtering.IIR;
using Settings;
using ShoNS.Array;
using ShoNS.MathFunc;
using ShoNS.Visualization;
using WeifenLuo.WinFormsUI.Docking;

namespace BCIExplorer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			Test();
			Init();
		}

		private unsafe void Test()
		{
			//int count = 100;
			//double[][] points = new double[ count ][];

			//for( int i = 0; i < count; i++ )
			//{
			//	points[ i ] = new double[ 2 ];
			//	DoubleArray x = ArrayRandom.RandomDoubleArray( 2, 10 );
			//	x = x * x.T;
			//	EigenValsSym ged = new EigenValsSym( x );
			//	DoubleArray D = ged.D;

			//	for( int j = D.Count; --j >= 0; )
			//	{
			//		if( D[ j ] > 0 )
			//		{
			//			points[ i ][ j ] = Math.Log( D[ j ] );
			//		}
			//	}
			//}

			//ScatterPlot s = new ScatterPlot();
			//s.Draw( points );

			//3034.9285116778669
			//double exp = Math.Exp( 1000 * 0.5 );
			//"".ToCharArray();

			//IOHelper.Save( Path.GetDirectoryName( Application.ExecutablePath ) + "\\1.xml", new Transcriptions( 1 ) );
			//IOHelper.Save( Path.GetDirectoryName( Application.ExecutablePath ) + "\\3.xml", new Transcriptions( 3 ) );
			//IOHelper.Save( Path.GetDirectoryName( Application.ExecutablePath ) + "\\5.xml", new Transcriptions( 5 ) );
			//Transcriptions a;
			//Transcriptions b;
			//Transcriptions c;
			//IOHelper.Load( Path.GetDirectoryName( Application.ExecutablePath ) + "\\1.xml", out a );
			//IOHelper.Load( Path.GetDirectoryName( Application.ExecutablePath ) + "\\3.xml", out b );
			//IOHelper.Load( Path.GetDirectoryName( Application.ExecutablePath ) + "\\5.xml", out c );
			//a.Init();
			//b.Init();
			//c.Init();

			//int n = 4;
			//double low = 1;
			//double high = 30;
			//int rate = 128;

			//#region LP test
			//{
			//	double f2 = 2 * high / rate;

			//	Butterworth bwA = new Butterworth( rate, n, high, true );
			//	IIR.Butterworth bwB = new IIR.Butterworth();

			//	double* dcof = bwB.dcof_bwlp( n, f2 );
			//	int* ccof = bwB.ccof_bwlp( n );
			//	double sf = bwB.sf_bwlp( n, f2 );

			//	double[] a = new double[ n + 1 ];
			//	for( int i = 0; i <= n; ++i )
			//	{
			//		a[ i ] = (double)ccof[ i ] * sf;
			//	}

			//	double[] d = new double[ n + 1 ];
			//	for( int i = 0; i <= n; ++i )
			//	{
			//		d[ i ] = dcof[ i ];
			//	}
			//}
			//#endregion

			//#region HP test
			//{
			//	double f1 = 2 * low / rate;

			//	Butterworth bwA = new Butterworth( rate, n, low, false );
			//	IIR.Butterworth bwB = new IIR.Butterworth();

			//	double* dcof = bwB.dcof_bwhp( n, f1 );
			//	int* ccof = bwB.ccof_bwhp( n );
			//	double sf = bwB.sf_bwhp( n, f1 );
			//	double sf2 = bwB.sf_bwlp( n, f1 );

			//	double[] a = new double[ n + 1 ];
			//	for( int i = 0; i <= n; ++i )
			//	{
			//		a[ i ] = (double)ccof[ i ] * sf;
			//	}

			//	double[] d = new double[ n + 1 ];
			//	for( int i = 0; i <= n; ++i )
			//	{
			//		d[ i ] = dcof[ i ];
			//	}
			//	Console.WriteLine( "sf: " + sf );
			//	Console.WriteLine( "bw: " + bwA.scale );
			//}
			//#endregion

			//#region BP test
			//{
			//	double f1 = 2 * low / rate;
			//	double f2 = 2 * high / rate;

			//	Butterworth bwA = new Butterworth( rate, n, low, high, true );
			//	IIR.Butterworth bwB = new IIR.Butterworth();

			//	double* dcof = bwB.dcof_bwbp( n, f1, f2 );
			//	int* ccof = bwB.ccof_bwbp( n );
			//	double sf = bwB.sf_bwbp( n, f1, f2 );

			//	int count = 2 * n + 1;
			//	double[] a = new double[ count ];
			//	for( int i = 0; i < count; ++i )
			//	{
			//		a[ i ] = (double)ccof[ i ] * sf;
			//	}

			//	double[] d = new double[ count ];
			//	for( int i = 0; i < count; ++i )
			//	{
			//		d[ i ] = dcof[ i ];
			//	}
			//}
			//#endregion

			//double* dcof = bwB.dcof_bwbs( n, f1, f2 );
			//double* ccof = bwB.ccof_bwbs( n, f1, f2 );
			//double sf = bwB.sf_bwbs( n, f1, f2 );

			//double* dcof = bwB.dcof_bwhp( n, f1 );
			//int* ccof = bwB.ccof_bwhp( n );
			//double sf = bwB.sf_bwhp( n, f1 );

			//double[] samples = new double[]{
			//	4029.7432,4024.1023,4024.6125,4037.9465,4039.4827,4029.7432,4025.1284,4026.1545,4024.1023,4022.0500,4029.2271,4038.9727,4046.1497,4048.7180,4031.2795,4011.2786,4022.5601,4045.6396,4040.5088,4028.2009,4031.2795,4025.6387,4022.5601,4038.4624,4041.5349,4035.3840,4044.1033,4042.0510,4029.7432,4042.0510,4044.6135,4017.4353,4016.9192,4037.9465,4032.3057,4025.1284,4044.1033,4041.0249,4007.1797,4009.2261,4035.3840,4025.6387,4012.3047,4030.2532,4043.0771,4034.8679,4033.3318,4024.6125,4009.2261,4013.8467,4021.0239,4021.0239,4017.4353,4016.9192,4021.0239,4023.5862,4022.0500,4019.9978,4023.0762,4024.1023,4019.4875,4012.8206,4014.3569,4021.0239,4019.9978,4021.5339,4022.5601,4019.4875,4028.2009,4032.8215,4017.9456,4016.4092,4027.6909,4017.4353,4015.3831,4029.2271,4016.9192,4009.2261,4033.3318,4034.3579,4011.2786,4007.6899,4013.3308,4017.9456,4028.7170,4028.7170,4021.0239,4023.5862,4026.1545,4013.8467,4005.6377,4020.5137,4031.2795,4015.3831,4006.1536,4014.8730,4018.4614,4025.1284,4028.7170,4016.9192,4009.7422,4016.4092,4015.3831,4005.1274,4012.3047,4025.6387,4015.8931,3996.9243,3999.9968,4015.3831,4019.9978,4016.4092,4011.2786,4007.1797,4011.2786,4021.5339,4018.9717,4009.7422,4015.3831,4024.6125,4019.4875,4010.2522,4006.6638,4014.3569,4034.8679,4037.4363,4012.8206,4001.5391,
			//	4008.7266,4009.7266,4006.6848,4006.6848,4005.6431,4004.6013,4011.2683,4013.3101,4007.1848,4007.1848,4008.1848,4006.1431,4012.3101,4019.4771,4016.9353,4017.4353,4019.9771,4012.3101,4005.6431,4013.3101,4018.9771,4014.8518,4016.3936,4025.6440,4024.6023,4023.0605,4026.1440,4023.6023,4022.0605,4028.7275,4033.3113,4027.1858,4023.0605,4024.6023,4021.5188,4017.4353,4018.4771,4022.0605,4026.6443,4039.9783,4047.1870,4034.3530,4023.6023,4026.1440,4029.7278,4027.1858,4027.6858,4028.7275,4024.6023,4022.5605,4021.0188,4015.3936,4016.9353,4030.7693,4035.3945,4032.3110,4038.4780,4036.9363,4026.6443,4030.2693,4039.4783,4038.4780,4032.8113,4024.6023,4019.4771,4024.6023,4028.7275,4029.7278,4041.0200,4051.2705,4043.6035,4031.2695,4024.6023,4022.0605,4028.7275,4035.8948,4032.8113,4031.2695,4034.8530,4032.3110,4032.8113,4042.0618,4039.9783,4029.7278,4030.2693,4031.8110,4026.1440,4024.1023,4030.2693,4036.9363,4039.4783,4039.4783,4038.9783,4037.9365,4037.4365,4036.9363,4031.8110,4025.6440,4027.6858,4034.8530,4039.9783,4042.0618,4044.6035,4047.6870,4049.7288,4046.6453,4039.9783,4039.4783,4044.6035,4040.5200,4030.7693,4033.8528,4041.5200,4039.9783,4041.0200,4044.1035,4040.5200,4039.9783,4045.1453,4044.1035,4039.9783,4040.5200,4039.9783,4037.9365,4038.4780,4041.5200,4045.6453,4047.6870
			//};

			//Matrix<double> A = new DenseMatrix( 3, 3, new double[] { -3, 8, 10, -3, 7, 9, 2, -4, -5 } ); // Note! Math.Net assumes "Column-major order" - http://en.wikipedia.org/wiki/Row-major_order#Column-major_order.
			//Matrix<double> A = new DenseMatrix( 2, 128, samples );
			//Matrix<double> cov1 = new DenseMatrix( 2, 2, new double[] { 16311784, 16337511, 16337511, 16363644 } );
			//Matrix<double> cov2 = new DenseMatrix( 2, 2, new double[] { 16212453, 16346177, 16346177, 16481932 } );
			//Matrix<double> B = A * A.Transpose();
			//Matrix<double> C = A.Covariance();

			//Complex d = Riemannian.Distance( cov1, cov2 );
		}

		private void Init()
		{
			Logger.PrintToConsole = true;
			Logger.Log( "LAA Enabled: " + LAA.IsLargeAware( Assembly.GetExecutingAssembly().Location ), Logger.Level.Level_2 );
			LoadSettings();

			Size = new System.Drawing.Size( 1600, 800 );
			KeyPreview = true;
			new SharedForms();
			SharedForms.main = this;

			if( !LayoutController.LoadLayout( dockPanel1 ) )
			{
				InitDocking();
			}

#if DEBUG
			//Project.LoadFile( SettingsIO.Default.RecentFiles[ 0 ] );
#endif

			SetCaption();
		}

		private void SetCaption()
		{
			this.Text = Form_AboutBox.AssemblyTitle + " v" + Form_AboutBox.AssemblyVersion;
			if( Project.LoadedFile != null )
			{
				this.Text += " - " + Project.LoadedFile.FilePath;
			}
		}

		public void InitDocking()
		{
			dockPanel1.DocumentStyle = DocumentStyle.DockingSdi;
			dockPanel1.DockLeftPortion = 200;
			dockPanel1.DockBottomPortion = 260;

			SharedForms.channelView.Show( dockPanel1, DockState.Document );
			SharedForms.clusterView.Show( dockPanel1, DockState.DockLeftAutoHide );
			SharedForms.log.Show( dockPanel1, DockState.DockBottomAutoHide );
			SharedForms.control.Show( dockPanel1, DockState.DockBottom );
			SharedForms.sliders.Show( SharedForms.control.Pane, DockAlignment.Right, 0.7d );
		}

		private void LoadSettings()
		{
			SettingsController.AddClass( SettingsIO.Default, SettingsController.SettingsType.local );
			SettingsController.AddClass( ClusterOptions.Default, SettingsController.SettingsType.local );
			SettingsController.Load();
		}

		private void SaveSettings()
		{
			SettingsController.Save();
			LayoutController.SaveLayout( dockPanel1 );
		}

		public void PopulateRecentMenu()
		{
			recentToolStripMenuItem.DropDownItems.Clear();

			foreach( string file in SettingsIO.Default.RecentFiles )
			{
				ToolStripMenuItem item = new ToolStripMenuItem();
				item.Text = file;
				item.Click += new EventHandler( RecentFileMenu_Click );

				recentToolStripMenuItem.DropDownItems.Add( item );
			}
		}

		private void fileToolStripMenuItem_DropDownOpened( object sender, EventArgs e )
		{
			PopulateRecentMenu();
		}

		private void openToolStripMenuItem_Click( object sender, EventArgs e )
		{
			FileDialog fd = new OpenFileDialog();
			fd.Filter = "EDF files (*.edf)|*.edf|All files (*.*)|*.*";
			fd.SupportMultiDottedExtensions = true;

			if( fd.ShowDialog() == DialogResult.OK )
			{
				if( Project.LoadFile( fd.FileName ) )
				{
					SetCaption();
					AddRecent( fd.FileName );
				}
			}
		}

		private void loadTranscriptionToolStripMenuItem_Click( object sender, EventArgs e )
		{
			FileDialog fd = new OpenFileDialog();
			fd.Filter = "XML files (*.xml)|*.xml";
			fd.SupportMultiDottedExtensions = true;

			if( fd.ShowDialog() == DialogResult.OK )
			{
				if( Project.LoadTranscriptions( fd.FileName ) )
				{

				}
			}
		}

		private void RecentFileMenu_Click( object sender, EventArgs e )
		{
			if( Project.LoadFile( sender.ToString() ) )
			{
				SetCaption();
				AddRecent( sender.ToString() );
			}
		}

		private void exitToolStripMenuItem1_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
		{
			new Form_AboutBox().ShowDialog();
		}

		private void Form1_FormClosing( object sender, FormClosingEventArgs e )
		{
			SaveSettings();
		}

		private static void AddRecent( string filePath )
		{
			if( !SettingsIO.Default.RecentFiles.Contains( filePath ) )
			{
				SettingsIO.Default.RecentFiles.Add( filePath );
				if( SettingsIO.Default.RecentFiles.Count > 10 )
				{
					SettingsIO.Default.RecentFiles.RemoveAt( 0 );
				}
			}
		}
	}
}