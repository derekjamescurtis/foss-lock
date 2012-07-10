using System;

namespace FossLock.Core
{
	public class ShallowCaretaker<T>
	{
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


		public T SavedState
		{
			get { return _memento; }
		}

	}
}

