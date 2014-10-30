using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using Robokassa;

namespace AstRostov
{
    public partial class PaymentSuccess : Page
    {
        private Order _order;

        protected void Page_Load(object sender, EventArgs e)
        {
            var merchantOrderId = RobokassaCore.ProcessSuccessResponse(HttpContext.Current);

            int orderId;
            if (int.TryParse(merchantOrderId, out orderId))
            {
                _order =
                    CoreData.Context.Orders.SingleOrDefault(
                        o => o.OrderId == orderId && o.OrderState == OrderState.PaymentConfirmed);
                if (_order == null)
                {
                    lblError.Text = "Заказ не найден или в неправильном состоянии";
                    gridLineItems.Visible = false;
                    return;
                }

                if (!Page.IsPostBack)
                {
                    litPaymrntSuccess.Text = String.Format("Оплата заказа №{0} от {1:dd.MM.yyyy} прошла успешно.", _order.OrderId, _order.CreateDate);

                    gridLineItems.DataSource = _order.OrderLineItems;
                    gridLineItems.DataBind();
                }
            }
            else
            {
                lblError.Text = "Не удалось распознать ответ от Robokassa.";
                gridLineItems.Visible = false;
                return;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
        }

        protected void GridItemDataBount(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var litSubtotal = e.Item.FindControl("litSubtotal") as Literal;
                var litShippingPrice = e.Item.FindControl("litShippingPrice") as Literal;
                var litShippingName = e.Item.FindControl("litShippingName") as Literal;
                var litCommission = e.Item.FindControl("litCommission") as Literal;
                var litTotal = e.Item.FindControl("litTotal") as Literal;

                if (litSubtotal != null && litShippingPrice != null && litShippingName != null && litCommission != null && litTotal != null)
                {
                    litSubtotal.Text = _order.ItemsSubtotal.ToString("c");
                    litShippingPrice.Text = _order.ShippingCost.ToString("c");
                    litShippingName.Text = _order.ShippingType.GetDescription();
                    litCommission.Text = _order.CommissionTotal.ToString("c");
                    litTotal.Text = _order.Total.ToString("c");
                }
            }
        }
    }
}