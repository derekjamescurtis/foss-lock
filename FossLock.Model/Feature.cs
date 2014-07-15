using FossLock.Model.Base;

namespace FossLock.Model
{
    /// <summary>
    ///     An optional feature that may be purchased as part of a license.
    /// </summary>
    public class Feature : NamedEntityBase
    {
        /// <summary>
        ///     The product that this feature is available for.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        ///     Any additional information that describes this product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     The maximum number of times this feature can be added
        ///     to a product's license.  Ex, a property management system
        ///     that is billed out based on the number of 'rooms' available.
        /// </summary>
        public int MaximumAllowedPerLicense { get; set; }
    }
}