using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;
using AstECommerce;

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
                BindImages();
                CheckInventory(null, null);
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

        private void BindAvailability()
        {
            if (Product.TotalInventory > 0)
            {
                pnlAddToCart.Visible = true;
                btnReserveProduct.Visible = false;
            }
            else
            {
                pnlAddToCart.Visible = false;
                btnReserveProduct.Visible = true;
            }
        }

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
                ShoppingCart.AddToCart(Product.DefaultSku, count);//TODO
                Response.Redirect("~/ShoppingCart.aspx");
            }
        }

        protected void ReserveProduct(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Preorder.aspx?id={0}&count={1}", Product.DefaultSku.SkuId, tbProductAddCount.Text));
        }

        protected void CheckInventory(object sender, EventArgs e)
        {
            int count;
            if (int.TryParse(tbProductAddCount.Text, out count))
            {
                var cartItem = ShoppingCart.ShoppingCartItems.SingleOrDefault(i => i.SkuId == Product.DefaultSku.SkuId);//TODO
                var countsInCart = cartItem != null ? cartItem.Count : 0;
                if (count > Product.DefaultSku.Inventory + countsInCart)//TODO
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
        }

        protected void BindAttrValuesForRptItem(object sender, RepeaterItemEventArgs e)
        {
            var attribute = e.Item.DataItem as AstCore.Models.Attribute;
            var ddlAttrValues = e.Item.FindControl("ddlAttrValues") as DropDownList;
            if (attribute != null && ddlAttrValues != null)
            {
                var attrDefaultValue = new AttributeValue
                    {
                        AttributeId = -1,
                        Value = String.Format("Выберите {0}", attribute.Name)
                    };

                var attrValListToView = new List<AttributeValue>();
                attrValListToView.Add(attrDefaultValue);
                attrValListToView.AddRange(attribute.AttributeValues.ToArray());
                ddlAttrValues.DataSource = attrValListToView;
                ddlAttrValues.DataTextField = "Value";
                ddlAttrValues.DataValueField = "AttributeId";
                ddlAttrValues.DataBind();
            }
        }
    }
}