using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindStatistic();
            }
        }

        private void BindStatistic()
        {
            DateTime todayDate = DateTime.Now.Date;
            DateTime weekStartDate = todayDate.Date.AddDays(-(int)todayDate.DayOfWeek);

            var orders = CoreData.Context.Orders.Where(o=>o.OrderState == OrderState.Shipped).ToArray();
            litOrderCountToday.Text =
                orders.Count(o => o.CreateDate.Date == todayDate).ToString(CultureInfo.InvariantCulture);

            litOrderCountWeek.Text =
                orders.Count(o => o.CreateDate.Date >= weekStartDate).ToString(CultureInfo.InvariantCulture);

            litOrderCountTotal.Text =
                orders.Count().ToString(CultureInfo.InvariantCulture);

            litOrderSumToday.Text =
                orders.Where(o => o.CreateDate.Date == todayDate).Sum(o => o.Total).ToString("c");

            litOrderSumWeek.Text =
                orders.Where(o => o.CreateDate.Date >= weekStartDate).Sum(o => o.Total).ToString("c");

            litOrderSumTotal.Text =
                orders.Sum(o => o.Total).ToString("c");


            var membership = CoreData.Context.Memberships.ToArray();
            litUsersRegisteredToday.Text =
                membership.Count(u => u.CreateDate.Date == todayDate).ToString(CultureInfo.InvariantCulture);

            litUsersRegisteredWeek.Text =
                membership.Count(u => u.CreateDate.Date >= weekStartDate).ToString(CultureInfo.InvariantCulture);

            litUsersRegisteredTotal.Text =
                membership.Count().ToString(CultureInfo.InvariantCulture);

            tbUsersOnline.Text =
                System.Web.Security.Membership.GetNumberOfUsersOnline().ToString(CultureInfo.InvariantCulture);

            var posts = CoreData.Context.Posts.ToArray();
            litPostCountToday.Text =
                posts.Count(p => p.Created.Date == todayDate).ToString(CultureInfo.InvariantCulture);

            litPostCountWeek.Text =
                posts.Count(p => p.Created.Date >= weekStartDate).ToString(CultureInfo.InvariantCulture);

            litPostCountTotal.Text =
                posts.Count().ToString(CultureInfo.InvariantCulture);

            litSkuCount.Text = CoreData.Context.Skus.Sum(s => s.Inventory).ToString(CultureInfo.InvariantCulture);
        }
    }
}