using System;

namespace HardwareFingerprint
{
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
				getCPUID();
			}
		}

		public abstract String motherboardID
		{
			get
			{
				getMotherboardID();
			}
		}

		public abstract String primaryHddID
		{
			get
			{
				getPrimaryHDDID();
			}
		}

		public abstract String biosID
		{
			get
			{
				getBIOSID();
			}
		}

		public abstract String systemUUID
		{
			get
			{
				getSystemUUID();
			}
		}

		public abstract String videoCardID
		{
			get
			{
				getVideoCardID();
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

