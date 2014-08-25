using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FossLock.Model;

namespace FossLock.DAL.EF.Map
{
    internal class CustomerMap : NamedEntityBaseMap<Customer>
    {
        public CustomerMap()
        {
            // nothing special here at the current time.

            Property(e => e.OfficePhone1).HasMaxLength(40);
            Property(e => e.OfficePhone2).HasMaxLength(40);
            Property(e => e.OfficeFax).HasMaxLength(40);
        }
    }
}
