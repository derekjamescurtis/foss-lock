using FossLock.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FossLock.Model.Component
{
    [ComplexType]
    public class Address
    {
        /// <summary> A string that represents the billing address street and building number.
        /// </summary>
        [Display(Name = "Address 1")]
        [MaxLength(255)]        
        public string Address1 { get; set; }

        /// <summary> A string that represents the billing address apt/ste string.
        /// </summary>
        [Display(Name = "Address 2")]
        [MaxLength(255)]        
        public string Address2 { get; set; }

        /// <summary> A string that represents the billing address city.
        /// </summary>
        [MaxLength(255)]
        public string City { get; set; }

        /// <summary>
        /// A two character string that represents the billing address state/province.
        /// </summary>
        [Display(Name = "State/Province")]
        [MaxLength(3)]
        public string State { get; set; }

        /// <summary> A string that represents the billing address postal (or zip) code.
        /// </summary>
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [MaxLength(25)]
        public string PostalCode { get; set; }

        /// <summary> An ISO 3166 alpha-3 code that represents the billing address country.
        /// </summary>
        [MaxLength(3)]
        public string Country { get; set; }
    }
}
