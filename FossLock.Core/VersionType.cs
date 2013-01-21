using System;
namespace FossLock.Core
{
    /// <summary>
    /// Indicates the development stage of this version.
    /// This is a flag-type enumeration.
    /// </summary>
    [Flags]
    public enum VersionType
    {
        /// <summary>
        /// Planning/Initial development stage.
        /// </summary>
        PreAlpha = 0x0,

        /// <summary>
        /// Development cycle in which features are still being constructed.
        /// </summary>
        Alpha = 1 << 0,

        /// <summary>
        /// Development cycle in which features are tested.
        /// </summary>
        Beta = 1 << 1,

        /// <summary>
        /// All features are complete and no significant bugs in this product.
        /// </summary>
        ReleaseCandidate = 1 << 2,

        /// <summary>
        /// Early release to dealerships/resellers.
        /// </summary>
        ReleaseToManufacturing = 1 << 3,

        /// <summary>
        /// Software deployed to consumers.
        /// </summary>
        GeneralAvailability = 1 << 4,

        /// <summary>
        /// Software no longer under active development.
        /// </summary>
        EndOfLife = 1 << 5,
    }
}