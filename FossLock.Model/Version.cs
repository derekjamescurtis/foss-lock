using FossLock.Model.Base;

namespace FossLock.Model
{
    /// <summary>
    ///     An individual version of a product.
    ///     In FossLock, <see cref="Customers"/> actually license a particular
    ///     ProductVersion, instead of the Product itself.
    /// </summary>
    public class Version : EntityBase
    {
        /// <summary>
        ///     String representation of the versioning data.
        ///     This string must be parsable to either a <see cref="System.Version"/> instance
        ///     or a <see cref="Summerset.SemanticVersion"/> instance, depending on which type
        ///     is being used for the product.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     A reference to the <see cref="Product"/> this
        ///     version is associated with.
        /// </summary>
        public virtual Product Product { get; set; }
    }
}