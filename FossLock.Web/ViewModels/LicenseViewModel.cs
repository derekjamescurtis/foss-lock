using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels
{
    public class LicenseViewModel : IFossLockViewModel
    {
        public LicenseViewModel()
        {
            AllLockProperties = new SelectList(
                ((IEnumerable<int>)Enum.GetValues(typeof(LockPropertyType)))
                .Where(e => (LockPropertyType)e != LockPropertyType.None)
                .Select(e =>
                    new
                    {
                        Text = Enum.GetName(typeof(LockPropertyType), e),
                        Value = e.ToString()
                    }), "Value", "Text"
                );

            OverrideDefaultLockProperties = false;
        }

        public int Id { get; set; }

        [Required]
        public DateTimeOffset GenerationDateTime { get; set; }

        public DateTimeOffset? DestroyedDateTime { get; set; }

        public DateTimeOffset? ExpirationDate { get; set; }

        public string Notes { get; set; }

        public int? NetworkLicenseCount { get; set; }

        public bool OverrideDefaultLockProperties { get; set; }

        public IList<string> RequiredLockProperties { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductVersionId { get; set; }

        public ICollection<ProductFeature> LicensedFeatures { get; set; }

        public ICollection<Activation> Activations { get; set; }

        public SelectList AllLockProperties { get; set; }
    }
}