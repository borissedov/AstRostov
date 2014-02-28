using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using AstCore.DataAccess;
using AstCore.Models;

namespace AstRostov.Admin
{
    public partial class EditProduct : System.Web.UI.Page
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
            set { hdnItemId.Value = value.ToString(); }
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
                BindCategories();
                BindBrands();
                BindProductForm();
            }
        }

        private void BindBrands()
        {
            ddlBrands.Items.Add(new ListItem
            {
                Text = "Выберите производителя",
                Value = "0",
                Selected = true
            });

            ddlBrands.Items.AddRange(
                CoreData.Context.Brands.ToArray()
                .Select(c =>
                    new ListItem(c.Name, c.BrandId.ToString(CultureInfo.InvariantCulture)))
                    .ToArray());
        }

        private void BindCategories()
        {
            ddlCategories.Items.Add(new ListItem
                {
                    Text = "Выберите категорию",
                    Value = "0",
                    Selected = true
                });
            ddlCategories.Items.AddRange(
                CoreData.Context.Categories.ToArray()
                .Select(c =>
                    new ListItem(c.Name, c.CategoryId.ToString(CultureInfo.InvariantCulture)))
                    .ToArray());
        }

        private void BindProductForm()
        {
            Product product;
            if (ItemId == 0)
            {
                product = new Product();
            }
            else
            {
                product = CoreData.Context.Products.SingleOrDefault(i => i.ProductId == ItemId);
            }
            if (product == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            tbProductName.Text = product.Name;
            //tbProductNum.Text = product.ProductNum;
            tbDescription.Text = product.Description;
            tbRetailPrice.Text = product.RetailPrice.ToString("F");
            tbSalePrice.Text = product.SalePrice.HasValue ? product.SalePrice.Value.ToString("F") : String.Empty;
            //tbInventory.Text = product.Inventory.ToString(CultureInfo.InvariantCulture);
            ddlCategories.SelectedValue = product.CategoryId.ToString(CultureInfo.InvariantCulture);
            chbIsFeatured.Checked = product.IsFeatured;
            if (product.Brand != null)
            {
                //tbBrandName.Text = product.Brand.Name;
                ddlBrands.SelectedValue = product.Brand.BrandId.ToString(CultureInfo.InvariantCulture);
            }

            if (ItemId == 0)
            {
                litEditTitle.Text = @"Создание нового продукта";
                phImages.Visible = false;
                phSkuList.Visible = false;
            }
            else
            {
                litEditTitle.Text = String.Format(@"Редактирование продукта #{0}", ItemId);
                phImages.Visible = true;
                gridImages.DataSource = product.Images.ToArray();
                gridImages.DataBind();

                phSkuList.Visible = true;
                gridSkus.DataSource = product.SkuCollection.ToArray();
                gridSkus.DataBind();

                hlEditAttrConfig.NavigateUrl = ResolveUrl(String.Format("~/Admin/EditProductConfiguration.aspx?pid={0}", ItemId));
                hlAddSku.NavigateUrl = ResolveUrl(String.Format("~/Admin/AddSku.aspx?pid={0}", ItemId));
            }

        }

        protected void SaveProduct(object sender, EventArgs e)
        {

            Product product = ItemId == 0
                                  ? new Product()
                                  : CoreData.Context.Products.SingleOrDefault(i => i.ProductId == ItemId);
            if (product == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            product.Name = tbProductName.Text.Trim();
            //product.ProductNum = tbProductNum.Text.Trim();
            product.Description = tbDescription.Text.Trim();

            //int inventory;
            //int.TryParse(tbInventory.Text, out inventory);
            //product.Inventory = inventory;

            Decimal retailPrice;
            if (Decimal.TryParse(tbRetailPrice.Text, out retailPrice) && retailPrice > 0)
            {
                product.RetailPrice = retailPrice;
            }
            else
            {
                ErrorLabel.Text = "Введена некорректная цена";
                return;
            }

            Decimal salePrice;
            if (Decimal.TryParse(tbSalePrice.Text, out salePrice) && salePrice > 0)
            {
                product.SalePrice = salePrice;
            }
            else
            {
                product.SalePrice = null;
            }

            int categoryId;
            if (int.TryParse(ddlCategories.SelectedValue, out categoryId) && categoryId > 0)
            {
                Category category = CoreData.Context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
                if (category != null)
                {
                    product.Category = category;
                }
                else
                {
                    ErrorLabel.Text = "Катерия не найдена";
                    return;
                }
            }
            else
            {
                ErrorLabel.Text = "Катерия не выбрана";
                return;
            }

            int brandId;
            if (int.TryParse(ddlBrands.SelectedValue, out brandId) && brandId > 0)
            {
                Brand brand = CoreData.Context.Brands.SingleOrDefault(b => b.BrandId == brandId);
                if (brand != null)
                {
                    product.Brand = brand;
                }
                else
                {
                    ErrorLabel.Text = "Производитель не найден";
                    return;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(tbBrandName.Text) && Page.IsValid)
                {
                    var brand = CoreData.Context.Brands.SingleOrDefault(b => b.Name.ToLower() == tbBrandName.Text.Trim().ToLower());

                    if (brand == null)
                    {
                        brand = new Brand
                        {
                            Name = tbBrandName.Text.Trim()
                        };
                        CoreData.Context.Brands.Add(brand);
                        CoreData.Context.SaveChanges();
                    }

                    product.Brand = brand;
                }
            }

            product.IsFeatured = chbIsFeatured.Checked;

            if (ItemId == 0)
            {
                var sku = new Sku {Inventory = 0, IsDefault = true, Product = product};
                CoreData.Context.Skus.Add(sku);
                //product.SkuCollection.Add(sku);
                CoreData.Context.Products.Add(product);
            }

            CoreData.Context.SaveChanges();
            Response.Redirect("~/Admin/ProductList.aspx");
        }

        protected void ValidateUniqueName(object source, ServerValidateEventArgs args)
        {
            if (CoreData.Context.Brands.Count(b => b.Name == tbBrandName.Text.Trim()) > 1)
            {
                args.IsValid = false;
            }
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
            Product product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ItemId);
            if (product == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            ProductImage image = product.Images.SingleOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                if (image.IsMain)
                {
                    var nextMainImage = product.Images.Where(i => i.Id != imageId).OrderByDescending(i => i.Id).FirstOrDefault();
                    if (nextMainImage != null)
                    {
                        nextMainImage.IsMain = true;
                    }
                }
                CoreData.Context.ProductImages.Remove(image);
            }
            else
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", ItemId));
        }

        private void MakeImageMain(int imageId)
        {
            Product product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ItemId);
            if (product == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            var image = product.Images.SingleOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                image.IsMain = true;

                foreach (var imageToNotMain in product.Images.Where(i => i.Id != imageId))
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
            Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", ItemId));
        }

        protected void UploadImage(object sender, EventArgs e)
        {
            if (imageUploader.PostedFile != null && !string.IsNullOrEmpty(imageUploader.PostedFile.FileName))
            {
                imageUploader.AppendToFileName = String.Format("-{0}", ItemId);
                imageUploader.SaveImage();

                Product product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ItemId);
                if (product == null)
                {
                    ErrorLabel.Text = "Редактируемая сущность не найдена.";
                    return;
                }

                foreach (ProductImage image in product.Images)
                {
                    image.IsMain = false;
                }

                product.Images.Add(new ProductImage
                    {
                        FileName = imageUploader.FileInformation.Name,
                        IsMain = true
                    });

                CoreData.Context.SaveChanges();
                Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", ItemId));
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var categoryId = Request.Params["cid"];
                if (!String.IsNullOrEmpty(categoryId) && ddlCategories.Items.Cast<ListItem>().Any(li => li.Value == categoryId))
                {
                    ddlCategories.SelectedValue = categoryId;
                }
            }

            ErrorLabel.Visible = !String.IsNullOrEmpty(ErrorLabel.Text);
        }

        protected void OnAttributeRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(String.Format("~/Admin/EditSku.aspx?sid={0}", Convert.ToInt32(e.CommandArgument)));
                    break;
                case "DeleteSku":
                    DeleteSku(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void DeleteSku(int skuId)
        {
            Product product = CoreData.Context.Products.SingleOrDefault(p => p.ProductId == ItemId);
            if (product == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }
            if (product.SkuCollection.Count == 1)
            {
                ErrorLabel.Text = "У продукта должна быть хотя бы одна конфигурация.";
                return;
            }

            var skuToDelete = product.SkuCollection.SingleOrDefault(s => s.SkuId == skuId);
            if (skuToDelete == null)
            {
                ErrorLabel.Text = "Редактируемая сущность не найдена.";
                return;
            }

            product.SkuCollection.Remove(skuToDelete);
            CoreData.Context.Skus.Remove(skuToDelete);

            CoreData.Context.SaveChanges();
            Response.Redirect(String.Format("~/Admin/EditProduct.aspx?id={0}", ItemId));
        }
    }
}