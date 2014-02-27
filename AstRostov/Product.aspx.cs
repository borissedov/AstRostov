using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;
using AstCore.SearchEngine;
using AstECommerce;
using Microsoft.Ajax.Utilities;

namespace AstRostov
{
    public partial class ProductPage : System.Web.UI.Page
    {
        protected Product Product
        {
            get;
            set;
        }

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

        private int SelectedSkuId
        {
            get
            {
                int id;
                if (hdnSkuId.Value != null && int.TryParse(hdnSkuId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set
            {
                hdnSkuId.Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        private Sku _selectedSkuInstance;

        protected Sku SelectedSku
        {
            get
            {
                if (_selectedSkuInstance == null)
                {
                    //if (SelectedSkuId == 0)
                    //{
                    //    SelectedSkuId = Product.DefaultSku.SkuId;
                    //}
                    if (SelectedSkuId != 0)
                    {
                        _selectedSkuInstance = Product.SkuCollection.SingleOrDefault(s => s.SkuId == SelectedSkuId);
                    }
                }
                return _selectedSkuInstance;
            }
            private set
            {
                _selectedSkuInstance = value;
                SelectedSkuId = value == null ? 0 : value.SkuId;
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
            Product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ItemId);
            if (Product == null)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                BindAttributes();
                BindAttributeSku(sender, e);
                BindImages();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (AstMembership.CurrentUser != null && AstMembership.CurrentUser.IsAdmin)
            {
                hlEdit.Visible = true;
                hlEdit.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?id={0}", ItemId));
            }
        }

        //private void BindAvailability()
        //{
        //    if (Product.TotalInventory > 0)
        //    {
        //        pnlAddToCart.Visible = true;
        //        btnReserveProduct.Visible = false;
        //    }
        //    else
        //    {
        //        pnlAddToCart.Visible = false;
        //        btnReserveProduct.Visible = true;
        //    }
        //}

        private void BindAttributes()
        {
            if (Product.Attributes.Count > 0)
            {
                rptAttrs.DataSource = Product.Attributes.ToArray();
                rptAttrs.DataBind();
            }
        }

        private void BindImages()
        {
            rptThumbnails.DataSource = Product.Images;
            rptThumbnails.DataBind();
        }

        protected void AddToCart(object sender, EventArgs e)
        {
            int count;
            if (int.TryParse(tbProductAddCount.Text, out count) && count > 0 && ItemId > 0)
            {
                ShoppingCart.AddToCart(SelectedSku, count);
                Response.Redirect("~/ShoppingCart.aspx");
            }
        }

        protected void ReserveProduct(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Preorder.aspx?id={0}&count={1}", SelectedSku.SkuId, tbProductAddCount.Text));
        }

        /// <summary>
        /// Checking inventory of SelectedSku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckInventory(object sender, EventArgs e)
        {
            int count;
            if (int.TryParse(tbProductAddCount.Text, out count))
            {
                var cartItem = ShoppingCart.ShoppingCartItems.SingleOrDefault(i => i.SkuId == SelectedSku.SkuId);
                var countsInCart = cartItem != null ? cartItem.Count : 0;
                if (count > SelectedSku.Inventory + countsInCart)
                {
                    btnAddToCart.Visible = false;
                    btnReserveProduct.Visible = true;
                }
                else
                {
                    btnAddToCart.Visible = true;
                    btnReserveProduct.Visible = false;
                }
            }
            litInventory.Text = SelectedSku.Inventory == 0 ? "нет на складе" : SelectedSku.Inventory > 10 ? "более 10" : "менее 10";
        }

        protected void BindAttrValuesForRptItem(object sender, RepeaterItemEventArgs e)
        {
            var attribute = e.Item.DataItem as AstCore.Models.Attribute;
            var ddlAttrValues = e.Item.FindControl("ddlAttrValues") as DropDownList;
            if (attribute != null && ddlAttrValues != null)
            {
                var attrDefaultValue = new AttributeValue
                    {
                        AttributeValueId = -1,
                        Value = String.Format("Выберите {0}", attribute.Name)
                    };

                var attrValListToView = new List<AttributeValue>();
                attrValListToView.Add(attrDefaultValue);

                var attrValsForProduct =
                    Product.SkuCollection.Select(
                        s => s.AttributeValues.Single(v => v.AttributeId == attribute.AttributeId)).Distinct(AttributeValueComparer.Instance).ToArray();
                attrValListToView.AddRange(attrValsForProduct);
                ddlAttrValues.DataSource = attrValListToView;
                ddlAttrValues.DataTextField = "Value";
                ddlAttrValues.DataValueField = "AttributeValueId";
                ddlAttrValues.DataBind();
                //ddlAttrValues.SelectedIndexChanged += BindAttributeSku;
            }
        }

        private int[] SelectedAttributeValueIds
        {
            get
            {
                return rptAttrs.Items.OfType<RepeaterItem>()
                    .Select(i => i.FindControl("ddlAttrValues") as DropDownList)
                    .Select(ddl => int.Parse(ddl.SelectedValue)).ToArray();
            }
        }

        protected void BindAttributeSku(object sender, EventArgs e)
        {
            var attrValIds = SelectedAttributeValueIds; 
                //new List<int>();
            //foreach (RepeaterItem rptItem in rptAttrs.Items)
            //{
            //    var ddlAttrValues = rptItem.FindControl("ddlAttrValues") as DropDownList;
            //    if (ddlAttrValues != null)
            //    {
            //        var attrValIdStr = ddlAttrValues.SelectedValue;
            //        int attrValId;
            //        if (!String.IsNullOrEmpty(attrValIdStr) && int.TryParse(attrValIdStr, out attrValId))
            //        {
            //            attrValIds.Add(attrValId);
            //        }
            //    }
            //}
            if (attrValIds.Any())
            {
                if (attrValIds.All(id => id != -1))
                {
                    var sku =
                        Product.SkuCollection.SingleOrDefault(
                            s => s.AttributeValues.All(v => attrValIds.Contains(v.AttributeValueId)));
                    if (sku != null)
                    {
                        SelectedSku = sku;
                        phProductActions.Visible = true;
                        CheckInventory(null, null);
                    }
                    else
                    {
                        SelectedSku = null;
                        phProductActions.Visible = false;
                        litInventory.Text = "Данной конфтгурации не существует";
                    }
                }
                else
                {
                    phProductActions.Visible = false;
                    litInventory.Text = "Заполните значения атрибутов";
                }
            }
            else
            {
                SelectedSku = Product.DefaultSku;
                phProductActions.Visible = true;
                CheckInventory(null, null);
            }
        }
    }
}