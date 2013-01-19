using FossLock.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FossLock.Model
{

    /// <summary>
    /// Description of Product.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public LockPropertyType DefaultLockProperties { get; set; }
        public ActivationType PermittedActivationTypes { get; set; }
        public VersioningStyle VersioningStyle { get; set; }
        public string Notes { get; set; }        
        public bool FailOnNullHardwareIdentifier { get; set; }
        public virtual ICollection<ProductFeature> AvailableFeatures { get; set; }
        public virtual ICollection<ProductVersion> Versions { get; set; }

    }
}