using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FossLock.Model.Component;

namespace FossLock.Web.ViewModels
{
    /// <summary>
    ///     Represents a Customer instance that can be properly rendered
    ///     by the Razor template engine.
    /// </summary>
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            StreetAddress = new Address();
            BillingAddress = new Address();
            PrimaryContact = new HumanContact();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name = "Can License Pre-release Versions")]
        public bool CanLicensePreReleaseVersions { get; set; }

        [Display(Name = "Street Address")]
        public Address StreetAddress { get; set; }

        [Display(Name = "Same as Street Address")]
        public bool BillingMatchesStreetAddress { get; set; }

        [Display(Name = "Billing Address")]
        public Address BillingAddress { get; set; }

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
        public HumanContact PrimaryContact { get; set; }
    }
}