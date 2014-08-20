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
    [RoutePrefix("Customer")]
    [Route("{customerId:int}/License/{licenseId:int}/{action}")]
    public class LicenseController : Controller
    {
        public LicenseController(IFossLockService<License> service, IEntityConverter<License, LicenseViewModel> converter,
            IFossLockService<Customer> customerService)
        {
            this.service = service;
            this.converter = converter;
            this.customerService = customerService;
        }

        private IFossLockService<Customer> customerService;
        private IFossLockService<License> service;
        private IEntityConverter<License, LicenseViewModel> converter;

        [Route("{customerId:int}/License/Create")]
        public ActionResult Create(int customerId)
        {
            var customer = customerService.GetById(customerId);

            var vm = new LicenseViewModel { CustomerId = customerId, CustomerName = customer.Name };
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