using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossLock.Model
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}