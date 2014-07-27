using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FossLock.Web.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}