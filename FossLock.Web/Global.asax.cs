﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FossLock.Web;
using System.Data.Entity;

namespace FossLock.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();


            // Initialize the database
            Database.SetInitializer(new Model.SiteDatabaseInitializer());


            // Add Administrator.
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
            if (Membership.GetUser("admin") == null)
            {

                MembershipCreateStatus status; // we don't actually do anything with this.  we just need to pass it to the CreateUser function as an out variable

                Membership.CreateUser("admin", "password", "admin@contoso.com", "Life, the universe and everything", "42", true, out status);
                Roles.AddUserToRole("admin", "Administrator");

                // add the admin required information
                using (var db = new Models.CastrolSiteContext())
                {
                    var newCustomer = new Models.Customer()
                    {
                        Email = "admin@contoso.com",
                        Username = "admin",
                        LicenseNumber = "42",
                        PhoneCell = "+60 3 1234.1234",
                        PhoneHome = "+60 3 1234.1234",
                        RegistrationDate = DateTime.Now
                    };

                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                }

            }


        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}