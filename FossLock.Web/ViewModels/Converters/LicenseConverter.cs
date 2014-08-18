using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FossLock.Core;
using FossLock.Model;

namespace FossLock.Web.ViewModels.Converters
{
    public class LicenseConverter : IEntityConverter<License, LicenseViewModel>
    {
        public LicenseViewModel EntityToViewmodel(License entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            // todo: I think we're going to need to perform some checks here to
            // make sure that entity and vm's reference properties are set (ex, customer product version)

            var vm = new LicenseViewModel
            {
                GenerationDateTime = entity.GenerationDateTime,
                DestroyedDateTime = entity.DestroyedDateTime,
                ExpirationDate = entity.ExpirationDate,
                Notes = entity.Notes,
                NetworkLicenseCount = entity.NetworkLicenseCount,
                CustomerId = entity.Customer.Id,
                CustomerName = entity.Customer.Name,
                ProductId = entity.ProductVersion.Product.Id,
                ProductVersionId = entity.ProductVersion.Id,
                LicensedFeatures = entity.LicensedFeatures,
                Activations = entity.Activations
            };

            var allLockProperties = Enum.GetValues(typeof(LockPropertyType));
            foreach (LockPropertyType lockType in allLockProperties)
            {
                // skip this, we don't want to display it.
                if (lockType == LockPropertyType.None)
                    continue;

                if (entity.RequiredLockProperties.HasFlag(lockType))
                {
                    vm.RequiredLockProperties.Add(((int)lockType).ToString());
                }
            }

            return vm;
        }

        public void ViewmodelToEntity(LicenseViewModel vm, ref License entity)
        {
            // can't take null parameters
            if (entity == null)
                throw new ArgumentNullException("entity");
            else if (vm == null)
                throw new ArgumentNullException("vm");

            // todo: I think we're going to need to perform some checks here to
            // make sure that entity and vm's reference properties are set (ex, customer product version)
            if (entity.Customer == null && entity.CustomerId == 0)
                throw new ArgumentException("CustomerId or Customer properties must be set prior to calling this method.", "entity");
            else if (entity.ProductVersion == null & entity.ProductVersionId == 0)
                throw new ArgumentException("ProductVersion or ProductVersionId must be set prior to calling this method.", "entity");

            entity.GenerationDateTime = vm.GenerationDateTime;
            entity.DestroyedDateTime = vm.DestroyedDateTime;
            entity.ExpirationDate = vm.ExpirationDate;
            entity.Notes = vm.Notes;
            entity.NetworkLicenseCount = vm.NetworkLicenseCount;

            foreach (var val in vm.RequiredLockProperties)
            {
                var enumFlag = (LockPropertyType)Enum.Parse(typeof(LockPropertyType), val);
                entity.RequiredLockProperties |= enumFlag;
            }
        }
    }
}