using System;
using System.Collections.Generic;
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
        public int Id { get; set; }

        public string Name { get; set; }

        public bool CanLicensePreReleaseVersions { get; set; }

        public Address StreetAddress { get; set; }

        public Address BillingAddress { get; set; }

        public string OfficePhone1 { get; set; }

        public string OfficePhone2 { get; set; }

        public string OfficeFax { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public HumanContact PrimaryContact { get; set; }
    }
}