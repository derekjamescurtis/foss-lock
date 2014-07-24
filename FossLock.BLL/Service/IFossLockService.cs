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

        /// <summary>
        ///     Adds the specified entity to the object graph and commits all
        ///     pending changes to the repository.
        /// </summary>
        /// <param name="entity">
        ///     The entity to add to the object graph.
        /// </param>
        /// <returns>
        ///     Returns the entity that was passed in to the function.  Depending
        ///     on the repository implementation this may be a reference to the
        ///     instance as was passed in or it may be a new instance representing
        ///     the same entity.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if a null argument is provided for 'entity'
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if 'entity' fails validation for any reason.
        ///     The details of all the validation failures can be discovered
        ///     by calling the 'ValidateAdd' method with the same entity.
        /// </exception>
        T Add(T entity);

        /// <summary>
        ///     Updates the provided entity in the object graph
        ///     and commits any pending changes to the repository.
        /// </summary>
        /// <param name="entity">
        ///     The entity with changes that we want to persist.
        /// </param>
        /// <returns>
        ///     An object representing the same entity that was provided as
        ///     an argument to this method.  Depending on the repository
        ///     implementation this may be the same object reference or
        ///     it may be a new object representing the same entity.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if a null argument is provided for 'entity'
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if 'entity' fails validation for any reason.
        ///     The details of all the validation failures can be discovered
        ///     by calling the 'ValidateUpdate' method with the same entity.
        /// </exception>
        T Update(T entity);

        /// <summary>
        ///     Removes the provided entity from the object graph and
        ///     commits any pending changes to the repository.
        /// </summary>
        /// <param name="entity">
        ///     The entity that we wish to remove from persistence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if a null argument is provided for 'entity'
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if 'entity' fails validation for any reason.
        ///     The details of all the validation failures can be discovered
        ///     by calling the 'ValidateDelete' method with the same entity.
        /// </exception>
        void Delete(T entity);

        /// <summary>
        ///     Checks the entity for any validation problems
        ///     with adding it to the object graph.
        /// </summary>
        /// <param name="entity">
        ///     The entity to validate.
        /// </param>
        /// <returns>
        ///     A list of 'ValidationResult' objects, one for each validation
        ///     failure.  If no failures, an empty list will be returned.
        /// </returns>
        ICollection<ValidationResult> ValidateAdd(T entity);

        /// <summary>
        ///     Checks the entity for any validation problems
        ///     with persisting it's changes to the repository.
        /// </summary>
        /// <param name="entity">
        ///     The entity to validate.
        /// </param>
        /// <returns>
        ///     A list of 'ValidationResult' objects, one for each validation
        ///     failure.  If no failures, an empty list will be returned.
        /// </returns>
        ICollection<ValidationResult> ValidateUpdate(T entity);

        /// <summary>
        ///     Checks the entity for any validation problems
        ///     with removing it from the  object graph.  Note:
        ///     entity-level validation checks are skipped
        ///     (such as whether all required fields are present).
        /// </summary>
        /// <param name="entity">
        ///     The entity to validate.
        /// </param>
        /// <returns>
        ///     A list of 'ValidationResult' objects, one for each validation
        ///     failure.  If no failures, an empty list will be returned.
        /// </returns>
        ICollection<ValidationResult> ValidateDelete(T entity);
    }
}
