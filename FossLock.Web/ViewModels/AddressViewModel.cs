using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FossLock.Web.ViewModels
{
    public class AddressViewModel
    {
        [Display(Name = "Address 1")]
        [MaxLength(255)]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        [MaxLength(255)]
        public string Address2 { get; set; }

        [MaxLength(3)]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        [MaxLength(3)]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [MaxLength(25)]
        public string PostalCode { get; set; }

        [MaxLength(25)]
        public string Country { get; set; }
    }
}