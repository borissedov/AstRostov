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
        private Category _category;

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

        private int[] SelectedAttributeValueIds
        {
            get
            {
                var attrStr = Request.Params["attrs"];
                if (!String.IsNullOrEmpty(attrStr))
                {
                    return attrStr
                        .Split(new[] { '|' })
                        .Select(int.Parse)
                        .ToArray();
                }
                else
                {
                    return new int[0];
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == ItemId);
                if (_category == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                litCategoryName.Text = _category.Name;
                litCategoryDescription.Text = _category.Description;
                litCategoryNameHead.Text = _category.Name;

                if (!_category.HasChildren)
                {
                    phLeftContent.Visible = true;
                    BindBrands();
                    BindFilters();
                    BindProductList();
                }
                else
                {
                    BindChildren();
                }
                
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

        private void BindChildren()
        {
            Category[] paginatedSubCategoryList = _category.ChildCategories
                .Skip((CurrentPageNo - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToArray();
            int count = _category.ChildCategories.Count();

            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }

            var subcategoryIndexRows =
                paginatedSubCategoryList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = subcategoryIndexRows.Select(pir => pir.Select(i => paginatedSubCategoryList[i]).ToArray()).ToArray();//groups of products by 3
            rptChildCategoriesRows.Visible = true;
            rptChildCategoriesRows.DataSource = productRows;
            rptChildCategoriesRows.DataBind();
        }

        private void BindProductList()
        {
            IEnumerable<Product> filteredList;
            Product[] paginatedProductList;
            int count;

            filteredList = _category.ProductList;

            if (ddlBrands.SelectedValue != "-1")
            {
                filteredList =
                    filteredList.Where(p => p.Brand != null && p.BrandId.ToString() == ddlBrands.SelectedValue);
            }

            if (SelectedAttributeValueIds.Any())
            {
                filteredList =
                    filteredList.Where(
                        p =>
                            p.SkuCollection
                                .Any(
                                s => s.AttributeValues
                                    .Select(v => v.AttributeValueId)
                                    .Intersect(SelectedAttributeValueIds)
                                    .Count() == SelectedAttributeValueIds.Length
                                    )

                            );
            }

            var resultList = filteredList.ToArray();

            paginatedProductList = resultList
                .Skip((CurrentPageNo - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToArray();
            count = resultList.Count();

            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }

            var productIndexRows =
                paginatedProductList.Select((p, i) => i).GroupBy(i => i / 3).Cast<IEnumerable<int>>().ToArray();//groups of indexes in array by 3
            var productRows = productIndexRows.Select(pir => pir.Select(i => paginatedProductList[i]).ToArray()).ToArray();//groups of products by 3
            rptProductListRows.Visible = true;
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

        private void BindFilters()
        {
            var attributes = _category.ProductList.SelectMany(p => p.Attributes).Distinct(AttributeComparer.Instance).ToArray();
            rptAttributes.DataSource = attributes;
            rptAttributes.DataBind();
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

        protected void ChildCategoryRowDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rptChildCategoriesItems = e.Item.FindControl("rptChildCategoriesItems") as Repeater;
            var categoryRowDataSource = e.Item.DataItem as Category[];
            if (rptChildCategoriesItems != null && categoryRowDataSource != null)
            {
                rptChildCategoriesItems.DataSource = categoryRowDataSource;
                rptChildCategoriesItems.DataBind();
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

        protected void Filter(object sender, EventArgs e)
        {
            var selectedAttrValIds = rptAttributes.Items.OfType<RepeaterItem>()
                    .Where(i => i.ItemType == ListItemType.Item || i.ItemType == ListItemType.AlternatingItem)
                    .Select(i => i.FindControl("ddlAttributeValues") as DropDownList)
                    .Select(ddl => int.Parse(ddl.SelectedValue))
                    .Where(i => i != -1).ToArray();

            var attrsString = String.Join("|", selectedAttrValIds);
            Response.Redirect(String.Format("~/Category.aspx?id={0}&brand={1}&attrs={2}", ItemId, ddlBrands.SelectedValue, attrsString));
        }

        protected void BindAttrValuesToRepeaterItem(object sender, RepeaterItemEventArgs e)
        {
            var attribute = e.Item.DataItem as AstCore.Models.Attribute;
            var ddlAttrValues = e.Item.FindControl("ddlAttributeValues") as DropDownList;
            if (attribute != null && ddlAttrValues != null)
            {
                var attrDefaultValue = new AttributeValue
                {
                    AttributeValueId = -1,
                    Value = String.Format("Выберите {0}", attribute.Name)
                };

                var attrValListToView = new List<AttributeValue>();
                attrValListToView.Add(attrDefaultValue);

                var attrValsForCategory =
                    _category
                        .ProductList
                        .SelectMany(p => p.SkuCollection)
                        .Select(s =>
                            s.AttributeValues.SingleOrDefault(v => v.AttributeId == attribute.AttributeId))
                        .Where(v => v != null)
                            .Distinct(AttributeValueComparer.Instance).ToArray();
                attrValListToView.AddRange(attrValsForCategory);
                ddlAttrValues.DataSource = attrValListToView;
                ddlAttrValues.DataTextField = "Value";
                ddlAttrValues.DataValueField = "AttributeValueId";
                ddlAttrValues.DataBind();

                var selectedVals = SelectedAttributeValueIds;
                int selectedVal =
                    attrValListToView.Select(v => v.AttributeValueId).Intersect(selectedVals).SingleOrDefault();
                if (selectedVal != 0)
                {
                    ddlAttrValues.SelectedValue = selectedVal.ToString(CultureInfo.InvariantCulture);
                }
            }
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