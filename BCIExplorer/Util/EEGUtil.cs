using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDFReader;
using BCIExplorer.Chart;
using BCIExplorer.Geometry;
using MathNet.Numerics.Filtering;
using MathNet.Numerics.Filtering.IIR;
using Settings;
using SharpDXForms.Helper;
using ShoNS.Array;

namespace BCIExplorer.Util
{
	public static class EEGUtil
	{
		public static unsafe EDFFile FilterData( EDFFile file, FilterType type, double lowCutOff, double highCutOff, int order )
		{
			Profiler p = Profiler.StartNew( type.ToString() + " Filter [" + lowCutOff + ":" + highCutOff + ", " + order + "]" );

			List<EDFSignal> signals = file.Header.Signals;
			EDFFile f = new EDFFile();
			f.FilePath = file.FilePath;
			file.Header.CopyTo( f.Header );

			double[] coefficients = Butterworth.Create( type, file.SamplesPerSecond, order, lowCutOff, highCutOff );

			//#region validation
			//double[] coefficientsB = CalculateCoefficients( type, file.SamplesPerSecond, lowCutOff, highCutOff, order );

			//if( coefficients.Length != coefficientsB.Length )
			//{
			//	throw new Exception( "Coefficient error" );
			//}

			//for( int i = 0; i < coefficients.Length; i++ )
			//{
			//	if( Math.Abs( coefficients[ i ] - coefficientsB[ i ] ) > 0.000000001d )
			//	{
			//		throw new Exception( "Coefficient error" );
			//	}
			//}
			//#endregion

			Dictionary<EDFSignal, double[]> tmp = new Dictionary<EDFSignal, double[]>( signals.Count );

			Parallel.ForEach( signals, delegate( EDFSignal signal )
			{
				OnlineIirFilter filter = new OnlineIirFilter( coefficients );
				tmp.Add( signal, filter.ProcessSamples( file.DataRecords[ signal.IndexNumberWithLabel ] ).RemoveMean() );
			} );

			foreach( KeyValuePair<EDFSignal, double[]> kvp in tmp.OrderBy( s => s.Key.IndexNumber ) )
			{
				f.addSignal( kvp.Key, kvp.Value );
			}

			p.Stop();
			Logger.Log( p.ToShortString(), Logger.Level.Level_1 );
			return f;
		}

		//private static unsafe double[] CalculateCoefficients( FilterType type, double rate, double low, double high, int n )
		//{
		//	IIR.Butterworth bwB = new IIR.Butterworth();
		//	double f1 = 2 * low / rate;
		//	double f2 = 2 * high / rate;

		//	if( type == FilterType.BandStop )
		//	{
		//		double* dcof = bwB.dcof_bwbs( n, f1, f2 );
		//		double* ccof = bwB.ccof_bwbs( n, f1, f2 );
		//		double sf = bwB.sf_bwbs( n, f1, f2 );
		//		int count = 2 * n + 1;

		//		double[] a = new double[ count ];
		//		double[] d = new double[ count ];

		//		for( int i = 0; i < count; ++i )
		//		{
		//			a[ i ] = ccof[ i ] * sf;
		//		}
		//		for( int i = 0; i < count; ++i )
		//		{
		//			d[ i ] = dcof[ i ];
		//		}

		//		return a.Concat( d ).ToArray();
		//	}

		//	{
		//		double* dcof = null;
		//		int* ccof = null;
		//		double sf = 0;
		//		int count = 0;

		//		switch( type )
		//		{
		//			case FilterType.LowPass:
		//				dcof = bwB.dcof_bwlp( n, f2 );
		//				ccof = bwB.ccof_bwlp( n );
		//				sf = bwB.sf_bwlp( n, f2 );
		//				count = n + 1;
		//				break;

		//			case FilterType.HighPass:
		//				dcof = bwB.dcof_bwhp( n, f1 );
		//				ccof = bwB.ccof_bwhp( n );
		//				sf = bwB.sf_bwhp( n, f1 );
		//				count = n + 1;
		//				break;

		//			case FilterType.BandPass:
		//				dcof = bwB.dcof_bwbp( n, f1, f2 );
		//				ccof = bwB.ccof_bwbp( n );
		//				sf = bwB.sf_bwbp( n, f1, f2 );
		//				count = 2 * n + 1;
		//				break;
		//		}

		//		double[] a = new double[ count ];
		//		double[] d = new double[ count ];

