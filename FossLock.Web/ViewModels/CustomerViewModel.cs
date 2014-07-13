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
        public string Name { get; set; }

        public bool CanLicensePreReleaseVersions { get; set; }

        public Address StreetAddress { get; set; }

        public bool BillingMatchesStreetAddress { get; set; }

        public Address BillingAddress { get; set; }

        [Phone]
        public string OfficePhone1 { get; set; }

        [Phone]
        public string OfficePhone2 { get; set; }

        [Phone]
        public string OfficeFax { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        public HumanContact PrimaryContact { get; set; }
    }
}