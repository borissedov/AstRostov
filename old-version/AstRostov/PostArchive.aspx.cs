using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class PostArchive : System.Web.UI.Page
    {
        private const int ItemsPerPage = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPosts();
                BindPaging();
            }
        }

        private void BindPosts()
        {
            int count = CoreData.Context.Posts.Count();
            PageCount = count / ItemsPerPage + (count % ItemsPerPage == 0 ? 0 : 1);
            if (PageCount == 0)
            {
                PageCount++;
            }
            var pagePosts = CoreData.Context.Posts.OrderByDescending(n => n.Created).Skip((CurrentPageNo - 1) * ItemsPerPage).Take(ItemsPerPage);
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
            var lbDelete = e.Item.FindControl("lbDelete") as LinkButton;
            var dataItem = e.Item.DataItem as Post;
            if (hlinkEdit != null && dataItem != null && lbDelete!=null)
            {
                if (dataItem.Author != AstMembership.CurrentUser || AstMembership.CurrentUser.IsAdmin)
                {
                    hlinkEdit.Visible = false;
                    lbDelete.Visible = false;
                }
                else
                {
                    hlinkEdit.NavigateUrl = ResolveUrl(String.Format("~/EditPost.aspx?id={0}", dataItem.PostId));
                }
            }
        }

        protected void ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int postId;
                if (int.TryParse(e.CommandArgument.ToString(), out postId))
                {
                    var post = CoreData.Context.Posts.SingleOrDefault(p => p.PostId == postId);
                    if (post != null)
                    {
                        foreach (var comment in post.Comments.ToArray())
                        {
                            CoreData.Context.PostComments.Remove(comment);
                        }

                        CoreData.Context.Posts.Remove(post);
                        CoreData.Context.SaveChanges();

                        Response.Redirect("~/PostArchive.aspx");
                    }
                }
            }
        }
    }
}