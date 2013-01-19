using System;

namespace FossLock.Core
{

	/// <summary>
	/// A static class that provides some commonly-implemented validation routines.
	/// </summary>
	public static class Validation
	{

		/// <summary>
		/// Validates a provided string meets certain criteria.
		/// </summary>
		/// <param name='value'>
		/// The string that will be validated.
		/// </param>
		/// <param name='minLength'>
		/// The minimum allowable length for validation to pass.
		/// </param>
		/// <param name='maxLength'>
		/// The maximum allowable length for validation to pass.
		/// </param>
		/// <param name='allowNull'>
		/// When <c>true</c>, null references will pass validation; otherwise, validation will fail.
		/// </param>
		public static void ValidateString(string value, int minLength, int maxLength, bool allowNull = false)
		{

			//TODO: This method needs to pass string exception messages to all of the exception constructors here.
			// once I figure out how mono handles resource files.

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

