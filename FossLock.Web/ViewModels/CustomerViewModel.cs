using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FossLock.Model;
using FossLock.Model.Component;

namespace FossLock.Web.ViewModels
{
    /// <summary>
    ///     Represents a Customer instance that can be properly rendered
    ///     by the Razor template engine.
    /// </summary>
    public class CustomerViewModel : IFossLockViewModel
    {
        public CustomerViewModel()
        {
            StreetAddress = new AddressViewModel();
            BillingAddress = new AddressViewModel();
            PrimaryContact = new HumanContactViewModel();
            ProductLicenses = new List<License>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name = "Can License Pre-release Versions")]
        public bool CanLicensePreReleaseVersions { get; set; }

        [Display(Name = "Street Address")]
        public AddressViewModel StreetAddress { get; set; }

        [Display(Name = "Same as Street Address")]
        public bool BillingMatchesStreetAddress { get; set; }

        [Display(Name = "Billing Address")]
        public AddressViewModel BillingAddress { get; set; }

        [Phone]
        [Display(Name = "Office Phone 1")]
        public string OfficePhone1 { get; set; }

        [Phone]
        [Display(Name = "Office Phone 2")]
        public string OfficePhone2 { get; set; }

        [Phone]
        [Display(Name = "Office Fax")]
        public string OfficeFax { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Primary Contact")]
        public HumanContactViewModel PrimaryContact { get; set; }

        public IEnumerable<License> ProductLicenses { get; set; }
    }
}