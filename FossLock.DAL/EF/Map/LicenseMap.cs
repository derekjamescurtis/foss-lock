using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model;

namespace FossLock.DAL.EF.Map
{
    internal class LicenseMap : EntityTypeConfiguration<License>
    {
        public LicenseMap()
        {
            HasRequired(e => e.Customer);

            // prevents multiple cascade paths.
            // todo: make sure generated sql matches what I want here
            HasRequired(e => e.ProductVersion)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasMany(e => e.LicensedFeatures)
                .WithMany(v => v.LicensesWithFeature);
        }
    }
}
