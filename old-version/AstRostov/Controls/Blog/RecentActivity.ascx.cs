using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Controls.Blog
{
    public partial class RecentActivity : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRecentPosts();
            }
        }

        private void BindRecentPosts()
        {
            rptRecentPosts.DataSource = CoreData.Context.Posts.OrderByDescending(p => p.Created).Take(5).ToArray();
            rptRecentPosts.DataBind();
        }
    }
}