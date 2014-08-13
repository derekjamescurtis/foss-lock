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
    [RoutePrefix("Product")]
    [Route("{productId:int}/Version/{versionId:int}/{action}")]
    public class ProductVersionController : Controller
    {
        private GenericService<Product> service = new ProductService(new EFRepository<Product>());
        private IEntityConverter<ProductVersion, ProductVersionViewModel> converter = new ProductVersionConverter();

        // todo: tests
        [Route("{productId:int}/Version/Create")]
        public ActionResult Create(int productId)
        {
            var product = service.GetById(productId);

            if (product == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                var vm = new ProductVersionViewModel { ProductId = product.Id };
                return View(vm);
            }
        }

        // todo: tests
        [Route("{productId:int}/Version/Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductVersionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var product = service.GetById(vm.ProductId);

                if (product == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    var version = new ProductVersion { Product = product };
                    converter.ViewmodelToEntity(vm, ref version);

                    product.Versions.Add(version);

                    service.Update(product);

                    return RedirectToAction("Edit", "Product", new { id = product.Id });
                }
            }
            else
            {
                return View(vm);
            }
        }

        // todo: tests
        public ActionResult Edit(int productId, int versionId)
        {
            var product = service.GetById(productId);
            var version = product.Versions.FirstOrDefault(e => e.Id == versionId);

            if (product == null || version == null)
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
                var product = service.GetById(vm.ProductId);
                var version = product.Versions.FirstOrDefault(e => e.Id == vm.Id);

                if (product == null || version == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    converter.ViewmodelToEntity(vm, ref version);

                    service.Update(product);

                    return RedirectToAction("Edit", "Product", new { id = product.Id });
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
            var product = service.GetById(productId);
            var version = product.Versions.FirstOrDefault(e => e.Id == versionId);

            if (product == null || version == null)
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
            var product = service.GetById(productId);
            var version = product.Versions.FirstOrDefault(e => e.Id == versionId);

            if (product == null || version == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                product.Versions.Remove(version);

                service.Update(product);

                return RedirectToAction("Edit", "Product", new { id = product.Id });
            }
        }
    }
}