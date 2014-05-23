using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model;

namespace FossLock.DAL.EF.Map
{
    internal class ProductMap : NamedEntityBaseMap<Product>
    {
        public ProductMap()
        {
            Property(e => e.PublicKey).IsRequired();
            Property(e => e.PrivateKey).IsRequired();
        }
    }
}
