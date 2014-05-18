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
    public class GenericService<T> : IFossLockService<T> where T : EntityBase
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
            ValidateAdd(entity);   
            return _repository.Add(entity);
        }
        public T Update(T entity)
        {
            ValidateUpdate(entity);
            return _repository.Update(entity);
        }
        public void Delete(T entity)
        {
            ValidateDelete(entity);
            _repository.Delete(entity);
        }

        protected virtual ICollection<ValidationResult> ValidateAdd(T entity) { throw new NotImplementedException(); }
        protected virtual ICollection<ValidationResult> ValidateUpdate(T entity) { throw new NotImplementedException(); }
        protected virtual ICollection<ValidationResult> ValidateDelete(T entity) { throw new NotImplementedException(); }
        protected virtual ICollection<ValidationResult> Validate(T entity) { throw new NotImplementedException(); }
    }
}
