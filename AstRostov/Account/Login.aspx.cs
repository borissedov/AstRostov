using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AstRostov.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LoginError(object sender, EventArgs e)
        {
            litError.Text = "<span class='label label-warning'>Неправильный логин или пароль.</span><br>";
        }

        protected void LoggedIn(object sender, EventArgs e)
        {
            var returnUrl = Request.QueryString["ReturnUrl"];
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }
            Response.Redirect(returnUrl);
        }
    }
}