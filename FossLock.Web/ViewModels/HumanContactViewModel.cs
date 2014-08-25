using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FossLock.Web.ViewModels
{
    public class HumanContactViewModel
    {
        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [Phone]
        public string Phone1 { get; set; }

        [Phone]
        public string Phone2 { get; set; }

        [Phone]
        public string Fax { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}