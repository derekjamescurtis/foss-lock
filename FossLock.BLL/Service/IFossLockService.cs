using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FossLock.Model.Base;

namespace FossLock.BLL.Service
{
    /// <summary>
    ///     Service layer interface that performs validation passes
    ///     requests to the data access layer.
    /// </summary>
    /// <typeparam name="T">
    ///     Entity type that this service will return.
    /// </typeparam>
    public interface IFossLockService<T>
        where T : IEntityBase, new()
    {
        /// <summary>
        ///     Returns the entity with the specified key value.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier for an object in the object graph.
        ///     This should typically be assumed to the primary key field 
        ///     from the database.
        /// </param>
        /// <returns>
        ///     Entity of T with the matching unique identifier.
        ///     If none can be found, null will be returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Raised if the argument 'id' is less than 1.
        /// </exception>
        T GetById(int id);

        /// <summary>
        ///     Returns the full object graph.  No sorting or filtering is done.
        /// </summary>
        /// <returns>
        ///     A list containing all entities in the repository.
        /// </returns>
        IList<T> GetList();

        /// <summary>
        ///     Returns a paginated subset of the object graph.
        /// </summary>
        /// <param name="pageNumber">
        ///     The requested 'slice/page' of the object graph to return.
        ///     This index is 1-based.  If a 'slice/page' is requested that's beyond
        ///     the range of the object graph, an empty list should be returned.
        /// </param>
        /// <param name="pageSize">
        ///     The requested maximum number of entities to return on each 'slice/page' 
        ///     of the object graph to return.
        /// </param>
        /// <returns>
        ///     A list representing a subset of the full object graph.
        /// </returns>
        /// <exception cref="ArgumentInvalidException">
        ///     Thrown if either 'pageNumber' or 'pageSize' is less than 1.
        /// </exception>
        IList<T> GetList(int pageNumber, int pageSize);

        /// <summary>
        ///     Returns a new transient entity of the type handled by this service.
        ///     This entity is not stored in the object graph, and will not be available
        ///     Until it is configured and passed to the 'Add' method.
        /// </summary>
        /// <returns>
        ///     An object of type T that is not placed in the object graph.
        /// </returns>
        T New();

        T Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        ICollection<ValidationResult> ValidateAdd(T entity);

        ICollection<ValidationResult> ValidateUpdate(T entity);

        ICollection<ValidationResult> ValidateDelete(T entity);
    }
}
