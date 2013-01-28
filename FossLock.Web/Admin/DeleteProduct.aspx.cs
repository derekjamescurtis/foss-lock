using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class DeleteProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int id;

            if (!int.TryParse(Request.QueryString["id"], out id))
                throw new Exception("Error looking up product for deletion.");

            // lookup the product we're going to delete
            var db = new Model.SiteContext();
            var prodToDelete = db.Products.Where(p => p.Id == id).FirstOrDefault();

            // used 
            var prodName = prodToDelete.Name;

            try
            {
                db.Products.Remove(prodToDelete);
                db.SaveChanges();

                Session[SessionKeys.ALERT_MESSAGE] = "Deleted Product: " + prodName;
            }
            catch (Exception ex)
            {
                Session[SessionKeys.ALERT_MESSAGE] = ex.Message;
            }
            finally
            {
                Response.Redirect(FossLock.Web.RedirectPaths.PRODUCT_LIST);
            }
            
        }
    }
}