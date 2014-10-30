using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstECommerce;

namespace AstRostov
{
    public partial class ShoppingCartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCartItemsGrid();
                BindTotals();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            pnlActionsAndTotals.Visible = ShoppingCart.ShoppingCartItems.Any();

            if (!ShoppingCart.AvailabilityCheck)
            {
                btnChechout.Visible = false;

                var item = ShoppingCart.ShoppingCartItems.FirstOrDefault(i => i.Count > i.Sku.Inventory);
                if (item != null)
                {
                    litProductNotAvailableName.Text = item.Product.Name;
                    hlPreorder.NavigateUrl =
                        ResolveUrl(String.Format("~/Preorder.aspx?id={0}&count={1}", item.SkuId, item.Count));

                    spanNotAvailable.Visible = true;
                }
            }
            else
            {
                btnChechout.Visible = true;
                spanNotAvailable.Visible = false;
            }
        }

        private void BindTotals()
        {
            ShoppingCart.Calculate();
            litRetailSum.Text = ShoppingCart.TotalWithoutDiscount.ToString("c");
            litDiscountSum.Text = ShoppingCart.Discount.ToString("c");
            litTotal.Text = ShoppingCart.Total.ToString("c");
        }

        private void BindCartItemsGrid()
        {
            gridShoppingCartItems.DataSource = ShoppingCart.ShoppingCartItems;
            gridShoppingCartItems.DataBind();
        }

        protected void ShoppingCartItemRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void RemoveItem(object sender, EventArgs e)
        {
            var linkbutton = sender as LinkButton;
            int skuId;
            if (linkbutton != null && int.TryParse(linkbutton.Attributes["ItemId"], out skuId) && skuId > 0)
            {
                ShoppingCart.RemoveItem(skuId);
                BindCartItemsGrid();
                BindTotals();
            }
        }

        protected void ItemQuantityChange(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
            {
                int skuId;
                int count;
                if (int.TryParse(textbox.Attributes["ItemId"], out skuId) && skuId > 0 && int.TryParse(textbox.Text, out count) && count > 0)
                {
                    ShoppingCart.UpdateQuantity(skuId, count);
                    BindCartItemsGrid();
                    BindTotals();
                }
            }

        }

        protected void ClearCart(object sender, EventArgs e)
        {
            ShoppingCart.ClearCart();
            BindCartItemsGrid();
            BindTotals();
        }

        protected void Checkout(object sender, EventArgs e)
        {
            AstECommerce.Checkout.LoadOrderFromCart();

            Response.Redirect("~/Address.aspx");
        }
    }
}