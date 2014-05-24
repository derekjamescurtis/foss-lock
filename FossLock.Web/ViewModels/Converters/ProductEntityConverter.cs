using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductEntityConverter : IEntityConverter<Product, ProductViewModel>
    {
        public ProductViewModel EntityToViewmodel(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var vm = new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ReleaseDate = entity.ReleaseDate,
                FailOnNullHardwareIdentifier = entity.FailOnNullHardwareIdentifier,
                Notes = entity.Notes,
                VersioningStyle = entity.VersioningStyle,
                VersionLeeway = entity.VersionLeeway,
                PermittedActivationTypes = new List<ActivationType>(),
                SelectedDefaultLockProperties = new List<LockPropertyType>()
            };

            var allActivationTypes = Enum.GetValues(typeof(ActivationType));
            foreach (ActivationType activationType in allActivationTypes)
            {
                if (entity.PermittedActivationTypes.HasFlag(activationType))
                    vm.PermittedActivationTypes.Add(activationType);
            }

            var allLockProperties = Enum.GetValues(typeof(LockPropertyType));
            foreach (LockPropertyType lockType in allLockProperties)
            {
                if (entity.DefaultLockProperties.HasFlag(lockType))
                    vm.SelectedDefaultLockProperties.Add(lockType);
            }

            return vm;
        }

        public Product ViewmodelToEntity(ProductViewModel viewmodel)
        {
            if (viewmodel == null)
            {
                throw new ArgumentNullException("viewmodel");
            }

            var p = new Product
            {
                Id = viewmodel.Id,
                Name = viewmodel.Name,
                ReleaseDate = viewmodel.ReleaseDate,
                FailOnNullHardwareIdentifier = viewmodel.FailOnNullHardwareIdentifier,
                Notes = viewmodel.Notes,
                VersioningStyle = viewmodel.VersioningStyle,
                PermittedActivationTypes = ActivationType.None,
                DefaultLockProperties = LockPropertyType.None
            };

            foreach (var activationType in viewmodel.PermittedActivationTypes)
            {
                p.PermittedActivationTypes |= activationType;
            }

            foreach (var lockProperty in viewmodel.SelectedDefaultLockProperties)
            {
                p.DefaultLockProperties |= lockProperty;
            }

            return p;
        }
    }
}