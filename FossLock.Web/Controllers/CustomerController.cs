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
    /*
     * TODO: just a reminder, there is absolutely NO security implemented here..
     *      haven't decided whether or not to provide that or leave it up to the end user.
     *
     * TODO: pagination on the index page would probably be majorly useful.
     *      That means changing the repository and the service layers if I really want
     *      to do that properly.  I could just be a jerk and just retrieve all the results
     *      and trim off the bits I don't want.. But that would be stupidly lazy and a
     *      waste of my time.
     */

    public class CustomerController : PrimaryEntityCrudController<Customer, CustomerViewModel>
    {
        public CustomerController(IFossLockService<Customer> service, IEntityConverter<Customer, CustomerViewModel> converter)
            : base(service, converter)
        {
            return;
        }
    }
}