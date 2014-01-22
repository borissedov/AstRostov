using System;
using System.Globalization;
//using System.IO;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditNewsItem : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                BindNewsItemForm();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }
        

        #region News
        private void BindNewsItemForm()
        {
            NewsItem newsItem;
            if (ItemId == 0)
            {
                newsItem = new NewsItem();

                btnGoToComments.Visible = false;
            }
            else
            {

                {
                    newsItem = CoreData.Context.News.SingleOrDefault(i => i.NewsItemId == ItemId);
                }
            }
            if (newsItem == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbNewsItemTitle.Text = newsItem.Title;
            tbNewsItemContent.Value = newsItem.Content;

            litEditTitle.Text = ItemId == 0
                                        ? "Создание новой новости"
                                        : String.Format("Редактирование новости #{0}", ItemId);

            pnlNewsItem.Visible = true;
        }

        protected void SaveNewsItem(object sender, EventArgs e)
        {

            NewsItem newsItem = ItemId == 0 ? new NewsItem() : CoreData.Context.News.SingleOrDefault(i => i.NewsItemId == ItemId);
            if (newsItem == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            newsItem.Title = tbNewsItemTitle.Text;
            newsItem.Content = tbNewsItemContent.Value;

            if (ItemId == 0)
            {
                newsItem.Created = DateTime.Now;
                newsItem = CoreData.Context.News.Add(newsItem);
            }

            newsItem.Author = AstMembership.CurrentUser;
            newsItem.Updated = DateTime.Now;

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditNewsItem.aspx?id={0}", newsItem.NewsItemId));

        }

        protected void GoToCommentsList(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Admin/NewsCommentList.aspx?newsid={0}", ItemId));
        }

        #endregion


    }
}