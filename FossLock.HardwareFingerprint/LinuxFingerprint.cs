using System;
using System.Diagnostics;

namespace FossLock.HardwareFingerprint
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
	internal class LinuxFingerprint : IHardwareIdentifiers
	{
		Process forkedProcess = null;
		
		public String MACAddress
		{
			get
			{
				String val = String.Empty;
				
				/*
				 * Uses a bash shell command to get the MAC address of the primary
				 * network card (default is eth0 on Linux). The actual command sequence is
				 * listed below:
				 *
				 * ifconfig eth0 | grep HWaddr | sed s/.*HWaddr\\s/''/
				 *
				 * Because of the awkward nature of how ProcessStartInfo handles populating
				 * and launching a process, the actual program (what ProcessStartInfo refers
				 * to as FileName) is set as bash and the argument passed to bash is the
				 * actual line that gets the MAC address with quotations around the entire
				 * program list and single quotes around the regex used by sed. Thankfully,
				 * ifconfig, grep, and sed don't require root access so making this a silent
				 * operation is fairly straightforward.
				 */
				forkedProcess = new Process();
				
				// Keep the Process silent
				forkedProcess.EnableRaisingEvents = false;
				
				// Launch bash
				forkedProcess.StartInfo.FileName = "bash";
				
				/*
				 * Since we have to launch bash instead of actually launching the string of
				 * commands directly, we need to pack them into a proper String and pass that
				 * to bash.
				 */ 
				forkedProcess.StartInfo.Arguments = "-c \"ifconfig eth0 | grep HWaddr | sed 's/.*HWaddr\\s//'\"";
				
				// Capture stdio
				forkedProcess.StartInfo.RedirectStandardOutput = true;
				
				// Hide forked shell to keep Process silent
				forkedProcess.StartInfo.UseShellExecute = false;
				forkedProcess.StartInfo.CreateNoWindow = true;
				
				// Launch the process
				forkedProcess.Start();
				
				// Grab the result of the Process (the MAC address of eth0)
				val = forkedProcess.StandardOutput.ReadToEnd();
				
				return val;
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
				/*
				 * This field is probably the second most difficult to obtain.
				 * 
				 * Under Unix, hard drives are referenced differently depending upon several factors.
				 * If the device is connected via PATA/IDE, the first letter of the drive ID in /proc is
				 * 'h'. If the device is connected via SATA/SCSI, the first letter of the ID in /proc
				 * is 's'. Because of this, there's no safe way to make assumptions about the client's
				 * configuration (even though a majority of modern systems use SATA, there are still
				 * a deal of Linux systems that run PATA drives).
				 * 
				 * The second problem is that a deal of terminal-based utilties tend to either
				 * overlook the drives entirely or only grab extremely basic metrics (like the
				 * geometry only) even with root permissions.
				 * 
				 * hdparm is the best utility (probably the only reliable one) for getting the drive
				 * information required. It gets its data directly from the Linux core headers (hdreg.h)
				 * so things like model, manufacturer, and serial number.
				 * 
				 * The first thing that needs done is to determine if the user's drives are being
				 * references as either PATA or SATA devices. Once that's done, it then becomes trivial
				 * to get the drive's data.
				 */ 
				String val = String.Empty;
				String driveTypeString = String.Empty;
				
				// Configure the process for getting a shallow list of mounted devices
				forkedProcess = new Process();
				forkedProcess.EnableRaisingEvents = false;
				forkedProcess.StartInfo.UseShellExecute = false;
				forkedProcess.StartInfo.CreateNoWindow = true;
				forkedProcess.StartInfo.RedirectStandardOutput = true;
				forkedProcess.StartInfo.FileName = "bash";
				forkedProcess.StartInfo.Arguments = "-c \"cat /proc/mounts | grep -e '/dev/[s|h]d'\"";
				forkedProcess.Start();
				driveTypeString = forkedProcess.StandardOutput.ReadToEnd();
				
				// Check to see what type of drive was returned
                if (driveTypeString.Contains("/dev/sd"))
                {
                    forkedProcess = new Process();
                    forkedProcess.EnableRaisingEvents = true;
                    forkedProcess.StartInfo.UseShellExecute = false;
                    forkedProcess.StartInfo.CreateNoWindow = true;
                    forkedProcess.StartInfo.RedirectStandardOutput = true;
                    forkedProcess.StartInfo.FileName = "bash";
                    forkedProcess.StartInfo.Arguments = "-c \"sudo hdparm -i /dev/sda | grep SerialNo | sed 's/.*SerialNo=//'\"";

                    forkedProcess.Start();
                    val = forkedProcess.StandardOutput.ReadToEnd();

                    return val;
                }
                else if (driveTypeString.Contains("/dev/hd"))
                {
                    forkedProcess = new Process();
                    forkedProcess.EnableRaisingEvents = true;
                    forkedProcess.StartInfo.UseShellExecute = false;
                    forkedProcess.StartInfo.CreateNoWindow = true;
                    forkedProcess.StartInfo.RedirectStandardOutput = true;
                    forkedProcess.StartInfo.FileName = "bash";
                    forkedProcess.StartInfo.Arguments = "-c \"sudo hdparm -i /dev/hda | grep SerialNo | sed 's/.*SerialNo=//'\"";

                    forkedProcess.Start();
                    val = forkedProcess.StandardOutput.ReadToEnd();

                    return val;
                }

                else
                    throw new Exception();
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