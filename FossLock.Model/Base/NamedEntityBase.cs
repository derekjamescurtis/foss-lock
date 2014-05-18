using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FossLock.Model.Base
{
    public abstract class NamedEntityBase : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
