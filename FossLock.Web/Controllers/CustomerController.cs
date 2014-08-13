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
    ///     Controller for performing basic CRUD functions against Customer entities.
    /// </summary>
    /// <remarks>
    ///     We have 3 routes defined which just wrap calls for the base class, only because
    ///     Route attributes are not inherited from the base class.
    /// </remarks>
    [RoutePrefix("Customer")]
    [Route("{id:int}/{action}")]
    public class CustomerController : PrimaryEntityCrudController<Customer, CustomerViewModel>
    {
        public CustomerController(IFossLockService<Customer> service,
                IEntityConverter<Customer, CustomerViewModel> converter)
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
        public override ActionResult Create(CustomerViewModel vm)
        {
            return base.Create(vm);
        }
    }
}