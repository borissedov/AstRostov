using System;
using System.Globalization;
using System.Linq;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditSku : System.Web.UI.Page
    {
        private int SkuId
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
            set { hdnSkuId.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseSkuId();
            }
        }

        private void ParseSkuId()
        {
            int id;
            if (int.TryParse(Request.Params["sid"], out id))
            {
                SkuId = id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSkuForm();
            }
        }

        private void BindSkuForm()
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(i => i.SkuId == SkuId);
            
            if (sku == null)
            {
                lblError.Text = "Редактируемая сущность не найдена.";
                return;
            }

            gridAttributes.DataSource = sku.AttributeValues.ToArray();
            gridAttributes.DataBind();

            tbInventory.Text = sku.Inventory.ToString(CultureInfo.InvariantCulture);
            if (sku.RetailPrice.HasValue)
            {
                tbRetailPrice.Text = sku.RetailPrice.ToString();
            }
            if (sku.SalePrice.HasValue)
            {
                tbSalePrice.Text = sku.SalePrice.ToString();
            }

            hlBack.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?id={0}", sku.ProductId));
        }


        protected void SaveSku(object sender, EventArgs e)
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(i => i.SkuId == SkuId);

            if (sku == null)
            {
                lblError.Text = "Редактируемая сущность не найдена.";
                return;
            }

            int inventory;
            if (int.TryParse(tbInventory.Text, out inventory))
            {
                sku.Inventory = inventory;
            }
            else
            {
                lblError.Text = "Не указано количество на складе";
                return;
            }

            var salePriceString = tbSalePrice.Text.Trim();
            if (!String.IsNullOrEmpty(salePriceString))
            {
                decimal salePrice;
                if (decimal.TryParse(salePriceString, out salePrice))
                {
                    sku.SalePrice = salePrice;
                }
                else
                {
                    lblError.Text = "Цена со скидкой указана не верно";
                    return;
                }
            }
            else
            {
                sku.SalePrice = null;
            }

            var retailPriceString = tbRetailPrice.Text.Trim();
            if (!String.IsNullOrEmpty(retailPriceString))
            {
                decimal retailPrice;
                if (decimal.TryParse(retailPriceString, out retailPrice))
                {
                    sku.RetailPrice = retailPrice;
                }
                else
                {
                    lblError.Text = "Цена указана не верно";
                    return;
                }
            }
            else
            {
                sku.RetailPrice = null;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", sku.ProductId));
        }
    }
}