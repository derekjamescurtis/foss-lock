# FossLock.HardwareFingerprint

## Synopsis

An assembly for generating hardware identification strings 
for uniquely-identifying the system that our software is installed on.

On Windows, this is accomplished using [WMI (Windows Management Instrumentation)
](http://technet.microsoft.com/en-us/library/cc181125.aspx)

On Linux, this is done using the bash shell to cat various devices.

We're still missing support for identifying when we're running on a Mac OS 
platform, but from my limited knowledge of OS X, we may be able to use most
of the LinuxFingerprint.cs class.

## Notes

This assembly references some runtime-specific libraries.

- Mono.Posix
- System.Management
- System.Management.Instrumentation

All 3 assemblies have been included in this project under the LocalRefs
directory and the assembly build has been configured to embed these
inside the FossLock.HardwareFingerprint.dll assembly when it is built.