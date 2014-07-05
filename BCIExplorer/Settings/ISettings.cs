public interface ISettings
{
	/// <summary>
	/// Sets the current instance of the class. Inheriting classes must implement this method with a type cast of 'object o' to its own type.
	/// </summary>
	/// <param name="o">Instance of the class to set.</param>
	void SetDefault( ISettings o );
}