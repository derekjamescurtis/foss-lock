using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.BLL.Util;
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

        public override Product Add(Product entity)
        {
            // assign our keys
            var kpGen = new RsaKeypairGenerator();
            var kp = kpGen.GenerateKeypair(entity.LicenseEncryptionType);
            entity.PublicKey = kp.PubKey;
            entity.PrivateKey = kp.PrivKey;

            return base.Add(entity);
        }

        public override ICollection<ValidationResult> ValidateAdd(Product entity)
        {
            // make sure these values make sense

            // check required things.

            return base.ValidateAdd(entity);
        }

        public override ICollection<ValidationResult> ValidateUpdate(Product entity)
        {
            // make sure the keypairs didn't change.
            return base.ValidateUpdate(entity);
        }
    }
}
