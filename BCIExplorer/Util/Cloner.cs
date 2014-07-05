using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

public static class Cloner {
	private static BinaryFormatter formatter;

	static Cloner() {
		formatter = new BinaryFormatter();
		formatter.Binder = new Binder();
		formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
	}

	/// <summary>
	/// Returns a cloned object using serialization.
	/// </summary>
	/// http://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-an-object-in-net-c-specifically.
	/// <typeparam name="T">Object type.</typeparam>
	/// <param name="obj">Object to clone.</param>
	public static T DeepClone<T>(T obj) {
		using(MemoryStream ms = new MemoryStream()) {
			formatter.Serialize(ms, obj);
			ms.Position = 0;

			return (T)formatter.Deserialize(ms);
		}
	}

	private class Binder: SerializationBinder {
		public override Type BindToType(string assemblyName, string typeName) {
			string shortAssemblyName = assemblyName.Split(',')[0];
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach(Assembly assembly in assemblies) {
				if(shortAssemblyName.Equals(assembly.FullName.Split(',')[0])) {
					return assembly.GetType(typeName);
				}
			}

			return null;
		}
	}
}