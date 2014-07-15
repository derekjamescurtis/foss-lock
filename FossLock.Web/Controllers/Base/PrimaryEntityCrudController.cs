using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.Model.Base;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers.Base
{
    public abstract class PrimaryEntityCrudController<TEntity, TViewModel> : Controller
        where TEntity : EntityBase, new()
        where TViewModel : class, IFossLockViewModel, new()
    {
        public PrimaryEntityCrudController(IFossLockService<TEntity> service, IEntityConverter<TEntity, TViewModel> converter)
        {
            this.service = service;
            this.converter = converter;
        }

        private IFossLockService<TEntity> service = null;
        private IEntityConverter<TEntity, TViewModel> converter = null;

        public ActionResult Index()
        {
            var vmList = service
                        .GetList()
                        .Select(e => converter.EntityToViewmodel(e));
            return View(vmList);
        }

        public ActionResult Create()
        {
            var vm = new TViewModel();
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(TViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = converter.ViewmodelToEntity(vm);
                service.Add(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = service.GetById(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(entity);
                return View(vm);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(TViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = service.GetById(vm.Id);
                entity = converter.ViewmodelToEntity(vm, entity);
                service.Update(entity);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = service.GetById(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(entity);
                return View(vm);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var entity = service.GetById(id);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                service.Delete(entity);
                return RedirectToAction("Index");
            }
        }
    }
}