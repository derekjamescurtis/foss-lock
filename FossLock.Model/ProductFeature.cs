using FossLock.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FossLock.Model
{
    /// <summary> An optional feature that may be purchased as part of a license.
    /// </summary>
    public class ProductFeature : EntityBase
    {

        /// <summary> The product that this feature is available for.
        /// </summary>
        [Required]
        public virtual Product Product { get; set; }

        /// <summary> A human-readable name for this product.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary> Any additional information that describes this product.
        /// </summary>
        public string Description { get; set; }
    }
}