using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;
using AstECommerce;
using NLog;
using Robokassa;

namespace AstRostov
{
    public partial class CheckoutPage : System.Web.UI.Page
    {
        private Order _order
        {
            get
            {
                return Checkout.Order;
            }
        }

        private ShippingType SelectedShippingType
        {
            get
            {
                ShippingType selectedShippingType;
                if (Enum.TryParse(rblShippingMethod.SelectedValue, out selectedShippingType))
                {
                    return selectedShippingType;
                }
                return default(ShippingType);
            }
        }



        private PaymentMethod SelectedPaymentMethod
        {
            get
            {
                PaymentMethod selectedPaymentMethod;
                if (Enum.TryParse(rblPaymentMethod.SelectedValue, out selectedPaymentMethod))
                {
                    return selectedPaymentMethod;
                }

                return default(PaymentMethod);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindLineItems();
                BindMethods();
                BindTotals(null, null);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            switch (_order.PaymentMethod)
            {
                case PaymentMethod.BankTransfer:
                    btnCheckout.Text = "Оформить заказ";
                    break;
                case PaymentMethod.Robokassa:
                    btnCheckout.Text = "Оплатить";
                    break;
            }

            pShippingPickUp.Visible = false;
            pShippingTK.Visible = false;
            switch (rblShippingMethod.SelectedValue)
            {
                case "1":
                    pShippingPickUp.Visible = true;
                    break;
                case "2":
                    pShippingTK.Visible = true;
                    break;
            }
        }

        private void BindLineItems()
        {
            if (!_order.OrderLineItems.Any())
            {
                Response.Redirect("~/Default.aspx");
            }

            gridLineItems.DataSource = _order.OrderLineItems;
            gridLineItems.DataBind();

            litDiscountSum.Text = _order.DiscountTotal.ToString("c");
            litSubtotal.Text = _order.ItemsSubtotal.ToString("c");
        }

        private void BindMethods()
        {
            rblShippingMethod.DataSource =
                CoreData.Context.ShippingTariffs.ToArray().Select(
                    t => new ListItem(t.ShippingType.GetDescription(), ((int)t.ShippingType).ToString(CultureInfo.InvariantCulture)));
            rblShippingMethod.DataTextField = "Text";
            rblShippingMethod.DataValueField = "Value";
            rblShippingMethod.DataBind();
            rblShippingMethod.SelectedValue =
                ((int)CoreData.Context.ShippingTariffs.OrderBy(t => t.ShippingCost).ToArray().Last().ShippingType).ToString(
                    CultureInfo.InvariantCulture);

            rblPaymentMethod.DataSource =
                CoreData.Context.PaymentTariffs.ToArray().Select(
                    t => new ListItem(t.PaymentMethod.GetDescription(), ((int)t.PaymentMethod).ToString(CultureInfo.InvariantCulture)));
            rblPaymentMethod.DataTextField = "Text";
            rblPaymentMethod.DataValueField = "Value";
            rblPaymentMethod.DataBind();
            rblPaymentMethod.SelectedValue =
                ((int)CoreData.Context.PaymentTariffs.OrderBy(t => t.CommissionPercent).ToArray().Last().PaymentMethod).ToString(
                    CultureInfo.InvariantCulture);
        }

        protected void BindTotals(object sender, EventArgs e)
        {
            _order.ShippingType = SelectedShippingType;

            _order.PaymentMethod = SelectedPaymentMethod;

            Checkout.CalculateTotals();

            litShipping.Text = _order.ShippingCost.ToString("c");
            litCommission.Text = String.Format("{0:c} ({1:p})", _order.CommissionTotal, _order.CommissionTotal / (_order.Total - _order.CommissionTotal));
            litTotal.Text = _order.Total.ToString("c");
        }


        protected void ProcessChechout(object sender, EventArgs e)
        {
            int orderId = 0;
            bool checkoutResult = false;

            try
            {
                checkoutResult = Checkout.ProccessCheckout(out orderId);
            }
            catch 
            {
                Response.Redirect("Error.aspx");
            }

            if (checkoutResult)
            {
                switch (SelectedPaymentMethod)
                {
                    case PaymentMethod.BankTransfer:
                        {
                            SendOrderRecieptBankTransfer(orderId);

                            Response.Redirect(String.Format("~/HowToPay.aspx?id={0}", orderId));
                            break;
                        }
                    case PaymentMethod.Robokassa:
                        {
                            var order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == orderId);
                            Response.Redirect(RobokassaCore.FormatPostPaymentUrl(order));
                            break;
                        }
                }
            }
            //Check inventory failed
            Response.Redirect("~/ShoppingCart.aspx");
        }

        private void SendOrderRecieptBankTransfer(int orderId)
        {
            var order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                try
                {
                    var message = String.Format(File.ReadAllText(Server.MapPath("~/MessageTemplates/OrderBankTransfer.txt")),
                       order.Account,
                       order.OrderId);

                    AstMail.SendEmail(order.Email, message, true, String.Format("АСТ-Ростов: Заказ №{0}", order.OrderId));

                    AstMail.SendEmail("sasha2507@aaanet.ru", String.Format("Выставлен новый заказ №{0} от {1:G}", order.OrderId, DateTime.Now), false, String.Format("АСТ-Ростов: Новый заказ №{0}", order.OrderId));
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Fatal(ex);
                    logger.ErrorException("Ошибка при отправке почты", ex);
                }
            }
        }
    }
}