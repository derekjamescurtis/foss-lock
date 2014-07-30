using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class ProductConverter : IEntityConverter<Product, ProductViewModel>
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
                VersionLeeway = ((int)entity.VersionLeeway).ToString(),
                PermittedActivationTypes = new List<string>(),
                SelectedDefaultLockProperties = new List<string>(),
                Versions = entity.Versions
            };

            var allActivationTypes = Enum.GetValues(typeof(ActivationType));
            foreach (ActivationType activationType in allActivationTypes)
            {
                // skip this, we don't want to display it.
                if (activationType == ActivationType.None)
                    continue;

                // figure out which activation types have already been selected.
                if (entity.PermittedActivationTypes.HasFlag(activationType))
                {
                    vm.PermittedActivationTypes.Add(((int)activationType).ToString());
                }
            }

            var allLockProperties = Enum.GetValues(typeof(LockPropertyType));
            foreach (LockPropertyType lockType in allLockProperties)
            {
                // skip this, we don't want to display it.
                if (lockType == LockPropertyType.None)
                    continue;

                if (entity.DefaultLockProperties.HasFlag(lockType))
                {
                    vm.SelectedDefaultLockProperties.Add(((int)lockType).ToString());
                }
            }

            return vm;
        }

        public void ViewmodelToEntity(ProductViewModel viewmodel, ref Product entity)
        {
            if (viewmodel == null)
                throw new ArgumentNullException("viewmodel");
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = viewmodel.Id;
            entity.Name = viewmodel.Name;
            entity.ReleaseDate = viewmodel.ReleaseDate;
            entity.FailOnNullHardwareIdentifier = viewmodel.FailOnNullHardwareIdentifier;
            entity.Notes = viewmodel.Notes;
            entity.VersionLeeway = (VersionLeewayType)Enum.Parse(typeof(VersionLeewayType), viewmodel.VersionLeeway);

            entity.PermittedActivationTypes = ActivationType.None;
            foreach (var activationType in viewmodel.PermittedActivationTypes)
            {
                entity.PermittedActivationTypes |= (ActivationType)Enum.Parse(typeof(ActivationType), activationType);
            }

            entity.DefaultLockProperties = LockPropertyType.None;
            foreach (var lockProperty in viewmodel.SelectedDefaultLockProperties)
            {
                entity.DefaultLockProperties |= (LockPropertyType)Enum.Parse(typeof(LockPropertyType), lockProperty);
            }
        }
    }
}