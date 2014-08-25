using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model.Component;

namespace FossLock.Web.ViewModels.Extensions
{
    public static partial class ComponentExtensions
    {
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