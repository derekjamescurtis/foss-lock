using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    /// <summary>
    ///     Provides methods to convert Product instances to their corresponding ViewModel
    ///     instance, and vice versa.
    /// </summary>
    public class ProductEntityConverter : IEntityConverter<Product, ProductViewModel>
    {
        /// <summary>
        ///     Converts a Product object to it's corresponding ProductViewModel
        ///     for display by the Razor templates.
        /// </summary>
        /// <param name="entity">The Product that we want to display to the user.</param>
        /// <returns>A ProductViewModel that represents the entity argument.</returns>
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
                VersioningStyle = ((int)entity.VersioningStyle).ToString(),
                VersionLeeway = ((int)entity.VersionLeeway).ToString(),
                PermittedActivationTypes = new List<string>(),
                SelectedDefaultLockProperties = new List<string>()
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

        /// <summary>
        ///     Creates an entirely new entity based on a viewmodel.
        /// </summary>
        /// <param name="viewmodel">The viewmodel that our entity will be based on.</param>
        /// <returns>
        ///     A new Product with it's properties set based on the provided
        ///     viewmodel instance.
        /// </returns>
        public Product ViewmodelToEntity(ProductViewModel viewmodel)
        {
            var entity = new Product();
            ViewmodelToEntity(viewmodel, entity);
            return entity;
        }

        /// <summary>
        ///     Updates an existing Product instance with properties
        ///     set on a viewmodel.
        /// </summary>
        /// <param name="viewmodel">
        ///     The viewmodel that will be used as a reference when setting
        ///     property values on our entity.
        /// </param>
        /// <param name="entity">The entity that will have it's properties set.</param>
        /// <returns>The same instance of Product that was provided to this method.</returns>
        public Product ViewmodelToEntity(ProductViewModel viewmodel, Product entity)
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
            entity.VersioningStyle = (VersioningStyle)Enum.Parse(typeof(VersioningStyle), viewmodel.VersioningStyle);
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

            return entity;
        }
    }
}