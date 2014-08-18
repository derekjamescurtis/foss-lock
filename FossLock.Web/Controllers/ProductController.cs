using System.Linq;
using System.Net;
using System.Web.Mvc;
using FossLock.BLL.Service;
using FossLock.BLL.Util;
using FossLock.DAL.Repository;
using FossLock.Model;
using FossLock.Web.Controllers.Base;
using FossLock.Web.ViewModels;
using FossLock.Web.ViewModels.Converters;

namespace FossLock.Web.Controllers
{
    /// <summary>
    ///     Controller for performing basic CRUD functions against Product entities.
    /// </summary>
    /// <remarks>
    ///     We have 3 routes defined which just wrap calls for the base class, only because
    ///     Route attributes are not inherited from the base class.
    /// </remarks>
    [RoutePrefix("Product")]
    [Route("{id:int}/{action}")]
    public class ProductController : PrimaryEntityCrudController<Product, ProductViewModel>
    {
        private RsaKeypairGenerator keyPairGenerator = new RsaKeypairGenerator();

        public ProductController(IFossLockService<Product> service,
                IEntityConverter<Product, ProductViewModel> converter)
            : base(service, converter)
        {
            return;
        }

        [Route]
        public override ActionResult Index()
        {
            return base.Index();
        }

        [Route("Create")]
        public override ActionResult Create()
        {
            return base.Create();
        }

        [Route("Create")]
        public override ActionResult Create(ProductViewModel vm)
        {
            var kp = keyPairGenerator.GenerateKeypair(vm.LicenseEncryptionType);
            vm.PublicKey = kp.PubKey;
            vm.PrivateKey = kp.PrivKey;

            return base.Create(vm);
        }

        public ActionResult GetVersionsJson(int id)
        {
            var p = service.GetById(id);
            if (p == null)
                return new HttpNotFoundResult();

            return Json(p.Versions);
        }
    }
}
