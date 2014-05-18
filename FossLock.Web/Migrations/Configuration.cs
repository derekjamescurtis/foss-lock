namespace FossLock.Web.Migrations
{
    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FossLock.Core;
using FossLock.DAL.EF;
using FossLock.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDb context)
        {

            if (context.Products.Count() == 0)
            {

                // todo: remove this after testing..

                for (int i = 0; i < 20; i++)
                {
                    var p = new Product
                    {
                        Name = Faker.Company.Name(),
                        Notes = Faker.Lorem.Paragraph(),
                        AvailableFeatures = new List<ProductFeature> { },
                        DefaultLockProperties = LockPropertyType.CPU | LockPropertyType.Harddisk,
                        MaximumTrialDays = Faker.RandomNumber.Next(100),
                        FailOnNullHardwareIdentifier = false,
                        ReleaseDate = DateTime.Today,
                        PermittedActivationTypes = ActivationType.Email | ActivationType.OnlineAPI | ActivationType.Manual,
                        PermittedExpirationTypes = ExpirationType.Permanent | ExpirationType.ExpiresOnCalendarDate,
                        Versions = new List<ProductVersion> { },
                        VersioningStyle = VersioningStyle.DotNet,
                        VersionLeeway = VersionLeewayType.Strict
                    };
                    context.Products.AddOrUpdate(p);
                }
             
            }
        }

    }
}
