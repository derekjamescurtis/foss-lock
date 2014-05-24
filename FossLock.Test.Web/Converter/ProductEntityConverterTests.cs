using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Converter
{
    [TestFixture]
    internal class ProductEntityConverterTests
    {
        private ProductEntityConverter converter = null;
        private Product testEntity = null;
        private ProductViewModel testVm = null;

        [SetUp]
        public void TestSetup()
        {
            converter = new ProductEntityConverter();
        }

        [Test]
        public void EntityToViewmodel_NullEntity_ThrowsException()
        {
            try
            {
                Product p = null;
                var vm = converter.EntityToViewmodel(p);
                Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
            }
        }

        [Test]
        public void ViewmodelToEntity_NullEntity_ThrowsException()
        {
            try
            {
                ProductViewModel vm = null;
                var p = converter.ViewmodelToEntity(vm);
                Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("viewmodel", ((ArgumentNullException)ex).ParamName);
            }
        }
    }
}
