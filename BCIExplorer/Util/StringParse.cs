using System;
using System.Globalization;

namespace BCIExplorer.Util
{
	public static class StringParse
	{
		/// <summary>
		/// Parses a value from a string containing either an absolute or percentual number.
		/// </summary>
		/// <typeparam name="T">Value type to parse result as.</typeparam>
		/// <param name="valueString">String containing the value. Can be defined as an absolute or percentual value. E.g. "240" or "50%".</param>
		/// <param name="relativeValue">Relative value to calculate result from if the value string contains a percentual number.</param>
		/// <param name="boundsCheck">If true the result cannot be smaller than 0 or bigger than the relative value. If the string is percentual it cannot be smaller than 0% or bigger than 100%.</param>
		public static T ValueString<T>( string valueString, object relativeValue, bool boundsCheck )
		{
			string s;
			return ValueString<T>( valueString, relativeValue, boundsCheck, out s );
		}

		/// <summary>
		/// Parses a value from a string containing either an absolute or percentual number.
		/// </summary>
		/// <typeparam name="T">Value type to parse result as.</typeparam>
		/// <param name="valueString">String containing the value. Can be defined as an absolute or percentual value. E.g. "240" or "50%".</param>
		/// <param name="relativeValue">Relative value to calculate result from if the value string contains a percentual number.</param>
		/// <param name="boundsCheck">If true the result cannot be smaller than 0 or bigger than the relative value. If the string is percentual it cannot be smaller than 0% or bigger than 100%.</param>
		/// <param name="var">The string to store the result in. E.g. "240" or "50%".</param>
		public static T ValueString<T>( string valueString, object relativeValue, bool boundsCheck, out string var )
		{
			string decimalChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			valueString = valueString.Replace( ".", decimalChar );
			valueString = valueString.Replace( ",", decimalChar );

			double val;
			double tmpRelativeValue = Convert.ToDouble( relativeValue );

			if( valueString.EndsWith( "%" ) )
			{
				valueString = valueString.Replace( "%", string.Empty );
				val = double.Parse( valueString );

				if( boundsCheck )
				{
					if( val > 100 )
					{
						val = 100;
					}
					else if( val < 0 )
					{
						val = 0;
					}
				}

				var = val.ToString() + "%";
				val = ( val * tmpRelativeValue ) * 0.01f;
			}
			else
			{
				val = double.Parse( valueString );

				if( boundsCheck )
				{
					if( val > tmpRelativeValue )
					{
						val = tmpRelativeValue;
					}
					else if( val < 0 )
					{
						val = 0;
					}
				}

				var = val.ToString();
			}

			return (T)Convert.ChangeType( val, typeof( T ) );
		}
	}
}