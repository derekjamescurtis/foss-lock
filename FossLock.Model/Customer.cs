using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FossLock.Model
{

    /// <summary>
    /// Entity that represents an individual customer that can purchase and license products. 
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// An integer that uniquely represents this Customer within the database.
        /// </summary>
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// String that helps the user identify this Customer.  This name should generally be unique.
        /// </summary>
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// First name of this Customer, or the first name of the point of contact.
        /// </summary>
        [Required, StringLength(200), Display(Name="Contact First Name")]
        public string ContactFirstName { get; set; }

        /// <summary>
        /// Last name of this Customer, or the last name of the point of contact.
        /// </summary>
        [Required, StringLength(200), Display(Name = "Contact Last Name")]
        public string ContactLastName { get; set; }

        /// <summary>
        /// When using Semantic Versioning for a product, this customer may license prerelease versions of that product.
        /// </summary>
        [Required]
        public bool CanLicensePreReleaseVersions { get; set; }

        /// <summary>
        /// A string that represents the billing address street and building number.
        /// </summary>
        [StringLength(200), Display(Name="Address 1")]
        public string Address1 { get; set; }

        /// <summary>
        /// A string that represents the billing address apt/ste string.
        /// </summary>
        [StringLength(200), Display(Name="Address 2")]
        public string Address2 { get; set; }

        /// <summary>
        /// A string that represents the billing address city.
        /// </summary>
        [StringLength(200)]
        public string City { get; set; }

        /// <summary>
        /// A two character string that represents the billing address state/province.
        /// </summary>
        [StringLength(2), Display(Name="State/Province")]
        public string State { get; set; }

        /// <summary>
        /// A string that represents the billing address postal (or zip) code.
        /// </summary>
        [StringLength(20), Display(Name="Postal Code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// An ISO 3166 alpha-3 code that represents the billing address country.
        /// </summary>
        [StringLength(3)]
        public string Country { get; set; }

        /// <summary>
        /// The primary phone number for this customer.
        /// </summary>
        [Phone]
        public string Phone1 { get; set; }

        /// <summary>
        /// Any secondary phone number for this customer.
        /// </summary>
        [Phone]
        public string Phone2 { get; set; }

        /// <summary>
        /// The fax number for this customer.
        /// </summary>
        [Phone]
        public string Fax { get; set; }

        /// <summary>
        /// The e-mail address for this customer.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Any additional information, such as preferred contact times or the first name of the point of contact.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// All the product licenses purchased by this Customer.
        /// </summary>
        public virtual ICollection<License> ProductLicenses { get; set; }

    }
}