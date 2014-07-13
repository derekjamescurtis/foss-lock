using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Web.ViewModels;
using NUnit.Framework;

namespace FossLock.Test.Web.ViewModel
{
    [TestFixture]
    internal class CustomerViewModelTests
    {
        private CustomerViewModel vm = null;

        [SetUp]
        public void SetUp()
        {
            vm = new CustomerViewModel();
        }

        [Test]
        public void ViewModel_ComplexTypes_AreInitialized()
        {
            Assert.IsNotNull(vm.StreetAddress);
            Assert.IsNotNull(vm.BillingAddress);
            Assert.IsNotNull(vm.PrimaryContact);
        }
    }
}
