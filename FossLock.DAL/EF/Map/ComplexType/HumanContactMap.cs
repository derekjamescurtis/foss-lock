using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Component;

namespace FossLock.DAL.EF.Map.ComplexType
{
    internal class HumanContactMap : ComplexTypeConfiguration<HumanContact>
    {
        public HumanContactMap()
        {
            Property(e => e.FirstName)
                .HasMaxLength(255);

            Property(e => e.LastName)
                .HasMaxLength(255);

            Property(e => e.Phone1)
                .HasMaxLength(40);

            Property(e => e.Phone2)
                .HasMaxLength(40);

            Property(e => e.Fax)
                .HasMaxLength(40);

            Property(e => e.Email)
                .HasMaxLength(255);

            Property(e => e.Notes)
                .IsMaxLength();
        }
    }
}
