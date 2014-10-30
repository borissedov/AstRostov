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
    public partial class EditBlog : System.Web.UI.Page
    {
        protected Blog Blog;

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
            set { hdnItemId.Value = value.ToString(); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseItemId();
            }
        }

        private void ParseItemId()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Blog = CoreData.Context.Blogs.Include("Author").SingleOrDefault(o => o.BlogId == ItemId);

            if (Blog == null)
            {
                Response.Redirect("~/Admin/BlogList.aspx");
                return;
            }

            if (!Page.IsPostBack)
            {
                BindBlogForm();
                BindPosts();
            }
        }

        
        private void BindBlogForm()
        {
            tbAuthor.Text = Blog.Author.UserName;
            tbTitle.Text = Blog.Title;
            tbBrand.Text = Blog.Brand;
            tbModel.Text = Blog.Model;
            tbManufacturedYear.Text = Blog.ManufacturedYear;
            tbBuyYear.Text = Blog.BuyYear;
            tbColor.Text = Blog.Color;
            tbModelComment.Text = Blog.ModelComment;
            rblDriving.SelectedValue = Blog.Driving ? "1" : "0";
            rblSell.SelectedValue = Blog.Sell ? "1" : "0";
            tbPower.Text = Blog.Power;
            tbEngineVolume.Text = Blog.EngineVolume;
            tbGosNumber.Text = Blog.GosNumber;
            tbVin.Text = Blog.Vin;
        }

       

        protected void SaveBlog(object sender, EventArgs e)
        {
            Blog.Title = tbTitle.Text;
            Blog.Brand = tbBrand.Text;
            Blog.Model = tbModel.Text;
            Blog.ManufacturedYear = tbManufacturedYear.Text;
            Blog.BuyYear = tbBuyYear.Text;
            Blog.Color = tbColor.Text;
            Blog.ModelComment = tbModelComment.Text;
            Blog.Driving = rblDriving.SelectedValue == "1";
            Blog.Sell = rblSell.SelectedValue == "1";
            Blog.Power = tbPower.Text;
            Blog.EngineVolume = tbEngineVolume.Text;
            Blog.GosNumber = tbGosNumber.Text;
            Blog.Vin = tbVin.Text;

            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/BlogList.aspx");
        }

        private void BindPosts()
        {
            gridPosts.DataSource = Blog.Posts.ToArray();
            gridPosts.DataBind();
        }
        
        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditPost.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeletePost(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void DeletePost(int postId)
        {
            var newsItemToDelete = CoreData.Context.Posts.SingleOrDefault(n => n.PostId == postId);
            CoreData.Context.Posts.Remove(newsItemToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditBlog.aspx?id={0}", ItemId));
        }
    }
}