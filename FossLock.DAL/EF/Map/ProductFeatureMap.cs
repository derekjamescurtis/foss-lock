using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model;

namespace FossLock.DAL.EF.Map
{
    class ProductFeatureMap : NamedEntityBaseMap<ProductFeature>
    {
        public ProductFeatureMap()
        {
            HasRequired(e => e.Product);
        }
    }
}
