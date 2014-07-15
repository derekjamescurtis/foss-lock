using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FossLock.Web.Controllers
{
    public class ProductFeatureController : Controller
    {
        //
        // GET: /ProductFeature/
        public ActionResult Index(int productId)
        {
            return View();
        }

        public ActionResult Create(int productId)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(object vm)
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int productId, int? featureId)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(object vm)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int productId, int? featureId)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int productId, int? featureId)
        {
            throw new NotImplementedException();
        }
    }
}