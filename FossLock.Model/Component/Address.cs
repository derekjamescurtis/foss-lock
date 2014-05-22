namespace FossLock.Model.Component
{
    /// <summary>
    ///     Complex type that simply represents a common street address.
    /// </summary>
    public class Address
    {
        /// <summary>
        ///     A string that represents the billing address street and building number.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        ///     A string that represents the billing address apt/ste string.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        ///     A string that represents the billing address city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     A two character string that represents the billing address state/province.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     A string that represents the billing address postal (or zip) code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///     An ISO 3166 alpha-3 code that represents the billing address country.
        /// </summary>
        public string Country { get; set; }
    }
}