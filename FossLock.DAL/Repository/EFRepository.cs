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
    public class EFRepository<T> : IRepository<T>
        where T : EntityBase
    {
        /// <summary>Initializes a new instance of this class.
        /// Automatically instantiates a new DbContext.
        /// </summary>
        public EFRepository()
        {
            _db = new AppDb();
        }

        // TODO: source parameter might need to change to OBJECT .. we'll see if I ever get around to implementing a repo for anything other than EF..

        /// <summary>Initializes a new instance using whatever
        /// repository is provided to the constructor.
        /// </summary>
        /// <param name="source"></param>
        public EFRepository(AppDb source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            _db = source;
        }

        private readonly AppDb _db = null;

        /// <summary>Returns a reference to the DbContext
        /// being used by this repository instance.
        /// </summary>
        public object Source
        {
            get { return _db; }
        }

        #region IRepository<T> Members

        public T GetById(int id)
        {
            var dbSet = _db.Set<T>();
            return dbSet.Find(id);
        }

        public IList<T> GetAll()
        {
            var dbSet = _db.Set<T>();
            return dbSet.ToList();
        }

        /// <summary>Adds the entity to the object graph and
        /// immediately writes all pending changes to the data store.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            var dbSet = _db.Set<T>();
            var returnEntity = dbSet.Add(entity);
            _db.SaveChanges();
            return returnEntity;
        }

        /// <summary>Makes the specified entity as 'Modified' and
        /// immediately writes all pending changes to the data store.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            var dbSet = _db.Set<T>();
            _db.Entry<T>(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }

        /// <summary>Removes the specified object from the object graph
        /// and immediately writes all pending changes to the data store.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            var dbSet = _db.Set<T>();
            dbSet.Remove(entity);
            _db.SaveChanges();
        }

        #endregion IRepository<T> Members
    }
}