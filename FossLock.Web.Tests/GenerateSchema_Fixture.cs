using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FossLock.Web.Model;

namespace FossLock.Web.Tests
{
    [TestClass]
    public class GenerateSchema_Fixture
    {
        [TestMethod]
        public void Can_Generate_Schema()
        {
            // This is why we need a copy of our configuration data 
            var cfg = new Configuration();
            cfg.Configure();

            cfg.AddAssembly(typeof(Customer).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}
