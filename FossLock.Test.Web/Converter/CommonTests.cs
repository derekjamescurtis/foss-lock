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
    /// <summary>
    ///     Contains classes that run against all objects defined in the
    ///     FossLock.Web.ViewModels.Converters namespace (Classes whose
    ///     job is to convert ViewModels <-> Entity classes.) and are
    ///     resolved using reflection.
    /// </summary>
    [TestFixture]
    internal class CommonTests
    {
        private IEnumerable<Type> allConverterTypes = null;

        [SetUp]
        public void SetUp()
        {
            allConverterTypes = Assembly.GetAssembly(typeof(FossLock.Web.MvcApplication))
                .GetTypes().Where(
                    t => t.IsClass &&
                        // yeah, that's not 100% ambigious at all what's going on here..
                        // http://msdn.microsoft.com/en-us/library/ayfa0fcd(v=vs.110).aspx
                        // ... w.t.f.
                         t.GetInterface("IEntityConverter`2") != null ? true : false
                );
        }

        /// <summary>
        ///     Verify EntityToViewmodel a non-null argument.  When encountered, we're expecting
        ///     an ArgumentNullException with the proper ParamType set.
        /// </summary>
        [Test]
        public void EntityToViewmodel_NullArgument_ThrowsException()
        {
            foreach (var converterType in allConverterTypes)
            {
                // get the types that were used to construct the converter
                var genericTypes = converterType.GetInterface("IEntityConverter`2").GetGenericArguments();
                var entityType = genericTypes.First(typeParam => typeof(EntityBase).IsAssignableFrom(typeParam));
                var viewModelType = genericTypes.First(typeParam => typeof(IFossLockViewModel).IsAssignableFrom(typeParam));

                var converterInstance = Activator.CreateInstance(converterType);

                var entityToVm = converterType.GetMethod("EntityToViewmodel");

                try
                {
                    entityToVm.Invoke(converterInstance, new object[] { null });
                    Assert.Fail(string.Format("{0:s}.EntityToViewmodel(null) did not throw an exception.", converterType.Name));
                }
                catch (TargetInvocationException ex)
                {
                    // because we're calling the method through reflection,
                    // Type.Invoke will wrap the exception in a TargetInvocationException
                    Assert.IsInstanceOf<ArgumentNullException>(ex.InnerException,
                        string.Format("{0:s}.EntityToViewmodel(null) threw an unexpected exception type", converterType.Name));
                    Assert.That(ex.InnerException, Is.InstanceOf<ArgumentNullException>());

                    var actualException = ex.InnerException as ArgumentNullException;
                    Assert.That(actualException.ParamName, Is.EqualTo("entity"));
                }
            }
        }

        /// <summary>
        ///     First ViewmodelToEntity overload test.
        ///     Verify requires a single non-null argument.  If not provided, expect an
        ///     ArgumentNullException with the ParamType property set.
        /// </summary>
        public void ViewmodelToEntity_SingleParamOverload_NullViewmodel_ThrowsException()
        {
            foreach (var converterType in allConverterTypes)
            {
                // get the types that were used to construct the converter
                var genericTypes = converterType.GetInterface("IEntityConverter`2").GetGenericArguments();
                var entityType = genericTypes.First(typeParam => typeof(EntityBase).IsAssignableFrom(typeParam));
                var viewModelType = genericTypes.First(typeParam => typeof(IFossLockViewModel).IsAssignableFrom(typeParam));

                var converterInstance = Activator.CreateInstance(converterType);

                var entityToVm = converterType.GetMethod("ViewmodelToEntity", new Type[] { viewModelType });

                try
                {
                    entityToVm.Invoke(converterInstance, new object[] { null });
                    Assert.Fail("An ArgumentNullException was expected, but none was thrown.");
                }
                catch (TargetInvocationException ex)
                {
                    Assert.IsInstanceOf<ArgumentNullException>(ex.InnerException,
                        string.Format("{0:s}.ViewmodelToEntity(null) threw an unexpected exception type", converterType.Name));
                    Assert.That(ex.InnerException, Is.InstanceOf<ArgumentNullException>());

                    var actualException = ex.InnerException as ArgumentNullException;
                    Assert.That(actualException.ParamName, Is.EqualTo("vm"));
                }
            }
        }

        /// <summary>
        ///     Test the second overload of ViewmodelToEntity.
        ///     This requires two non-null arguments (a viewmodel and an entity).
        /// </summary>
        [Test]
        public void ViewmodelToEntity_FromEntity_NullViewmodelOrEntity_ThrowsException()
        {
            foreach (var converterType in allConverterTypes)
            {
                // get the types that were used to construct the converter
                var genericTypes = converterType.GetInterface("IEntityConverter`2").GetGenericArguments();
                var entityType = genericTypes.First(typeParam => typeof(EntityBase).IsAssignableFrom(typeParam));
                var viewModelType = genericTypes.First(typeParam => typeof(IFossLockViewModel).IsAssignableFrom(typeParam));

                var converterInstance = Activator.CreateInstance(converterType);

                var entityToVm = converterType.GetMethod("ViewmodelToEntity", new Type[] { viewModelType, entityType.MakeByRefType() });

                var entityInstance = Activator.CreateInstance(entityType);
                var viewModelInstance = Activator.CreateInstance(viewModelType);

                // make sure each possible combination of parameters throws an exception
                var ex1 = Assert.Throws<TargetInvocationException>(
                    () => { entityToVm.Invoke(converterInstance, new object[] { null, null }); },
                    string.Format("{0:s}.ViewmodelToEntity(null, null) did not throw an Exception", converterType.Name));

                var ex2 = Assert.Throws<TargetInvocationException>(
                    () => { entityToVm.Invoke(converterInstance, new object[] { viewModelInstance, null }); },
                    string.Format("{0:s}.ViewmodelToEntity(viewmodel, null) did not throw an Exception", converterType.Name));

                var ex3 = Assert.Throws<TargetInvocationException>(
                    () => { entityToVm.Invoke(converterInstance, new object[] { null, entityInstance }); },
                    string.Format("{0:s}.ViewmodelToEntity(null, entity) did not throw an Exception", converterType.Name));

                // note: we can't actually test this here.. because if there are additional constraints placed on
                // the input parameters, there might still be exceptions thrown.
                // and that it doesn't, if we provide valid arguments
                //Assert.DoesNotThrow(() => { entityToVm.Invoke(converterInstance, new object[] { viewModelInstance, entityInstance }); },
                //    string.Format("{0:s}.ViewmodelToEntity(viewmodel, entity) Threw an Exception", converterType.Name));

                // test to make sure the exception thrown was what we expected
                foreach (var ex in new TargetInvocationException[] { ex1, ex2, ex3 })
                {
                    Assert.IsInstanceOf<ArgumentNullException>(ex.InnerException,
                        string.Format("{0:s}.ViewmodelToEntity thew an unexpected exception type.", converterType.Name));
                    Assert.That(((ArgumentNullException)ex.InnerException).ParamName, Is.EqualTo("vm").Or.EqualTo("entity"),
                        string.Format("{0:s}.ViewmodelToEntity did not properly report the parameter causing the ArgumentNullException", converterType.Name));
                }
            }
        }
    }
}
