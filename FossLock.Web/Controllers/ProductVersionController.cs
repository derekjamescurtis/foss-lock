using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.DAL.Repository;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers
{
    // we don't actually use the productId part of the route.. it's just a throwaway

    [RoutePrefix("Product")]
    [Route("{productId:int}/Version/{versionId:int}/{action}")]
    public class ProductVersionController : Controller
    {
        private GenericService<ProductVersion> service =
            new GenericService<ProductVersion>(new EFRepository<ProductVersion>());

        private IEntityConverter<ProductVersion, ProductVersionViewModel> converter =
            new ProductVersionConverter();

        // todo: tests
        [Route("{productId:int}/Version/Create")]
        public ActionResult Create(int productId)
        {
            var vm = new ProductVersionViewModel { ProductId = productId };
            return View(vm);
        }

        // todo: tests
        [Route("{productId:int}/Version/Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductVersionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var version = new ProductVersion { ProductId = vm.ProductId };
                converter.ViewmodelToEntity(vm, ref version);

                service.Add(version);

                return RedirectToAction("Edit", "Product", new { id = version.ProductId });
            }
            else
            {
                return View(vm);
            }
        }

        // todo: tests
        public ActionResult Edit(int productId, int versionId)
        {
            var version = service.GetById(versionId);
            if (version == null || version.Product.Id != productId)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                var vm = converter.EntityToViewmodel(version);
                return View(vm);
            }
        }

        // todo: tests
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVersionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var version = service.GetById(vm.Id);
                if (version == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    converter.ViewmodelToEntity(vm, ref version);

                    service.Update(version);

                    return RedirectToAction("Edit", "Product", new { id = version.Product.Id });
                }
            }
            else
            {
                return View(vm);
            }
        }

        // todo: tests
        public ActionResult Delete(int productId, int versionId)
        {
            var version = service.GetById(versionId);
            if (version == null || version.Product.Id != productId)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                var vm = converter.EntityToViewmodel(version);
                return View(vm);
            }
        }

        // todo: tests
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int productId, int versionId)
        {
            var version = service.GetById(versionId);
            if (version == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                service.Delete(version);
                return RedirectToAction("Edit", "Product", new { id = productId });
            }
        }
    }
}