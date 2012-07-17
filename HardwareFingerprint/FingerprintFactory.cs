using System;

namespace FossLock.HardwareFingerprint
{
	public class FingerprintFactory
	{
		/// <summary>
		/// Used to determine the OS the client is running on.
		/// 
		/// Pulled from the Mono technical documentation at 
		/// http://mono-project.com/FAQ:_Technical#How_to_detect_the_execution_platform_.3F
		/// they really haven't made any great efforts to maintain
		/// consistency with enumeration values for OSVersion. The
		/// skinny version is that to check if the client is running
		/// on Unix, this variable's value will be either a 4, 6, or
		/// 128. Any other value and it can be assumed the client is running
		/// a non-Unix system (we can assume that they're running Windows)
		/// </summary>
		public static int clientOSVersion
		{
			get
			{
				return (int)Environment.OSVersion.Platform;
			}
		}

		private FingerprintFactory ()
		{
		}

		/// <summary>
		/// Attempts to get the system's hardware fingerprinter. Check the
		/// documentation on the clientOSVersion property for the sanity
		/// behind the supposed magic numbers in the switch construct.
		/// </summary>
		/// <returns>
		/// The system fingerprinter.
		/// </returns>
		public static IHardwareIdentifiers getSystemFingerprinter ()
		{
			switch (FingerprintFactory.clientOSVersion)
			{
				case 4:
				case 6:
				case 128:
					return new LinuxFingerprint();
				default:
					return new WindowsFingerprint();
			}
		}
	}
}

