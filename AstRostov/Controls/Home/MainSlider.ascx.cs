using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Controls.Home
{
    public partial class MainSlider : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindMainSlider();
            }
        }

        private void BindMainSlider()
        {
            rptMailSliderItems.DataSource = CoreData.Context.MainSliderItems.ToArray();
            rptMailSliderItems.DataBind();
        }
    }
}