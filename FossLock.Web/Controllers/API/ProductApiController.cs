using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.Core;
using FossLock.DAL.Repository;
using FossLock.Model;

namespace FossLock.Web.Controllers.API
{
    public class ProductApiController : Controller
    {
        private GenericService<Product> service =
            new GenericService<Product>(new EFRepository<Product>());

        [Route("API/Product")]
        public ActionResult GetProducts()
        {
            var allProducts = service.GetList();

            // turn all of our product data into a structure that we can render
            // out into json.
            var vm = new List<object>();
            foreach (var p in allProducts)
            {
                var node = new
                {
                    Id = p.Id,
                    Name = p.Name,
                    DefaultLocks = new List<Object>(),
                    Versions = p.Versions.Select(v => new { Id = v.Id, Version = v.Version })
                };

                // turn our lock-property flag enum property into a list that will make sense
                // as a json object
                var allLockProperties = Enum.GetValues(typeof(LockPropertyType));
                foreach (LockPropertyType lockType in allLockProperties)
                {
                    if (lockType == LockPropertyType.None) continue;

                    if (p.DefaultLockProperties.HasFlag(lockType))
                    {
                        node.DefaultLocks.Add(
                            new
                            {
                                Name = Enum.GetName(typeof(LockPropertyType), lockType),
                                Value = lockType
                            });
                    }
                }

                vm.Add(node);
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [Route("API/Product/{id:int}")]
        public ActionResult GetProductDetail(int id)
        {
            var p = service.GetById(id);
            if (p == null)
                return null;

            var vm = new
            {
                Id = p.Id,
                Name = p.Name,
                DefaultLocks = new List<Object>(),
                Versions = p.Versions.Select(v => new { Id = v.Id, Version = v.Version })
            };

            var allLockProperties = Enum.GetValues(typeof(LockPropertyType));
            foreach (LockPropertyType lockType in allLockProperties)
            {
                if (lockType == LockPropertyType.None) continue;

                if (p.DefaultLockProperties.HasFlag(lockType))
                {
                    vm.DefaultLocks.Add(
                        new
                        {
                            Name = Enum.GetName(typeof(LockPropertyType), lockType),
                            Value = lockType
                        });
                }
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}