using System;
using System.Collections.Generic;
using FossLock.Core;
using FossLock.Model.Base;

namespace FossLock.Model
{
    /// <summary>
    ///     An instance of a specific <see cref="Product"/> purchased
    ///     by a <see cref="Customer"/>.
    /// </summary>
    public class License : EntityBase
    {
        /// <summary>
        ///     The date and time this license was generated.
        /// </summary>
        public DateTimeOffset GenerationDateTime { get; set; }

        /// <summary>
        ///     If this license is no longer valid, this field is set
        ///     to indicate when the license was destroyed.
        /// </summary>
        public DateTimeOffset? DestroyedDateTime { get; set; }

        /// <summary>
        ///     Indicates the date/time this product will expire. This is
        ///     preset on types that ExpireOnDate, and will be set by the
        ///     ActivationRequest on licenses that ExpireDaysAfterActivation
        /// </summary>
        public DateTimeOffset? ExpirationDate { get; set; }

        /// <summary>
        ///     Optional information about the license.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     On a network license, this will indicate the number
        ///     of workstations the product is licensed for.
        /// </summary>
        public int? NetworkLicenseCount { get; set; }

        /// <summary>
        ///     The hardware identifiers that must be returned from the
        ///     customer to generate the license.
        ///     NOTE: If this value is set to LockPropertyType.None, it is assumed
        ///         that the user wants to inherit the DefaultLockProperties of the
        ///         Product this license was generated for.
        /// </summary>
        public LockPropertyType RequiredLockProperties { get; set; }

        /// <summary>
        ///     A reference to the customer that has purchased this license.
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        ///     Exposed foreign key for Customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        ///     The version of the product that was purchased by this customer.
        /// </summary>
        public virtual ProductVersion ProductVersion { get; set; }

        public int ProductVersionId { get; set; }

        /// <summary>
        ///     A collection of the features the software is permitted to use.
        /// </summary>
        public virtual ICollection<ProductFeature> LicensedFeatures { get; set; }

        /// <summary>
        ///     A collection of all the times this license has been activated.
        ///     Only one active license will be permitted by the API.
        /// </summary>
        public virtual ICollection<Activation> Activations { get; set; }
    }
}