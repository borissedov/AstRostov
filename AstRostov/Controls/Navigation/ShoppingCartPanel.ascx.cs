using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstECommerce;

namespace AstRostov.Controls.Navigation
{
    public partial class ShoppingCartPanel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShoppingCart.Calculate();
            lblAmount.Text = ShoppingCart.Total.ToString("c");

            int itemCount = ShoppingCart.ShoppingCartItems.Count();
            string itemCountLit;
            var num = itemCount % 100;
            if (num < 20 && num > 10)
            {
                itemCountLit = String.Format("{0} покупок", itemCount);
            }
            else
            {
                num %= 10;
                switch (num)
                {
                    case (1):
                        itemCountLit = String.Format("{0} покупка", itemCount);
                        break;
                    case (2):
                    case (3):
                    case (4):
                        itemCountLit = String.Format("{0} покупки", itemCount);
                        break;
                    default:
                        itemCountLit = String.Format("{0} покупок", itemCount);
                        break;
                }
            }
            litCartItemsCount.Text = itemCountLit;
        }
    }
}