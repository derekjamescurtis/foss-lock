using System;

namespace HardwareFingerprint
{
	/// <summary>
	/// Defines how higher-tiered Fingerprinters reveal their data to clients
	/// (which is simply through properties that call down to corresponding
	/// methods).
	/// </summary>
	public abstract class Fingerprint : IHardwareIdentifiers
	{
		public String macAddress
		{
			get
			{
				return getMacAddress();
			}
		}

		public abstract String cpuID
		{
			get
			{
				return getCPUID();
			}
		}

		public abstract String motherboardID
		{
			get
			{
				return getMotherboardID();
			}
		}

		public abstract String primaryHddID
		{
			get
			{
				return getPrimaryHDDID();
			}
		}

		public abstract String biosID
		{
			get
			{
				return getBIOSID();
			}
		}

		public abstract String systemUUID
		{
			get
			{
				return getSystemUUID();
			}
		}

		public abstract String videoCardID
		{
			get
			{
				return getVideoCardID();
			}
		}

		private abstract String getMacAddress();
		private abstract String getCPUID();
		private abstract String getMotherboardID();
		private abstract String getPrimaryHDDID();
		private abstract String getBIOSID();
		private abstract String getSystemUUID();
		private abstract String getVideoCardID();
	}
}

