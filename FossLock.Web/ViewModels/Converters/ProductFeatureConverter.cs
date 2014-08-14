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
                ProductId = entity.Product.Id,
                ProductName = entity.Product.Name
            };

            return vm;
        }

        public void ViewmodelToEntity(ProductFeatureViewModel vm, ref ProductFeature entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            else if (vm == null)
                throw new ArgumentNullException("vm");

            // todo: write a test for this
            if (entity.Product == null && entity.ProductId == 0)
                throw new ArgumentException("entity", new InvalidOperationException("entity's Product or ProductId property must be set first."));

            entity.Id = vm.Id;
            entity.Name = vm.Name;
            entity.Description = vm.Description;
            entity.MaximumAllowedPerLicense = vm.MaxAllowed;
        }
    }
}