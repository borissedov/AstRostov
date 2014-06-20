using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class StaticPageList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNewsList();
            }
        }

        private void BindNewsList()
        {
            gridStaticPages.DataSource = CoreData.Context.StaticPages.ToArray();
            gridStaticPages.DataBind();
        }

        protected void DeleteStaticPage(int staticPageId)
        {

            var newsItemToDelete = CoreData.Context.StaticPages.SingleOrDefault(n => n.Id == staticPageId);
            CoreData.Context.StaticPages.Remove(newsItemToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/StaticPageList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditStaticPage.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteStaticPage(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}