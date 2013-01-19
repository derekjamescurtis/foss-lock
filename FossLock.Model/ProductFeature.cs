using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossLock.Model
{
    /// <summary>
    /// An optional feature that may be purchased as part of a license.
    /// </summary>
    public class ProductFeature
    {
        /// <summary>
        /// Uniquely identifies this object within the database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The product that this feature is available for.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// A human-readable name for this product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Any additional information that describes this product.
        /// </summary>
        public string Description { get; set; }
    }
}