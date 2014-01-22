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
    public partial class FeaturedItemList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindFeaturedList();
            }
        }

        private void BindFeaturedList()
        {
            gridFeaturedItems.DataSource = CoreData.Context.FeaturedItems.ToArray();
            gridFeaturedItems.DataBind();
        }

        protected void DeleteFeaturedItem(int featuredItemId)
        {
            var featuredItemToDelete = CoreData.Context.FeaturedItems.SingleOrDefault(n => n.FeaturedItemId == featuredItemId);
            CoreData.Context.FeaturedItems.Remove(featuredItemToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/FeaturedItemList.aspx");
        }


        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditItem.aspx?mode=FeaturedItem&id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteFeaturedItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}