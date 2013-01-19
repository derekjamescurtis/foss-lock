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
                    Name        = "Sample Product",
                    ReleaseDate = DateTime.Now,
                    DefaultLockProperties = LockPropertyType.BIOS | LockPropertyType.CPU | LockPropertyType.MACAddress,
                    TrialDays   = 0, 
                    AvailableFeatures = new List<ProductFeature>()
                        {
                            new ProductFeature
                            {
                                Name        = "",
                                Description = ""
                            },
                            new ProductFeature
                            {
                                Name = "",
                                Description = ""
                            },
                        },
                    Versions = new List<ProductVersion>()
                        {
                        
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