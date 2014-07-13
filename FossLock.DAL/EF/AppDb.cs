using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.DAL.EF.Map;
using FossLock.DAL.EF.Map.ComplexType;
using FossLock.Model;
using FossLock.Model.Component;

namespace FossLock.DAL.EF
{
    public class AppDb : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
             * Database-wide configs
             * Just a personal preference, but I don't like using pluralized tables
             */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*
             * Complex types
             * These aren't directly represented by tables in the database.
             * Instead they're attached into other tables so for things like an 'address',
             * which is a distinct type, but doesn't require a separate table to represent it.
             */
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new HumanContactMap());

            /*
             * Entities
             */
            // products
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductFeatureMap());
            modelBuilder.Configurations.Add(new ProductVersionMap());

            // customers
            modelBuilder.Configurations.Add(new CustomerMap());
        }
    }
}
