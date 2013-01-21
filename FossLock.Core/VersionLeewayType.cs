using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FossLock.Core
{
    /// <summary>
    /// Indicates the allowed range of versions a product may be licensed with.
    /// </summary>
    public enum VersionLeewayType
    {
        /// <summary>
        /// Products licensed must be activated with the exact version number specified.
        /// </summary>
        Strict,

        /// <summary>
        /// Products may be licensed with any version within their licensed major version.
        /// </summary>
        WithinSameMajorVersion,

        /// <summary>
        /// Products may be licensed with any version within their minor version.
        /// </summary>
        WithinSameMinorVersion
    }
}
