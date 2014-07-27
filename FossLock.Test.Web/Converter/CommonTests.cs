using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;
using NUnit.Framework;

namespace FossLock.Test.Web.Converter
{
    [TestFixture]
    internal class CommonTests
    {
        private IEnumerable<Type> allConverters = null;

        [SetUp]
        public void SetUp()
        {
            allConverters = Assembly.GetAssembly(typeof(FossLock.Web.MvcApplication))
                .GetTypes().Where(
                    t => t.IsClass &&
                        // yeah, that's not 100% ambigious at all what's going on here..
                        // http://msdn.microsoft.com/en-us/library/ayfa0fcd(v=vs.110).aspx
                        // ... w.t.f.
                         t.GetInterface("IEntityConverter`2") != null ? true : false
                );
        }

        [Test]
        public void EntityToViewmodel_NullArgument_ThrowsException()
        {
            foreach (var t in allConverters)
            {
                // get the types that were used to construct the converter
                var genericTypes = t.GetInterface("IEntityConverter`2").GetGenericArguments();
                var entityType = genericTypes.First(typeParam => typeof(EntityBase).IsAssignableFrom(typeParam));
                var viewModelType = genericTypes.First(typeParam => typeof(IFossLockViewModel).IsAssignableFrom(typeParam));

                var converterInstance = Activator.CreateInstance(t);

                var entityToVm = t.GetMethod("EntityToViewmodel");

                try
                {
                    entityToVm.Invoke(converterInstance, new object[] { null });
                    Assert.Fail(string.Format("{0:s}.EntityToViewmodel(null) did not throw an exception.", t.Name));
                }
                catch (TargetInvocationException ex)
                {
                    Assert.IsInstanceOf<ArgumentNullException>(ex.InnerException, string.Format("{0:s}.EntityToViewmodel(null) threw an unexpected exception type", t.Name));
                    Assert.That(ex.InnerException, Is.InstanceOf<ArgumentNullException>());
                }
            }
        }

        //public void ViewmodelToEntity_NullViewmodel_ThrowsException()
        //{
        //    // test the first overload
        //    try
        //    {
        //        ProductViewModel vm = null;
        //        var p = converter.ViewmodelToEntity(vm);
        //        Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOf<ArgumentNullException>(ex);
        //        Assert.AreEqual("viewmodel", ((ArgumentNullException)ex).ParamName);
        //    }

        //    // test our second overload
        //    try
        //    {
        //        ProductViewModel vm = null;
        //        converter.ViewmodelToEntity(vm);
        //        Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOf<ArgumentNullException>(ex);
        //        Assert.AreEqual("viewmodel", ((ArgumentNullException)ex).ParamName);
        //    }
        //}

        //[Test(Description = "ViewmodelToEntity requires two, non-null arguments.  Additionally, the entity.Id property cannot be 0 (indicates it is not already in the database)")]
        //public void ViewmodelToEntity_FromEntity_NullViewmodelOrEntity_ThrowsException()
        //{
        //    var exceptions = new List<Exception>();

        //    try
        //    {
        //        converter.ViewmodelToEntity(null, fakeProduct);
        //    }
        //    catch (Exception ex)
        //    {
        //        exceptions.Add(ex);
        //    }

        //    try
        //    {
        //        converter.ViewmodelToEntity(fakeViewmodel, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        exceptions.Add(ex);
        //    }

        //    try
        //    {
        //        converter.ViewmodelToEntity(null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        exceptions.Add(ex);
        //    }

        //    var nullArgumentExceptionCount =
        //        exceptions.Where(e => e.GetType() == typeof(ArgumentNullException)).Count();

        //    Assert.AreEqual(3, nullArgumentExceptionCount);
        //}

        ///// <summary>
        /////     Make sure the entity returned from ViewmodelToEntity is the same instance
        /////     as the entity passed into the function.
        /////
        /////     NOTE: This is done simply to keep the return type consistant with the other
        /////           overload of this method to avoid confusion.
        ///// </summary>
        //[Test]
        //public void ViewmodelToEntity_TwoArgumentOverload_ReturnsSameEntityReference()
        //{
        //    var returnedEntity = converter.ViewmodelToEntity(fakeViewModel, fakeEntity);
        //    Assert.AreSame(fakeEntity, returnedEntity);
        //}

        // TODO: make sure that viewmodeltoentity(vm) calls the other overload with moq.
    }
}
