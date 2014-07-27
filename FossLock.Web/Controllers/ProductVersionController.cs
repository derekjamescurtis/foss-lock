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
    [RoutePrefix("Product")]
    public class ProductVersionController : Controller
    {
        private GenericService<Product> service = new ProductService(new EFRepository<Product>());
        private IEntityConverter<ProductVersion, ProductVersionViewModel> converter = new ProductVersionConverter();

        [Route("{productId:int}/Version/Create")]
        public ActionResult Create(int productId)
        {
            return View();
        }

        [Route("{productId:int}/Version/Create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ProductVersionViewModel vm)
        {
            throw new NotImplementedException();
        }

        [Route("{productId:int}/Version/{versionId:int}/Edit")]
        public ActionResult Edit(int productId, int versionId)
        {
            return View();
        }

        [Route("{productId:int}/Version/{versionId:int}/Edit"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVersionViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}