		//		Console.WriteLine( sf );
		//		for( int i = 0; i < count; ++i )
		//		{
		//			a[ i ] = (double)ccof[ i ] * sf;
		//		}
		//		for( int i = 0; i < count; ++i )
		//		{
		//			d[ i ] = dcof[ i ];
		//		}

		//		return a.Concat( d ).ToArray();
		//	}
		//}

		public static EDFFile SelectChannels( EDFFile file, params string[] channelLabels )
		{
			EDFFile f = new EDFFile();
			file.Header.CopyTo( f.Header );
			f.FilePath = file.FilePath;

			foreach( string label in channelLabels )
			{
				string indexStr = label.Split( new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries )[ 0 ];
				int num;
				bool hasIndexNumber = int.TryParse( indexStr, out num );

				foreach( EDFSignal signal in file.Header.Signals )
				{
					if( hasIndexNumber )
					{
						if( label.Equals( signal.IndexNumberWithLabel.ToLower() ) )
						{
							f.addSignal( signal, file.DataRecords[ signal.IndexNumberWithLabel ] );
							break;
						}
					}
					else if( label.Equals( signal.Label.ToLower() ) )
					{
						f.addSignal( signal, file.DataRecords[ signal.IndexNumberWithLabel ] );
						break;
					}
				}
			}

			return f;
		}

		public static EDFFile SelectChannels( EDFFile file, params int[] channelIds )
		{
			EDFFile f = new EDFFile();
			file.Header.CopyTo( f.Header );
			f.FilePath = file.FilePath;

			foreach( EDFSignal signal in file.Header.Signals )
			{
				if( channelIds.Contains( signal.IndexNumber ) )
				{
					f.addSignal( signal, file.DataRecords[ signal.IndexNumberWithLabel ] );
				}
			}

			return f;
		}

		public static List<DoubleArray> GetEpochs( EDFFile file, int epochSizeMs, int slidingWindows )
		{
			EDFDataRecord records = file.DataRecords;
			int rowCount = records.Count;
			int sampleCount = file.SampleCount;
			int epochSampleSize = (int)( file.SamplesPerSecond * epochSizeMs * 0.001f );
			slidingWindows = Math.Min( epochSampleSize, slidingWindows );
			float slidingWindowSampleSize = epochSampleSize / (float)slidingWindows;
			int epochCount = (int)Math.Ceiling( sampleCount / ( epochSampleSize / (float)slidingWindows ) );
			List<DoubleArray> epochs = new List<DoubleArray>( epochCount );

			for( int i = 0; i < epochCount; i++ )
			{
				int startSample = (int)( i * slidingWindowSampleSize );
				int endSample = startSample + epochSampleSize;

				if( i >= epochCount - slidingWindows ) // Last epochs
				{
					epochSampleSize = sampleCount - startSample;
					endSample = sampleCount;
				}

				DoubleArray epoch = new DoubleArray( rowCount, epochSampleSize );

				int r = 0;
				foreach( double[] channelData in records.Values )
				{
					for( int s = startSample, c = 0; s < endSample; s++, c++ )
					{
						epoch[ r, c ] = channelData[ s ];
					}
					++r;
				}

				epochs.Add( epoch );
			}

			return epochs;
		}

