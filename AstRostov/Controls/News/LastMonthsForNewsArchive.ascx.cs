using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Controls.News
{
    public partial class LastMonthsForNewsArchive : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindLastMonthsForNewsArchive();
            }
        }

        private void BindLastMonthsForNewsArchive()
        {
            rptLastMonthsForNewsArchive.DataSource = CoreData.Context.News.AsEnumerable().OrderByDescending(n => n.Created)
                .Select(n => new
                {
                    Month = n.Created.Month,
                    Year = n.Created.Year,
                    DateTitle = n.Created.ToString("MMMM yyyy")
                }).Distinct().Take(10).ToArray();
            rptLastMonthsForNewsArchive.DataBind();
        }
    }
}