// <copyright file="OnlineIirFilter.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2010 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.Filtering.IIR
{
	using System;
	using System.Linq;

	/// <summary>
	/// Infinite Impulse Response (FIR) Filters need much
	/// less coefficients (and are thus much faster) than
	/// comparable FIR Filters, but are potentially unstable.
	/// IIR Filters are always online and causal. This IIR
	/// Filter implements the canonic Direct Form II structure.
	/// </summary>
	/// <remarks>
	/// System Descripton: H(z) = (b0 + b1*z^-1 + b2*z^-2) / (1 + a1*z^-1 + a2*z^-2)
	/// </remarks>
	public class OnlineIirFilter : OnlineFilter
	{
		double[] bCoefficients, aCoefficients;
		double[] bufferX;
		double[] bufferY;
		readonly int size, halfSize;
		int offset;

		/// <summary>
		/// Infinite Impulse Response (IIR) Filter.
		/// </summary>
		public OnlineIirFilter( double[] a, double[] b ) : this( b.Concat( a ).ToArray() ) { }

		/// <summary>
		/// Infinite Impulse Response (IIR) Filter.
		/// </summary>
		public OnlineIirFilter( double[] coefficients )
		{
			if( null == coefficients )
				throw new ArgumentNullException( "coefficients" );
			if( ( coefficients.Length & 1 ) != 0 )
				throw new ArgumentException( "Even number of coefficients required.", "coefficients" );

			size = coefficients.Length;
			halfSize = size >> 1;
			bCoefficients = new double[ size ];
			aCoefficients = new double[ size ];
			for( int i = 0; i < halfSize; i++ )
			{
				bCoefficients[ i ] = bCoefficients[ halfSize + i ] = coefficients[ i ];
				aCoefficients[ i ] = aCoefficients[ halfSize + i ] = coefficients[ halfSize + i ];
			}
			bufferX = new double[ size ];
			bufferY = new double[ size ];
		}

		/// <summary>
		/// Process a single sample.
		/// </summary>
		public override double ProcessSample( double sample )
		{
			offset = ( offset != 0 ) ? offset - 1 : halfSize - 1;
			bufferX[ offset ] = sample;
			bufferY[ offset ] = 0d;
			double yn = 0d;
			for( int i = 0, j = halfSize - offset; i < halfSize; i++, j++ )
			{
				yn += bufferX[ i ] * bCoefficients[ j ];
			}
			for( int i = 0, j = halfSize - offset; i < halfSize; i++, j++ )
			{
				yn -= bufferY[ i ] * aCoefficients[ j ];
			}
			bufferY[ offset ] = yn;
			return yn;
		}

		/// <summary>
		/// Reset internal state (not coefficients!).
		/// </summary>
		public override void Reset()
		{
			for( int i = 0; i < bufferX.Length; i++ )
			{
				bufferX[ i ] = 0d;
				bufferY[ i ] = 0d;
			}
		}
	}
}