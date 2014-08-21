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
                ProductName = entity.ProductVersion.Product.Name,
                ProductVersionId = entity.ProductVersion.Id,
                ProductVersionText = entity.ProductVersion.Version,
                LicensedFeatures = entity.LicensedFeatures,
                Activations = entity.Activations,
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

            entity.GenerationDateTime = vm.GenerationDateTime;
            entity.DestroyedDateTime = vm.DestroyedDateTime;
            entity.ExpirationDate = vm.ExpirationDate;
            entity.Notes = vm.Notes;
            entity.NetworkLicenseCount = vm.NetworkLicenseCount;
            entity.CustomerId = vm.CustomerId;
            entity.ProductVersionId = vm.ProductVersionId;

            foreach (var val in vm.RequiredLockProperties)
            {
                var enumFlag = (LockPropertyType)Enum.Parse(typeof(LockPropertyType), val);
                entity.RequiredLockProperties |= enumFlag;
            }
        }
    }
}