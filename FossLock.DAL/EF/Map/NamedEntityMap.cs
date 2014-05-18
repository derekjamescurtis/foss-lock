using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;

namespace FossLock.DAL.EF.Map
{
    abstract class NamedEntityBaseMap<T> : EntityTypeConfiguration<T> 
        where T : NamedEntityBase
    {
        public NamedEntityBaseMap()
        {
            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
