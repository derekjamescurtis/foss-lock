using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Component;

namespace FossLock.DAL.EF.Map.ComplexType
{
    internal class AddressMap : ComplexTypeConfiguration<Address>
    {
        public AddressMap()
        {
            Property(e => e.Address1)
                .HasMaxLength(255);

            Property(e => e.Address2)
                .HasMaxLength(255);

            Property(e => e.City)
                .HasMaxLength(255);

            Property(e => e.State)
                .HasMaxLength(255);

            Property(e => e.PostalCode)
                .HasMaxLength(25);

            Property(e => e.Country)
                .HasMaxLength(3);
        }
    }
}
