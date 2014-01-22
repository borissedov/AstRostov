using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
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
            gridOrders.DataSource = CoreData.Context.Orders.ToArray();
            gridOrders.DataBind();
        }
    }
}