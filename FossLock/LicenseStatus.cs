using System;

namespace FossLock
{
	public enum LicenseStatus
	{
		/// <summary>
		/// The license is currently avaialble for locking to another hardware platform.  
		/// </summary>
		Available = 0,

		/// <summary>
		/// The license has been activated and is currently locked to a specific hardware fingerprint.
		/// </summary>
		Activated,

		/// <summary>
		/// The license is broken and cannot be relicensed.  
		/// </summary>
		Broken
	}
}

