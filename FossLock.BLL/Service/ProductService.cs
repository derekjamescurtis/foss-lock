using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.DAL.Repository;
using FossLock.Model;

namespace FossLock.BLL.Service
{

    ///<summary>
    ///
    ///</summary>
    public sealed class ProductService : GenericService<Product>
    {
        public ProductService(IRepository<Product> repository) :
            base(repository)
        {
        }
    }
}
