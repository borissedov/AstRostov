using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class MainSliderItemList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MainSliderList();
            }
        }

        private void MainSliderList()
        {
            gridMainSliderItems.DataSource = CoreData.Context.MainSliderItems.ToArray();
            gridMainSliderItems.DataBind();
        }

        protected void DeleteMainSliderItem(int mainSliderItemId)
        {
            var mainSliderItemToDelete = CoreData.Context.MainSliderItems.SingleOrDefault(n => n.MainSliderItemId == mainSliderItemId);
            CoreData.Context.MainSliderItems.Remove(mainSliderItemToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/MainSliderItemList.aspx");
        }


        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditMainSliderItem.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteMainSliderItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}