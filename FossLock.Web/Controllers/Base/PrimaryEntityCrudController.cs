namespace FossLock.Web.Controllers.Base
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using FossLock.BLL.Service;
    using FossLock.Model.Base;
    using FossLock.Web.ViewModels;
    using FossLock.Web.ViewModels.Converters;

    /// <summary>
    ///     Base class for basic CRUD controllers for primary (logical parent) entities.
    ///     This provides functionality for List, Create, Update and Deleting entities.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     Type that is retrieved/manipulated in the underlying service by this controller,
    ///     (typically an Entity Framework POCO class).
    /// </typeparam>
    /// <typeparam name="TViewModel">
    ///     Type that will be used as a viewmodel to represent TEntity as the model in
    ///     the various responses (typically used in rendering Razor templates).
    /// </typeparam>
    /// <remarks>
    ///     Right now, this class is only used for the Customer and Product entity types.
    /// </remarks>
    public abstract class PrimaryEntityCrudController<TEntity, TViewModel> : Controller
        where TEntity : EntityBase, new()
        where TViewModel : class, IFossLockViewModel, new()
    {
        /// <summary>
        ///     Instantiates a new controller object.
        /// </summary>
        /// <param name="service">
        ///     The service object that will be used by all the CRUD methods for
        ///     manipulation and retrieval of data.
        /// </param>
        /// <param name="converter"></param>
        public PrimaryEntityCrudController(IFossLockService<TEntity> service, IEntityConverter<TEntity, TViewModel> converter)
        {
            this.service = service;
            this.converter = converter;
        }

        protected IFossLockService<TEntity> service = null;
        protected IEntityConverter<TEntity, TViewModel> converter = null;

        /// <summary>
        ///     Displays a non-paginated list of entities returned from the underlying
        ///     service in no particular order at all.
        /// </summary>
        /// <returns>
        ///     ViewResult with an IEnumerable of TViewModel as it's model.
        /// </returns>
        public virtual ActionResult Index()
        {
            var vmList = service
                            .GetList()
                            .Select(e => converter.EntityToViewmodel(e));
            return View(vmList);
        }

        /// <summary>
        ///     Displays a form with the fields for creating a new entity.
        /// </summary>
        /// <returns>
        ///    ViewResult for displaying a form for creating a new entity.
        /// </returns>
        public virtual ActionResult Create()
        {
            var vm = new TViewModel();
            return View(vm);
        }

        /// <summary>
        ///     Validates the provided viewmodel can be converted into it's corresponding entity
        ///     and passed to the underlying service for persistence.
        ///     If successful, the requested changes are passed on to the underlying service.
        ///     If unsuccessful, the same view as the Http Get version of this method is displayed
        ///     however retaining the user's changes in the viewmodel.
        /// </summary>
        /// <param name="vm">
        ///     ViewModel instance containing the new entity that should be validated and made to the
        ///     entity that it represents.
        /// </param>
        /// <returns>
        ///     HttpRedirectResult when the updated request has been successfully made.
        ///     ViewResult if there are validation problems with the requested changes.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Create(TViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = new TEntity();
                converter.ViewmodelToEntity(vm, ref entity);
                service.Add(entity);
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        /// <summary>
        ///     Requests the entity identified by the id parameter from the underlying service.
        ///     If lookup is successful, an edit form for the entity's corresponding viewmodel is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///     ViewResult when the requested entity can by found by the underlying service.
        ///     HttpStatusCodeResult (HTTP 400) when the entity id is not provided.
        ///     HttpNotFoundResult (HTTP 404) when the entity id was provided but not found in the underlying service.
        /// </returns>
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = service.GetById(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(entity);
                return View(vm);
            }
        }

        /// <summary>
        ///     Validates changes a user has requested be made to a specific entity.
        ///     If successful, the requested changes are passed on to the underlying service.
        ///     If unsuccessful, the same view as the Http Get version of this method is displayed
        ///     however retaining the user's changes in the viewmodel.
        /// </summary>
        /// <param name="vm">
        ///     ViewModel instance containing the changes that should be validated and made to the
        ///     entity that it represents.
        /// </param>
        /// <returns>
        ///     HttpRedirectResult when the updated request has been successfully made.
        ///     ViewResult if there are validation problems with the requested changes.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entity = service.GetById(vm.Id);
                converter.ViewmodelToEntity(vm, ref entity);
                service.Update(entity);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }
        }

        /// <summary>
        ///     Lookups up the entity identified by id in the underlying service and then displays
        ///     a confirmation page to the user confirming they actually intend to delete it.
        /// </summary>
        /// <param name="id">
        ///     Identifier of the entity the user would like to remove (typically a database primary key).
        /// </param>
        /// <returns>
        ///     HttpNotFoundResult if the service is unable to locate an entity identified by id.
        ///     ViewResult when the entity was successfully retrieved from the underlying service.
        /// </returns>
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entity = service.GetById(id.Value);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                var vm = converter.EntityToViewmodel(entity);
                return View(vm);
            }
        }

        /// <summary>
        ///     Looks up the entity specified by the id parameter and requests the underlying service remove it.
        ///     After completion, redirects to the Index action.
        /// </summary>
        /// <param name="id">
        ///     Identifier of the entity we'd like to remove (typically a database primary key).
        /// </param>
        /// <returns>
        ///     HttpNotFoundResult if the service is unable to locate an entity identified by id.
        ///     HttpRedirectResult when otherwise successful in asking the service to remove the entity.
        /// </returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            var entity = service.GetById(id);

            if (entity == null)
            {
                return HttpNotFound();
            }
            else
            {
                service.Delete(entity);
                return RedirectToAction("Index");
            }
        }
    }
}