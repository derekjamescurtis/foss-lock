using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FossLock.BLL.Service;
using FossLock.DAL.Repository;
using FossLock.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FossLock.Test.BLL
{

    /*
     * TODO: data retrieval tests
     */
    [TestClass]
    public class GenericService_UnitTests
    {
        #region Test Setup

        GenericService<Product> productService = null;

        // keeps track of the number of times these methods are called on productRepository
        Dictionary<string, int> productRepoCalls = new Dictionary<string, int> 
        { 
            { "Add", 0 },
            { "Update", 0 },
            { "Delete", 0 }
        };

        [TestInitialize]
        public void SetUp() 
        { 
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
                .Callback(() =>  { productRepoCalls["Update"] += 1; })
                .Returns((Product entity) => { return entity; });
            mockProductRepo
                .Setup(e => e.Delete(It.IsAny<Product>()))
                .Callback(() => { productRepoCalls["Delete"] += 1; });


            
            // we want our service to call all of it's real methods
            // they're all marked as virtual so we have to explicitally tell moq to call them.
            var mockProductService = new Mock<GenericService<Product>>(mockProductRepo.Object);
            mockProductService.Setup(e => e.New()).CallBase();

            mockProductService.Setup(e => e.Add(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.Update(It.IsAny<Product>())).CallBase();

            // TODO: blah make sure this works right
            // mockProductService.Setup(e => e.Delete(It.IsAny<Product>())); -- does this not work with void return types??

            mockProductService.Setup(e => e.ValidateAdd(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.ValidateUpdate(It.IsAny<Product>())).CallBase();
            mockProductService.Setup(e => e.ValidateDelete(It.IsAny<Product>())).CallBase();



            productService = mockProductService.Object;
        }

        #endregion

        [TestMethod]
        [Description("GenericService<T>.New() method should return transient instance of type T")]
        public void NewMethod_Returns_TransientOfExpectedType()
        {
            // make sure the repositories are only returning the expected types
            var p = productService.New();            
            Assert.IsInstanceOfType(p, typeof(Product));
            Assert.IsNotInstanceOfType(p, typeof(ProductFeature));
            Assert.IsTrue(p.IsTransient());    
        }

        #region Retrieve Data Tests



        #endregion

        #region .Add(T entity) Tests

        [TestMethod]
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
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);

                // make sure underlying repository .Add hasn't been called.
                Assert.AreEqual(repoAddCalls, productRepoCalls["Add"]);
            }
        }

        [TestMethod]
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
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);

                // make sure underlying repository .Add hasn't been called.
                Assert.AreEqual(repoAddCalls, productRepoCalls["Add"]);
            }
        }

        [TestMethod]
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


        #endregion

        #region .Update(T entity) Tests

        [TestMethod]
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
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
                Assert.AreEqual(repoUpdateCalls, productRepoCalls["Update"]);
            }
        }

        [TestMethod]
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
                Assert.IsInstanceOfType(ex, typeof(ArgumentException), ex.ToString());
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);
                Assert.AreEqual(repoUpdateCalls, productRepoCalls["Update"]);
            }
        }

        // add -- repository IS called
        [TestMethod]
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

        #endregion

        #region .Delete(T entity) Tests

        // add -- repository not called
        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_NullEntity_ThrowsException()
        {
            Assert.Inconclusive("not implemented");
        }

        // add -- repository not called
        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_NonValidEntity_ThrowsException()
        {
            Assert.Inconclusive("not implemented.");
        }

        // add -- repository IS called
        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_ValidEntity_Succeeds()
        {
            Assert.Inconclusive("not implemented.");
        }

        #endregion


        /*
         * For the following method calls, if the function returns an 
         * empty list, that is considered successful validation.  Null
         * returns should never occur, and exceptions should not be thrown here.
         */

        #region ValidateAdd Tests

        public void ValidateAdd_NonTransientEntity_Fails()
        { }

        public void ValidateAdd_TransientEntity_Succeeds()
        { }

        public void ValidateAdd_InvalidEntity_Fails()
        { }

        public void ValidateAdd_ValidTransient_Succeeds()
        { }

        #endregion

        #region ValidateUpdate Tests

        public void ValidateUpdate_TransientEntity_Fails()
        { }

        public void ValidateUpdate_NonTransient_Succeeds()
        { }

        public void ValidateUpdate_InvalidEntity_Fails()
        { }

        public void ValidateUpdate_NonTransientValid_Succeeds()
        { }

        #endregion

        #region ValidateDelete Tests

        public void ValidateDelete_TransientEntity_Fails()
        { }

        public void ValidateDelete_NonTransient_Succeeds()
        { }

        // validation errors at the entity level should not have any impact on delete 
        // succeeding.. for constraints, like "You can't delete this entity because --whatever-- reason",
        // this should be enforced by overriding ValidateDelete in a derrived class.
        public void ValidateDelete_InvalidEntity_Succeeds()
        { }

        #endregion


    }
}
