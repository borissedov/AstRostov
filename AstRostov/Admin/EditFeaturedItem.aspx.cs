using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditFeaturedItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindProductList();
                //BindFeaturedItemForm();
            }
        }

        private void BindProductList()
        {
            ddlProducts.Items.Add(new ListItem
            {
                Text = "Выберите продукт",
                Value = "0",
                Selected = true
            });
            ddlProducts.Items.AddRange(
                CoreData.Context.Products.Where(p=>p.FeaturedItem == null).ToArray()
                .Select(p =>
                    new ListItem(p.Name, p.ProductId.ToString(CultureInfo.InvariantCulture)))
                    .ToArray());

        }

        protected void SaveFeaturedItem(object sender, EventArgs e)
        {
            int productId;
            if (int.TryParse(ddlProducts.SelectedValue, out productId) && productId != 0)
            {
                var product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                var featuredItem = new FeaturedItem
                {
                    Product = product
                };
                
                CoreData.Context.FeaturedItems.Add(featuredItem);
               
                CoreData.Context.SaveChanges();
                Response.Redirect("~/Admin/FeaturedItemList.aspx");
            }
        }
    }
}