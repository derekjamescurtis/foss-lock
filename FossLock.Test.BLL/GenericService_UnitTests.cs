using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FossLock.BLL.Service;
using FossLock.DAL.Repository;
using FossLock.Model;
using Moq;
using NUnit.Framework;

namespace FossLock.Test.BLL
{
    [TestFixture]
    public class GenericService_UnitTests
    {
        #region Test Setup

        private GenericService<Product> productService = null;

        // keeps track of the number of times these methods are called on productRepository
        private Dictionary<string, int> productRepoCalls = new Dictionary<string, int>
        {
            { "Add", 0 },
            { "Update", 0 },
            { "Delete", 0 },
            { "GetById", 0 },
            { "GetList", 0 }
        };

        [TestFixtureSetUp]
        public void SetUp()
        {
            // will be used in GetById and GetAll mocks
            var entities = new List<Product>();
            for (int i = 0; i < 100; i++)
                entities.Add(new Product { Id = i, Name = "Product" });

            // setup a fake repository for productService .. retrieves data from the above list.
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Add(It.IsAny<Product>()))
                .Callback(() => { productRepoCalls["Add"] += 1; })
                .Returns((Product entity) =>
                {
                    entity.Id = 1; // give this an ID so it registers as 'non transient'
                    return entity;
                });
            mockProductRepo
                .Setup(e => e.Update(It.IsAny<Product>()))
                .Callback(() => { productRepoCalls["Update"] += 1; })
                .Returns((Product entity) => { return entity; });
            mockProductRepo
                .Setup(e => e.Delete(It.IsAny<Product>()))
                .Callback(() => { productRepoCalls["Delete"] += 1; });
            mockProductRepo
                .Setup(e => e.GetById(It.IsAny<int>()))
                .Callback(() => { productRepoCalls["GetById"] += 1; })
                .Returns((int id) =>
                {
                    return entities.Find(e => e.Id == id);
                });
            mockProductRepo
                .Setup(e => e.GetAll())
                .Callback(() => { productRepoCalls["GetList"] += 1; })
                .Returns(entities);

            // we want our service to call all of it's real methods
            // they're all marked as virtual so we have to explicitally tell moq to call them.
            var mockProductService = new Mock<GenericService<Product>>(mockProductRepo.Object);
            mockProductService.Setup(e => e.New()).CallBase();

