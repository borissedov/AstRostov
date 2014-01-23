using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Helpers;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditOrder : System.Web.UI.Page
    {
        protected Order Order;

        private int ItemId
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
            set { hdnItemId.Value = value.ToString(); }
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
            Order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == ItemId);

            if (Order == null)
            {
                Response.Redirect("~/Admin/OrderList.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                BindOrderForm();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnMarkAsPaid.Visible = false;
            btnDecline.Visible = false;
            btnMarkAsShipped.Visible = false;

            switch (Order.OrderState)
            {
                case OrderState.Pending:
                case OrderState.PaymentSent:
                    btnMarkAsPaid.Visible = true;
                    btnDecline.Visible = true;
                    break;
                case OrderState.PaymentConfirmed:
                    btnMarkAsShipped.Visible = true;
                    break;
                default: break;
            }
        }

        private void BindOrderForm()
        {
            tbAdminComment.Text = Order.AdminComment;
            tbCustomerComment.Text = Order.CustomerComment;
            tbPaymentMethod.Text = Order.PaymentMethod.GetDescription();
            tbShippingType.Text = Order.ShippingType.GetDescription();

            tbFullName.Text = Order.FullName;
            tbEmail.Text = Order.Email;
            tbPhone.Text = Order.Phone;
            tbRegion.Text = Order.Region;
            tbCity.Text = Order.City;
            tbAddress1.Text = Order.Address1;
            tbAddress2.Text = Order.Address2;
            tbZipCode.Text = Order.ZipCode;
            tbRegion.Text = Order.Region;
            tbDocumentNumber.Text = Order.DocumentNumber;
            tbCountry.Text = Order.Country;
            tbDocumentType.Text = Order.DocumentType;

            tbOrderState.Text = Order.OrderState.GetDescription();

            gridLineItems.DataSource = Order.OrderLineItems;
            gridLineItems.DataBind();
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
                    litSubtotal.Text = Order.ItemsSubtotal.ToString("c");
                    litShippingPrice.Text = Order.ShippingCost.ToString("c");
                    litShippingName.Text = Order.ShippingType.GetDescription();
                    litCommission.Text = Order.CommissionTotal.ToString("c");
                    litTotal.Text = Order.Total.ToString("c");
                }
            }
        }

        protected void SaveOrderComment(object sender, EventArgs e)
        {
            Order.AdminComment = tbAdminComment.Text;
            CoreData.Context.SaveChanges();
        }

        protected void DeclineOrder(object sender, EventArgs e)
        {
            Order.OrderState = OrderState.Declined;

            foreach (var lineItem in Order.OrderLineItems)
            {
                var sku = CoreData.Context.Skus.SingleOrDefault(s => s.SkuId == lineItem.SkuId);
                if (sku != null)
                {
                    sku.Inventory += lineItem.Count;
                }
            }

            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/OrderList.aspx");
        }

        protected void MarkAsPaid(object sender, EventArgs e)
        {
            Order.OrderState = OrderState.PaymentConfirmed;
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/OrderList.aspx");
        }

        protected void MarkAsShipped(object sender, EventArgs e)
        {
            Order.OrderState = OrderState.Shipped;
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/OrderList.aspx");
        }
    }
}