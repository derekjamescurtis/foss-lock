using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductFeatureConverter : IEntityConverter<ProductFeature, ProductFeatureViewModel>
    {
        public ProductFeatureViewModel EntityToViewmodel(ProductFeature entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

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

        public ProductFeature ViewmodelToEntity(ProductFeatureViewModel vm, ProductFeature entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            else if (vm == null)
                throw new ArgumentNullException("vm");

            entity.Id = vm.Id;
            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.MaximumAllowedPerLicense = vm.MaxAllowed;

            return entity;
        }
    }
}