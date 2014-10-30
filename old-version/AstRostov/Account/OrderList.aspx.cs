using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;
using Robokassa;

namespace AstRostov.Account
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPreorders();
            }
        }

        private void BindPreorders()
        {
            gridOrders.DataSource = CoreData.Context.Orders.Where(o => o.Account.UserId == AstMembership.CurrentUser.UserId).ToArray();
            gridOrders.DataBind();
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var hlEditOrder = e.Row.FindControl("hlEditOrder") as HyperLink;
            var btnPay = e.Row.FindControl("btnPay") as Button;
            var order = e.Row.DataItem as Order;
            if (order != null && hlEditOrder != null && btnPay != null)
            {
                if (order.OrderState == OrderState.Pending)
                {
                    if (order.PaymentMethod == PaymentMethod.BankTransfer)
                    {
                        hlEditOrder.Text = "Уведомить об оплате";
                    }
                    else if (order.PaymentMethod == PaymentMethod.Robokassa)
                    {
                        //btnPay.NavigateUrl = RobokassaCore.FormatPostPaymentUrl(order);
                        btnPay.Visible = true;
                    }
                }
            }

        }

        protected void RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Pay":
                    int orderId;
                    if (int.TryParse(e.CommandArgument as string, out orderId))
                    {
                        var order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                        if (order != null)
                        {
                            Response.Redirect(RobokassaCore.FormatPostPaymentUrl(order));
                        }
                    }
                    break;
            }
        }
    }
}