using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductVersionConverter : IEntityConverter<ProductVersion, ProductVersionViewModel>
    {
        public ProductVersionViewModel EntityToViewmodel(ProductVersion entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            throw new NotImplementedException();
        }

        public void ViewmodelToEntity(ProductVersionViewModel vm, ref ProductVersion entity)
        {
            if (vm == null)
                throw new ArgumentNullException("vm");
            else if (entity == null)
                throw new ArgumentNullException("entity");

            throw new NotImplementedException();
        }
    }
}