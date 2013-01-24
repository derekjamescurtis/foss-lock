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
                _product = new Product();
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


            PermittedExpirationTypesClbx.Items.AddRange(
                new ListItem[]
                {
                    new ListItem("Permanent", ((int)FossLock.Core.ExpirationType.Permanent).ToString()),
                    new ListItem("Expires on Calendar Date", ((int)FossLock.Core.ExpirationType.ExpiresOnCalendarDate).ToString()),
                    new ListItem("Expires (x) Days After Activation", ((int)FossLock.Core.ExpirationType.ExpiresDaysAfterActivation).ToString()),
                }
            );


            VersionStyleCbx.Items.AddRange(
                new ListItem[]
                {
                    new ListItem("Semantic", ((int)FossLock.Core.VersioningStyle.Semantic).ToString()),
                    new ListItem(".NET (System.Version)", ((int)FossLock.Core.VersioningStyle.DotNet).ToString()),
                }
            );

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
            // expiration

            VersionStyleCbx.SelectedValue = ((int)this.Product.VersioningStyle).ToString();
            VersionLeewayCbx.SelectedValue = ((int)this.Product.VersionLeeway).ToString();
            NotesTbx.Text = this.Product.Notes;

        }

        // writes any pending changes to the database
        void UpdateDatabase()
        {

        }


    }
}