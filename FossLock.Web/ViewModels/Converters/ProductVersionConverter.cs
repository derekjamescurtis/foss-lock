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
                throw new ArgumentNullException("entity");
            else if (entity.Product == null)
                throw new ArgumentException("ProductVersion.Product cannot be null", "entity");

            var version = Version.Parse(entity.Version);

            var vm = new ProductVersionViewModel
            {
                Id = entity.Id,
                ProductId = entity.Product.Id,
                ProductName = entity.Product.Name,
                Major = version.Major.ToString(),
                Minor = version.Minor.ToString(),
                Build = version.Build.ToString(),
                Patch = version.Revision.ToString(),
            };

            return vm;
        }

        public void ViewmodelToEntity(ProductVersionViewModel vm, ref ProductVersion entity)
        {
            if (vm == null)
                throw new ArgumentNullException("vm");
            else if (entity == null)
                throw new ArgumentNullException("entity");

            // todo: write a test for this
            if (entity.Product == null)
                throw new ArgumentException("entity", new InvalidOperationException("entity's Product property must be set first."));

            var versionString = string.Format("{0}.{1}.{2}.{3}", vm.Major, vm.Minor, vm.Build, vm.Patch);
            // we don't assign the output to anything -- we just want to make sure this parses.. otherwise we should get an exception
            // TODO: write a test for this
            Version.Parse(versionString);

            entity.Version = versionString;
        }
    }
}