using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Exceptions;
using AstCore.Models;
using AstCore.SearchEngine;
using AstECommerce;

namespace AstRostov
{
    public partial class Search : Page
    {
        protected String[] KeyWords
        {
            get
            {
                var keyWords = ViewState["KeyWords"] as String[];
                if (keyWords == null)
                {
                    keyWords = new string[] { };
                }
                return keyWords;
            }
            set
            {
                ViewState["KeyWords"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
                ParseKeyWords();
            //}
        }

        private void ParseKeyWords()
        {
            var keyWords = Request.Params["keywords"];
            if (!String.IsNullOrEmpty(keyWords))
            {
                KeyWords = keyWords.Split(new[] { '|' });
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindProductList();
                BindPaging();
            }
        }

        private void BindProductList()
        {
            var filteredProductListByNames = new List<Product>();
            foreach (var keyword in KeyWords)
            {
                filteredProductListByNames.AddRange(CoreData.Context.Products.Where(p => p.Name.Contains(keyword)));
            }

            var filteredProductListByDescriptions = new List<Product>();
            foreach (var keyword in KeyWords)
            {
                filteredProductListByDescriptions.AddRange(CoreData.Context.Products.Where(p => p.Description.Contains(keyword)));
            }

            Product[] productList =
                filteredProductListByNames.OrderBy(p => p.Name)
                    .Concat(filteredProductListByDescriptions.OrderBy(p => p.Name))
                    .Distinct(ProductComparer.GetInstance()).ToArray();

            #region BindBrands
            var brands =
                productList
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
            #endregion

            if (ddlBrands.SelectedValue != "-1")
            {
                productList = productList.Where(p => p.Brand != null && p.BrandId.ToString() == ddlBrands.SelectedValue).ToArray();
            }

            int count = productList.Length;
            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }

            var paginatedProductList = productList.Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();

            var productIndexRows =
                paginatedProductList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = productIndexRows.Select(pir => pir.Select(i => paginatedProductList[i]).ToArray()).ToArray();//groups of products by 3
            rptProductListRows.DataSource = productRows;
            rptProductListRows.DataBind();

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
                    if (product.SkuCollection.Count > 1)
                    {
                        Response.Redirect(String.Format("~/Product.aspx?id={0}", product.ProductId));
                    }
                    else
                    {
                        ShoppingCart.AddToCart(product.DefaultSku);
                        Response.Redirect("~/ShoppingCart.aspx");
                    }
                }
                else if (e.CommandName == "Reserve")
                {
                    if (product.SkuCollection.Count > 1)
                    {
                        Response.Redirect(String.Format("~/Product.aspx?id={0}", product.ProductId));
                    }
                    else
                    {
                        Response.Redirect(String.Format("~/Preorder.aspx?id={0}", product.DefaultSku.SkuId));
                    }
                }
            }
        }

        protected void FilterBrand(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Search.aspx?keywords={0}&brand={1}", Server.UrlEncode(String.Join("|", KeyWords)), ddlBrands.SelectedValue));
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

            rptPaging.DataSource = list.Select(i => new
            {
                PageNo = i
            });
            rptPaging.DataBind();
        }

        #endregion
    }
}