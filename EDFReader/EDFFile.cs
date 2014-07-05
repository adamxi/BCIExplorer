/*
 * Modified version of the EDF project at https://edf.codeplex.com/.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EDFReader
{
	[Serializable]
	public class EDFFile
	{
		private EDFHeader header;
		private EDFDataRecord dataRecords;
		
		public EDFFile()
		{
			header = new EDFHeader();
			dataRecords = new EDFDataRecord();
		}

		public EDFFile Copy()
		{
			EDFFile file = new EDFFile();
			Header.CopyTo( file.Header );
			file.FilePath = FilePath;

			foreach( EDFSignal signal in Header.Signals )
			{
				file.addSignal( signal, dataRecords[ signal.IndexNumberWithLabel ] );
			}

			return file;
		}

		#region Properties
		public EDFHeader Header
		{
			get { return header; }
		}

		public EDFDataRecord DataRecords
		{
			get { return dataRecords; }
			set { dataRecords = value; }
		}

		public int SamplesPerSecond { get; private set; }

		public int SampleCount { get; set; }

		public float SamplePeriod { get; private set; }
		#endregion

		public void readFile( string file_path )
		{
			FilePath = file_path;
			//open the file to read the header
			FileStream file = new FileStream( file_path, FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader( file );
			readStream( sr );
			file.Close();
			sr.Close();
		}

		public string FilePath { get; set; }

		public void readStream( StreamReader sr )
		{
			parseHeaderStream( sr );
			parseDataRecordStream( sr );
		}

		public byte[] getEDFFileBytes()
		{
			System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
			byte[] byteArray = encoding.GetBytes( this.Header.ToString().ToCharArray() );
			List<byte> byteList = new List<byte>( byteArray );
			byteList.AddRange( getCompressedDataRecordsBytes() );
			return byteList.ToArray();
		}

		public List<byte> getCompressedDataRecordsBytes()
		{
			List<byte> byteList = new List<byte>();
			byte[] byteArraySample = new byte[ 2 ];

			foreach( EDFSignal signal in this.Header.Signals )
			{
				foreach( float sample in this.DataRecords[ signal.IndexNumberWithLabel ] )
				{
					byteArraySample = BitConverter.GetBytes( Convert.ToInt16( ( sample / signal.AmplifierGain ) - signal.Offset ) );
					byteList.Add( byteArraySample[ 0 ] );
					byteList.Add( byteArraySample[ 1 ] );
				}
			}
			return byteList;
		}

		public void saveFile( string filePath )
		{
			if( File.Exists( filePath ) )
			{
				File.Delete( filePath );
			}
			FileStream newFile = new FileStream( filePath, FileMode.CreateNew, FileAccess.Write );

			StreamWriter sw = new StreamWriter( newFile );
			this.Header.NumberOfDataRecords = this.DataRecords.Count;

			char[] headerCharArray = this.Header.ToString().ToCharArray();
			sw.Write( headerCharArray, 0, headerCharArray.Length );
			sw.Flush();

			newFile.Seek( ( 256 + this.Header.NumberOfSignalsInDataRecord * 256 ), SeekOrigin.Begin );
			BinaryWriter bw = new BinaryWriter( newFile );

			byte[] byteList = getCompressedDataRecordsBytes().ToArray();

			bw.Write( byteList, 0, byteList.Length );
			bw.Flush();
			sw.Close();
			bw.Close();
			newFile.Close();
		}

		private void parseHeaderStream( StreamReader sr )
		{
			//parse the header to get the number of Signals (size of the Singal Header)
			char[] header = new char[ 256 ];
			sr.ReadBlock( header, 0, 256 );
			this.header = new EDFHeader( header );

			//parse the signals within the header
			char[] signals = new char[ this.Header.NumberOfSignalsInDataRecord * 256 ];
			sr.ReadBlock( signals, 0, this.Header.NumberOfSignalsInDataRecord * 256 );
			this.Header.parseSignals( signals );
		}

		private void parseDataRecordStream( StreamReader sr )
		{
			//set the seek position in the file stream to the beginning of the data records.
			sr.BaseStream.Seek( ( 256 + Header.NumberOfSignalsInDataRecord * 256 ), SeekOrigin.Begin );

			int dataRecordSize = 0;
			int maxSampleCount = 0;
			foreach( EDFSignal signal in Header.Signals )
			{
				int sampleCount = signal.NumberOfSamplesPerDataRecord;
				if( sampleCount > maxSampleCount )
				{
					maxSampleCount = sampleCount;
				}

				dataRecordSize += sampleCount;
				signal.SamplePeriodWithinDataRecord = Header.DurationOfDataRecordInSeconds / (float)sampleCount;
				dataRecords.Add( signal.IndexNumberWithLabel, new double[ sampleCount * Header.NumberOfDataRecords ] );
			}
			//Matrix<double> data = DenseMatrix.Create( Header.Signals.Count, maxSampleCount * Header.NumberOfDataRecords, delegate { return 0; } );

			dataRecordSize *= 2;
			byte[] dataRecordBytes = new byte[ dataRecordSize ];

			int readCount = 0;
			while( sr.BaseStream.Read( dataRecordBytes, 0, dataRecordSize ) > 0 )
			{
				EDFDataRecord dataRecord = new EDFDataRecord();
				int samplesWritten = 0;

				for( int row = 0; row < header.Signals.Count; row++ )
				{
					EDFSignal signal = header.Signals[ row ];
					int sampleCount = signal.NumberOfSamplesPerDataRecord;
					double[] samples = dataRecords[ signal.IndexNumberWithLabel ];
					int offset = (int)signal.Offset;
					float amplifierGain = signal.AmplifierGain;

					int startIndex = readCount * sampleCount;
					int endIndex = startIndex + sampleCount;

					for( int i = startIndex; i < endIndex; i++ )
					{
						double num = ( BitConverter.ToInt16( dataRecordBytes, samplesWritten ) + offset ) * amplifierGain;
						samples[ i ] = num;
						samplesWritten += 2;
					}
				}
				readCount++;
			}
		}

		public void deleteSignal( EDFSignal signal )
		{
			if( Header.Signals.Contains( signal ) )
			{
				//Remove Signal DataRecords
				foreach( EDFSignal s in Header.Signals )
				{
					if( s.IndexNumberWithLabel.Equals( signal.IndexNumberWithLabel ) )
					{
						DataRecords.Remove( signal.IndexNumberWithLabel );
					}
				}

				//After removing the DataRecords then Remove the Signal from the Header
				Header.Signals.Remove( signal );

				//Finally decrement the NumberOfSignals in the Header by 1
				Header.NumberOfSignalsInDataRecord = Header.NumberOfSignalsInDataRecord - 1;

				//Change the Number Of Bytes in the Header.
				Header.NumberOfBytes = 256 + ( 256 * Header.Signals.Count );
			}
		}

		public void addSignal( EDFSignal signal, double[] samples )
		{
			if( Header.Signals.Contains( signal ) )
			{
				throw new Exception( "Signal duplicate" );
				//deleteSignal( signal );
			}

			//Remove Signal DataRecords
			int sampleCount = signal.NumberOfSamplesPerDataRecord;

			DataRecords.Add( signal.IndexNumberWithLabel, samples );

			//After removing the DataRecords then Remove the Signal from the Header
			Header.Signals.Add( signal );

			//Finally increment the NumberOfSignals in the Header by 1
			Header.NumberOfSignalsInDataRecord = Header.NumberOfSignalsInDataRecord + 1;

			//Change the Number Of Bytes in the Header.
			Header.NumberOfBytes = 256 + ( 256 * Header.Signals.Count );

			if( header.Signals.Count == 1 )
			{
				UpdateGlobals();
			}
		}

		public void exportAsCompumedics( string file_path )
		{
			foreach( EDFSignal signal in this.Header.Signals )
			{
				string signal_name = this.Header.StartDateTime.ToString( "MMddyyyy_HHmm" ) + "_" + signal.Label;
				string new_path = string.Empty;
				if( file_path.LastIndexOf( '/' ) == file_path.Length )
				{
					new_path = file_path + signal_name.Replace( ' ', '_' );
				}
				else
				{
					new_path = file_path + '/' + signal_name.Replace( ' ', '_' );
				}

				if( File.Exists( new_path ) )
				{
					File.Delete( new_path );
				}
				FileStream newFile = new FileStream( new_path, FileMode.CreateNew, FileAccess.Write );

				StreamWriter sw = new StreamWriter( newFile );

				if( signal.NumberOfSamplesPerDataRecord <= 0 )
				{
					//need to pad it to be sampled every second.
					sw.WriteLine( signal.Label + " " + "RATE:1.0Hz" );
				}
				else
				{
					sw.WriteLine( signal.Label + " " + "RATE:" + Math.Round( (double)( signal.NumberOfSamplesPerDataRecord / this.Header.DurationOfDataRecordInSeconds ), 2 ) + "Hz" );
				}

				foreach( float sample in this.DataRecords[ signal.IndexNumberWithLabel ] )
				{
					sw.WriteLine( sample );
				}
				sw.Flush();
			}
		}

		private void UpdateGlobals()
		{
			EDFSignal signal = header.Signals[ 0 ];
			SamplesPerSecond = signal.NumberOfSamplesPerDataRecord;
			SamplePeriod = signal.SamplePeriodWithinDataRecord;
			SampleCount = dataRecords.First().Value.Length;
		}
	}
}