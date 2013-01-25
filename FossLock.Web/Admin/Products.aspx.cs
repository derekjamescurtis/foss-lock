using System;
using System.Collections.Generic;
using System.Linq;
using FossLock.Model;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class Products : System.Web.UI.Page
    {

        
        SiteContext _db = new SiteContext();

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public IQueryable<Product> GetProducts()
        {
            return _db.Products.OrderBy(p => p.Name).AsQueryable();
        }

    }
}