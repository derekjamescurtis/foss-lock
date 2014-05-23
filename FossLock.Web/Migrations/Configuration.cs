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
        }
    }
}
