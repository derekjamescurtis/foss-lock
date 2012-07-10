using System;

namespace FossLock.Core
{
	/// <summary>
	/// A memento-pattern caretaker object that creates a shallow copy of the originator provided in the constructor.
	/// </summary>
	public class ShallowCaretaker<T>
	{

		/// <summary>
		/// Initializes a new instance of the ShallowCaretaker class and creates a shallow-copy of the provided originator.
		/// </summary>
		/// <param name='originator'>
		/// Originator.
		/// </param>
		public ShallowCaretaker (T originator)
		{

			// get the default constructor and instantiate our _memento field
			var mementoCtorInfo = typeof(T).GetConstructor(new Type[]{});
			_memento = (T)mementoCtorInfo.Invoke (new object[]{});


			// use reflections to get all properties in originator
			var properties = originator.GetType().GetProperties(System.Reflection.BindingFlags.Public);		
			foreach(var prop in properties)
			{
				// skip properties that aren't read+write accessible, or indexed properties
				if (prop.CanRead && prop.CanWrite && prop.GetIndexParameters().Length > 0)
				{
					object valueToSet = prop.GetValue(originator, null);
					prop.SetValue(_memento, valueToSet, null);
				}
			}
		}
		T _memento;

		/// <summary>
		/// Gets a reference to the Memento that was saved by the constructor.
		/// </summary>
		public T SavedState
		{
			get { return _memento; }
		}

		/// <summary>
		/// Restores the state of the provided argument based on this instances Memento.
		/// </summary>
		/// <param name='restoreTo'>
		/// The object that will have it's properties restored based on the current Memento.
		/// </param>
		/// <remarks>
		/// This doesn't totally agree with the memento pattern, but using reflection this is significantly better.
		/// </remarks>
		public void RestoreState(T restoreTo)
		{
			var properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public);		
			foreach(var prop in properties)
			{
				// skip properties that aren't read+write accessible, or indexed properties
				if (prop.CanRead && prop.CanWrite && prop.GetIndexParameters().Length > 0)
				{
					// get the value from the memento
					object mementoValue = prop.GetValue(_memento, null);

					// apply to the restoreTo object
					prop.SetValue(restoreTo, mementoValue, null);
				}
			}
		}

	}
}

