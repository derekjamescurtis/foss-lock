using System.Collections.Generic;
using FossLock.Model.Base;
using FossLock.Model.Component;

namespace FossLock.Model
{
    /// <summary>
    ///     Entity that represents an individual customer that can purchase and
    ///     license products.
    /// </summary>
    public class Customer : NamedEntityBase
    {
        /// <summary>
        ///     Street address information
        /// </summary>
        public Address StreetAddress { get; set; }

        /// <summary>
        ///     Where do you send their invoices?
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        ///     The main office phone number.
        /// </summary>
        public string OfficePhone1 { get; set; }

        /// <summary>
        ///     A secondary office phone number.
        /// </summary>
        public string OfficePhone2 { get; set; }

        /// <summary>
        ///     The Office Fax number.
        /// </summary>
        public string OfficeFax { get; set; }

        /// <summary>
        ///     The e-mail address for this customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The main contact that should be tried first.
        /// </summary>
        public HumanContact PrimaryContact { get; set; }

        /// <summary>
        ///     Any additional information, such as preferred contact times
        ///     or the first name of the point of contact.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     All the product licenses purchased by this Customer.
        /// </summary>
        public virtual IList<License> ProductLicenses { get; set; }
    }
}