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
            var lblPrice = e.Item.FindControl("lblPrice") as Label;
            var lblTitle = e.Item.FindControl("lblTitle") as Label;
            var item = e.Item.DataItem as MainSliderItem;
            if (lblPrice != null && item != null && lblTitle != null)
            {
                if (item.Price != 0M)
                {
                    lblPrice.Text = item.Price.ToString("C");
                }
                else
                {
                    lblPrice.Visible = false;
                }

                if (String.IsNullOrEmpty(item.Title))
                {
                    lblTitle.Visible = false;
                }
                else
                {
                    lblTitle.Text = item.Title;
                }
            }
        }
    }
}