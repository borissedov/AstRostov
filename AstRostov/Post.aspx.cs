using System;
using System.Globalization;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class PostPage : System.Web.UI.Page
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

        private Post _post;
        protected Post CurrentPost
        {
            get
            {
                if (_post == null)
                {
                    BindPostItem();
                }
                return _post;
            }
        }

        private void BindPostItem()
        {
            if (ItemId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            _post = CoreData.Context.Posts.SingleOrDefault(n => n.PostId == ItemId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CurrentPost == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                if (AstMembership.CurrentUser != null)
                {
                    pnlAddComment.Visible = true;
                    pleaseLogin.Visible = false;
                    rptComments.DataSource = CurrentPost.Comments;
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
                CurrentPost.Comments.Add(new PostComment
                {
                    Author = AstMembership.CurrentUser,
                    Content = tbCommentBody.Text.Trim(),
                    Created = DateTime.Now,
                    Post = CurrentPost
                });
                CoreData.Context.SaveChanges();

                tbCommentBody.Text = String.Empty;

                rptComments.DataSource = CurrentPost.Comments;
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