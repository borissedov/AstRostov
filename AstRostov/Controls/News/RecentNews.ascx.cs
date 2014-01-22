using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Controls.News
{
    public partial class RecentNews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindItems();
            }
        }

        private void BindItems()
        {
            if (!CoreData.Context.News.Any())
            {
                this.Visible = false;
                return;
            }

            rptNews.DataSource = CoreData.Context.News.OrderByDescending(n => n.Created).Take(3).ToArray();
            rptNews.DataBind();
        }

        protected string GetAuthorName(object userObj)
        {
            var user = userObj as User;
            if (user != null)
            {
                return user.UserName;
            }
            return String.Empty;
        }
    }
}