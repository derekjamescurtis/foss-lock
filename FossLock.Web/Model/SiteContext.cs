using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FossLock.Core.Model
{
    public class SiteContext : DbContext
    {
        public DbSet<Activation> Activations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; } // TODO: we might be able to get rid of this
        public DbSet<ProductVersion> ProductVersions { get; set; } // TODO: we might be able to get rid of this as well
        protected DbSet<Settings> Settings { get; set; }
    }
}