using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    public class ProductVersionViewModel : IFossLockViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required, Range(0, int.MaxValue), Display(Name = "Major")]
        public string Major { get; set; }

        [Required, Range(0, int.MaxValue), Display(Name = "Minor")]
        public string Minor { get; set; }

        [Required, Range(0, int.MaxValue), Display(Name = "Build")]
        public string Build { get; set; }

        [Required, Range(0, int.MaxValue), Display(Name = "Patch")]
        public string Patch { get; set; }
    }
}