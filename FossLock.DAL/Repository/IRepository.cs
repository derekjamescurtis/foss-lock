using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model.Base;

namespace FossLock.DAL.Repository
{
    public interface IRepository<T> 
        where T : IEntityBase
    {
        T GetById(int id);
        ICollection<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
