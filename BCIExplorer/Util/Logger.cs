using System;
using System.Collections.Generic;
using System.Text;

namespace BCIExplorer.Util
{
	public static class Logger
	{
		public static bool PrintToConsole { get; set; }
		private static List<LogItem> log;

		static Logger()
		{
			log = new List<LogItem>();
			Enable();
		}

		public static bool Enabled { get; private set; }

		public static void Enable()
		{
			Enabled = true;
		}

		public static void Disable()
		{
			Enabled = false;
		}

		public static void Clear()
		{
			log = new List<LogItem>();
		}


		public static void NewLine()
		{
			if( Enabled )
			{
				_Log( string.Empty );
			}
		}

		public static void Log( string msg, Level level = Level.Level_0 )
		{
			if( Enabled )
			{
				string logMsg = DateTime.Now.ToString( "HH:mm:ss" ) + ": " + msg;
				_Log( logMsg, level );
				if( PrintToConsole )
				{
					Console.WriteLine( logMsg );
				}
			}
		}

		private static void _Log( string msg, Level level = Level.Level_0 )
		{
			log.Add( new LogItem( msg, level ) );
			if( SharedForms.log != null )
			{
				SharedForms.log.WriteToLog( msg, level );
			}
		}

		public static string GetLog( Level level = Level.Level_5 )
		{
			StringBuilder sb = new StringBuilder();
			foreach( LogItem item in log )
			{
				if( level.CompareTo( item.Level ) >= 0 )
				{
					sb.AppendLine( item.Message );
				}
			}
			return sb.ToString();
		}

		private struct LogItem
		{
			private string message;
			private Level level;

			public LogItem( string message, Level level )
			{
				this.message = message;
				this.level = level;
			}

			public string Message
			{
				get { return message; }
			}

			public Level Level
			{
				get { return level; }
			}
		}

		public enum Level : byte
		{
			Level_0,
			Level_1,
			Level_2,
			Level_3,
			Level_4,
			Level_5,
		}
	}
}