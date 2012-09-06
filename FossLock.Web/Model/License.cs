using System;
using System.Collections.Generic;

namespace FossLock.Web.Model
{
	public class License
	{
		public License(){}

        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProductVersion ProductVersion { get; set; }
        public DateTimeOffset GenerationDateTime { get; set; }
        public DateTimeOffset? DestroyedDateTime { get; set; }
        public string Notes { get; set; }
        public int? NetworkLicenseCount { get; set; }
        public LockProperties RequiredLockProperties { get; set; }
        public virtual ICollection<ProductFeature> LicensedFeatures { get; set; }

	}
}
