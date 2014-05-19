using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FossLock.Model.Base;
namespace FossLock.BLL.Service
{
    /// <summary>Service layer interface that performs validation passes
    /// requests to the data access layer.
    /// </summary>
    /// <typeparam name="T">Entity type that this service will return.</typeparam>
    public interface IFossLockService<T> 
        where T : IEntityBase, new()
    {
        /// <summary>Returns the entity with the specified key value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity of T</returns>
        T GetById(int id);

        /// <summary>Returns the full object graph.  No sorting or filtering is done.
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetList();

        /// <summary>Returns a paginated subset of the object graph.  If re
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentInvalidException"></exception>
        ICollection<T> GetList(int pageNumber, int pageSize);

        T New();
        T Add(T entity);        
        T Update(T entity);
        void Delete(T entity);

        ICollection<ValidationResult> ValidateAdd(T entity);
        ICollection<ValidationResult> ValidateUpdate(T entity);
        ICollection<ValidationResult> ValidateDelete(T entity);
    }
}
