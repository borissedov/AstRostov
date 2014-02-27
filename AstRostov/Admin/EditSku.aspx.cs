using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditSku : System.Web.UI.Page
    {
        private int SkuId
        {
            get
            {
                int id;
                if (hdnSkuId.Value != null && int.TryParse(hdnSkuId.Value, out id))
                {
                    return id;
                }
                return 0;
            }
            set { hdnSkuId.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParseSkuId();
            }
        }

        private void ParseSkuId()
        {
            int id;
            if (int.TryParse(Request.Params["sid"], out id))
            {
                SkuId = id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindSkuForm();
            }
        }

        private void BindSkuForm()
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(i => i.SkuId == SkuId);

            if (sku == null)
            {
                lblError.Text = "Редактируемая сущность не найдена.";
                return;
            }

            gridAttributes.DataSource = sku.AttributeValues.ToArray();
            gridAttributes.DataBind();

            tbInventory.Text = sku.Inventory.ToString(CultureInfo.InvariantCulture);
            if (sku.RetailPrice.HasValue)
            {
                tbRetailPrice.Text = sku.RetailPrice.ToString();
            }
            if (sku.SalePrice.HasValue)
            {
                tbSalePrice.Text = sku.SalePrice.ToString();
            }

            hlBack.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProduct.aspx?id={0}", sku.ProductId));


            gridImages.DataSource = sku.Images.ToArray();
            gridImages.DataBind();
        }


        protected void SaveSku(object sender, EventArgs e)
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(i => i.SkuId == SkuId);

            if (sku == null)
            {
                lblError.Text = "Редактируемая сущность не найдена.";
                return;
            }

            int inventory;
            if (int.TryParse(tbInventory.Text, out inventory))
            {
                sku.Inventory = inventory;
            }
            else
            {
                lblError.Text = "Не указано количество на складе";
                return;
            }

            var salePriceString = tbSalePrice.Text.Trim();
            if (!String.IsNullOrEmpty(salePriceString))
            {
                decimal salePrice;
                if (decimal.TryParse(salePriceString, out salePrice))
                {
                    sku.SalePrice = salePrice;
                }
                else
                {
                    lblError.Text = "Цена со скидкой указана не верно";
                    return;
                }
            }
            else
            {
                sku.SalePrice = null;
            }

            var retailPriceString = tbRetailPrice.Text.Trim();
            if (!String.IsNullOrEmpty(retailPriceString))
            {
                decimal retailPrice;
                if (decimal.TryParse(retailPriceString, out retailPrice))
                {
                    sku.RetailPrice = retailPrice;
                }
                else
                {
                    lblError.Text = "Цена указана не верно";
                    return;
                }
            }
            else
            {
                sku.RetailPrice = null;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", sku.ProductId));
        }

        protected void ImageGridRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "MakeMain":
                    MakeImageMain(Convert.ToInt32(e.CommandArgument));

                    break;
                case "Delete":
                    DeleteImage(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void DeleteImage(int imageId)
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(p => p.SkuId == SkuId);
            if (sku == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            SkuImage image = sku.Images.SingleOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                if (image.IsMain)
                {
                    var nextMainImage = sku.Images.Where(i => i.Id != imageId).OrderByDescending(i => i.Id).FirstOrDefault();
                    if (nextMainImage != null)
                    {
                        nextMainImage.IsMain = true;
                    }
                }
                CoreData.Context.SkuImages.Remove(image);
            }
            else
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditSku.aspx?sid={0}", SkuId));
        }

        private void MakeImageMain(int imageId)
        {
            Sku sku = CoreData.Context.Skus.SingleOrDefault(p => p.SkuId == SkuId);
            if (sku == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            var image = sku.Images.SingleOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                image.IsMain = true;

                foreach (var imageToNotMain in sku.Images.Where(i => i.Id != imageId))
                {
                    imageToNotMain.IsMain = false;
                }
            }
            else
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditSku.aspx?sid={0}", SkuId));
        }

        protected void UploadImage(object sender, EventArgs e)
        {
            if (imageUploader.PostedFile != null && !string.IsNullOrEmpty(imageUploader.PostedFile.FileName))
            {
                imageUploader.AppendToFileName = String.Format("-sku-{0}", SkuId);
                imageUploader.SaveImage();

                Sku sku = CoreData.Context.Skus.SingleOrDefault(p => p.SkuId == SkuId);
                if (sku == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                foreach (SkuImage image in sku.Images)
                {
                    image.IsMain = false;
                }

                sku.Images.Add(new SkuImage
                {
                    FileName = imageUploader.FileInformation.Name,
                    IsMain = true
                });

                CoreData.Context.SaveChanges();
                Response.Redirect(String.Format("~/Admin/EditSku.aspx?sid={0}", SkuId));
            }
        }
    }
}