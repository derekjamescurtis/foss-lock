using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    /// <summary>
    ///     Provides methods for convertering between our Customer domain model
    ///     objects, and their corresponding ViewModels.
    /// </summary>
    public class CustomerConverter : IEntityConverter<Customer, CustomerViewModel>
    {
        public Customer ViewmodelToEntity(CustomerViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException("vm");

            Customer entity = new Customer();

            return ViewmodelToEntity(vm, entity);
        }

        // NOTE THIS IS VERY VERY IMPORTANT.
        // when copying the FossLock.Model.Component.* fields, we are not performing
        // any type of cloning.. they are set to references from the original object
        // this is not a problem in a web application with a request/response cycle
        // but if you end up reusing any of this code inside a GUI appliction, be warned!

        public Customer ViewmodelToEntity(CustomerViewModel vm, Customer entity)
        {
            if (vm == null)
                throw new ArgumentNullException("vm");
            else if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = vm.Id;
            entity.Name = vm.Name;
            entity.CanLicensePreReleaseVersions = vm.CanLicensePreReleaseVersions;

            entity.StreetAddress = vm.StreetAddress;
            entity.BillingAddress = vm.BillingMatchesStreetAddress ? entity.StreetAddress : vm.BillingAddress;

            entity.OfficePhone1 = vm.OfficePhone1;
            entity.OfficePhone2 = vm.OfficePhone2;
            entity.OfficeFax = vm.OfficeFax;
            entity.Email = vm.Email;
            entity.Notes = vm.Notes;
            entity.PrimaryContact = vm.PrimaryContact;

            return entity;
        }

        public CustomerViewModel EntityToViewmodel(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            throw new NotImplementedException();
        }
    }
}