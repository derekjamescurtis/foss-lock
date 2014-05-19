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
    public class GenericService<T> : IFossLockService<T> 
        where T : IEntityBase, new()
    {            
        public GenericService(IRepository<T> repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            _repository = repository;
        }

        readonly IRepository<T> _repository = null;

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }
        public ICollection<T> GetList()
        {
            return GetList(1, int.MaxValue);
        }
        public virtual ICollection<T> GetList(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        
        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var results = ValidateAdd(entity);            
            if (results.Any())
            {
                throw new ArgumentException("Entity failed to validate", "entity");
            }

            return _repository.Add(entity);
        }
        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ValidateUpdate(entity);
            return _repository.Update(entity);
        }
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ValidateDelete(entity);
            _repository.Delete(entity);
        }

        public T New()
        {
            return new T();
        }

        public virtual ICollection<ValidationResult> ValidateAdd(T entity) 
        {
            var results = new List<ValidationResult>();

            // if the entity IS NOT transient, then Add() cannot be called.
            if (entity.IsTransient() == false)
            {
                results.Add(new ValidationResult("Entity is already in the database."));
            }

            // run entity-level validation
            if (entity.IsValid() == false)
            {
                results.AddRange(entity.ValidationResults());
            }           
            
            return results;
        }
        public virtual ICollection<ValidationResult> ValidateUpdate(T entity) { throw new NotImplementedException(); }
        public virtual ICollection<ValidationResult> ValidateDelete(T entity) { throw new NotImplementedException(); }
        public virtual ICollection<ValidationResult> Validate(T entity) { throw new NotImplementedException(); }
    }
}
