using System;
using System.Globalization;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class EditBlog : System.Web.UI.Page
    {
        private int ItemId
        {
            get
            {
                int id;
                if (hdnItemId.Value != null && int.TryParse(hdnItemId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set
            {
                hdnItemId.Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseRequest();
            }
        }

        private void ParseRequest()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBlogForm();
            }
        }

        private void BindBlogForm()
        {
            if (ItemId == 0)
            {
                litTitle.Text = "Создание нового бортжурнала";
                htmlartIntoduceCreation.Visible = true;
            }
            else
            {
                Blog blog = CoreData.Context.Blogs.SingleOrDefault(p => p.BlogId == ItemId);

                if (blog == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                if (blog.Author != AstMembership.CurrentUser)
                {
                    ErrorLabel.Text = "Вы не можете редактировать чужое сообщение.";
                    return;
                }

                tbTitle.Text = blog.Title;
                tbBrand.Text = blog.Brand;
                tbModel.Text = blog.Model;
                tbManufacturedYear.Text = blog.ManufacturedYear;
                tbBuyYear.Text = blog.BuyYear;
                tbColor.Text = blog.Color;
                tbModelComment.Text = blog.ModelComment;
                rblDriving.SelectedValue = blog.Driving ? "1" : "0";
                rblSell.SelectedValue = blog.Sell ? "1" : "0";
                tbPower.Text = blog.Power;
                tbEngineVolume.Text = blog.EngineVolume;
                tbGosNumber.Text = blog.GosNumber;
                tbVin.Text = blog.Vin;

                litTitle.Text = String.Format(@"Редактирование бортжурнала #{0}", ItemId);
                htmlartIntoduceCreation.Visible = false;
            }

            

        }

        protected void SaveBlog(object sender, EventArgs e)
        {
            Blog blog = ItemId == 0 ? new Blog { Author = AstMembership.CurrentUser } : CoreData.Context.Blogs.SingleOrDefault(i => i.BlogId == ItemId);
            if (blog == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }
            if (blog.Author != AstMembership.CurrentUser && AstMembership.CurrentUser.IsAdmin)
            {
                ErrorLabel.Text = "Вы не можете редактировать чужой бортжурнал.";
                return;
            }

            blog.Title = tbTitle.Text;
            blog.Brand = tbBrand.Text;
            blog.Model = tbModel.Text;
            blog.ManufacturedYear = tbManufacturedYear.Text;
            blog.BuyYear = tbBuyYear.Text;
            blog.Color = tbColor.Text;
            blog.ModelComment = tbModelComment.Text;
            blog.Driving = rblDriving.SelectedValue == "1";
            blog.Sell = rblSell.SelectedValue == "1";
            blog.Power = tbPower.Text;
            blog.EngineVolume = tbEngineVolume.Text;
            blog.GosNumber = tbGosNumber.Text;
            blog.Vin = tbVin.Text;

            CoreData.Context.Blogs.Add(blog);

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/MyBlog.aspx"));
        }
    }
}