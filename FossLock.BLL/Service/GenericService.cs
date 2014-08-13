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
    /// <summary>
    ///     A generic service for retrieving business entities
    ///     and validating any changes made to those entities before passing
    ///     them back to the repository to be Added/Updated/Deleted.
    /// </summary>
    /// <remarks>
    ///     Note: Upon calls to Add()/Update()/Delete(), the underlying
    ///     repository should immediately commit all pending changes to the data store.
    /// </remarks>
    /// <typeparam name="T">
    ///     Any entity type from under the FossLock.Models namespace.
    /// </typeparam>
    public class GenericService<T> : IFossLockService<T>
        where T : IEntityBase, new()
    {
        /// <summary>
        ///     Initializes a new instance of this class with the
        ///     provided repository.
        /// </summary>
        /// <param name="repository">
        ///     An IRepository object that will be used for
        ///     retrieving entities and persisting changes to the data store.
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

        public virtual IList<T> GetList()
        {
            return GetList(1, int.MaxValue);
        }

        public virtual IList<T> GetList(int pageNumber, int pageSize)
        {
            // make sure these make sense.
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "Must be 1 or greater.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "Must be 1 or greater.");

            var slice = _repository.GetAll()
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize);

            return slice.ToList();
        }

        #endregion Data Retrieval

        #region Data Modification

        public virtual T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateAdd(entity);
            if (results.Any())
                throw new ArgumentException("Entity failed to validate", "entity");

            return _repository.Add(entity);
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateUpdate(entity);
            if (results.Any())
                throw new ArgumentException("Entity failed to validate", "entity");

            return _repository.Update(entity);
        }

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

            // run entity-level validation
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
