using System;

namespace FossLock
{
	/// <summary>
	/// Enumeration of possible identifiers that a license can be locked to.
	/// </summary>
	[Flags]
	public enum LockProperties
	{

		/// <summary>
		/// License is not locked to any hardware identifiers.
		/// </summary>
		None 			= 0x0,

		/// <summary>
		/// Locks to a CPU-related identifier.
		/// </summary>
		CPU 			= 1 << 0,

		/// <summary>
		/// Locks to a motherboard-related identifier.
		/// </summary>
		Motherboard 	= 1 << 1,

		/// <summary>
		/// Locks to a harddisk-related identifier.
		/// </summary>
		Harddisk 		= 1 << 2,

		/// <summary>
		/// Locks to the video identifier.
		/// </summary>
		Video 			= 1 << 3,

		/// <summary>
		/// Locks to the BIOS.
		/// </summary>
		BIOS 			= 1 << 4,

		/// <summary>
		/// Locks to a MAC address.
		/// </summary>
		MACAddress 		= 1 << 5,

		/// <summary>
		/// Locks to the system UUID number.
		/// </summary>
		UUID 			= 1 << 6
	}
}

