namespace FossLock.Core
{
    /// <summary>
    /// Indicates the versioning style for a given product.
    /// </summary>
    public enum VersioningStyle
    {
        /// <summary>
        /// Versioning style recommended at http://www.semver.org.  Format is Major.Minor.Patch.
        /// </summary>
        Semantic,

        /// <summary>
        /// Versioning style compatible with the .NET <seealso cref="System.Version"/> object.  Format for this version type is Major.Minor.[Build.[Revision]].
        /// </summary>
        DotNet,
    }
}
