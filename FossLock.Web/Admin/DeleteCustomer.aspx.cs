using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class DeleteCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                int id;

                if (!int.TryParse(Request.QueryString["id"], out id))
                    throw new Exception("Error looking up customer for deletion.");
            
                var db = new Model.SiteContext();
                var cust = db.Customers.Where(c => c.Id == id).First();

                // used 
                var custName = cust.Name;

                // actually delete 
                db.Customers.Remove(cust);
                db.SaveChanges();

                Session[SessionKeys.ALERT_MESSAGE] = "Deleted Customer: " + custName;
            }
            catch (Exception ex)
            {
                Session[SessionKeys.ALERT_MESSAGE] = ex.Message;
            }
            finally
            {
                Response.Redirect(FossLock.Web.RedirectPaths.CUSTOMER_LIST);
            }

        }
    }
}