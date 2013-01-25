using FossLock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class AddEditProduct : System.Web.UI.Page
    {
        
        private SiteContext _db = new SiteContext();
        private Product _product;
        public Product Product
        {
            get { return _product; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // 
            PrepForm();

            // we're editing an existing product -- try to look it up in the db
            if (Request.QueryString["id"] != null)
            {
                var id = int.Parse(Request.QueryString["id"]);
                _product = _db.Products.Where(prod => prod.Id == id).FirstOrDefault();

                if (_product == null)
                {
                    Session["alertMessage"] = "Error looking up Product ID: " + id;
                    Response.Redirect("/Admin/Products.aspx");
                }

            }
            else
            {
                _product = new Product { Name = "New Product", ReleaseDate = DateTime.Now.Date };

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


        // populate all the list controls with their values -- most of these are enum values, so we're doing this manually rather than fanagle databinding to work here
        void PrepForm()
        {
                       

            if (LockPropertiesClbx.Items.Count == 0)
                LockPropertiesClbx.Items.AddRange(
                    new ListItem[]
                    {
                        new ListItem("CPU", ((int)FossLock.Core.LockPropertyType.CPU).ToString()),
                        new ListItem("Motherboard", ((int)FossLock.Core.LockPropertyType.Motherboard).ToString()),
                        new ListItem("Harddisk", ((int)FossLock.Core.LockPropertyType.Harddisk).ToString()),
                        new ListItem("Video Card", ((int)FossLock.Core.LockPropertyType.Video).ToString()),
                        new ListItem("BIOS", ((int)FossLock.Core.LockPropertyType.BIOS).ToString()),
                        new ListItem("MAC Address", ((int)FossLock.Core.LockPropertyType.MACAddress).ToString()),
                        new ListItem("UUID", ((int)FossLock.Core.LockPropertyType.UUID).ToString()),
                    }
                );


            if (PermittedActivationTypesClbx.Items.Count == 0)
                PermittedActivationTypesClbx.Items.AddRange(
                    new ListItem[]
                    {
                        new ListItem("Manual", ((int)FossLock.Core.ActivationType.Manual).ToString()),
                        new ListItem("E-Mail", ((int)FossLock.Core.ActivationType.Email).ToString()),
                        new ListItem("Online API", ((int)FossLock.Core.ActivationType.OnlineAPI).ToString()),
                        new ListItem("SMS", ((int)FossLock.Core.ActivationType.SMS).ToString()),
                        /*new ListItem("Phone", ((int)FossLock.Core.ActivationType.Phone).ToString()), -- not currently supported*/
                    }
                );


            if (PermittedExpirationTypesClbx.Items.Count == 0)
                PermittedExpirationTypesClbx.Items.AddRange(
                    new ListItem[]
                    {
                        new ListItem("Permanent", ((int)FossLock.Core.ExpirationType.Permanent).ToString()),
                        new ListItem("Expires on Calendar Date", ((int)FossLock.Core.ExpirationType.ExpiresOnCalendarDate).ToString()),
                        new ListItem("Expires (x) Days After Activation", ((int)FossLock.Core.ExpirationType.ExpiresDaysAfterActivation).ToString()),
                    }
                );


            if (VersionStyleCbx.Items.Count == 0)
                VersionStyleCbx.Items.AddRange(
                    new ListItem[]
                    {
                        new ListItem("Semantic", ((int)FossLock.Core.VersioningStyle.Semantic).ToString()),
                        new ListItem(".NET (System.Version)", ((int)FossLock.Core.VersioningStyle.DotNet).ToString()),
                    }
                );


            if (VersionLeewayCbx.Items.Count == 0)
                VersionLeewayCbx.Items.AddRange(
                    new ListItem[]
                    {
                        new ListItem("Strict", ((int)FossLock.Core.VersionLeewayType.Strict).ToString()),
                        new ListItem("Within Same Minor Version", ((int)FossLock.Core.VersionLeewayType.WithinSameMinorVersion).ToString()),
                        new ListItem("Within Same Major Version", ((int)FossLock.Core.VersionLeewayType.WithinSameMajorVersion).ToString()),
                    }
                );
        }


        // reads 
        void SetFormData()
        {
            NameTbx.Text        = this.Product.Name;
            ReleaseDateTbx.Text = this.Product.ReleaseDate.Date.ToShortDateString();

            // lock props
            foreach (ListItem prop in this.LockPropertiesClbx.Items)
            {
                FossLock.Core.LockPropertyType val = (FossLock.Core.LockPropertyType)Enum.Parse(typeof(FossLock.Core.LockPropertyType), prop.Value);

                if ((this.Product.DefaultLockProperties & val) == val)
                    prop.Selected = true;

            }

            FailOnNullHardwareIdChx.Checked = this.Product.FailOnNullHardwareIdentifier;
            
            // activation
            foreach (ListItem prop in this.PermittedActivationTypesClbx.Items)
            {
                FossLock.Core.ActivationType val = (FossLock.Core.ActivationType)Enum.Parse(typeof(FossLock.Core.ActivationType), prop.Value);

                if ((this.Product.PermittedActivationTypes & val) == val)
                    prop.Selected = true;
            }

            // expiration
            foreach (ListItem prop in this.PermittedExpirationTypesClbx.Items)
            {
                FossLock.Core.ExpirationType val = (FossLock.Core.ExpirationType)Enum.Parse(typeof(FossLock.Core.ExpirationType), prop.Value);

                if ((this.Product.PermittedExpirationTypes & val) == val)
                    prop.Selected = true;
            }

            MaximumTrialDaysTbx.Text        = this.Product.MaximumTrialDays.ToString();
            VersionStyleCbx.SelectedValue   = ((int)this.Product.VersioningStyle).ToString();
            VersionLeewayCbx.SelectedValue  = ((int)this.Product.VersionLeeway).ToString();
            NotesTbx.Text                   = this.Product.Notes;

        }

        // writes any pending changes to the database
        void UpdateDatabase()
        {
            // update the object based on the ui selection

            this.Product.Name = this.NameTbx.Text;
            this.Product.ReleaseDate = DateTime.Parse(this.ReleaseDateTbx.Text);
            this.Product.DefaultLockProperties = this.GetLockPropertiesValue();
            this.Product.FailOnNullHardwareIdentifier = this.FailOnNullHardwareIdChx.Checked;
            this.Product.PermittedActivationTypes = this.GetActivationTypesValue();
            this.Product.PermittedExpirationTypes = this.GetExpireationTypesValue();
            
            if (string.IsNullOrWhiteSpace(this.MaximumTrialDaysTbx.Text)) 
                this.Product.MaximumTrialDays = null;
            else
                this.Product.MaximumTrialDays = int.Parse(this.MaximumTrialDaysTbx.Text);
            this.Product.VersioningStyle = (FossLock.Core.VersioningStyle)Enum.Parse(typeof(FossLock.Core.VersioningStyle), this.VersionStyleCbx.SelectedValue);
            this.Product.VersionLeeway = (FossLock.Core.VersionLeewayType)Enum.Parse(typeof(FossLock.Core.VersionLeewayType), this.VersionLeewayCbx.SelectedValue);
            this.Product.Notes = this.NotesTbx.Text;


            try
            {


                if (this.Product.Id == 0)
                {
                    // actually we're adding the product
                    _db.Products.Add(this.Product);
                    _db.SaveChanges();

                    // reload the page
                    Session[SessionKeys.ALERT_MESSAGE] = "Product Created - " + this.Product.Name;
                    Response.Redirect("/Admin/Products.aspx");
                    //Response.Redirect(string.Format("/Admin/AddEditProduct.aspx?id={0}", this.Product.Id));
                }
                else
                {
                    // we're just updating the database
                    _db.SaveChanges();

                    // redirect back to the products page
                    Session[SessionKeys.ALERT_MESSAGE] = "Changes Saved - " + this.Product.Name; 
                    Response.Redirect("/Admin/Products.aspx");
                }
            }
            catch (Exception ex)
            {
                Session[SessionKeys.ALERT_MESSAGE] = "Error: " + ex.Message;
                Response.Redirect("/Admin/Products.aspx"); // todo: probably best to refactor these out to constants
            }

        }

        #region Calculate Enum Values from User Selection

        // used to calculate each of these values based on user selection in the ui
        FossLock.Core.LockPropertyType GetLockPropertiesValue()
        {
            FossLock.Core.LockPropertyType returnValue = Core.LockPropertyType.None;

            foreach (ListItem prop in this.LockPropertiesClbx.Items)
            {
                if (prop.Selected)                
                    returnValue |= (FossLock.Core.LockPropertyType)Enum.Parse(typeof(FossLock.Core.LockPropertyType), prop.Value);                
            }

            return returnValue;
        }
        FossLock.Core.ActivationType GetActivationTypesValue()
        {
            FossLock.Core.ActivationType returnValue = FossLock.Core.ActivationType.None;

            foreach (ListItem prop in this.LockPropertiesClbx.Items)
            {
                if (prop.Selected)
                    returnValue |= (FossLock.Core.ActivationType)Enum.Parse(typeof(FossLock.Core.ActivationType), prop.Value);
            }

            return returnValue;
        }
        FossLock.Core.ExpirationType GetExpireationTypesValue()
        {
            FossLock.Core.ExpirationType returnValue = FossLock.Core.ExpirationType.None;

            foreach (ListItem prop in this.PermittedExpirationTypesClbx.Items)
            {
                if (prop.Selected)
                    returnValue |= (FossLock.Core.ExpirationType)Enum.Parse(typeof(FossLock.Core.ExpirationType), prop.Value);
            }

            return returnValue;
        }

        #endregion

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // nothing exciting here           
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

        }


    }
}