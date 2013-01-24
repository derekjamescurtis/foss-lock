using FossLock.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FossLock.Model
{
    public class SiteDatabaseInitializer : DropCreateDatabaseIfModelChanges<SiteContext> // TODO: after development, this should be changed to something other than a dropcreate
    {
        protected override void Seed(SiteContext context)
        {

            GetSampleProducts().ForEach(product => context.Products.Add(product));
            GetSampleCustomers().ForEach(customer => context.Customers.Add(customer));

            base.Seed(context);
        }



        // things that can be bought by customers
        static List<Product> GetSampleProducts()
        {
            return new List<Product>(){
                new Product()
                {
                    Name                        = "Sample Product",
                    ReleaseDate                 = DateTime.Now,
                    DefaultLockProperties       = LockPropertyType.BIOS | LockPropertyType.CPU | LockPropertyType.MACAddress,
                    FailOnNullHardwareIdentifier = false,
                    PermittedActivationTypes    = Core.ActivationType.OnlineAPI | Core.ActivationType.Manual,
                    PermittedExpirationTypes    = Core.ExpirationType.Permanent | Core.ExpirationType.ExpiresDaysAfterActivation | Core.ExpirationType.ExpiresOnCalendarDate,
                    MaximumTrialDays            = 30,
                    Notes                       = "This is a sample product that demonstrates some simple capabilities of FossLock",
                    VersioningStyle             = Core.VersioningStyle.Semantic,
                    VersionLeeway               = Core.VersionLeewayType.WithinSameMajorVersion,
                    AvailableFeatures           = new List<ProductFeature>()
                    {
                        new ProductFeature()
                        {
                            Name        = "Module A",
                            Description = "Feature A that is licensed"
                        },
                        new ProductFeature()
                        {
                            Name        = "Module B",
                            Description = "Feature B that is licensed"
                        },
                    },
                    Versions = new List<ProductVersion>()
                    {
                        new ProductVersion()
                        {
                            Version = "0.0.1-Alpha.1",
                        },
                        new ProductVersion()
                        {
                            Version = "0.9.0-Beta.1",
                        },
                        new ProductVersion()
                        {
                            Version = "1.0.0",
                        },
                    },
                },
            };


        }


        // the people that buy products
        static List<Customer> GetSampleCustomers() 
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Name            = "j_smith",
                    ContactFirstName = "John",
                    ContactLastName = "Smith",                    
                    Email           = "jsmith@contoso.com",
                    Fax             = "8001234567",
                    Phone1          = "8001234567",
                    Phone2          = "8001234567",
                    Address1        = "123 West State Street.",
                    Address2        = "Ste 2",
                    City            = "Seattle",
                    State           = "WA",
                    PostalCode      = "98101",
                    Country         = "USA",
                    CanLicensePreReleaseVersions = true,
                    Notes           = "Our only customer! =(",
                },
            };
        }

    }
}