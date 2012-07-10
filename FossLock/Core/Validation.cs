using System;

namespace FossLock.Core
{
	public static class Validation
	{


		//TODO: This method needs to pass string exception messages to all of the exception constructors here.
		public static void ValidateString(string value, int minLength, int maxLength, bool allowNull = false)
		{
			if (value == null)
			{
				if (!allowNull)
					throw new ArgumentNullException();
				else
					return;
			}
			else
			{
				if (value.Length < minLength)
					throw new ArgumentOutOfRangeException();
				else if (value.Length > maxLength)
					throw new ArgumentOutOfRangeException();
				else
					return;
			}

		}
	}
}

