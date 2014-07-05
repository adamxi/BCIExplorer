#if DEBUG
#define ENABLED
#else
#define ENABLED
#endif

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SharpDXForms.Helper
{
	/// <summary>
	/// Lightweight high performance profiler for debug builds.
	/// Release builds will not include any profiling code.
	/// </summary>
	public struct Profiler
	{
#if ENABLED
		[DllImport( "Kernel32.dll" )]
		private static extern bool QueryPerformanceCounter( out long lpPerformanceCount );
		[DllImport( "Kernel32.dll" )]
		private static extern bool QueryPerformanceFrequency( out long lpFrequency );
		private static double inverseFreq;

		static Profiler()
		{
			long freq;
			if( !QueryPerformanceFrequency( out freq ) )
			{
				throw new Win32Exception();
			}
			inverseFreq = ( 1d / freq );
		}
#endif

		public static Profiler StartNew( string description = null )
		{
			Profiler p = new Profiler( description );
			p.Start();
			return p;
		}

#if ENABLED
		private double totalElapsed;
		private string description;
		private int count;
		private long startTime;
		private double lastElapsed;
		private bool running;
#endif

		public Profiler( string description = null )
		{
#if ENABLED
			this.description = description == null ? null : description + ": ";
			this.count = 0;
			this.startTime = 0;
			this.lastElapsed = 0;
			this.totalElapsed = 0;
			this.running = false;
#endif
		}

		#region Properties
		/// <summary>
		/// Profiler description
		/// </summary>
		public string Description
		{
			get
			{
#if ENABLED
				return description;
#else
				return string.Empty;
#endif
			}
		}

		/// <summary>
		/// How many times this profiler has been started.
		/// </summary>
		public int Count
		{
			get
			{
#if ENABLED
				return count;
#else
				return 0;
#endif
			}
		}

		/// <summary>
		/// Last elapsed time in seconds.
		/// </summary>
		public double LastElapsed
		{
			get
			{
#if ENABLED
				return lastElapsed;
#else
				return 0;
#endif
			}
		}

		/// <summary>
		/// Total elapsed time in seconds.
		/// </summary>
		public double TotalElapsed
		{
			get
			{
#if ENABLED
				return totalElapsed;
#else
				return 0;
#endif
			}
		}

		/// <summary>
		/// True if started and not stopped.
		/// </summary>
		public bool Running
		{
			get
			{
#if ENABLED
				return running;
#else
				return false;
#endif
			}
		}
		#endregion

		/// <summary>
		/// Starts the profiler. Call 'Stop' to benchmark times.
		/// </summary>
		public void Start()
		{
#if ENABLED
			count++;
			running = true;
			QueryPerformanceCounter( out startTime );
#endif
		}

		/// <summary>
		/// Stops the profiler.
		/// </summary>
		/// <param name="printOut">True to print out latest benchmark.</param>
		public void Stop( bool printOut = false )
		{
#if ENABLED
			if( running )
			{
				long stopTime;
				QueryPerformanceCounter( out stopTime );
				running = false;
				lastElapsed = ( stopTime - startTime ) * inverseFreq;
				totalElapsed += lastElapsed;

				if( printOut )
				{
					Console.WriteLine( ToShortString() );
				}
			}
			else
			{
				throw new InvalidOperationException( "Profiler instance has not been started." );
			}
#endif
		}

		/// <summary>
		/// Resets the profiler.
		/// </summary>
		public void Reset()
		{
#if ENABLED
			count = 0;
			lastElapsed = 0;
			totalElapsed = 0;
			running = false;
#endif
		}

		/// <summary>
		/// Returns profiler information as a formated output string. Numbers are in seconds.
		/// </summary>
		/// <param name="tDigits">Number of digits to round total time to [0..15].</param>
		/// <param name="lDigits">Number of digits to round last-time to [0..15].</param>
		public string ToString( int tDigits, int lDigits )
		{
#if ENABLED
			return string.Join( string.Empty, description,
				" -- Total: ", Math.Round( totalElapsed, tDigits, MidpointRounding.AwayFromZero ).ToString(),
				" - Last: ", Math.Round( lastElapsed, lDigits, MidpointRounding.AwayFromZero ).ToString(),
				" - Count: ", count.ToString() );
#else
			return string.Empty;
#endif
		}

		/// <summary>
		/// Returns profiler information as a formated output string. Numbers are in seconds.
		/// </summary>
		/// <param name="tDigits">Number of digits to round total time to [0..15].</param>
		public string ToShortString( int digits = 4 )
		{
#if ENABLED
			return string.Join( string.Empty, description, Math.Round( totalElapsed, digits, MidpointRounding.AwayFromZero ).ToString(), " sec" );
#else
			return string.Empty;
#endif
		}

		public override string ToString()
		{
			return ToShortString();
		}
	}
}