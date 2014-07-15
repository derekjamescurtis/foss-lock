using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductFeatureConverter : IEntityConverter<ProductFeature, ProductFeatureViewModel>
    {
        #region IEntityConverter<ProductFeature,ProductFeatureViewModel> Members

        public ProductFeature ViewmodelToEntity(ProductFeatureViewModel vm)
        {
            throw new NotImplementedException();
        }

        public ProductFeature ViewmodelToEntity(ProductFeatureViewModel vm, ProductFeature entity)
        {
            throw new NotImplementedException();
        }

        public ProductFeatureViewModel EntityToViewmodel(ProductFeature entity)
        {
            throw new NotImplementedException();
        }

        #endregion IEntityConverter<ProductFeature,ProductFeatureViewModel> Members
    }
}