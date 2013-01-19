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
        /// 
        /// </summary>
        public virtual Customer Customer { get; set; }
        
        public virtual ProductVersion ProductVersion { get; set; }
        
        public DateTimeOffset GenerationDateTime { get; set; }
        
        public DateTimeOffset? DestroyedDateTime { get; set; }

        public string Notes { get; set; }
        public int? NetworkLicenseCount { get; set; }
        public LockPropertyType RequiredLockProperties { get; set; }
        public virtual ICollection<ProductFeature> LicensedFeatures { get; set; }
        public virtual ICollection<Activation> Activations { get; set; }

	}
}
