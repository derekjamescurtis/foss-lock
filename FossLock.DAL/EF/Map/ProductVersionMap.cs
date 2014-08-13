using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using FossLock.Model;

namespace FossLock.DAL.EF.Map
{
    internal class ProductVersionMap : EntityTypeConfiguration<ProductVersion>
    {
        public ProductVersionMap()
        {
            Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(e => e.Product);
        }
    }
}
