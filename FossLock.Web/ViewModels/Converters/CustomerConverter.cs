using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;
using FossLock.Model.Component;
using FossLock.Web.ViewModels.Extensions;

namespace FossLock.Web.ViewModels.Converters
{
    public class CustomerConverter : IEntityConverter<Customer, CustomerViewModel>
    {
        public CustomerViewModel EntityToViewmodel(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var vm = new CustomerViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                StreetAddress = entity.StreetAddress.ToViewModel(),
                BillingAddress = entity.BillingAddress.ToViewModel(),
                OfficePhone1 = entity.OfficePhone1,
                OfficePhone2 = entity.OfficePhone2,
                OfficeFax = entity.OfficeFax,
                Email = entity.Email,
                Notes = entity.Notes,
                PrimaryContact = entity.PrimaryContact.ToViewModel(),
                ProductLicenses = entity.ProductLicenses
            };

            vm.BillingMatchesStreetAddress = vm.BillingAddress.Equals(vm.StreetAddress);

            return vm;
        }

        public void ViewmodelToEntity(CustomerViewModel vm, ref Customer entity)
        {
            if (vm == null)
                throw new ArgumentNullException("vm");
            else if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = vm.Id;
            entity.Name = vm.Name;

            entity.StreetAddress = vm.StreetAddress.ToEntity();
            if (vm.BillingMatchesStreetAddress)
            {
                entity.BillingAddress = new Address
                {
                    Address1 = vm.StreetAddress.Address1,
                    Address2 = vm.StreetAddress.Address2,
                    City = vm.StreetAddress.City,
                    State = vm.StreetAddress.State,
                    PostalCode = vm.StreetAddress.PostalCode,
                    Country = vm.StreetAddress.Country,
                };
            }
            else
            {
                entity.BillingAddress = vm.BillingAddress.ToEntity();
            }

            entity.OfficePhone1 = vm.OfficePhone1;
            entity.OfficePhone2 = vm.OfficePhone2;
            entity.OfficeFax = vm.OfficeFax;
            entity.Email = vm.Email;
            entity.Notes = vm.Notes;
            entity.PrimaryContact = vm.PrimaryContact.ToEntity();
        }
    }
}