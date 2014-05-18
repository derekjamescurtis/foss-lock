using System;
using FossLock.BLL.Service;
using FossLock.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FossLock.Test.BLL
{
    [TestClass]
    public class GenericService_UnitTests
    {
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
        [Description("Not yet implemented!")]
        public void AddMethod_NonTransientEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void AddMethod_NonValidEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void AddMethod_ValidEntity_Succeeds() { Assert.Inconclusive("not implemented."); }
        // public void AddMethod_ValidEntity_ReturnsNonTransient() { } -> this actually should be tested as part of the repository.

        [TestMethod]
        [Description("Not yet implemented!")]
        public void UpdateMethod_TransientEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void UpdateMethod_NonValidEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void UpdateMethod_ValidEntity_Succeeds() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_TransientEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_NonValidEntity_ThrowsException() { Assert.Inconclusive("not implemented."); }

        [TestMethod]
        [Description("Not yet implemented!")]
        public void DeleteMethod_ValidEntity_Succeeds() { Assert.Inconclusive("not implemented."); }

    }
}
