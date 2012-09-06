using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossLock.Web.Model
{
    public class ProductVersion
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public Product Product { get; set; }
    }
}