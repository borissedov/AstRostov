using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AstRostov.Controls.Navigation
{
    public partial class Search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void InitiateSearch(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbSearch.Text))
            {
                var keywords = tbSearch.Text.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => !String.IsNullOrWhiteSpace(s));
                Response.Redirect(String.Format("~/Search.aspx?keywords={0}", Server.UrlEncode(String.Join("|", keywords))));
            }
        }
    }
}