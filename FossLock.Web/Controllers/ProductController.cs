using System.Linq;
using System.Net;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.DAL.Repository;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers
{
    public class ProductController : Controller
    {
        private GenericService<Product> service = new ProductService(new EFRepository<Product>());
        private IEntityConverter<Product, ProductViewModel> converter = new ProductConverter();

        public ActionResult Index()
        {
            var vms = service
                        .GetList()
                        .Select(e => converter.EntityToViewmodel(e));
            return View(vms);
        }

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
                service.Add(p);
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

            Product p = service.GetById(id.Value);

            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(p);
                return View(vm);
            }
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
            else
            {
                return View(vm);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product p = service.GetById(id.Value);

            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(p);
                return View(vm);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var p = service.GetById(id);

            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                service.Delete(p);
                return RedirectToAction("Index");
            }
        }
    }
}
