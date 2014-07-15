using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Faker;
using FossLock.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Controllers
{
    [TestFixture]
    internal class CustomerControllerTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        ///     Verifies that the Edit and Delete HTTP GET methods both return
        /// </summary>
        [Test]
        public void EditDelete_HttpGet_NoDbID_()
        {
            //var c = new CustomerController();
            //var r = (HttpStatusCodeResult)c.Edit(id: null);
        }
    }
}