		public static List<RPoint> GetCovarianceMatrices( EDFFile file, int epochSizeMs, int slidingWindows )
		{
			List<DoubleArray> epochs = EEGUtil.GetEpochs( file, epochSizeMs, slidingWindows );
			List<RPoint> covs = new List<RPoint>( epochs.Count );
			int channelCount = file.DataRecords.Count;
			double epsilon = Math.Pow( ClusterOptions.Default.CovarianceEpsilon, 1d / channelCount );
			int nextIndex = 0;
			int off = 0;

			Profiler p = Profiler.StartNew();
			for( int i = off; i < epochs.Count; i++ )
			{
				DoubleArray e = epochs[ i ];

				if( e.size1 > channelCount )
				{
					e.RemoveMean();
					DoubleArray cov = e.StableCovariance( epsilon );
					//DataItemCovarianceMatrix dataCov = new DataItemCovarianceMatrix( cov );
					//dataCov.Index = nextIndex++;
					//dataCov.EpochIndex = i;
					covs.Add( new RPoint( cov, i ) );
				}
			}
			p.Stop( true );

			//ScatterPlot.CloseAllPlots();
			//ScatterPlot plot2D = new ScatterPlot( "Covariance matrices - Euclidian distances - 2D" );
			//ScatterPlot plot1D = new ScatterPlot( "Covariance matrices - Riemannian distances - 1D" );

			//{
			//	plot2D.AddPoint( 0, 0, "-1" );
			//}

			//DoubleArray identity = new DoubleArray( 2, 2 );
			//identity.FillIdentity();

			//Vector2 v1 = new Vector2();
			//Vector2 v2 = new Vector2();

			//foreach( DataItem cov in covs )
			//{
			//	DoubleArray P = ( cov as DataItemCovarianceMatrix ).value;
			//	//DoubleArray S = RMath.Log( P );
			//	//DoubleArray P = new DoubleArray( 2, 2 );
			//	//P.FillValue( cov.Index );
			//	//P[ 0, 0 ] = cov.Index;
			//	//P[ 0, 1 ] = cov.Index * 2;
			//	//P[ 1, 0 ] = cov.Index * 3;
			//	//P[ 1, 1 ] = cov.Index * 4;
			//	//P = P * P.T;

			//	EigenSym eigenValues = new EigenSym( P );
			//	DoubleArray D = eigenValues.D;
			//	//DoubleArray diag = eigenValues.V.Diagonal;
			//	double[] point = new double[ D.Count ];

			//	double sum = 0;
			//	for( int i = D.Count; --i >= 0; )
			//	{
			//		point[ i ] = D[ i ] * D[ i ];
			//		//double num = D[ i ];
			//		//sum += num * num;
			//	}

			//	//kdTree.AddPoint( point, P );

			//	//double distance = Math.Sqrt( sum );

			//	plot2D.AddPoint( point, cov.Index.ToString() );

			//	//plot2D.AddPoint( point, cov.Index.ToString() );
			//	//plot1D.AddPoint( dist, 0, cov.Index.ToString() );
			//}

			//Console.WriteLine( "Dist 10: " + RMath.EuclidianDistance( RMath.Log( ( covs[ 10 ] as DataItemCovarianceMatrix ).value ) ) );
			//Console.WriteLine( "Dist 9: " + RMath.EuclidianDistance( RMath.Log( ( covs[ 9 ] as DataItemCovarianceMatrix ).value ) ) );

			//DoubleArray P1 = ( covs[ 9 ] as DataItemCovarianceMatrix ).value;
			//DoubleArray P2 = ( covs[ 10 ] as DataItemCovarianceMatrix ).value;
			//DoubleArray S1 = RMath.Log( identity, ( covs[ 10 ] as DataItemCovarianceMatrix ).value );
			//DoubleArray S2 = RMath.Log( identity, ( covs[ 9 ] as DataItemCovarianceMatrix ).value );

			//double pDist = Math.Sqrt( RMath.SquaredDistance( P1, P2 ) );

			//DoubleArray S3 = S1.Subtract( S2 );

			//double sDist = Distance( S1.ToVector().ToArray(), S2.ToVector().ToArray() );
			//Console.WriteLine( "R-dist 10 <-> 9: " + pDist );
			//Console.WriteLine( "S-dist 10 <-> 9: " + sDist );

			//double sDist2 = Distance( RMath.Log( identity, ( covs[ 3 ] as DataItemCovarianceMatrix ).value ).ToVector().ToArray(), RMath.Log( identity, ( covs[ 7 ] as DataItemCovarianceMatrix ).value ).ToVector().ToArray() );
			//double rDist2 = Math.Sqrt( RMath.SquaredDistance( ( covs[ 3 ] as DataItemCovarianceMatrix ).value, ( covs[ 7 ] as DataItemCovarianceMatrix ).value ) );
			//Console.WriteLine( "S-dist 3 <-> 7: " + sDist2 + " r: " + rDist2 + " V: " + Vector2.Distance( v1, v2 ) );

			//plot2D.Show();
			//plot1D.Show();
			//plot2D.Location = new System.Drawing.Point( 0, 0 );
			//plot1D.Location = new System.Drawing.Point( plot2D.Size.Width, 0 );

			//Application.DoEvents();
			return covs;
		}

		public static double Distance( double[] p1, double[] p2 )
		{
			double fSum = 0;
			for( int i = 0; i < p1.Length; i++ )
			{
				double fDifference = ( p1[ i ] - p2[ i ] );
				fSum += fDifference * fDifference;
			}
			return Math.Sqrt( fSum );
		}
	}
}