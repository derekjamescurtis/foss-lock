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
        private ProductEntityConverter converter = new ProductEntityConverter();

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

        // GET: /Product/Create
        public ActionResult Create()
        {
            var vm = new ProductViewModel();
            return View(vm);
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
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
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,ReleaseDate,DefaultLockProperties," +
                "FailOnNullHardwareIdentifier,PermittedActivationTypes," +
                "PermittedExpirationTypes,MaximumTrialDays,VersioningStyle," +
                "Notes,VersionLeeway,Name")]
            Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
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
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Products.Find(id);
            //db.Products.Remove(product);
            //db.SaveChanges();
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
