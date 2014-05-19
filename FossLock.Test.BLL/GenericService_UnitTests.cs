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
    [TestClass]
    public class GenericService_UnitTests
    {
        [TestInitialize]
        public void SetUp() { }


        [TestMethod]
        [Description("GenericService<T> constructor should never accept null arguments.")]
        public void Constructor_NullRepository_ThrowsException()
        {
            try
            {
                var productService = new GenericService<Product>(null);
                Assert.Fail("Exception was expected from constructor when passed null argument.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("repository", ex.ParamName);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception type: {0}\nMessage: {1}", ex.GetType(), ex.Message);
            }
        }

        [TestMethod]
        [Description("GenericService<T>.New() method should return transient instance of type T")]
        public void NewMethod_Returns_TransientOfExpectedType()
        {
            // setup our fake services
            var mockProductRepo = new Mock<IRepository<Product>>();
            var productService = new GenericService<Product>(mockProductRepo.Object);
            var mockFeatureRepo = new Mock<IRepository<ProductFeature>>();
            var featureService = new GenericService<ProductFeature>(mockFeatureRepo.Object);

            // make sure the repositories are only returning the expected types
            var p = productService.New();
            Assert.IsInstanceOfType(p, typeof(Product));
            Assert.IsNotInstanceOfType(p, typeof(ProductFeature));
            Assert.IsTrue(p.IsTransient());

            var f = featureService.New();
            Assert.IsInstanceOfType(f, typeof(ProductFeature));
            Assert.IsNotInstanceOfType(f, typeof(Product));
            Assert.IsTrue(f.IsTransient());            
        }

        #region Retrieve Data Tests



        #endregion

        #region .Add(T entity) Tests

        [TestMethod]
        [Description("GenericService<T>.Add() should throw ArgumentNullException is a null entity is provided.")]
        public void AddMethod_NullEntity_ThrowsException()
        {
            var mockProductRepo = new Mock<IRepository<Product>>();
            var productService = new GenericService<Product>(mockProductRepo.Object);
            try
            {
                productService.Add(null);
                Assert.Fail("Expected ArgumentNullException but none thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
            }
        }

        [TestMethod]
        [Description("Add method can only be called on transient entities.")]
        public void AddMethod_NonTransientEntity_ThrowsException() 
        {
            var mockProductRepo = new Mock<IRepository<Product>>();
            var productService = new GenericService<Product>(mockProductRepo.Object);

            /* hack: IsTransient() is just based on whether or not our entity has an id.. 
             * so we just sent an Id to make it think our entity is in the db
             * can't use interfaces for our generics because they require concrete types 
             * and ef won't play nicely with our inheritance chain if we use interfaces for type declarations
             * could just mark the method as virtual and then we could override it with Moq.
             */
            var p = productService.New();
            p.Id = 1;

            try
            {
                productService.Add(p);
                Assert.Fail("Supply a non-transient entity to GenericService<T>.Add should throw an ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                // this is what we want
                Assert.AreEqual("entity", ex.ParamName);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception type: {0}\nMessage: {1}", ex.GetType(), ex.Message);

            }
        }

        [TestMethod]
        [Description("Add method should throw an exception if any validation failures on entity.")]
        public void AddMethod_NonValidEntity_ThrowsException() 
        {
            // setup our repository
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Add(It.IsAny<Product>()))
                .Returns((Product entity) => { 
                    entity.Id = 1; 
                    return entity; 
                });            

            var productService = new GenericService<Product>(mockProductRepo.Object);

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
            catch (ArgumentException ex) 
            {
                Assert.AreEqual("entity", ex.ParamName, true, ex.ToString());
            }
            catch (Exception ex) 
            {
                Assert.Fail("Unexpected exception type: {0}\n" + 
                    "Message: {1}", ex.GetType(), ex.Message);
            }
        }

        [TestMethod]
        [Description("Add method should complete successfully as long as the ValidateAdd() returns an empty list.")]
        public void AddMethod_ValidEntity_Succeeds() 
        {
            // setup our repository
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Add(It.IsAny<Product>()))
                .Returns((Product entity) => {
                    entity.Id = 1;
                    return entity;
                });
            
            // make sure validation succeeds
            var mockProductService = new Mock<GenericService<Product>>(mockProductRepo.Object);
            mockProductService
                .Setup(e => e.ValidateAdd(It.IsAny<Product>()))
                .Returns(new List<ValidationResult>());
            
            var productService = mockProductService.Object;

            var p = productService.New();
            p = productService.Add(p);

            // this should succeed, and product should now have an id of 1 
            Assert.AreEqual(1, p.Id);

        }

        #endregion

        #region .Update(T entity) Tests

        [TestMethod]
        [Description("Update method should raise a NullArgumentException if it is passed a null.")]
        public void UpdateMethod_NullEntity_ThrowsException()
        {
            var mockProductRepo = new Mock<IRepository<Product>>();
            var productService = new GenericService<Product>(mockProductRepo.Object);

            try
            {
                productService.Update(null);
                Assert.Fail("Expected an ArgumentNullException but none thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual("entity", ((ArgumentNullException)ex).ParamName);
            }
        }

        [TestMethod]
        [Description("Update method can only be called on objects already in the data store.")]
        public void UpdateMethod_TransientEntity_ThrowsException() 
        {
            // setup our repository
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Update(It.IsAny<Product>()))
                .Returns((Product entity) => { return entity; });

            var productService = new GenericService<Product>(mockProductRepo.Object);
            var p = productService.New();

            Assert.IsTrue(p.IsTransient());

            try
            {
                productService.Update(p);
                Assert.Fail("Expected an ArgumentException but none thrown.");
            }
            catch (Exception ex) 
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);
            }
        }

        [TestMethod]
        [Description("Update method should throw an exception if the entity reports itself invalid.")]
        public void UpdateMethod_NonValidEntity_ThrowsException() 
        {
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Update(It.IsAny<Product>()))
                .Returns((Product entity) => { return entity; });

            var productService = new GenericService<Product>(mockProductRepo.Object);

            var mockProduct = new Mock<Product>();            
            mockProduct
                .Setup(e => e.ValidationResults())
                .Returns(new List<ValidationResult> { 
                    new ValidationResult("Yo dawg, you didn't validate."),
                    new ValidationResult("Yep.. really didn't validate.")
                });

            var p = mockProduct.Object;
            p.Id = 1;

            Assert.AreEqual(false, p.IsTransient());
            Assert.AreEqual(2, p.ValidationResults().Count);

            try
            {
                productService.Update(p);
                Assert.Fail("Expected an ArgumentException but none thrown.");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException), ex.ToString());
                Assert.AreEqual("entity", ((ArgumentException)ex).ParamName);
            }
        }

        [TestMethod]
        [Description("Update method should complete successfully as long as ValidateUpdate() returns an empty list.")]
        public void UpdateMethod_ValidEntity_Succeeds() 
        {
            var mockProductRepo = new Mock<IRepository<Product>>();
            mockProductRepo
                .Setup(e => e.Update(It.IsAny<Product>()))
                .Returns((Product entity) => { return entity; });

            var productService = new GenericService<Product>(mockProductRepo.Object);

            var p = productService.New();
            p.Id = 1;

            productService.Update(p);            
        }

        #endregion

        #region .Delete(T entity) Tests

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_NullEntity_ThrowsException()
        {
            Assert.Inconclusive("not implemented");
        }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_TransientEntity_ThrowsException() 
        { 
            Assert.Inconclusive("not implemented."); 
        }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_NonValidEntity_ThrowsException() 
        { 
            Assert.Inconclusive("not implemented."); 
        }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_ValidEntity_Succeeds() 
        { 
            Assert.Inconclusive("not implemented.");
        }

        #endregion

    }
}
