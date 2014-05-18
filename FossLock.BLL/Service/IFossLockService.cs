using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FossLock.Model.Base;
namespace FossLock.BLL.Service
{
    public interface IFossLockService<T> 
        where T : EntityBase
    {
        T Add(T entity);
        void Delete(T entity);
        T GetById(int id);
        ICollection<T> GetList();
        ICollection<T> GetList(int pageNumber, int pageSize);
        T Update(T entity);
    }
}
