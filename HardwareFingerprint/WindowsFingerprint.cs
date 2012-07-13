using System;

namespace HardwareFingerprint
{
	/// <summary>
	/// The Fingerprint class responsible for generating locking
	/// mechanism data on the Windows platform.
	/// 
	/// This particular Fingerprint uses WMI for .NET in order
	/// to obtain the requested hardware data. The only primary
	/// similarity to its Linux counterpart is that it may have
	/// to derive values from peripheral data in order to obtain
	/// what is actually requested in some cases.
	/// </summary>
	public class WindowsFingerprint : IHardwareIdentifiers
	{
		public WindowsFingerprint ()
		{
		}

		public String MACAddress
		{
			get
			{
				// TODO: Insert code to obtain MAC Address
				return "";
			}
		}

		public String CPUID
		{
			get
			{
				// TODO: Insert code to obtain CPU ID
				return "";
			}
		}

		public String MotherboardID
		{
			get
			{
				// TODO: Insert code to obtain Motherboard ID
				return "";
			}
		}

		public String PrimaryHDDID
		{
			get
			{
				// TODO: Insert code to obtain Primary HDD ID
				return "";
			}
		}

		public String BIOSID
		{
			get
			{
				// TODO: Insert code to obtain BIOS ID
				return "";
			}
		}

		public String SystemUUID
		{
			get
			{
				// TODO: Insert code to obtain System UUID
				return "";
			}
		}

		public String VideoCardID
		{
			get
			{
				// TODO: Insert code to obtain Video Card ID
				return "";
			}
		}
	}
}

