using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FossLock.Web.ViewModels
{
    public class ProductFeatureViewModel : IFossLockViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public FossLock.Model.Product Product { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxAllowed { get; set; }
    }
}