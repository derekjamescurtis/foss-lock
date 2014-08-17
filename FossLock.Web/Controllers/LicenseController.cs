using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FossLock.Web.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{customerId:int}/License/{licenseId:int}/{action}")]
    public class LicenseController : Controller
    {
        [Route("~/License")]
        public ActionResult AllLicensesIndex()
        {
            return View();
        }

        public ActionResult LicensesForCustomer()
        {
            throw new NotImplementedException();
        }

        public ActionResult LicensesForProduct()
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int customerId, int licenseId)
        {
            throw new NotImplementedException();
        }
    }
}