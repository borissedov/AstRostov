using System;
using System.Globalization;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class Check : System.Web.UI.Page
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
            Order = CoreData.Context.Orders.SingleOrDefault(o => o.OrderId == ItemId && o.Account.UserId == AstMembership.CurrentUser.UserId);

            if (Order == null)
            {
                Response.Redirect("~/Account/OrderList.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                BindOrderForm();
            }
        }

        private void BindOrderForm()
        {
            OrderName1.Text = String.Format("Оплата заказа №{0} от {1:dd.MM.yyyy}", Order.OrderId, Order.CreateDate);
            OrderName2.Text = String.Format("Оплата заказа №{0} от {1:dd.MM.yyyy}", Order.OrderId, Order.CreateDate);

            var rubs = (int)Order.Total;
            var kops = (int)((Order.Total - rubs) * 100);

            var rubsStr = String.Format("{0:#0}", rubs);
            var rubsKops = String.Format("{0:00}", kops);

            SumRub1.Text = rubsStr;
            SumRub2.Text = rubsStr;
            SumKop1.Text = rubsKops;
            SumKop2.Text = rubsKops;
        }
    }
}