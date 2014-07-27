using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductVersionConverter : IEntityConverter<ProductVersion, ProductVersionViewModel>
    {
        #region IEntityConverter<ProductVersion,ProduceVersionViewModel> Members

        public ProductVersion ViewmodelToEntity(ProductVersionViewModel vm)
        {
            throw new NotImplementedException();
        }

        public ProductVersion ViewmodelToEntity(ProductVersionViewModel vm, ProductVersion entity)
        {
            throw new NotImplementedException();
        }

        public ProductVersionViewModel EntityToViewmodel(ProductVersion entity)
        {
            throw new NotImplementedException();
        }

        #endregion IEntityConverter<ProductVersion,ProduceVersionViewModel> Members
    }
}