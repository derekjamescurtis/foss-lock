using FossLock.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FossLock.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductVersion
    {
        /// <summary>
        /// Uniquely identifies 
        /// </summary>
        public int Id { get; set; }

        public string Version { get; set; }

        public virtual Product Product { get; set; }

        public VersionType Type { get; set; }
    }
}