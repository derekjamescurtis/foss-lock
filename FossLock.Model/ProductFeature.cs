using FossLock.Model.Base;

namespace FossLock.Model
{
    /// <summary> An optional feature that may be purchased as part of a license.
    /// </summary>
    public class ProductFeature : NamedEntityBase
    {

        /// <summary> The product that this feature is available for.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary> Any additional information that describes this product.
        /// </summary>
        public string Description { get; set; }
    }
}