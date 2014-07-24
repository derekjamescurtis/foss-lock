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
    ///
    ///     This dependency exists because the controller class we're testing was refactored
    ///     out of a controller that worked with Customer entities.  So, many of these tests
    ///     were just copied over to these tests.
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
        ///     [Re]initializes all of our class-level fields before each test method.
        /// </summary>
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
        [Test]
        public void HttpPost_CreateMethod_InvalidData_RedisplaysSameView()
        {
            // NOTE: we can't actually mock the controller.modelstate propery
            // because it's not virtual, so moq can't handle it.
            // so, instead we do this to simulate a validation failure
            controller.ModelState.AddModelError("Id", new Exception("I'm a fake validation error."));
            Assert.That(controller.ModelState.IsValid, Is.False);

            var vm = new CustomerViewModel();
            var result = controller.Create(vm);

            // make sure we're not redirecting and that our viewmodel is the same
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(((ViewResult)result).Model, Is.SameAs(vm));
        }

        /// <summary>
        ///     Verifies that the service's Create method is never called
        ///     on HTTP Post, if the ViewModel reports itself as invalid.
        /// </summary>
        public void HttpPost_CreateMethod_InvalidData_ServiceAdd_NotCalled()
        {
            var createCalls = 0;
            serviceMocker.Setup(e => e.Add(It.IsAny<Customer>()))
                .Callback(() => { ++createCalls; });

            controller.ModelState.AddModelError("aValidationError", new Exception());

            Assert.That(controller.ModelState.IsValid, Is.False);

            controller.Create(new CustomerViewModel());

            Assert.That(createCalls, Is.EqualTo(0));
        }

        /// <summary>
        ///     When viewmodel is valid, the HTTP Post Create method should call
        ///     the service's Add method.
        /// </summary>
        [Test]
        public void HttpPost_CreateMethod_ValidData_CallsServiceAdd()
        {
            var callCount = 0;

            serviceMocker.Setup(e => e.Add(It.IsAny<Customer>()))
                .Callback(() => { ++callCount; });

            Assert.That(controller.ModelState.IsValid, Is.True);

            var result = controller.Create(new CustomerViewModel());
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

            Assert.That(callCount, Is.EqualTo(1));
        }

        /// <summary>
        ///     HTTP Post to Create method, will redirect to Index action
        ///     after processing a valid viewmodel.
        /// </summary>
        [Test]
        public void HttpPost_CreateMethod_ValidData_RedirectsToIndex()
        {
            Assert.That(controller.ModelState.IsValid, Is.True);

            var result = controller.Create(new CustomerViewModel());
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

            var redirectResult = result as RedirectToRouteResult;

            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("Index"));
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
            var result = controller.Edit(id: null);

            Assert.That(result, Is.InstanceOf<HttpStatusCodeResult>());
            Assert.That(((HttpStatusCodeResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        /// <summary>
        ///     The controller should not call the service's 'update' method on HTTP Post
        ///     if viewmodel validation fails.
        /// </summary>
        [Test]
        public void HttpPost_EditMethod_InvalidViewModel_DoesNotUpdateService()
        {
            var updateCalls = 0;
            serviceMocker.Setup(e => e.Update(It.IsAny<Customer>())).Callback(() => { ++updateCalls; });

            controller.ModelState.AddModelError("aValidationError", new Exception());

            Assert.That(controller.ModelState.IsValid, Is.False);

            controller.Edit(new CustomerViewModel());

            Assert.That(updateCalls, Is.EqualTo(0));
        }

        /// <summary>
        ///     The controller should redisplay the same view as 'get' after a 'post'
        ///     where the viewmodel validation fails.
        /// </summary>
        [Test]
        public void HttpPost_EditMethod_InvalidViewModel_RedisplaysTheSameView()
        {
            controller.ModelState.AddModelError("aValidationError", new Exception());

            Assert.That(controller.ModelState.IsValid, Is.False);

            var result = controller.Edit(new CustomerViewModel());

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        /// <summary>
        ///     The controller should call the service's 'update' method on
        ///     HTTP Post if the viewmodel validates successfully.
        /// </summary>
        [Test]
        public void HttpPost_EditMethod_ValidViewModel_UpdatesService()
        {
            var updateCalls = 0;

            serviceMocker.Setup(
                e => e.GetById(It.IsAny<int>()))
                    .Returns(new Customer());

            serviceMocker.Setup(
                e => e.Update(It.IsAny<Customer>()))
                    .Callback(() =>
                    {
                        ++updateCalls;
                    });

            Assert.That(controller.ModelState.IsValid, Is.True);

            controller.Edit(new CustomerViewModel());

            Assert.That(updateCalls, Is.EqualTo(1));
        }

        /// <summary>
        ///     The controller should redirect to the action 'index' after
        ///     a successful 'update' to an entity.
        /// </summary>
        [Test]
        public void HttpPost_EditMethod_ValidViewModel_RedirectsToIndex()
        {
            serviceMocker.Setup(
                e => e.GetById(It.IsAny<int>()))
                    .Returns(new Customer());

            Assert.That(controller.ModelState.IsValid, Is.True);

            var result = controller.Edit(new CustomerViewModel());

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

            var redirectResult = result as RedirectToRouteResult;

            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("Index"));
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
            var result = controller.Delete(id: null);

            Assert.That(result, Is.InstanceOf<HttpStatusCodeResult>());
            Assert.That(((HttpStatusCodeResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        /// <summary>
        ///     Calls to 'delete confirmed' should return a redirect respond to
        ///     the action named 'Index'
        /// </summary>
        [Test]
        public void HttpPost_DeleteConfirmedMethod_RedirectsToIndex()
        {
            serviceMocker.Setup(e => e.GetById(It.IsAny<int>())).Returns(new Customer { BillingAddress = new Address(), StreetAddress = new Address() });

            var result = controller.DeleteConfirmed(1234);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

            var redirectResult = result as RedirectToRouteResult;

            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("Index"));
        }

        /// <summary>
        ///     Calls to 'delete confirmed' should call the underlying service's
        ///     'delete' method.
        /// </summary>
        [Test]
        public void HttpPost_DeleteConfirmedMethod_ValidViewModel_CallsService()
        {
            var deleteCalls = 0;

            serviceMocker.Setup(e => e.GetById(It.IsAny<int>())).Returns(new Customer { BillingAddress = new Address(), StreetAddress = new Address() });
            serviceMocker.Setup(e => e.Delete(It.IsAny<Customer>())).Callback(() => { ++deleteCalls; });

            Assert.That(deleteCalls, Is.EqualTo(0));

            controller.DeleteConfirmed(1234);

            Assert.That(deleteCalls, Is.EqualTo(1));
        }

        #endregion Delete Tests
    }
}
