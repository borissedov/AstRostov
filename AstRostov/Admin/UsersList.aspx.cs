using System;
using System.Linq;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class UsersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindUsersList();
            }
        }

        private void BindUsersList()
        {
            gridUsers.DataSource = CoreData.Context.Users.Include("Address").Include("Membership").ToArray();
            gridUsers.DataBind();
        }
    }
}