            mockProductService.Setup(e => e.Add(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.Update(It.IsAny<Product>())).CallBase();
            // mockProductService.Setup(e => e.Delete(It.IsAny<Product>()));

            mockProductService.Setup(e => e.ValidateAdd(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.ValidateUpdate(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.ValidateDelete(It.IsAny<Product>())).CallBase();

            mockProductService.Setup(e => e.GetById(It.IsAny<int>())).CallBase();
            mockProductService.Setup(e => e.GetList()).CallBase();
            mockProductService.Setup(e => e.GetList(It.IsAny<int>(), It.IsAny<int>())).CallBase();

            productService = mockProductService.Object;
        }

        #endregion Test Setup

        [Test]
        [Description("GenericService<T>.New() method should return transient instance of type T")]
        public void NewMethod_Returns_TransientOfExpectedType()
        {
            // make sure the repositories are only returning the expected types
            var p = productService.New();
            Assert.IsInstanceOf<Product>(p);
            Assert.IsTrue(p.IsTransient());
        }

        #region Retrieve Data Tests

        [Test]
        [Description(".GetById() should require a positive integer argument.. If not, we want to make ")]
        public void GetById_InvalidParameter_ThrowsException()
        {
            var repoGetByIdCalls = productRepoCalls["GetById"];

            try
            {
                // id cannot be less than 1
                var p = productService.GetById(0);
                Assert.Fail("Expected an ArgumentOutOfRangeException, but none thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentOutOfRangeException>(ex);
                Assert.AreEqual("id", ((ArgumentOutOfRangeException)ex).ParamName);
                Assert.AreEqual(repoGetByIdCalls, productRepoCalls["GetById"]);
            }
        }

        [Test]
        [Description(".GetById() returns null if no matches found.")]
        public void GetById_NoMatchingIdentity_ReturnsNull()
        {
            var repoGetByIdCalls = productRepoCalls["GetById"];

            var p = productService.GetById(50000); // request an ID well outside of our range in SetUp()

            Assert.IsNull(p);
            Assert.AreEqual((repoGetByIdCalls + 1), productRepoCalls["GetById"]);
        }

        [Test]
        [Description(".GetById() should return expected entity.")]
        public void GetById_ValidId_ReturnsExpectedResult()
        {
            var repoGetByIdCalls = productRepoCalls["GetById"];

            var idRequested = 1;
            var p = productService.GetById(idRequested); // request an ID well outside of our range in SetUp()

            Assert.IsInstanceOf<Product>(p);
            Assert.AreEqual(idRequested, p.Id);
            Assert.AreEqual((repoGetByIdCalls + 1), productRepoCalls["GetById"]);
        }

        [Test]
        [Description(".GetList() should return the expected results.")]
        public void GetList_Returns_ExpectedSlice()
        {
            var repoGetList = productRepoCalls["GetList"];

            // make sure our full list is what we expect
            var fullList = productService.GetList();
            Assert.AreEqual(100, fullList.Count,
                "We're expecting 100 elements to exist in our mock"); // weaksauce.. dependent on list setup in SetUp()
            Assert.AreEqual((repoGetList + 1), productRepoCalls["GetList"],
                "A single call to the underlying repository is expected");

            var partialList = productService.GetList(2, 10);
            Assert.AreEqual(10, partialList.Count,
                "Partial list should contain 10 elements.");
            Assert.AreEqual(10, partialList[0].Id,
                "First element in the partial list should have Id==10");
            Assert.AreEqual(19, partialList[9].Id,
                "Last element in the partial list should have Id==19");
            Assert.AreEqual((repoGetList + 2), productRepoCalls["GetList"],
                "A second call to the underlying repo is now expected.");

            partialList = productService.GetList(pageNumber: 1000, pageSize: 10);
            Assert.AreEqual(0, partialList.Count,
                "A request outside of the bound of the list should return an empty list.");
            Assert.AreEqual((repoGetList + 3), productRepoCalls["GetList"],
                "A third call to the underlying repo is now expected.");

            partialList = productService.GetList(pageNumber: 10, pageSize: 11);
            Assert.AreEqual(1, partialList.Count,
                "A request extending past the bounds of the list should return a list with just the items until the end of the list.");
            Assert.AreEqual((repoGetList + 4), productRepoCalls["GetList"],
                "A fourth call to the underlying repo is now expected.");
        }

        #endregion Retrieve Data Tests

        #region .Add(T entity) Tests

        [Test]
        [Description("GenericService<T>.Add() should throw ArgumentNullException is " +
            "a null entity is provided.  Underlying repository .Add should never get called.")]
        public void AddMethod_NullEntity_ThrowsException()
        {
            var repoAddCalls = productRepoCalls["Add"];

            try
            {
                productService.Add(null);
                Assert.Fail("Expected ArgumentNullException but none thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);

                // make sure underlying repository .Add hasn't been called.
                Assert.AreEqual(repoAddCalls, productRepoCalls["Add"]);
            }
        }

        [Test]
        [Description("Add method should throw an exception if any validation " +
            " failures on entity.  Underlying repository .Add should never get called.")]
        public void AddMethod_NonValidEntity_ThrowsException()
        {
            var repoAddCalls = productRepoCalls["Add"];

            // setup a product that fails validation
            var mockProduct = new Mock<Product>();
            mockProduct
                .Setup(e => e.ValidationResults())
                .Returns(new List<ValidationResult> {
                    new ValidationResult("Yo dawg, you didn't validate."),
                    new ValidationResult("Yep.. really didn't validate.")
                });

            try
            {
                productService.Add(mockProduct.Object);
                Assert.Fail("Expected an ArgumentException but received none.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);

                // make sure underlying repository .Add hasn't been called.
                Assert.AreEqual(repoAddCalls, productRepoCalls["Add"]);
            }
        }

        [Test]
        [Description("Add method should complete successfully as long as the ValidateAdd() " +
            "returns an empty list.  Underlying repository .Add should get called one time.")]
        public void AddMethod_ValidEntity_Succeeds()
        {
            var repoAddCalls = productRepoCalls["Add"];

            var p = productService.New();

            Assert.IsTrue(p.IsTransient());

            p = productService.Add(p);

            Assert.IsTrue(p.IsTransient() == false);
            Assert.AreEqual((repoAddCalls + 1), productRepoCalls["Add"]);
        }

        #endregion .Add(T entity) Tests

        #region .Update(T entity) Tests

        [Test]
        [Description(".Update() should raise a NullArgumentException if it is passed a null. " +
            "Underlying repository .Update() should not be called.")]
        public void UpdateMethod_NullEntity_ThrowsException()
        {
            var repoUpdateCalls = productRepoCalls["Update"];

            try
            {
                productService.Update(null);
                Assert.Fail("Expected an ArgumentNullException but none thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
                Assert.AreEqual(repoUpdateCalls, productRepoCalls["Update"]);
            }
        }

        [Test]
        [Description("Update method should throw an exception if the entity reports itself invalid. " +
            "Underlying repository .Update() should not be called.")]
        public void UpdateMethod_NonValidEntity_ThrowsException()
        {
            var repoUpdateCalls = productRepoCalls["Update"];

            var validationErrors = new List<ValidationResult> {
                    new ValidationResult("Yo dawg, you didn't validate."),
                    new ValidationResult("Yep.. really didn't validate.")
                };

            var mockProduct = new Mock<Product>();
            mockProduct
                .Setup(e => e.ValidationResults())
                .Returns(validationErrors);

            var p = mockProduct.Object;
            p.Id = 1; // anything other than 0 looks like it's in the database

            Assert.AreEqual(false, p.IsTransient());
            Assert.AreEqual(validationErrors.Count, p.ValidationResults().Count);

            try
            {
                productService.Update(p);
                Assert.Fail("Expected an ArgumentException but none thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);
                Assert.AreEqual(repoUpdateCalls, productRepoCalls["Update"]);
            }
        }

        // add -- repository IS called
        [Test]
        [Description("Update method should complete successfully as long as ValidateUpdate() " +
            "returns an empty list. Underlying repository .Update() should be called exactly one time.")]
        public void UpdateMethod_ValidEntity_Succeeds()
        {
            var repoUpdateCalls = productRepoCalls["Update"];

            var p = productService.New();
            p.Id = 1;

            productService.Update(p);

            Assert.AreEqual((repoUpdateCalls + 1), productRepoCalls["Update"]);
        }

        #endregion .Update(T entity) Tests

        #region .Delete(T entity) Tests

        [Test]
        [Description("Delete() should throw an exception when passed a null. " +
            "Underlying repository's .Delete() method should not be called.")]
        public void DeleteMethod_NullEntity_ThrowsException()
        {
            var repoDeleteCalls = productRepoCalls["Delete"];

            try
            {
                productService.Delete(null);
                Assert.Fail("ArgumentNullException expected, but none thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
                Assert.AreEqual(repoDeleteCalls, productRepoCalls["Delete"]);
            }
        }

        [Test]
        [Description("Delete() should thrown an exception when validation fails. " +
            "Underlying repository's .Delete() method should not be called.")]
        public void DeleteMethod_NonValidEntity_ThrowsException()
        {
            var repoDeleteCalls = productRepoCalls["Delete"];

            var p = productService.New();
            Assert.IsTrue(p.IsTransient()); // not in database will cause this to fail

            try
            {
                productService.Delete(p);
                Assert.Fail("ArgumentException was expected, but none was thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex);
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);
                Assert.AreEqual(repoDeleteCalls, productRepoCalls["Delete"]);
            }
        }

        [Test]
        [Description("Delete method should succeed when validation passes. " +
            "Underlying repository's .Delete() method should be called exactly once.")]
        public void DeleteMethod_ValidEntity_Succeeds()
        {
            var repoDeleteCalls = productRepoCalls["Delete"];

            var p = productService.New();
            p.Id = 1;

            Assert.IsFalse(p.IsTransient());

            productService.Delete(p);

            Assert.AreEqual((repoDeleteCalls + 1), productRepoCalls["Delete"]);
        }

        #endregion .Delete(T entity) Tests

        /*
         * For the following method calls, if the function returns an
         * empty list, that is considered successful validation.  Null
         * returns should never occur, and exceptions should not be thrown here.
         */

        #region ValidateAdd Tests

        [Test]
        [Description("ValidateAdd should return failure results if the entity is not transient.")]
        public void ValidateAdd_NonTransientEntity_Fails()
        {
            var p = productService.New();
            p.Id = 1; // make entity think it has a database-generated id
            Assert.IsFalse(p.IsTransient());

            var results = productService.ValidateAdd(p);
            Assert.AreNotEqual(0, results.Count);
        }

        [Test]
        [Description("ValidateAdd() should return results if the Entity fails to validate itself.")]
        public void ValidateAdd_InvalidEntity_Fails()
        {
            var failProductMock = new Mock<Product>();
            failProductMock
                .Setup(e => e.ValidationResults())
                .Returns(new List<ValidationResult>
                    {
                        new ValidationResult("Fail is you"),
                        new ValidationResult("Still fail.")
                    });

            var failProduct = failProductMock.Object;

            Assert.IsTrue(failProduct.IsTransient());
            Assert.AreEqual(2, failProduct.ValidationResults().Count);

            var results = productService.ValidateAdd(failProduct);
            Assert.AreEqual(2, results.Count);

            // make sure that transient returns an extra error
            failProduct.Id = 1;
            Assert.IsFalse(failProduct.IsTransient());
            Assert.AreEqual(2, failProduct.ValidationResults().Count);

            results = productService.ValidateAdd(failProduct);
            Assert.AreEqual(3, results.Count);
        }

        [Test]
        [Description("ValidateAdd() should return nothing for a valid entity.")]
        public void ValidateAdd_ValidTransient_Succeeds()
        {
            var p = productService.New();

            Assert.IsTrue(p.IsTransient());
            Assert.AreEqual(0, p.ValidationResults().Count);

            var results = productService.ValidateAdd(p);
            Assert.AreEqual(0, results.Count);
        }

        #endregion ValidateAdd Tests

        #region ValidateUpdate Tests

        [Test]
        [Description("ValidateUpdate() should return failures for a transient entity.")]
        public void ValidateUpdate_TransientEntity_Fails()
        {
            var p = productService.New();
            Assert.IsTrue(p.IsTransient());
            Assert.AreEqual(0, p.ValidationResults().Count);

            var results = productService.ValidateUpdate(p);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        [Description("ValidateUpdate() should return failures for a transient entity.")]
        public void ValidateUpdate_InvalidEntity_Fails()
        {
            var validationFails = new List<ValidationResult> {
                    new ValidationResult("Fail is you"),
                    new ValidationResult("Still fail.")
                };

            var failProductMock = new Mock<Product>();
            failProductMock
                .Setup(e => e.ValidationResults())
                .Returns(validationFails);

            var failProduct = failProductMock.Object;
            failProduct.Id = 1; // fake having a db primary key

            Assert.IsFalse(failProduct.IsTransient());
            Assert.AreEqual(validationFails.Count, failProduct.ValidationResults().Count);

            var results = productService.ValidateUpdate(failProduct);
            Assert.AreEqual(validationFails.Count, results.Count);

            // pretend we're not in the db.. make sure that returns an extra fail
            failProduct.Id = 0;
            Assert.IsTrue(failProduct.IsTransient());
            results = productService.ValidateUpdate(failProduct);

            Assert.AreEqual((validationFails.Count + 1), results.Count);
        }

        [Test]
        [Description("Non-transient entity with no validation errors should validate.")]
        public void ValidateUpdate_NonTransientValid_Succeeds()
        {
            var p = productService.New();
            p.Id = 1; // fake a db primary key

            Assert.IsFalse(p.IsTransient());
            Assert.AreEqual(0, p.ValidationResults().Count);

            var results = productService.ValidateUpdate(p);
            Assert.AreEqual(0, results.Count);
        }

        #endregion ValidateUpdate Tests

        #region ValidateDelete Tests

        [Test]
        [Description("Transient entities can't be deleted because they're not in the data store yet.")]
        public void ValidateDelete_TransientEntity_Fails()
        {
            var p = productService.New();
            Assert.IsTrue(p.IsTransient());
            Assert.AreEqual(0, p.ValidationResults().Count);

            var results = productService.ValidateDelete(p);
            Assert.AreEqual(1, results.Count);
        }

        /* validation errors at the entity level should not have any impact on delete
         * succeeding.. for constraints, like "You can't delete this entity because --whatever-- reason",
         * this should be enforced by overriding ValidateDelete in a derrived class.
         * NOTE: the results of ValidateDelete() SHOULD NOT BE IGNORED by the .Delete() method.. the only
         * thing we're ignoring is the entity's validation
         */

        [Test]
        [Description("Entity validation results should be ignored because the object is being deleted.")]
        public void ValidateDelete_InvalidEntity_Succeeds()
        {
            var validationFails = new List<ValidationResult>{
                new ValidationResult("I am a fail."),
                new ValidationResult("So am I.")
            };

            var failProductMock = new Mock<Product>();
            failProductMock
                .Setup(e => e.ValidationResults())
                .Returns(validationFails);

            var p = failProductMock.Object;
            p.Id = 1; // fake being in the database

            Assert.IsFalse(p.IsTransient());
            Assert.AreEqual(validationFails.Count, p.ValidationResults().Count);

            var results = productService.ValidateDelete(p);
            Assert.AreEqual(0, results.Count);
        }

        #endregion ValidateDelete Tests
    }
}