﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindProductList();
            }
        }

        private void BindProductList()
        {
            IEnumerable<Product> productList = CoreData.Context.Products;

            int cid;
            if (int.TryParse(Request.Params["cid"], out cid))
            {
                productList = productList.Where(p=>p.CategoryId == cid);
            }

            gridProducts.DataSource = productList.ToArray();
            gridProducts.DataBind();
        }

        protected void DeleteProduct(int productId)
        {
            Product productToDelete = CoreData.Context.Products.Single(c => c.ProductId == productId);

            productToDelete.Attributes.Clear();

            foreach (var sku in productToDelete.SkuCollection)
            {
                sku.AttributeValues.Clear();
            }

            CoreData.Context.Products.Remove(productToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/ProductList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteProduct(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}