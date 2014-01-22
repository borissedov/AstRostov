using System;
using System.Linq;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditNewsComment : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNewsCommentForm();
            }
        }

        private void BindNewsCommentForm()
        {
            if (ItemId == 0)
            {
                Response.Redirect("~/Admin/NewsCommentList.aspx");
            }
            else
            {
                NewsComment newsComment = CoreData.Context.NewsComments.SingleOrDefault(i => i.NewsCommentId == ItemId);

                if (newsComment == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                tbContent.Text = newsComment.Content;


                litEditTitle.Text = String.Format(@"Редактирование комментария #{0}", ItemId);
            }
        }


        protected void SaveNewsComment(object sender, EventArgs e)
        {

            NewsComment newsComment = ItemId == 0 ? new NewsComment { Created = DateTime.Now } : CoreData.Context.NewsComments.SingleOrDefault(i => i.NewsCommentId == ItemId);
            if (newsComment == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            newsComment.Content = tbContent.Text;
            newsComment.Updated = DateTime.Now;

            if (ItemId == 0)
            {
                CoreData.Context.NewsComments.Add(newsComment);
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditNewsComment.aspx?id={0}", newsComment.NewsCommentId));
        }


        protected void BackToNewsList(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Admin/NewsCommentList.aspx?newsid={0}", CoreData.Context.NewsComments.Single(i => i.NewsCommentId == ItemId).NewsItem.NewsItemId));
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }
    }
}