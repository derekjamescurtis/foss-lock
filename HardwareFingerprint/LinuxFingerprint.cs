using System;

namespace HardwareFingerprint
{
	/// <summary>
	/// The Fingerprint class responsible for generating locking
	/// mechanism data on the Linux platform.
	/// 
	/// There are a lot of stipulations to building this class since
	/// there is no one universal method for querying hardware-specific
	/// information like there is on Windows (for example, there is no
	/// WMI implementation in Mono and nothing like it exists on Linux).
	/// Thus several programs are run, depending upon the information
	/// required, to obtain said information.
	/// 
	/// Currently, three programs are used to get hardware information.
	/// The first is dmidecode which provides certain information stored
	/// in the BIOS. Two issues with using this program are that it
	/// requires root access to use and secondly that it doesn't provide
	/// enough data to be used as the sole backer. The second program used
	/// is lspci with verbosity flags enabled (either two or three tiers)
	/// and with the k flag (used to poll the kernel about drivers controlling
	/// the hardware and their respective modules so that values can attempt
	/// to be derived if the proper value requested isn't immediately
	/// available).
	/// 
	/// It should also be noted too that if the client is running Linux
	/// under a hosted (type 2) hypervisor, this class is nearly useless.
	/// I still haven't tested this under a bare-metal (type 1) hypervisors.
	/// </summary>
	public class LinuxFingerprint : IHardwareIdentifiers
	{
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

		public LinuxFingerprint ()
		{
		}
	}
}

