using FossLock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class AddEditCustomer : System.Web.UI.Page
    {
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {

            // we're editing an existing product -- try to look it up in the db
            if (Request.QueryString["id"] != null)
            {
                var id = int.Parse(Request.QueryString["id"]);
                _customer = _db.Customers.Where(c => c.Id == id).FirstOrDefault();

                // these weren't the droids you were looking for. =/ sorry
                if (_customer == null)
                {
                    Session[SessionKeys.ALERT_MESSAGE] = "Error looking up Customer ID: " + id;
                    Response.Redirect(RedirectPaths.CUSTOMER_LIST); 
                }

                // set the postback url for the delete button -- need to check this user is an admin
                this.DeleteButton.PostBackUrl = RedirectPaths.CUSTOMER_DELETE + "?id=" + Request.QueryString["id"];
            }
            else
            {
                // just add a few simple properties
                _customer = new Customer { Name = "New Customer" };

                // update the buttons to reflect the fact that we're adding a new product
                this.SaveButton.Text = "Add";
                this.DeleteButton.Visible = false;
            }


            // 
            if (IsPostBack)
                UpdateDatabase();
            else
                SetFormData();
        }

        #endregion
        #region Fields + Properties

        Customer _customer;
        public Customer Customer
        {
            get { return _customer; }
        }
        SiteContext _db = new SiteContext();

        #endregion
        #region Private Methods
        
        void SetFormData()
        {
            NameTbx.Text = Customer.Name;
            ContactFNameTbx.Text = Customer.ContactFirstName;
            ContactLNameTbx.Text = Customer.ContactLastName;
            CanLicensePrereleaseChx.Checked = Customer.CanLicensePreReleaseVersions;
            Address1Tbx.Text = Customer.Address1;
            Address2Tbx.Text = Customer.Address2;
            CityTbx.Text = Customer.City;
            StateTbx.Text = Customer.State;
            PostalCodeTbx.Text = Customer.PostalCode;
            CountryTbx.Text = Customer.Country;
            Phone1Tbx.Text = Customer.Phone1;
            Phone2Tbx.Text = Customer.Phone2;
            FaxTbx.Text = Customer.Fax;
            EmailTbx.Text = Customer.Email;
            NotesTbx.Text = Customer.Notes;
        }

        void UpdateDatabase()
        {
            Customer.Name = NameTbx.Text;
            Customer.ContactFirstName = ContactFNameTbx.Text;
            Customer.ContactLastName = ContactLNameTbx.Text;
            Customer.CanLicensePreReleaseVersions = CanLicensePrereleaseChx.Checked;

            // the following all accept nulls, but for ef to be happy with our data annotations, we need to set them to null
            Customer.Address1   = string.IsNullOrWhiteSpace(Address1Tbx.Text) ? null : Address1Tbx.Text;
            Customer.Address2   = string.IsNullOrWhiteSpace(Address2Tbx.Text) ? null : Address2Tbx.Text;
            Customer.City       = string.IsNullOrWhiteSpace(CityTbx.Text) ? null : CityTbx.Text;
            Customer.State      = string.IsNullOrWhiteSpace(StateTbx.Text) ? null : StateTbx.Text;
            Customer.PostalCode = string.IsNullOrWhiteSpace(PostalCodeTbx.Text) ? null : PostalCodeTbx.Text;
            Customer.Phone1     = string.IsNullOrWhiteSpace(Phone1Tbx.Text) ? null : Phone1Tbx.Text;
            Customer.Phone2     = string.IsNullOrWhiteSpace(Phone2Tbx.Text) ? null : Phone2Tbx.Text;
            Customer.Fax        = string.IsNullOrWhiteSpace(FaxTbx.Text) ? null : FaxTbx.Text;
            Customer.Email      = string.IsNullOrWhiteSpace(EmailTbx.Text) ? null : EmailTbx.Text;
            Customer.Notes      = string.IsNullOrWhiteSpace(NotesTbx.Text) ? null : NotesTbx.Text;

            try
            {
                if (Customer.Id == 0)
                {
                    _db.Customers.Add(Customer);
                    _db.SaveChanges();
                    Session[SessionKeys.ALERT_MESSAGE] = "Customer Created - " + Customer.Name;
                }
                else
                {
                    _db.SaveChanges();
                    Session[SessionKeys.ALERT_MESSAGE] = "Changes Saved - " + Customer.Name;
                }

            }
            catch (Exception ex)
            {
                Session[SessionKeys.ALERT_MESSAGE] = "Error - " + ex.ToString();
            }
            finally
            {
                Response.Redirect(RedirectPaths.CUSTOMER_LIST);
            }
        }

        #endregion
    }
}