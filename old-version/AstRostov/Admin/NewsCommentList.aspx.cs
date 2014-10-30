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
    public partial class NewsCommentList : System.Web.UI.Page
    {
        private int NewsItemId
        {
            get
            {
                int newsItemId = 0;
                return int.TryParse(hdnNewsItemId.Value, out newsItemId) ? newsItemId : 0;
            }
        }
        private NewsItem _ownerNewsItem;
        protected NewsItem OwnerNewsItem
        {
            get
            {
                if (_ownerNewsItem == null)
                {
                    _ownerNewsItem = CoreData.Context.News.SingleOrDefault(n => n.NewsItemId == NewsItemId);
                }
                return _ownerNewsItem;
            }
        }

        private string AuthorUserName
        {
            get
            {
                return hdnAuthor.Value;
            }
        }
        private User _author;
        protected User Author
        {
            get
            {
                if (_author == null)
                {
                    _author = CoreData.Context.Users.SingleOrDefault(u => u.UserName == AuthorUserName);
                }
                return _author;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            hdnNewsItemId.Value = Request.QueryString["newsid"];
            hdnAuthor.Value = Request.QueryString["author"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Author == null)
                {
                    subheaderOfNewsItem.Visible = false;
                }
                if (OwnerNewsItem == null)
                {
                    subheaderFromUser.Visible = false;
                }

                BindComments();
            }
        }

        private void BindComments()
        {
            IEnumerable<NewsComment> src;
            if (Author != null)
            {
                src = Author.NewsComments;
            }
            else if (OwnerNewsItem != null)
            {
                src = OwnerNewsItem.Comments;
            }
            else
            {
                src = CoreData.Context.NewsComments;
            }
            gridComments.DataSource = src.ToArray();
            gridComments.DataBind();
        }

        protected void DeleteNewsComment(int newsCommentId)
        {
            var newsCommentToDelete = CoreData.Context.NewsComments.SingleOrDefault(n => n.NewsCommentId == newsCommentId);
            CoreData.Context.NewsComments.Remove(newsCommentToDelete);
            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/NewsCommentList.aspx?author={0}&newsid={1}", AuthorUserName, NewsItemId));
        }

        protected void OnGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditNewsComment.aspx?id={0}", e.CommandArgument));
                    break;
                case "Delete":
                    DeleteNewsComment(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }
    }
}