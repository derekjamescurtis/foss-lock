using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FossLock.Web.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        public IQueryable<MembershipUser> GetAllUsers()
        {
            if (_users == null)
            {
                _users = new List<MembershipUser>();

                foreach (MembershipUser user in Membership.GetAllUsers())
                    _users.Add(user);

            }

            return _users.AsQueryable().OrderBy(u => u.UserName);
        }
        List<MembershipUser> _users;

    }
}