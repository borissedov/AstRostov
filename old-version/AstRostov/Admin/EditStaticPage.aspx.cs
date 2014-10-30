using System;
using System.Globalization;
using System.Linq;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditStaticPage : System.Web.UI.Page
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
                BindStaticPageForm();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }


        private void BindStaticPageForm()
        {
            StaticPage staticPage;
            if (ItemId == 0)
            {
                staticPage = new StaticPage();
            }
            else
            {
                staticPage = CoreData.Context.StaticPages.SingleOrDefault(i => i.Id == ItemId);
            }
            if (staticPage == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbStaticPageTitle.Text = staticPage.Title;
            tbStaticPageKey.Text = staticPage.Key;
            tbStaticPageContent.Value = staticPage.Content;

            litEditTitle.Text = ItemId == 0
                                        ? "Создание новой страницы"
                                        : String.Format("Редактирование страницы #{0}", ItemId);
        }

        protected void SaveStaticPage(object sender, EventArgs e)
        {

            StaticPage staticPage = ItemId == 0 ? new StaticPage() : CoreData.Context.StaticPages.SingleOrDefault(i => i.Id == ItemId);
            if (staticPage == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            staticPage.Title = tbStaticPageTitle.Text;
            staticPage.Key = tbStaticPageKey.Text;
            staticPage.Content = tbStaticPageContent.Value;

            if (ItemId == 0)
            {
                staticPage = CoreData.Context.StaticPages.Add(staticPage);
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditStaticPage.aspx?id={0}", staticPage.Id));

        }


    }
}