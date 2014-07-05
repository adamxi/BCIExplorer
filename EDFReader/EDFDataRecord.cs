/*
 * Modified version of the EDF project at https://edf.codeplex.com/.
*/

using System;
using System.Collections.Generic;

namespace EDFReader
{
	/// <summary>
	/// A DataRecord holds all of the signals/channels for a defined interval.
	/// Each of the signals/channels has all of the samples for that interval bound to it.
	/// </summary>
	[Serializable]
	public class EDFDataRecord : Dictionary<string, double[]>
	{
	}
}