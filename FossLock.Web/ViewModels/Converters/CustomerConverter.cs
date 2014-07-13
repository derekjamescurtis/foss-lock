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
            throw new NotImplementedException();
        }

        public Customer ViewmodelToEntity(CustomerViewModel vm, Customer entity)
        {
            throw new NotImplementedException();
        }

        public CustomerViewModel EntityToViewmodel(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}