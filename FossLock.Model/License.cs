using FossLock.Core;
using System;
using System.Collections.Generic;

namespace FossLock.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class License
	{
		/// <summary>
		/// Uniquely identifies this instance within the database.
		/// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// A reference to the customer that has purchased this license.
        /// </summary>
        public virtual Customer Customer { get; set; }
        
        /// <summary>
        /// The version of the product that was purchased by this customer.  
        /// </summary>
        public virtual ProductVersion ProductVersion { get; set; }
        
        /// <summary>
        /// The date and time this license was generated (Meaning: 'when was this product sold?').
        /// </summary>
        public DateTimeOffset GenerationDateTime { get; set; }
        
        /// <summary>
        /// If this license is no longer valid, this field is set to indicate when the license was destroyed.
        /// </summary>
        public DateTimeOffset? DestroyedDateTime { get; set; }

        /// <summary>
        /// Determines whether this is a 
        /// </summary>
        public ActivationType ActivationType { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        /// <summary>
        /// Optional information about the license.  
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// On a network license, this will indicate the number of workstations the product is licensed for.
        /// </summary>
        public int? NetworkLicenseCount { get; set; }
        
        /// <summary>
        /// The hardware identifiers that must be returned from the customer to generate the license.
        /// </summary>
        public LockPropertyType RequiredLockProperties { get; set; }
        
        /// <summary>
        /// A collection of the features the software is permitted to use.  
        /// </summary>
        public virtual ICollection<ProductFeature> LicensedFeatures { get; set; }

        /// <summary>
        /// A collection of all the times this license has been activated.  Only one active license will be permitted by the API.
        /// </summary>
        public virtual ICollection<Activation> Activations { get; set; }

	}
}
