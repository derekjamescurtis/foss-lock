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
	public class WindowsFingerprint : Fingerprint
	{
		public WindowsFingerprint ()
		{
		}

		private override String getMACAddress ()
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

		private override String getBIOSId ()
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

