using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.Model;
using FossLock.Model.Component;
using FossLock.Web.Controllers;
using FossLock.Web.Controllers.Base;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.Web.Controllers
{
    /// <summary>
    ///     Unit tests for the PrimaryEntityCrudController.
    /// </summary>
    /// <remarks>
    ///     NOTE: Currently this test relies on the knowledge of the Customer and
    ///     CustomerViewModel classes, as well as reliance on the actual functionality
    ///     of the CustomerConverter object.  To make these tests more robust, it would
    ///     be ideal to remove this dependency and just mock up all those types.
    /// </remarks>
    [TestFixture]
    internal class PrimaryEntityCrudControllerTests
    {
        private IFossLockService<Customer> service = null;
        private Mock<IFossLockService<Customer>> serviceMocker = null;

        private IEntityConverter<Customer, CustomerViewModel> converter = null;

        private PrimaryEntityCrudController<Customer, CustomerViewModel> controller = null;
        private Mock<PrimaryEntityCrudController<Customer, CustomerViewModel>> controllerMocker = null;

        /// <summary>
        ///     Reinitializes all of our fields used in these tests.
        /// </summary>
        /// <remarks>
        ///     NOTE:  serviceMocker and controllerMocker can have further setup
        ///     defined within the test methods and everything will still work.
        /// </remarks>
        [SetUp]
        public void SetUp()
        {
            converter = new CustomerConverter();

            serviceMocker = new Mock<IFossLockService<Customer>>();
            service = serviceMocker.Object;

            controllerMocker = new Mock<PrimaryEntityCrudController<Customer, CustomerViewModel>>(
                service, converter)
                {
                    CallBase = true
                };
            controller = controllerMocker.Object;
        }

        #region List Tests

        /// <summary>
        ///     Makes sure the Index method is building a ViewModel that is of
        ///     the proper type we're expecting, and contains the proper number
        ///     of elements based on the service response.
        /// </summary>
        [Test]
        public void HttpGet_IndexMethod_ReturnsExpectedList()
        {
            // build our service and controller
            serviceMocker.Setup(e => e.GetList()).Returns(() =>
            {
                var customers = new List<Customer>();
                for (int i = 1; i <= 100; i++)
                {
                    // need to set the ID and all reference fields, or the converter
                    // instance will get super sad because it expectes reference types (except strings)
                    // to be initliazed.
                    customers.Add(new Customer
                    {
                        Id = i,
                        BillingAddress = new Address(),
                        StreetAddress = new Address(),
                        PrimaryContact = new HumanContact()
                    });
                }
                return customers;
            });

            // result is expected type
            var result = controller.Index();
            Assert.That(result, Is.InstanceOf<ViewResult>());

            // and it's viewmodel is what we expect
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.InstanceOf<IEnumerable<CustomerViewModel>>());

            // and that the viewmodel has the number of elements that we're expecting
            var viewResultModel = (IEnumerable<CustomerViewModel>)viewResult.Model;
            Assert.That(viewResultModel.Count(), Is.EqualTo(service.GetList().Count()));
        }

        #endregion List Tests

        #region Create Tests

        /// <summary>
        ///     Makes sure that a request to our GET Create returns the type of
        ///     result that we're expecting, and that it contains the viewmodel
        ///     type that we're expecting.
        /// </summary>
        [Test]
        public void HttpGet_CreateMethod_ReturnsExpectedViewmodel()
        {
            controller = controllerMocker.Object;

            var result = controller.Create();
            Assert.That(result, Is.InstanceOf<ActionResult>());

            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.InstanceOf<CustomerViewModel>());
        }

        /// <summary>
        ///     Makes sure that on POST, Create redisplays the same
        ///     page and same viewmodel if the viewmodel is reported
        ///     as invalid.
        /// </summary>
        /// <remarks>
        ///     TODO: Well, this doesn't work.  I can't override Controller.ModelState
        ///     with Moq becuase it's not virtual... I need to think more about how I'm going to solve this.
        /// </remarks>
        [Test]
        public void HttpPost_CreateMethod_InvalidData_RedisplaysSameView()
        {
            // See remarks above:
            Assert.Ignore("Can't override the propery I want to mock the controller.");

            // make sure our controller reports 'ModelState.IsValid == false'
            controllerMocker.Setup(e => e.ModelState).Returns(() =>
            {
                var msd = new ModelStateDictionary();
                msd.AddModelError("I'm a fake error.", new Exception());

                Assert.That(msd.IsValid, Is.False);

                return msd;
            });

            var vm = new CustomerViewModel();

            var result = controller.Create(vm);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(((ViewResult)result).Model, Is.SameAs(vm));
        }

        [Test]
        public void HttpPost_CreateMethod_ValidData_CallsServiceAdd()
        {
            Assert.Ignore("Not implemented");
        }

        [Test]
        public void HttpPost_CreateMethod_ValidData_RedirectsToIndex()
        {
            Assert.Ignore("Not implemented");
        }

        #endregion Create Tests

        #region Edit Tests

        /// <summary>
        ///     Tests that our controller returns an HTTP 400 'Bad Request' when requesting the
        ///     'edit' route with no id.
        /// </summary>
        [Test]
        public void HttpGet_EditMethod_MissingId_ReturnsBadRequest()
        {
            controllerMocker = new Mock<PrimaryEntityCrudController<Customer, CustomerViewModel>>(null, null) { CallBase = true };

            controller = controllerMocker.Object;
            var result = controller.Edit(id: null);

            Assert.That(result, Is.InstanceOf<HttpStatusCodeResult>());
            Assert.That(((HttpStatusCodeResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        [Test]
        public void HttpPost_EditMethod_InvalidViewModel_DoesNotUpdateService()
        {
            Assert.Ignore("Not implemented");
        }

        [Test]
        public void HttpPost_EditMethod_InvalidViewModel_RedisplaysTheSameView()
        {
            Assert.Ignore("Not implemented");
        }

        [Test]
        public void HttpPost_EditMethod_ValidViewModel_UpdatesService()
        {
            Assert.Ignore("Not implemented");
        }

        [Test]
        public void HttpPost_EditMethod_ValidViewModel_RedirectsToIndex()
        {
            Assert.Ignore("Not implemented");
        }

        #endregion Edit Tests

        #region Delete Tests

        /// <summary>
        ///     Tests that our controller returns an HTTP 400 'Bad Request' when requesting the
        ///     'delete' route with no id.
        /// </summary>
        [Test]
        public void HttpGet_DeleteMethod_MissingId_ReturnsBadRequest()
        {
            controllerMocker = new Mock<PrimaryEntityCrudController<Customer, CustomerViewModel>>(null, null) { CallBase = true };
            controller = controllerMocker.Object;

            var result = controller.Delete(id: null);

            Assert.That(result, Is.InstanceOf<HttpStatusCodeResult>());
            Assert.That(((HttpStatusCodeResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        #endregion Delete Tests
    }
}
