using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class NewsList : System.Web.UI.Page
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
            gridNews.DataSource = CoreData.Context.News.ToArray();
            gridNews.DataBind();
        }

        protected void DeleteNewsItem(int newsItemId)
        {

            var newsItemToDelete = CoreData.Context.News.SingleOrDefault(n => n.NewsItemId == newsItemId);
            CoreData.Context.News.Remove(newsItemToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/NewsList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditNewsItem.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteNewsItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}