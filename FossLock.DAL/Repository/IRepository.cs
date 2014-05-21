using System.Collections.Generic;
using FossLock.Model.Base;

namespace FossLock.DAL.Repository
{
    /// <summary>
    ///     A repository that is responsible for communicating with whatever
    ///     underlying datastore is being used.  It is responsible for
    ///     retrieving/modifying object of a single type (specified by
    ///     the type parameter), along with any of it's related objects.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of entity that will be retrieved/modified by this repository
    ///     in the underlying data store.
    /// </typeparam>
    public interface IRepository<T>
        where T : IEntityBase
    {
        /// <summary>
        ///     Returns a single instance of T from the data store, looked up
        ///     by it's Id property.
        /// </summary>
        /// <param name="id">
        ///     The unique identifier of an entity.  This is typically it's
        ///     database primary key field.
        /// </param>
        /// <returns>
        ///     An instance of type T from the datastore.
        ///     If an entity with a matching Id cannot be found, a null value
        ///     is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if an param 'id' is less than 1.
        /// </exception>
        T GetById(int id);

        /// <summary>
        ///     Returns the entire object graph for all entities of type T
        ///     in the data store.
        /// </summary>
        /// <returns>
        ///     The complete object graph for type T.
        /// </returns>
        IList<T> GetAll();

        /// <summary>
        ///     Inserts the provided 'entity' into the object graph and
        ///     immediately commits all pending changes to the underlying
        ///     data store.
        /// </summary>
        /// <param name="entity">
        ///     The object to be inserted in the object graph.
        /// </param>
        /// <returns>
        ///     The object once it has been written to the underlying
        ///     data store.  It will contain a valid Id field, once
        ///     returned by this method.
        /// </returns>
        T Add(T entity);

        /// <summary>
        ///     Updates the provided object in the underlying data store and
        ///     immediately commits all pending changes to the underlying data store.
        ///     NOTE: With the EFRepository, this essentially only has the effect of
        ///     manually flagging an object as 'modified' and writing changes to the
        ///     database.
        /// </summary>
        /// <param name="entity">
        ///     The object to be updated in the object graph.
        /// </param>
        /// <returns>
        ///     A reference to the same object that was updated.
        /// </returns>
        T Update(T entity);

        /// <summary>
        ///     Remove the provided entity from the object graph,
        ///     and immediately commit all pending changes to the data store.
        /// </summary>
        /// <param name="entity">
        ///     The entit yto be removed from the object graph and data store.
        /// </param>
        void Delete(T entity);

        /// <summary>
        ///     Gets a reference to this repository's underlying data store.
        /// </summary>
        object Source { get; }
    }
}