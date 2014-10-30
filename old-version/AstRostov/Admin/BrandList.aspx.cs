using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class BrandList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBrandList();
            }
        }

        private void BindBrandList()
        {
            gridBrands.DataSource = CoreData.Context.Brands.ToArray();
            gridBrands.DataBind();
        }

        protected void DeleteBrand(int brandId)
        {

            var brandToDelete = CoreData.Context.Brands.SingleOrDefault(c => c.BrandId == brandId);
            CoreData.Context.Brands.Remove(brandToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/BrandList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditBrand.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteBrand(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}