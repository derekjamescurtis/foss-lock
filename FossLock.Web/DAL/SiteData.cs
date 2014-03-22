using FossLock.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FossLock.Web.DAL
{



    public class SiteData : DbContext
    {        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<License> Licenses { get; set; }
    }
}