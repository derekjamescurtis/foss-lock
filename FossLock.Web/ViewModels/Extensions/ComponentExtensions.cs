using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model.Component;

namespace FossLock.Web.ViewModels.Extensions
{
    public static class ComponentExtensions
    {
        public static AddressViewModel ToViewModel(this Address address)
        {
            return new AddressViewModel
            {
                Address1 = address.Address1,
                Address2 = address.Address2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode
            };
        }

        public static Address ToEntity(this AddressViewModel vm)
        {
            return new Address
            {
                Address1 = vm.Address1,
                Address2 = vm.Address2,
                City = vm.City,
                State = vm.State,
                Country = vm.Country,
                PostalCode = vm.PostalCode
            };
        }

        public static HumanContactViewModel ToViewModel(this HumanContact contact)
        {
            return new HumanContactViewModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone1 = contact.Phone1,
                Phone2 = contact.Phone2,
                Fax = contact.Fax,
                Email = contact.Email,
                Notes = contact.Notes
            };
        }

        public static HumanContact ToEntity(this HumanContactViewModel vm)
        {
            return new HumanContact
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Phone1 = vm.Phone1,
                Phone2 = vm.Phone2,
                Fax = vm.Fax,
                Email = vm.Email,
                Notes = vm.Notes
            };
        }
    }
}