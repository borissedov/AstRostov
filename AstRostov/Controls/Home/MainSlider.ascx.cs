using System;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

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

        protected void MainSliderItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var label = e.Item.FindControl("lblPrice") as Label;
            var item = e.Item.DataItem as MainSliderItem;
            if (label != null && item != null)
            {
                if (item.Price != 0M)
                {
                    label.Text = item.Price.ToString("C");
                }
                else
                {
                    label.Visible = false;
                }
            }
        }
    }
}