using FossLock.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FossLock.Web.DAL.Map
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            HasRequired(e => e.BillingAddress)
                .WithOptional()
                .WillCascadeOnDelete(false);
            
            HasRequired(e => e.StreetAddress)
                .WithOptional()
                .WillCascadeOnDelete(false);
        }
    }
}