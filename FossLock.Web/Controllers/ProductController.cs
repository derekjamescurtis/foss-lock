using System.Linq;
using System.Net;
using System.Web.Mvc;
using FossLock.BLL.Service;
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
            return base.Create(vm);
        }
    }
}
