using FossLock.Model.Base;
using FossLock.Model.Component;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FossLock.Model
{

    /// <summary> Entity that represents an individual customer that can purchase and license products. 
    /// </summary>
    public class Customer : NamedEntityBase
    {
        
        /// <summary> When using Semantic Versioning for a product, 
        /// this customer may license prerelease versions of that product.
        /// </summary>
        [Required]
        public bool CanLicensePreReleaseVersions { get; set; }

        /// <summary> Street address information
        /// </summary>
        public Address StreetAddress { get; set; }

        /// <summary> Where do you send their invoices?
        /// </summary>        
        public Address BillingAddress { get; set; }

        /// <summary> The main office phone number.
        /// </summary>
        [Phone]
        public string OfficePhone1 { get; set; }

        /// <summary> A secondary office phone number.
        /// </summary>
        [Phone]
        public string OfficePhone2 { get; set; }

        /// <summary> The Office Fax number.
        /// </summary>
        [Phone]
        public string OfficeFax { get; set; }

        /// <summary> The e-mail address for this customer.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary> Any additional information, 
        /// such as preferred contact times or the first name of the point of contact.
        /// </summary>
        public string Notes { get; set; }

        /// <summary> All the product licenses purchased by this Customer.
        /// </summary>
        public virtual ICollection<License> ProductLicenses { get; set; }

        /// <summary> A list of all known contacts for this client.
        /// </summary>
        public virtual ICollection<HumanContact> Contacts { get; set; }

        /// <summary> The main contact that should be tried first.
        /// </summary>
        public virtual HumanContact PrimaryContact { get; set; }

    }
}