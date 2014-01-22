using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.Models;

namespace AstRostov
{
    public partial class MyBlog : System.Web.UI.Page
    {
        private const int ItemsPerPage = 10;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AstMembership.CurrentUser == null)
            {
                Response.Redirect("~/Account/Login.aspx");
                return;
            }

            if (AstMembership.CurrentUser.Blog == null)
            {
                Response.Redirect("~/EditBlog.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindBlog();
                BindPaging();
            }
        }

        private void BindBlog()
        {
            Blog blog = AstMembership.CurrentUser.Blog;
            if (blog != null)
            {
                litBlogTitle.Text = blog.Title;
                BindPosts(blog);
                BindAddPostForm(blog);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void BindPosts(Blog blog)
        {
            int count = blog.Posts.Count();
            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }
            var pagePosts = blog.Posts.Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage).ToArray();
            if (pagePosts.Length > 0)
            {
                rptPosts.DataSource = pagePosts;
                rptPosts.DataBind();
            }
            else
            {
                lblPostsEmpty.Visible = true;
            }
        }

        private void BindPaging()
        {
            var list = new List<int>();
            for (int i = 1; i <= PageCount; i++)
            {
                list.Add(i);
            }

            rptPaging.DataSource = list.Select(i => new
            {
                PageNo = i
            });
            rptPaging.DataBind();
        }

        protected int CurrentPageNo
        {
            get
            {
                int pageNo;
                if (int.TryParse(Request.Params["page"], out pageNo))
                {
                    return pageNo;
                }
                return 1;
            }
        }

        protected int PageCount
        {
            get
            {
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }

        private void BindAddPostForm(Blog blog)
        {
            btnAddPost.Visible = blog.Author == AstMembership.CurrentUser;
        }

        protected void AddPost(object sender, EventArgs e)
        {
            Response.Redirect("~/EditPost.aspx");
        }

        protected void rptPosts_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var hlinkEdit = e.Item.FindControl("hlinkEdit") as HyperLink;
            var dataItem = e.Item.DataItem as Post;
            if (hlinkEdit != null && dataItem != null)
            {
                if (dataItem.Author != AstMembership.CurrentUser || AstMembership.CurrentUser.IsAdmin)
                {
                    hlinkEdit.Visible = false;
                }
                else
                {
                    hlinkEdit.NavigateUrl = ResolveUrl(String.Format("~/EditPost.aspx?id={0}", dataItem.PostId));
                }
            }
            
        }

    }
}