using FossLock.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossLock.Model.Component
{
    public class Address : EntityBase
    {
        /// <summary> A string that represents the billing address street and building number.
        /// </summary>
        [MaxLength(255), Display(Name = "Address 1")]
        public string Address1 { get; set; }

        /// <summary> A string that represents the billing address apt/ste string.
        /// </summary>
        [MaxLength(255), Display(Name = "Address 2")]
        public string Address2 { get; set; }

        /// <summary> A string that represents the billing address city.
        /// </summary>
        [MaxLength(255)]
        public string City { get; set; }

        /// <summary>
        /// A two character string that represents the billing address state/province.
        /// </summary>
        [MaxLength(3), Display(Name = "State/Province")]
        public string State { get; set; }

        /// <summary> A string that represents the billing address postal (or zip) code.
        /// </summary>
        [MaxLength(20), Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        /// <summary> An ISO 3166 alpha-3 code that represents the billing address country.
        /// </summary>
        [MaxLength(3)]
        public string Country { get; set; }
    }
}
