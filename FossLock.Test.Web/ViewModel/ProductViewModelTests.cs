using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Web.ViewModels;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.ViewModel
{
    [TestFixture]
    internal class ProductViewModelTests
    {
        private ProductViewModel vm = null;

        [SetUp]
        public void SetUp()
        {
            this.vm = new ProductViewModel();
        }

        [Test(Description = "Make sure all list-type properties are automatically initialized.")]
        public void ViewModel_ListProperties_Initialized()
        {
            var t = typeof(ProductViewModel);

            var list_type_props = t.GetProperties().Where(e =>
                e.PropertyType.GetInterfaces().Contains(typeof(ICollection)) ||
                e.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)));

            foreach (var p in list_type_props)
            {
                Assert.IsNotNull(p.GetValue(vm), string.Format("{0:s} was not initialized", p.Name));
            }
        }
    }
}
