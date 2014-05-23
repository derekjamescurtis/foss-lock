using System;

namespace FossLock.Core
{
    /// <summary>
    ///     Indicates possible choices for how a license when a license will expire.
    ///     This is a flag-type enumeration because Products can have multiple expiration
    ///     types (Licenses, however, cannot).
    /// </summary>
    [Flags]
    public enum ExpirationType
    {
        None = 0x0,

        /// <summary>
        ///     A <see cref="License"/> that does not expire.
        /// </summary>
        Permanent = 1 << 0,

        /// <summary>
        ///     A <see cref="License"/> that expires on a specified calendar date,
        ///     regardless of when the <see cref="License"/> is first activated.
        /// </summary>
        Service = 1 << 1,

        /// <summary>
        ///     A <see cref="License"/> that expires a certain number of calendar
        ///     days after the <see cref="License"/> is first activated.
        /// </summary>
        Trial = 1 << 2,
    }
}
