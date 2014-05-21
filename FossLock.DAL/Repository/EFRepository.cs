using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.DAL.EF;
using FossLock.Model.Base;

namespace FossLock.DAL.Repository
{
    /// <summary>
    ///     A repository backed by an Entity Framework DbContext.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The entity type to be managed by this repository.
    /// </typeparam>
    public class EFRepository<TEntity> : RepositoryBase<TEntity, AppDb>
        where TEntity : EntityBase
    {
        #region Constructors

        public EFRepository()
            : base()
        { }

        public EFRepository(AppDb source)
            : base(source)
        { }

        #endregion Constructors

        #region RepositoryBase<TEntity, TDataSource>

        public override TEntity GetById(int id)
        {
            var dbSet = _source.Set<TEntity>();
            return dbSet.Find(id);
        }

        public override IList<TEntity> GetAll()
        {
            var dbSet = _source.Set<TEntity>();
            return dbSet.ToList();
        }

        public override TEntity Add(TEntity entity)
        {
            var dbSet = _source.Set<TEntity>();
            var returnEntity = dbSet.Add(entity);
            _source.SaveChanges();
            return returnEntity;
        }

        public override TEntity Update(TEntity entity)
        {
            var dbSet = _source.Set<TEntity>();
            _source.Entry<TEntity>(entity).State = EntityState.Modified;
            _source.SaveChanges();
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            var dbSet = _source.Set<TEntity>();
            dbSet.Remove(entity);
            _source.SaveChanges();
        }

        #endregion RepositoryBase<TEntity, TDataSource>
    }
}