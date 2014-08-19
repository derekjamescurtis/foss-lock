using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FossLock.BLL.Service;
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

            var vm = new List<object>();

            foreach (var p in allProducts)
            {
                vm.Add(new { Id = p.Id, Name = p.Name });
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
                Versions = p.Versions.Select(v => new { Id = v.Id, Version = v.Version })
            };

            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}