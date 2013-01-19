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
                    PermittedActivationTypes    = Core.ActivationType.OnlineAPI | Core.ActivationType.Manual,
                    Notes                       = "This is a sample product that demonstrates some simple capabilities of FossLock",
                    TrialDays                   = 0, 
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
                            Version = "0.0.1",
                            Type = Core.VersionType.Alpha
                        },
                        new ProductVersion
                        {
                            Version = "0.9.0",                                
                            Type = Core.VersionType.Beta
                        },
                        new ProductVersion
                        {
                            Version = "1.0.0",
                            Type = Core.VersionType.Release
                        },
                    },
                },
            };

            // version should have a 'alpha', 'beta', 'release', 'legacy' flag in addition to the version number


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