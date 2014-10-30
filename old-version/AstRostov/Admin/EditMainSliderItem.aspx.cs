using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditMainSliderItem : System.Web.UI.Page
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!Page.IsPostBack)
            {
                BindMainSliderItemForm();
            }
        }

        private void BindMainSliderItemForm()
        {
            MainSliderItem mainSliderItem;
            if (ItemId == 0)
            {
                mainSliderItem = new MainSliderItem();
            }
            else
            {
                mainSliderItem = CoreData.Context.MainSliderItems.SingleOrDefault(i => i.MainSliderItemId == ItemId);
            }
            if (mainSliderItem == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbMainSliderItemTitle.Text = mainSliderItem.Title;
            tbMainSliderItemUrl.Text = mainSliderItem.Url;
            tbMainSliderItemPrice.Text = mainSliderItem.Price.ToString();

            if (ItemId == 0)
            {
                rblImageMode.SelectedIndex = 2;
            }
            else
            {
                imgMainSliderItemImage.ImageUrl = ResolveUrl(String.Format("~/img/main-slider/{0}", mainSliderItem.ImageFile));
                rblImageMode.SelectedIndex = 0;
            }
            rblImageMode_OnSelectedIndexChanged(null, null);

            litEditTitle.Text = ItemId == 0
                                        ? @"Создание нового слайда"
                                        : String.Format(@"Редактирование слайда #{0}", ItemId);

        }

        protected string UploadNewImage()
        {
            if (ImageUploadControl.HasFile)
            {
                try
                {
                    if (ImageUploadControl.PostedFile.ContentType == "image/jpeg" || ImageUploadControl.PostedFile.ContentType == "image/png")
                    {
                        if (ImageUploadControl.PostedFile.ContentLength < 1024000)
                        {
                            string extantion = Path.GetExtension(ImageUploadControl.FileName);
                            string fileName = String.Format("{0}.{1}", Guid.NewGuid(), extantion);
                            if (extantion != null)
                            {
                                ImageUploadControl.SaveAs(String.Format("{0}{1}", Server.MapPath("~/img/main-slider/"), fileName));
                            }
                            return fileName;
                            //StatusLabel.Text = "Результат загрузки: Файл загружен!";
                        }
                        else
                        {
                            ErrorLabel.Text = "Результат загрузки: Файл должен быть меньше 1MB!";
                        }
                    }
                    else
                    {
                        ErrorLabel.Text = "Результат загрузки: Файл должен быть изображением!";
                    }
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Результат загрузки: Файл не может быть загружен. Ошибка: " + ex.Message;
                }
            }
            return string.Empty;
        }

        protected void SaveMainSliderItem(object sender, EventArgs e)
        {

            MainSliderItem mainSliderItem = ItemId == 0 ? new MainSliderItem() : CoreData.Context.MainSliderItems.SingleOrDefault(i => i.MainSliderItemId == ItemId);
            if (mainSliderItem == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            mainSliderItem.Title = tbMainSliderItemTitle.Text;
            mainSliderItem.Url = tbMainSliderItemUrl.Text;

            decimal price;
            if (decimal.TryParse(tbMainSliderItemPrice.Text, out price))
            {
                mainSliderItem.Price = price;
            }
            else
            {
                mainSliderItem.Price = 0M;
                //ErrorLabel.Text = "Цена введена некорректно.";
                return;
            }

            switch (rblImageMode.SelectedIndex)
            {
                case 1:
                    mainSliderItem.ImageFile = UploadNewImage();
                    break;
                case 2:
                    mainSliderItem.ImageFile = String.Empty;
                    break;
            }

            if (ItemId == 0)
            {
                CoreData.Context.MainSliderItems.Add(mainSliderItem);
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditMainSliderItem.aspx?id={0}", mainSliderItem.MainSliderItemId));
        }

        protected void rblImageMode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblImageMode.SelectedIndex)
            {
                case 0:
                    imgMainSliderItemImage.Visible = true;
                    ImageUploadControl.Visible = false;
                    break;

                case 1:
                    imgMainSliderItemImage.Visible = false;
                    ImageUploadControl.Visible = true;
                    break;

                case 2:
                    imgMainSliderItemImage.Visible = false;
                    ImageUploadControl.Visible = false;
                    break;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }
    }
}