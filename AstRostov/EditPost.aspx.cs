using System;
using System.Globalization;
using System.Linq;
using AstCore;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov
{
    public partial class EditPost : System.Web.UI.Page
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
            if (AstMembership.CurrentUser == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                ParseRequest();
            }
        }

        private void ParseRequest()
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
                BindPostForm();
            }
        }

        private void BindPostForm()
        {
            if (ItemId == 0)
            {
                litEditTitle.Text = "Создание новой записи бортжурнала";
            }
            else
            {
                Post post = CoreData.Context.Posts.SingleOrDefault(p => p.PostId == ItemId);

                if (post == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                if (post.Blog.Author != AstMembership.CurrentUser)
                {
                    ErrorLabel.Text = "Вы не можете редактировать чужое сообщение.";
                    return;
                }

                tbTitle.Text = post.Title;
                tbContent.Text = post.Content;

                litEditTitle.Text = String.Format(@"Редактирование записи #{0}", ItemId);
            }
        }

        protected void SavePost(object sender, EventArgs e)
        {
            Post post = ItemId == 0 ? new Post { Created = DateTime.Now, Author = AstMembership.CurrentUser } : CoreData.Context.Posts.SingleOrDefault(i => i.PostId == ItemId);
            if (post == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }
            if (ItemId != 0)
            {
                if (post.Blog != null)
                {
                    if (post.Blog.Author != AstMembership.CurrentUser && AstMembership.CurrentUser.IsAdmin)
                    {
                        ErrorLabel.Text = "Вы не можете редактировать чужое сообщение.";
                        return;
                    }
                }
                else
                {
                    ErrorLabel.Text = "Ошибка привязки сущности к блогу.";
                    return;
                }

                post.Updated = DateTime.Now;
            }

            post.Title = tbTitle.Text;
            post.Content = tbContent.Text;

            if (ItemId == 0)
            {
                var blog = AstMembership.CurrentUser.Blog;
                if (blog != null)
                {
                    blog.Posts.Add(post);
                }
                else
                {
                    ErrorLabel.Text = "Не указан блог";
                }
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/MyBlog.aspx"));
        }

        protected void BackToBlog(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/MyBlog.aspx"));
        }
    }
}