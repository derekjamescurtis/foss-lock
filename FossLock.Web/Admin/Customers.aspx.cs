using FossLock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class Customers : System.Web.UI.Page
    {

        int _pageNumber = 1;
        const int PAGE_SIZE = 20;        
        SiteContext _db = new SiteContext();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["page_num"], out _pageNumber) && _pageNumber > 0 && _pageNumber <= this.MaxPages)
            {
                // do nothing right now
            }
            else
            {
                // invalid page was entered... go back to page one.
                Response.Redirect(RedirectPaths.CUSTOMER_LIST + "?page_num=1");
            }
        }

        public IQueryable<Customer> GetCustomers()
        {
            if (_page_customers == null)
            {
                _page_customers = _db.Customers
                                    .OrderBy(c => c.Name)
                                    .Skip(PAGE_SIZE * (_pageNumber - 1))
                                    .Take(PAGE_SIZE)
                                    .AsQueryable();
            }

            return _page_customers;
        }
        IQueryable<Customer> _page_customers;

        int MaxPages
        {
            get
            {
                if (_maxPages == -1)
                {
                    var custCount = _db.Customers.Count();

                    _maxPages = custCount / PAGE_SIZE;

                    // add a final partial page
                    if (custCount % PAGE_SIZE > 0)
                        ++_maxPages;

                }

                return _maxPages;
            }
        }
        int _maxPages = -1;

    }
}