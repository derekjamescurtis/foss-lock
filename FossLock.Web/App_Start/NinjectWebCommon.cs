[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FossLock.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FossLock.Web.App_Start.NinjectWebCommon), "Stop")]

namespace FossLock.Web.App_Start
{
    using System;
    using System.Web;
    using FossLock.BLL.Service;
    using FossLock.DAL.Repository;
    using FossLock.Model;
    using FossLock.Web.ViewModels;
    using FossLock.Web.ViewModels.Converters;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // for customer controller
            // TODO:  These are really static bindings..
            // It's not obvious to me from the documentation how to actually accomplish this
            // in a generic mannor.  Come back here later and figure it out
            // i.e.) Ideally, I want to create one set of bindings that would work
            // for all the controllers (or maybe 2.. one for primaryentitycrud and another for
            // secondaryentitycrud)
            kernel.Bind<IRepository<Customer>>().To<EFRepository<Customer>>();
            kernel.Bind<IFossLockService<Customer>>().To<GenericService<Customer>>();
            kernel.Bind<IEntityConverter<Customer, CustomerViewModel>>().To<CustomerConverter>();

            kernel.Bind<IRepository<Product>>().To<EFRepository<Product>>();
            kernel.Bind<IFossLockService<Product>>().To<GenericService<Product>>();
            kernel.Bind<IEntityConverter<Product, ProductViewModel>>().To<ProductConverter>();

            kernel.Bind<IRepository<License>>().To<EFRepository<License>>();
            kernel.Bind<IFossLockService<License>>().To<GenericService<License>>();
            kernel.Bind<IEntityConverter<License, LicenseViewModel>>().To<LicenseConverter>();
        }
    }
}
