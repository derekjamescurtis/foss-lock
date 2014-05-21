using System;
using FossLock.Model.Base;

namespace FossLock.DAL.Repository
{
    /// <summary>
    ///     Base class for all repositories.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type to be managed by the repository.
    /// </typeparam>
    /// <typeparam name="TDataSource">
    ///     The data source for the repository (ex, an EntityFramework DbContext).
    ///     The datasource must provide a default, parameterless constructor.
    /// </typeparam>
    public abstract class RepositoryBase<TEntity, TDataSource> : IRepository<TEntity>
        where TEntity : IEntityBase
        where TDataSource : class, new()
    {
        #region Constructors

        /// <summary>
        ///     Creates a new instance of the repository backed with the specified
        ///     TDataSource type.  TDataSource is instantiated with it's default
        ///     constructor.
        /// </summary>
        public RepositoryBase()
        {
            _source = new TDataSource();
        }

        /// <summary>
        ///     Creates a new repository instance using whatever datasource
        ///     is provided as the 'source' parameter.
        /// </summary>
        /// <param name="source">
        ///     An instance of TDataSource that will be used to
        ///     back this repository.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if a null argument is provided.
        /// </exception>
        public RepositoryBase(TDataSource source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            _source = source;
        }

        #endregion Constructors

        #region IRepository<TEntity> Members

        protected readonly TDataSource _source;

        public object Source
        {
            get { return _source; }
        }

        public abstract TEntity GetById(int id);

        public abstract System.Collections.Generic.IList<TEntity> GetAll();

        public abstract TEntity Add(TEntity entity);

        public abstract TEntity Update(TEntity entity);

        public abstract void Delete(TEntity entity);

        #endregion IRepository<TEntity> Members
    }
}