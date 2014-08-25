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
    }
}