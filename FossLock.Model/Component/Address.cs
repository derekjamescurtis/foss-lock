using FossLock.Model.Base.SharpArchitecture;

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

        // TODO: refactor this out for all component classes
        // document the fact that it provides value, not reference equality.

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() != typeof(Address))
            {
                return false;
            }
            else
            {
                var allProps = typeof(Address).GetProperties();
                foreach (var propInfo in allProps)
                {
                    if (propInfo.GetValue(this) != propInfo.GetValue(obj))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     Returns this object as a human readable address in the format:
        ///
        ///     Street1
        ///     [Street2]
        ///     City, State PostalCode
        ///     Country
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var fullStreetAddress = string.IsNullOrWhiteSpace(Address2) ?
                Address1 :
                Address1 + "\n" + Address2;

            return string.Format("{0}\n{1}, {2} {3}\n{4}",
                fullStreetAddress, City, State, PostalCode, Country);
        }
    }
}