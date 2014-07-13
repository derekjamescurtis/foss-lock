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

        /// <summary>
        ///     Reinitializes our viewmodel prior to each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            vm = new CustomerViewModel();
        }

        /// <summary>
        ///     Ensures that our complex-type references are all initialized automatically
        ///     in our viewmodels.
        /// </summary>
        [Test]
        public void ViewModel_ComplexTypes_AreInitialized()
        {
            var referenceTypes = typeof(CustomerViewModel).GetProperties()
                .Where(e =>
                    e.PropertyType != typeof(System.String) &&
                    e.PropertyType.IsValueType == false
                );
            foreach (var propInfo in referenceTypes)
            {
                Assert.IsNotNull(propInfo.GetValue(vm));
            }
        }
    }
}
