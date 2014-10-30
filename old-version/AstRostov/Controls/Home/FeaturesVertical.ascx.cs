using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Controls.Home
{
    public partial class FeaturesVertical : UserControl
    {
        readonly Random _random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindFeaturedList();
            }
        }

        private void BindFeaturedList()
        {
            var productList =
                CoreData.Context.Products.Where(p => p.IsFeatured).ToArray().OrderBy(x => _random.Next()).ToArray();

            var productIndexRows =
                productList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = productIndexRows.Select(pir => pir.Select(i => productList[i]).ToArray()).ToArray();//groups of products by 3
            rptFeaturedListRows.DataSource = productRows;
            rptFeaturedListRows.DataBind();

        }

        protected void ProductRowDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rptFeaturedItems = e.Item.FindControl("rptFeaturedItems") as Repeater;
            var productRowDataSource = e.Item.DataItem as Product[];
            if (rptFeaturedItems != null && productRowDataSource != null)
            {
                rptFeaturedItems.DataSource = productRowDataSource;
                rptFeaturedItems.DataBind();
            }
        }

        protected void ProcessFeaturedCommand(object source, RepeaterCommandEventArgs e)
        {
            var productId = e.CommandArgument as int?;
            if (!String.IsNullOrEmpty(e.CommandName) && e.CommandName == "AddToCart" && productId.HasValue)
            {
                var product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == productId.Value);
                if (product != null)
                {
                    //ShoppingCart.Add(product)
                    Response.Redirect("~/ShoppingCart.aspx");
                }
            }
        }
    }
}