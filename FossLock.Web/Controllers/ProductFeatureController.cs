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
    [Route("{productId:int}/Feature/{featureId:int}/{action}")]
    public class ProductFeatureController : Controller
    {
        private GenericService<ProductFeature> service =
            new GenericService<ProductFeature>(new EFRepository<ProductFeature>());

        private IEntityConverter<ProductFeature, ProductFeatureViewModel> converter =
            new ProductFeatureConverter();

        [Route("{productId:int}/Feature/Create")]
        public ActionResult Create(int productId)
        {
            var vm = new ProductFeatureViewModel { ProductId = productId };
            return View(vm);
        }

        // todo: tests
        [Route("{productId:int}/Feature/Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductFeatureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var feature = new ProductFeature { ProductId = vm.ProductId };
                converter.ViewmodelToEntity(vm, ref feature);

                service.Add(feature);

                return RedirectToAction("Edit", "Product", new { id = feature.ProductId });
            }
            else
            {
                return View(vm);
            }
        }

        // todo: tests
        public ActionResult Edit(int productId, int featureId)
        {
            var feature = service.GetById(featureId);
            if (feature == null || feature.Product.Id != productId)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                var vm = converter.EntityToViewmodel(feature);
                return View(vm);
            }
        }

        // todo: tests
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductFeatureViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var feature = service.GetById(vm.Id);
                if (feature == null)
                {
                    return new HttpNotFoundResult();
                }
                else
                {
                    converter.ViewmodelToEntity(vm, ref feature);

                    service.Update(feature);

                    return RedirectToAction("Edit", "Product", new { id = feature.Product.Id });
                }
            }
            else
            {
                return View(vm);
            }
        }

        // todo: tests
        public ActionResult Delete(int productId, int featureId)
        {
            var feature = service.GetById(featureId);
            if (feature == null || feature.Product.Id != productId)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                var vm = converter.EntityToViewmodel(feature);
                return View(vm);
            }
        }

        // todo: tests
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int productId, int featureId)
        {
            var feature = service.GetById(featureId);
            if (feature == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                service.Delete(feature);
                return RedirectToAction("Edit", "Product", new { id = productId });
            }
        }
    }
}