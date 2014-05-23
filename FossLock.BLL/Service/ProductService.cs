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
        { }

        public override Product New()
        {
            return base.New();
        }

        public override ICollection<ValidationResult> ValidateAdd(Product entity)
        {
            // make sure these values make sense
            // generate a new keypair based on whatever was requested
            return base.ValidateAdd(entity);
        }

        public override ICollection<ValidationResult> ValidateUpdate(Product entity)
        {
            // make sure the keypairs didn't change.
            return base.ValidateUpdate(entity);
        }
    }
}
