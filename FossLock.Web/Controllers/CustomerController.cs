using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.Web.ViewModels;

namespace FossLock.Web.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel vm)
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int? id)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel vm)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int? id)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
        }
    }
}