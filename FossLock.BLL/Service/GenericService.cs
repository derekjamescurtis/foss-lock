using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.DAL.Repository;
using FossLock.Model.Base;

namespace FossLock.BLL.Service
{
    /*
     * TODO: move documentation from this class to it's underlying interface.
     */

    /// <summary> A generic service for retrieving business entities
    /// and validating any changes made to those entities before passing
    /// them back to the repository to be Added/Updated/Deleted.
    /// </summary>
    /// <remarks> Note: Upon calls to Add()/Update()/Delete(), the underlying
    /// repository should immediately commit all pending changes to the data store.
    /// </remarks>
    /// <typeparam name="T"> Any entity type from under the FossLock.Models namespace.
    /// </typeparam>
    public class GenericService<T> : IFossLockService<T>
        where T : IEntityBase, new()
    {
        /// <summary> Initializes a new instance of this class with the
        /// provided repository.
        /// </summary>
        /// <param name="repository"> An IRepository object that will be used for
        /// retrieving entities and persisting changes to the data store.
        /// </param>
        public GenericService(IRepository<T> repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            _repository = repository;
        }

        private readonly IRepository<T> _repository = null;

        #region Data Retrieval

        public virtual T New()
        {
            return new T();
        }

        public virtual T GetById(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException("id", id, "Must be 1 or greater.");

            return _repository.GetById(id);
        }

        public virtual ICollection<T> GetList()
        {
            return GetList(1, int.MaxValue);
        }

        /// <summary>Gets a paginated selection of data from the underlying repository.
        /// NOTE: this is a very, very inefficient call.  Basically, it grabs the full list
        /// from the underlying repository, and then strips off what it doesn't need.
        /// There isn't any real filtering capabilities here either.. it tries to be as dumb
        /// as possible about what the underlying repository could be based on.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual ICollection<T> GetList(int pageNumber, int pageSize)
        {
            // make sure these make sense.
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "Must be 1 or greater.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "Must be 1 or greater.");

            var slice = _repository.GetAll()
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize);

            return (ICollection<T>)slice;
        }

        #endregion Data Retrieval

        #region Data Modification

        /// <summary>Persists an object in the repository after validation.
        /// NOTE: In practice, when using the EFRepository, pending changes to ALL
        /// objects will be persisted by the repository.
        /// </summary>
        /// <param name="entity">The object to persist in the repository.</param>
        /// <returns>The same entity instance, but with the Id set to the repository-provided
        /// value.</returns>
        /// <exception cref="ArgumentNullException">Thrown if a null entity is provided.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if the entity fails validation,
        /// or if the entity is already in the repository.
        /// </exception>
        public virtual T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateAdd(entity);
            if (results.Any())
                throw new ArgumentException("Entity failed to validate", "entity");

            return _repository.Add(entity);
        }

        /// <summary>Persists all pending changes for an object in the repository after validation.
        /// NOTE: In practice, when using the EFRepository, pending changes to ALL
        /// objects will be persisted by the repository.
        /// </summary>
        /// <param name="entity">Object to commit all pending changes to in the repository.
        /// </param>
        /// <returns>The object that was updated.</returns>
        /// <exception cref="ArgumentNullException">Thrown if a null entity is provided.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if the entity fails validation,
        /// or if the entity is not already in the repository.
        /// </exception>
        public virtual T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateUpdate(entity);
            if (results.Any())
                throw new ArgumentException("Entity failed to validate", "entity");

            return _repository.Update(entity);
        }

        /// <summary>Requests that the repository remove this instance from persistent storage.
        /// NOTE: In practice, when using the EFRepository, pending changes to ALL
        /// objects will be persisted by the repository.
        /// </summary>
        /// <param name="entity">The entity to remove from the underlying repository.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if a null entity is provided.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if the entity fails validation,
        /// or if the entity is not already in the repository.
        /// </exception>
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateDelete(entity);
            if (results.Any())
                throw new ArgumentException("Entity failed to validate", "entity");

            _repository.Delete(entity);
        }

        #endregion Data Modification

        #region Validation

        public virtual ICollection<ValidationResult> ValidateAdd(T entity)
        {
            var results = new List<ValidationResult>();

            // if the entity IS NOT transient, then Add() cannot be called.
            if (entity.IsTransient() == false)
            {
                results.Add(new ValidationResult("Entity is already in the data store."));
            }

            // run entity-level validation
            if (entity.IsValid() == false)
            {
                results.AddRange(entity.ValidationResults());
            }

            return results;
        }

        public virtual ICollection<ValidationResult> ValidateUpdate(T entity)
        {
            var results = new List<ValidationResult>();

            // require entity to be in database already
            if (entity.IsTransient())
            {
                results.Add(new ValidationResult("Entity is not yet in the data store."));
            }

            if (entity.IsValid() == false)
            {
                results.AddRange(entity.ValidationResults());
            }

            return results;
        }

        public virtual ICollection<ValidationResult> ValidateDelete(T entity)
        {
            var results = new List<ValidationResult>();

            if (entity.IsTransient())
                results.Add(new ValidationResult("Entity is not yet in the data store."));

            // this should check for transient
            // should NOT check for entity-level validation
            // if additional checks are required, this should be overridden by a derrived class.
            return results;
        }

        #endregion Validation
    }
}