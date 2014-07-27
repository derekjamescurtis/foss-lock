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

        [SetUp]
        public void SetUp()
        {
            var vmInterfaceType = typeof(FossLock.Web.ViewModels.IFossLockViewModel);

            allViewModelTypes = Assembly.GetAssembly(vmInterfaceType)
                .GetTypes().Where(
                    t =>
                        t.IsClass &&
                        t.GetInterfaces().Contains(vmInterfaceType)
                );
        }

        [Test]
        public void ViewmodelListPropertiesInitialized()
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
