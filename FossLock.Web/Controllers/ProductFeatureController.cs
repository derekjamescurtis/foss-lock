using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.DAL.Repository;
using FossLock.Model;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers
{
    /// <summary>
    ///     Because product features are child entities from Product, they do not support their own
    ///     service directly.  Instead, they're updated as part of the Product service.
    /// </summary>
    public class ProductFeatureController : Controller
    {
        private GenericService<Product> service = new ProductService(new EFRepository<Product>());
        private IEntityConverter<ProductFeature, ProductFeatureViewModel> converter = new ProductFeatureConverter();

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