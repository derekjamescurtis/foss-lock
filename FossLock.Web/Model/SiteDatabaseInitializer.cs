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
            //GetPromotions().ForEach(promo => context.Promotions.Add(promo));

            base.Seed(context);
        }


        // the below lists need to be populated with seed data


        // things that can be bought by customers
        static List<Product> GetSampleProducts()
        {
            return new List<Product>(){
                new Product
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
                    AvailableFeatures = new List<ProductFeature>()
                    {
                        new ProductFeature
                        {
                            Name        = "Module A",
                            Description = "Feature A that is licensed"
                        },
                        new ProductFeature
                        {
                            Name        = "Module B",
                            Description = "Feature B that is licensed"
                        },
                    },
                    Versions = new List<ProductVersion>()
                    {
                        new ProductVersion
                        {
                            Version = "0.0.1-Alpha.1",
                        },
                        new ProductVersion
                        {
                            Version = "0.9.0-Beta.1",
                        },
                        new ProductVersion
                        {
                            Version = "1.0.0",
                        },
                    },
                },
            };


        }


        // the people that buy products
        static List<Customer> SampleCustomers
        {

        }


        // an instance of a product that was purchased by a customer
        static List<License> SampleLicenses
        {

        }



    }
}