using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.Web.ViewModels;

namespace FossLock.Web.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{customerId:int}/License/{licenseId:int}/{action}")]
    public class LicenseController : Controller
    {
        [Route("{customerId:int}/License/Create")]
        public ActionResult Create(int customerId)
        {
            var vm = new LicenseViewModel { CustomerId = customerId };
            return View(vm);
        }

        [Route("{customerId:int}/License/Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(LicenseViewModel vm)
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int customerId, int licenseId)
        {
            throw new NotImplementedException();
        }
    }
}