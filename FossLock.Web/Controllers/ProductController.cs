using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.Core;
using FossLock.DAL.EF;
using FossLock.DAL.Repository;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers
{
    public class ProductController : Controller
    {
        private ProductService service = new ProductService(new EFRepository<Product>());
        private IEntityConverter<Product, ProductViewModel> converter = new ProductEntityConverter();

        public ActionResult Index()
        {
            var vms = service
                        .GetList()
                        .Select(e => converter.EntityToViewmodel(e));
            return View(vms);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = service.GetById(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            var vm = converter.EntityToViewmodel(product);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var vm = new ProductViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var p = converter.ViewmodelToEntity(vm);
                try
                {
                    service.Add(p);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    var valErrors = service.ValidateAdd(p);
                    foreach (var valError in valErrors)
                    {
                        ModelState.AddModelError(
                            valError.MemberNames.First(), valError.ErrorMessage);
                    }
                }
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product p = service.GetById(id.Value);
            if (p == null)
            {
                return HttpNotFound();
            }
            var vm = converter.EntityToViewmodel(p);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var p = service.GetById(vm.Id);
                p = converter.ViewmodelToEntity(vm, p);
                service.Update(p);

                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product p = service.GetById(id.Value);
            if (p == null)
            {
                return HttpNotFound();
            }
            var vm = converter.EntityToViewmodel(p);
            return View(vm);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // db.Dispose();
                service = null;
            }
            base.Dispose(disposing);
        }
    }
}
