using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.DAL.EF.Map;
using FossLock.Model;

namespace FossLock.DAL.EF
{
    public class AppDb : DbContext
    {

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // just my preference..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // products
            modelBuilder.Configurations.Add(new ProductMap()); 
            modelBuilder.Configurations.Add(new ProductFeatureMap());
            modelBuilder.Configurations.Add(new ProductVersionMap());
        }
    }
}
