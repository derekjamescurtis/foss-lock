using System;

namespace HardwareFingerprint
{
	/// <summary>
	/// IHardwareIdentifiers Interface
	/// 
	/// An interface that defines methods for polling specific hardware
	/// information that is intended to be platform independent (currently
	/// Linux or Windows).
	/// 
	/// Each method declared here in this interface corresponds to a particular
	/// hardware-based metric that can be retreived by implementing classes.
	/// </summary>
	public interface IHardwareIdentifiers
	{
		String MACAddress
		{
			get;
		}
		String CPUID
		{
			get;
		}
		String MotherboardID
		{
			get;
		}
		String PrimaryHDDID
		{
			get;
		}
		String BIOSID
		{
			get;
		}
		String SystemUUID
		{
			get;
		}
		String VideoCardID
		{
			get;
		}
	}
}

