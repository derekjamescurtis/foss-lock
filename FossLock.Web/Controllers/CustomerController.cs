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
    /*
     * TODO: except for our service and converter fields, this is line-by-line idential to
     *      ProductController.. Unless something major changes, we'll refactor both these
     *      into a common base class.
     *
     * TODO: just a reminder, there is absolutely NO security implemented here..
     *      haven't decided whether or not to provide that or leave it up to the end user.
     *
     * TODO: pagination on the index page would probably be majorly useful.
     *      That means changing the repository and the service layers if I really want
     *      to do that properly.  I could just be a jerk and just retrieve all the results
     *      and trim off the bits I don't want.. But that would be stupidly lazy and a
     *      waste of my time.
     */

    public class CustomerController : Controller
    {
        private GenericService<Customer> service = new GenericService<Customer>(new EFRepository<Customer>());
        private IEntityConverter<Customer, CustomerViewModel> converter = new CustomerConverter();

        public ActionResult Index()
        {
            var vmList = service
                        .GetList()
                        .Select(e => converter.EntityToViewmodel(e));
            return View(vmList);
        }

        public ActionResult Create()
        {
            var vm = new CustomerViewModel();
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel vm)
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
        public ActionResult Edit(CustomerViewModel vm)
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