using System;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class NewsItemPage : System.Web.UI.Page
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
                hdnItemId.Value = value.ToString();
            }
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

        private NewsItem _newsItem;
        protected NewsItem CurrentNewsItem
        {
            get
            {
                if (_newsItem == null)
                {
                    BindNewsItem();
                }
                return _newsItem;
            }
        }

        private void BindNewsItem()
        {
            if (ItemId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            _newsItem = CoreData.Context.News.SingleOrDefault(n => n.NewsItemId == ItemId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CurrentNewsItem == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (AstMembership.CurrentUser != null)
                {
                    pnlAddComment.Visible = true;
                    pleaseLogin.Visible = false;
                    rptComments.DataSource = CurrentNewsItem.Comments;
                    rptComments.DataBind();
                }
                else
                {
                    pnlAddComment.Visible = false;
                    pleaseLogin.Visible = true;
                }
            }
        }

        protected void AddComment(object sender, EventArgs e)
        {
            if (ValidateComment())
            {
                CurrentNewsItem.Comments.Add(new NewsComment
                    {
                        Author = AstMembership.CurrentUser,
                        Content = tbCommentBody.Text.Trim(),
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        NewsItem = CurrentNewsItem
                    });
                CoreData.Context.SaveChanges();

                tbCommentBody.Text = String.Empty;

                rptComments.DataSource = CurrentNewsItem.Comments;
                rptComments.DataBind();

                lblError.Visible = false;
            }
            else
            {
                lblError.Visible = true;
            }
        }

        private bool ValidateComment()
        {
            return !String.IsNullOrEmpty(tbCommentBody.Text) && tbCommentBody.Text.Trim().Length < 1000;
        }
    }
}