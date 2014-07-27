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
            var entity = new ProductFeature();
            return ViewmodelToEntity(vm, entity);
        }

        public ProductFeature ViewmodelToEntity(ProductFeatureViewModel vm, ProductFeature entity)
        {
            entity.Id = vm.Id;
            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.MaximumAllowedPerLicense = vm.MaxAllowed;

            return entity;
        }

        public ProductFeatureViewModel EntityToViewmodel(ProductFeature entity)
        {
            var vm = new ProductFeatureViewModel
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                MaxAllowed = entity.MaximumAllowedPerLicense,
                Product = entity.Product
            };

            return vm;
        }

        #endregion IEntityConverter<ProductFeature,ProductFeatureViewModel> Members
    }
}