using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;

namespace AstRostov.Admin
{
    public partial class BlogList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBlogList();
            }
        }

        private void BindBlogList()
        {
            gridBlogs.DataSource = CoreData.Context.Blogs.Include("Author").ToArray();
            gridBlogs.DataBind();
        }

        private void DeleteBlog(int blogId)
        {
            var blogToDelete = CoreData.Context.Blogs.SingleOrDefault(n => n.BlogId == blogId);

            foreach (var post in blogToDelete.Posts.ToArray())
            {
                foreach (var comment in post.Comments.ToArray())
                {
                    CoreData.Context.PostComments.Remove(comment);
                }
                CoreData.Context.Posts.Remove(post);
            }
            CoreData.Context.Blogs.Remove(blogToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/BlogList.aspx");
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditBlog.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteBlog(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}