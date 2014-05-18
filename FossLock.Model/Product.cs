using System;
using System.Collections.Generic;
using FossLock.Core;
using FossLock.Model.Base;

namespace FossLock.Model
{

    /// <summary> An object, whose individual versions may be licensed out to customers.  
    /// </summary>
    public class Product : NamedEntityBase
    {
        
        /// <summary> The date this product was initially released.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary> Flag enum that indicates the hardware lock properties that will be required 
        /// by default for activation. This may be overridden on an individual license.
        /// </summary>
        public LockPropertyType DefaultLockProperties { get; set; }

        /// <summary> Indicates whether licensing should fail if 
        /// any of the required hardware identifiers cannot be read.
        /// </summary>
        public bool FailOnNullHardwareIdentifier { get; set; }

        /// <summary>
        /// Flag enum that indicates the methods in which this license may be activated.
        /// </summary>
        public ActivationType PermittedActivationTypes { get; set; }

        /// <summary> Indicates the types of expirations 
        /// permitted for new <see cref="License"/>s
        /// </summary>
        public ExpirationType PermittedExpirationTypes { get; set; }

        /// <summary> Enforces a maximum trial length when issuing new licenses.  
        /// </summary>
        public int? MaximumTrialDays { get; set; }

        /// <summary> Indicates whether this product will use Microsoft's 
        /// <see cref="System.Version"/> object for version numbering, 
        /// or Summerset Software's <see cref="Summerset.SemanticVersion"/> 
        /// object (complies with http://www.semver.org).  
        /// </summary>
        public VersioningStyle VersioningStyle { get; set; }

        /// <summary> Any additional notes about this product.
        /// </summary>
        public string Notes { get; set; }        

        /// <summary> Indicates how strict the system will be when activating.  
        /// <see cref="FossLock.Core.VersionLeewayType"/> has additional details.
        /// </summary>
        public VersionLeewayType VersionLeeway { get; set; }

        /// <summary> Optional collection of features that may be 
        /// licensed in addition to the main product license.
        /// </summary>
        public virtual ICollection<ProductFeature> AvailableFeatures { get; set; }
        
        /// <summary> A collection of versions of this product, 
        /// which are actually the objects licensed out to customers.
        /// </summary>
        public virtual ICollection<ProductVersion> Versions { get; set; }

    }
}