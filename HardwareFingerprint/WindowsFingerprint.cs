using System;
using System.Management;

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
	/// 
	/// A majority of the code here is borrowed from Derek's
	/// HardwareFingerprint class.
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
				return WindowsFingerprint.WMIInfo("Win32_NetworkAdapterConfiguration", "MACAddress");
			}
		}

		public String CPUID
		{
			get
			{
				String val = WindowsFingerprint.WMIInfo("Win32_Processor", "UniqueId");
				if(val == String.Empty)
				{
					val = WindowsFingerprint.WMIInfo("Win32_Processor", "ProcessodId");
				}

				if(val == String.Empty)
				{
					val = WindowsFingerprint.WMIInfo("Win32_Processor", "Name");
				}

				if(val == String.Empty)
				{
					val = WindowsFingerprint.WMIInfo("Win32_Processor", "Manufacturer");
				}

				return val;
			}
		}

		public String MotherboardID
		{
			get
			{
				String val = String.Empty;

				/*
				 * Pack several values into val with whitespace appended to the end of
				 * each query value so that methods like split can pull the values
				 * out into an array.
				 */
				val += WindowsFingerprint.WMIInfo("Win32_BaseBoard", "Manufacturer") + " ";
				val += WindowsFingerprint.WMIInfo("Win32_BaseBoard", "Model") + " ";
				val += WindowsFingerprint.WMIInfo("Win32_BaseBoard", "PartNumber") + " ";
				val += WindowsFingerprint.WMIInfo("Win32_BaseBoard", "SerialNumber") + " ";

				return val;
			}
		}

		public String PrimaryHDDID
		{
			get
			{
				// Attempt to determine the primary HDD and get the serial number from it
				ManagementClass wmiMgmt = new ManagementClass("Win32_DiskDrive");
				ManagementObjectCollection wmiMgmtCol = wmiMgmt.GetInstances();
				String val = String.Empty;

				foreach(ManagementObject wmiMgmtObj in wmiMgmtCol)
				{
					// Get the physical device ID associated with this current device
					String deviceId = WindowsFingerprint.WMIInfo("Win32_DiskDrive", "DeviceId");
					if(deviceId.Contains("PHYSICALDRIVE0"))
					{
						// Assume that this is the primary physical drive in the system
						val = WindowsFingerprint.WMIInfo("Win32_DiskDrive", "SerialNumber");

						// No need to continue looping
						break;
					}
				}

				return val;
			}
		}

		public String BIOSID
		{
			get
			{
				return WindowsFingerprint.WMIInfo("Win32_Bios", "IdentificationCode");
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

		/// <summary>
		/// Obtain hardware specific information on the Windows platform.
		/// </summary>
		/// <returns>
		/// A String referring to the requested WMI Class's property
		/// </returns>
		/// <param name='win32Class'>
		/// The Win32 class to access
		/// </param>
		/// <param name='win32Property'>
		/// The property to access in the specified Win32 class
		/// </param>
		private static String WMIInfo (String win32Class, String win32Property)
		{
			ManagementClass wmiMgmt = new ManagementClass (win32Class);
			ManagementObjectCollection wmiMgmtCol = wmiMgmt.GetInstances ();
			String propertyValue = String.Empty;

			// Loop through the collection to get the property
			foreach (ManagementObject wmiObject in wmiMgmtCol)
			{
				try
				{
					propertyValue = wmiObject[win32Property].ToString();
					break;
				}
				catch(Exception)
				{
				}
			}

			return propertyValue;
		}
	}
}

