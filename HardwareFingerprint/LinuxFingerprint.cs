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
	/// </summary>
	public class LinuxFingerprint : Fingerprint
	{
		public LinuxFingerprint ()
		{
		}

		private override String getMACAddress()
		{
		}

		private override String getCPUId ()
		{
		}

		private override String getMotherboardId ()
		{
		}

		private override String getPrimaryHDDId ()
		{
		}

		private override String getSystemUUID ()
		{
		}

		private override String getVideoCardId ()
		{
		}
	}
}

