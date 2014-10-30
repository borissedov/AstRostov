using System;
using AstCore.Helpers;

namespace AstRostov.Admin
{
    public partial class MailTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SendMail(object sender, EventArgs e)
        {
            AstMail.SendEmail(tbTo.Text, tbBody.Text);
        }
    }
}