using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Exceptions;
using AstCore.Models;
using AstCore.SearchEngine;
using AstECommerce;

namespace AstRostov
{
    public partial class CategoryPage : System.Web.UI.Page
    {
        protected int ItemId
        {
            get
            {
                int id;
                if (hdnItemId.Value != null && int.TryParse(hdnItemId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set
            {
                hdnItemId.Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseItemId();
            }
        }

        private void ParseItemId()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
                if (category == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                BindBrands();
                BindProductList();
                BindPaging();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (AstMembership.CurrentUser != null && AstMembership.CurrentUser.IsAdmin)
            {
                hlAddProduct.Visible = true;
                hlAddProduct.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?cid={0}", ItemId));
            }
        }

        private void BindProductList()
        {
            var category = CoreData.Context.Categories.Single(c => c.CategoryId == ItemId);

            litCategoryName.Text = category.Name;
            litCategoryNameHead.Text = category.Name;

            Product[] paginatedProductList;
            int count;
            if (ddlBrands.SelectedValue != "-1")
            {
                paginatedProductList = category.ProductList.Where(p => p.Brand != null && p.BrandId.ToString() == ddlBrands.SelectedValue).Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();
                count = category.ProductList.Count(p => p.Brand != null && p.BrandId.ToString() == ddlBrands.SelectedValue);
            }
            else
            {
                paginatedProductList = category.ProductList.Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();
                count = category.ProductList.Count();
            }

            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }

            var productIndexRows =
                paginatedProductList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = productIndexRows.Select(pir => pir.Select(i => paginatedProductList[i]).ToArray()).ToArray();//groups of products by 3
            rptProductListRows.DataSource = productRows;
            rptProductListRows.DataBind();

        }

        private void BindBrands()
        {
            var brands =
                CoreData.Context.Categories.Single(c => c.CategoryId == ItemId).ProductList
                    .Select(p => p.Brand)
                    .Where(b => b != null).ToArray()
                    .Distinct(BrandComparer.GetInstance())
                    .OrderBy(b => b.Name);
            ddlBrands.DataSource = new[] { new ListItem("Выберите производителя", "-1") }.Concat(brands.Select(b => new ListItem
                {
                    Text = b.Name,
                    Value = b.BrandId.ToString(CultureInfo.InvariantCulture)
                }));
            ddlBrands.DataTextField = "Text";
            ddlBrands.DataValueField = "Value";
            ddlBrands.DataBind();

            int brandId;
            if (int.TryParse(Request.Params["brand"], out brandId) && brands.Any(b => b.BrandId == brandId))
            {
                ddlBrands.SelectedValue = brandId.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                ddlBrands.SelectedValue = "-1";
            }
        }

        protected void ProductRowDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rptProductListItems = e.Item.FindControl("rptProductListItems") as Repeater;
            var productRowDataSource = e.Item.DataItem as Product[];
            if (rptProductListItems != null && productRowDataSource != null)
            {
                rptProductListItems.DataSource = productRowDataSource;
                rptProductListItems.DataBind();
            }
        }

        protected void ProcessProductCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId;
            if (int.TryParse(e.CommandArgument.ToString(), out productId) && productId > 0 && !String.IsNullOrEmpty(e.CommandName))
            {
                var product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    throw new CatalogException("Product not found");
                }

                if (e.CommandName == "AddToCart")
                {
                    ShoppingCart.AddToCart(product.DefaultSku);
                    Response.Redirect("~/ShoppingCart.aspx");
                }
                else if (e.CommandName == "Reserve")
                {
                    Response.Redirect(String.Format("~/Preorder.aspx?id={0}", product.DefaultSku.SkuId));
                }
            }
        }

        protected void FilterBrand(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Category.aspx?id={0}&brand={1}", ItemId, ddlBrands.SelectedValue));
        }

        #region Pagination

        private const int ItemsPerPage = 12;

        protected int CurrentPageNo
        {
            get
            {
                int pageNo;
                if (int.TryParse(Request.Params["page"], out pageNo))
                {
                    return pageNo;
                }
                return 1;
            }
        }

        protected int PageCount
        {
            get
            {
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }

        private void BindPaging()
        {
            var list = new List<int>();
            for (int i = 1; i <= PageCount; i++)
            {
                list.Add(i);
            }

            if (list.Count > 1)
            {
                rptPaging.DataSource = list.Select(i => new
                {
                    PageNo = i
                });
                rptPaging.DataBind();
            }
            else
            {
                divPagination.Visible = false;
            }
        }

        #endregion
    }
}