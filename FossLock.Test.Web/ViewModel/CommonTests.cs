using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FossLock.Test.Web.ViewModel
{
    [TestFixture]
    internal class CommonTests
    {
        private IEnumerable<Type> allViewModelTypes = null;

        /// <summary>
        ///     Reinitializes all fields in this class before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var vmInterfaceType = typeof(FossLock.Web.ViewModels.IFossLockViewModel);

            // find all of our viewmodel types from the FossLock.Web assembly.
            allViewModelTypes = Assembly.GetAssembly(vmInterfaceType)
                .GetTypes().Where(
                    t =>
                        t.IsClass &&
                        t.GetInterfaces().Contains(vmInterfaceType)
                );
        }

        /// <summary>
        ///     Checks all the classes that implement IFossLockViewModel and make
        ///     sure that when instantiated, they are automatically initializing all
        ///     of their list-type properties automatically.
        /// </summary>
        [Test]
        public void Viewmodel_ListProperties_Initialized()
        {
            foreach (var t in allViewModelTypes)
            {
                var constructorInfo = t.GetConstructor(Type.EmptyTypes);

                var vm = Activator.CreateInstance(t);

                var listTypeProperties = t.GetProperties().Where(e =>
                    e.PropertyType != typeof(System.String) && (
                        e.PropertyType.GetInterfaces().Contains(typeof(ICollection)) ||
                        e.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
                    )
                );

                foreach (var p in listTypeProperties)
                {
                    Assert.IsNotNull(p.GetValue(vm),
                        string.Format("{0:s}.{1:s} was not initialized", t.Name, p.Name));
                }
            }
        }
    }
}
