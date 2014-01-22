using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategoryList();
            }
        }

        private void BindCategoryList()
        {
            gridCategories.DataSource = CoreData.Context.Categories.ToArray();
            gridCategories.DataBind();
        }

        protected void DeleteCategory(int categoryId)
        {
            var categoryToDelete = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
            if (categoryToDelete != null)
            {
                foreach (var product in categoryToDelete.Products.ToArray())
                {
                    CoreData.Context.Products.Remove(product);
                }

                foreach (var childCategory in categoryToDelete.ChildCategories.ToArray())
                {
                    childCategory.ParentCategories.Remove(categoryToDelete);
                }

                foreach (var parentCategory in categoryToDelete.ParentCategories.ToArray())
                {
                    parentCategory.ChildCategories.Remove(categoryToDelete);
                }

                CoreData.Context.Categories.Remove(categoryToDelete);
                CoreData.Context.SaveChanges();
            }
            Response.Redirect("~/Admin/CategoryList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditCategory.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteCategory(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}