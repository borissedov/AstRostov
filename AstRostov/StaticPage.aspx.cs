using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class StaticPagePage : Page
    {
        protected StaticPage StaticPage { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindStaticPage();
            }
        }

        private void BindStaticPage()
        {
            string key = Request.Params["key"];
            StaticPage = CoreData.Context.StaticPages.FirstOrDefault(sp => sp.Key == key);
            if (StaticPage == null)
            {
                throw new Exception("Static page not found");
            }
        }
    }
}