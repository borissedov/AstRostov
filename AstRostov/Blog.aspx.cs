using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class BlogPage : System.Web.UI.Page
    {
        private const int ItemsPerPage = 10;

        protected int ItemId
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
                ParseBlogId();
            }
        }

        private void ParseBlogId()
        {
            int id;
            if (int.TryParse(Request.Params["id"], out id))
            {
                ItemId = id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindBlog();
                BindPaging();
            }
        }

        private void BindBlog()
        {
            var blog = CoreData.Context.Blogs.SingleOrDefault(b => b.BlogId == ItemId);
            if (blog != null)
            {
                litBlogTitle.Text = blog.Title;
                BindPosts(blog);
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
            var pagePosts = blog.Posts.Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage);
            rptPosts.DataSource = pagePosts.ToArray(); 
            rptPosts.DataBind();
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